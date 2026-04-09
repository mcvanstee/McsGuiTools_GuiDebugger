using IRL_Gui_Debugger.Exceptions;
using IRL_Gui_Debugger.Logging;

namespace IRL_Gui_Debugger.DisplaySimulator
{
    internal struct FsFileInfo
    {
        public uint dataOffset;
        public ushort properties;
        public ushort width;
        public ushort height;
    }

    internal struct FsCharInfo
    {
        public uint dataOffset;
        public byte width;
        public byte height;
    }

    public class GuiImageFile
    {
        const uint FS_FILE_DUMMY = 0xFFFFFFFF;
        const int FS_CHAR_INFOS_IN_FULL_FONT = 95;

        public const uint BytesPerPixel = 3;
        private byte[] m_imageFile = [];
        private List<DataLocation> m_dataLocations = [];

        private int m_headerSize = 0;
        private int m_fileInfoOffset;
        private int m_charInfoOffset;              // offset to char info data from start of file
        private int m_fileInfoSearchSize;
        private int m_charInfoSearchSize;
        private int m_fileInfoSize;
        private int m_charInfoSize;
        private int m_dataLocationsSize;
        private int m_numFiles;
        private int m_numFonts;
        private int m_maxNumProperties;
        private int m_noOfPropertiesUsed;
        private byte[] m_maxProperty = [];
        private int[] m_fontInfos = [];
        private List<FsFileInfo> m_fileInfos = [];
        private List<List<FsCharInfo>> m_fonts = [];

        private Color m_foreColor = Color.Black;
        private Color m_backColor = Color.White;
        private readonly List<byte> m_colorPaletteValues = [0, 0x10, 0x20, 0x30, 0x40, 0x50, 0x60, 0x70, 0x80, 0x90, 0xA0, 0xB0, 0xC0, 0xD0, 0xE0, 0xF0, 0x00, 0xFF];
        private readonly List<Color> m_colorPalette = [];

        public bool IsEmpty => m_imageFile.Length == 0;

        public DataLocation GetDataLocation(int id)
        {
            foreach (DataLocation dataLocation in m_dataLocations)
            {
                if (dataLocation.Id == id)
                {
                    return dataLocation;
                }
            }

            return null;
        }

        public void ReadGuiImageFile(BinaryReader binaryReader)
        {
            m_imageFile = binaryReader.ReadBytes((int)binaryReader.BaseStream.Length);

            if (m_imageFile[0] != 3)
            {
                throw new GuiImageFileException("Invalid image file.");
            }

            InitColorPalette();

            m_headerSize = m_imageFile[1];
            ReadVersion();

            m_fileInfoSize = m_imageFile[18];
            m_charInfoSize = m_imageFile[19];

            m_fileInfoSearchSize = BitConverter.ToInt32(m_imageFile, 20);
            m_charInfoSearchSize = BitConverter.ToInt32(m_imageFile, 14);
            m_dataLocationsSize = BitConverter.ToInt32(m_imageFile, 28);
            m_numFiles = BitConverter.ToInt32(m_imageFile, 32);
            m_numFonts = BitConverter.ToInt32(m_imageFile, 36);
            m_maxNumProperties = BitConverter.ToInt32(m_imageFile, 40);
            m_fileInfoOffset = m_headerSize + m_dataLocationsSize + m_maxNumProperties + (m_numFonts * sizeof(int));
            m_charInfoOffset = m_fileInfoOffset + m_fileInfoSearchSize;

            ReadDataLocations();
            ReadProperties();
            ReadFontInfos();
            ReadFileInfos();
            ReadCharInfos();
        }

        public void InitColorPalette()
        {
            m_colorPalette.Clear();

            foreach (byte value in m_colorPaletteValues)
            {
                m_colorPalette.Add(Color.FromArgb(value, value, value));
            }
        }

        public byte[] GetPixelDataImageRLE(int width, int height, uint bmpKey, byte[] properties)
        {
            int dataOffset = GetImageDataOffset(bmpKey, ref properties);

            if (dataOffset < 0)
            {
                throw new GuiImageFileException($"Invalid data offset: {dataOffset} for bmpKey: {bmpKey} with properties: {string.Join(", ", properties)}.");
            }

            return GetPixelDataRLE(width, height, dataOffset);
        }

        public byte[] GetPixelDataFontRLE(int width, int height, byte character, byte font)
        {
            int dataOffset = GetFontDataOffset(character, font);

            if (dataOffset < 0)
            {
                throw new GuiImageFileException($"Invalid data offset: {dataOffset} for character: {character} with font: {font}.");
            }

            return GetPixelDataRLE(width, height, dataOffset);
        }

        public byte[] GetPixelDataImageRLE_A(int width, int height, uint bmpKey, byte[] properties, Color foreColor, Color backColor)
        {
            int dataOffset = GetImageDataOffset(bmpKey, ref properties);

            if (dataOffset < 0)
            {
                throw new GuiImageFileException($"Invalid data offset: {dataOffset} for bmpKey: {bmpKey} with properties: {string.Join(", ", properties)}.");
            }

            return GetPixelDataRLE_A(width, height, dataOffset, foreColor, backColor);
        } 

        public byte[] GetPixelDataFontRLE_A(int width, int height, byte character, byte font, Color foreColor, Color backColor)
        {
            int dataOffset = GetFontDataOffset(character, font);

            if (dataOffset < 0)
            {
                throw new GuiImageFileException($"Invalid data offset: {dataOffset} for character: {character} with font: {font}.");
            }

            return GetPixelDataRLE_A(width, height, dataOffset, foreColor, backColor);
        }

        private byte[] GetPixelDataRLE(int width, int height, int dataOffset)
        {
            int pixelsToRead = width * height;
            byte[] pixelDataBytes = new byte[pixelsToRead * BytesPerPixel];
            int writeIndex = 0;

            while (pixelsToRead > 0)
            {
                byte noOfPixels = m_imageFile[dataOffset++];

                for (int i = 0; i < noOfPixels; i++)
                {
                    pixelDataBytes[writeIndex++] = m_imageFile[dataOffset + 2];
                    pixelDataBytes[writeIndex++] = m_imageFile[dataOffset + 1];
                    pixelDataBytes[writeIndex++] = m_imageFile[dataOffset];

                    pixelsToRead--;

                    if (pixelsToRead == 0)
                    {
                        break;
                    }
                }

                dataOffset += 3;
            }

            return pixelDataBytes;
        }

        private byte[] GetPixelDataRLE_A(int width, int height, int dataOffset, Color foreColor, Color backColor)
        {
            int pixelsToRead = width * height;
            byte[] pixelDataBytes = new byte[pixelsToRead * BytesPerPixel];
            int writeIndex = 0;

            SetColorPalette(foreColor, backColor);

            while (pixelsToRead > 0)
            {
                byte dataByte = m_imageFile[dataOffset++];
                int noOfPixels = 0;
                Color pixelColor;

                if (dataByte == 0)
                {
                    pixelColor = m_colorPalette[16];
                    noOfPixels = m_imageFile[dataOffset++];
                }
                else if (dataByte == 0x0F)
                {
                    pixelColor = m_colorPalette[17];
                    noOfPixels = m_imageFile[dataOffset++];
                }
                else
                {
                    noOfPixels = (dataByte & 0xF0) >> 4;
                    int colorIndex = dataByte & 0x0F;
                    pixelColor = m_colorPalette[colorIndex];
                }

                for (int i = 0; i < noOfPixels; i++)
                {
                    pixelDataBytes[writeIndex++] = pixelColor.B;
                    pixelDataBytes[writeIndex++] = pixelColor.G;
                    pixelDataBytes[writeIndex++] = pixelColor.R;

                    pixelsToRead--;

                    if (pixelsToRead == 0)
                    {
                        break;
                    }
                }
            }

            return pixelDataBytes;
        }

        private void SetColorPalette(Color foreColor, Color backColor)
        {
            m_foreColor = foreColor;
            m_backColor = backColor;

            m_colorPalette.Clear();

            foreach (byte value in m_colorPaletteValues)
            {
                m_colorPalette.Add(GetOptimizedPixel(foreColor, backColor, value));
            }
        }

        private void ReadVersion()
        {
            uint major = BitConverter.ToUInt32(m_imageFile, 2);
            uint minor = BitConverter.ToUInt32(m_imageFile, 6);
            uint patch = BitConverter.ToUInt32(m_imageFile, 10);
            uint revision = BitConverter.ToUInt32(m_imageFile, 14);

            Logger.Message($"Open Image file V{major}.{minor}.{patch}.{revision}");
        }

        private void ReadDataLocations()
        {
            int numDataLocations = BitConverter.ToInt32(m_imageFile, m_headerSize);
            m_dataLocations = new List<DataLocation>(numDataLocations);

            for (int i = 0; i < numDataLocations; i++)
            {
                int id = BitConverter.ToInt32(m_imageFile, m_headerSize + 4 + (i * 8));
                int type = BitConverter.ToInt32(m_imageFile, m_headerSize + 4 + (i * 8) + 4);

                DataType dataType = (type == 2) ? DataType.RLE_Alpha : DataType.RLE;

                m_dataLocations.Add(new DataLocation(id, dataType));
            }
        }

        private void ReadProperties()
        {
            m_maxProperty = new byte[m_maxNumProperties];
            int offset = m_headerSize + m_dataLocationsSize;

            for (int i = 0; i < m_maxNumProperties; i++)
            {
                m_maxProperty[i] = m_imageFile[offset + i];
            }
        }

        private void ReadFontInfos()
        {
            m_fontInfos = new int[m_numFonts];
            int offset = m_headerSize + m_dataLocationsSize + m_maxNumProperties;

            for (int i = 0; i < m_numFonts; i++)
            {
                m_fontInfos[i] = BitConverter.ToInt32(m_imageFile, offset + (i * sizeof(int)));
            }
        }

        private void ReadFileInfos()
        {
            m_fileInfos = new List<FsFileInfo>(m_numFiles);

            for (int i = 0; i < m_numFiles; i++)
            { 
                int index = m_fileInfoOffset + (i * m_fileInfoSize);

                uint dataOffset = BitConverter.ToUInt32(m_imageFile, index);
                index += 4;

                ushort properties = 0;
                if (m_fileInfoSize == 10)
                {
                    properties = BitConverter.ToUInt16(m_imageFile, index);
                    i += 2;
                }
                else if (m_fileInfoSize == 9)
                {
                    properties = m_imageFile[index++];
                }
                else
                {
                }

                ushort width = BitConverter.ToUInt16(m_imageFile, index);
                index += 2;
                ushort height = BitConverter.ToUInt16(m_imageFile, index);

                m_fileInfos.Add(new FsFileInfo { dataOffset = dataOffset, properties = properties, width = width, height = height });
            }
        }

        private void ReadCharInfos()
        {
            int index = m_charInfoOffset;

            for (int i = 0; i < m_numFonts; i++)
            {
                int noOfCharInfos = m_fontInfos[i];
                List<FsCharInfo> charInfos = [];

                for (int j = 0; j < noOfCharInfos; j++)
                {
                    uint dataOffset = BitConverter.ToUInt32(m_imageFile, index);
                    index += 4;
                    byte width = m_imageFile[index++];
                    byte height = m_imageFile[index++];

                    charInfos.Add(new FsCharInfo { dataOffset = dataOffset, width = width, height = height });
                }

                m_fonts.Add(charInfos);
            }

        }

        private int GetImageDataOffset(uint bmpKey, ref byte[] properties)
        {
            int fileIndex = (int)bmpKey - 1;
            int propertiesLength = properties.Length;

            if ((fileIndex < 0) || (fileIndex > m_numFiles))
            {
                throw new GuiImageFileException($"Invalid bmpKey: {bmpKey}. It should be between 1 and {m_numFiles}.");      
            }

            if (0 == propertiesLength)
            {
                return (int)m_fileInfos[fileIndex].dataOffset;
            }

            if (m_maxNumProperties > propertiesLength)
            {
                throw new GuiImageFileException($"Invalid properties length: {propertiesLength}. It should be {m_maxNumProperties}.");
            }

            FsFileInfo fileInfo = m_fileInfos[fileIndex];

            if (fileInfo.properties == 0)
            {
                return (int)fileInfo.dataOffset;
            }

            uint keyOffset = 0;
            uint multiplier = 1;

            for (int i = (propertiesLength - 1); i >= -1; i--)
            {
                ushort propertyBit = (ushort)(fileInfo.properties & (0x01U << i));

                if (propertyBit > 0)
                {
                    if (m_maxProperty[i] > properties[i])
                    {
                        keyOffset += (multiplier * properties[i]);
                        multiplier *= m_maxProperty[i];
                    }
                    else
                    {
                        throw new GuiImageFileException($"Invalid property value: {properties[i]} for property index: {i}. It should be between 0 and {m_maxProperty[i] - 1}.");
                    }
                }
            }

            fileInfo = m_fileInfos[fileIndex + (int)keyOffset];

            if (FS_FILE_DUMMY == fileInfo.dataOffset)
            {
                return 0;
            }

            return (int)fileInfo.dataOffset;
        }

        private int GetFontDataOffset(byte character, byte font)
        {
            int charIndex = GetCharIndex(character, font);
            int fontIndex = font;

            if ((m_fontInfos[font] > charIndex) && (m_numFonts > fontIndex) && (charIndex >= 0) && (fontIndex >= 0))
            {
                return (int)m_fonts[fontIndex][charIndex].dataOffset;  
            }

            return 0;
        }

        private int GetCharIndex(byte c, byte font)
        {
            int charIndex;
            int noOfCharactersInFont = m_fontInfos[font];

            if (noOfCharactersInFont == FS_CHAR_INFOS_IN_FULL_FONT)
            {
                charIndex = c - 32;
            }
            else
            {
                if ((c >= '0') && (c <= '9'))
                {
                    charIndex = (c - '0' + 2);
                }
                else if (c == '.')
                {
                    charIndex = 0;
                }
                else if (c == ',')
                {
                    charIndex = 1;
                }
                else
                {
                    throw new GuiImageFileException("Character index not found");
                }
            }

            return charIndex;
        }


        private Color GetOptimizedPixel(Color foreColor, Color backColor, byte pixelValue)
        {
            if (pixelValue == 0)
            {
                return foreColor;
            }
            else if (pixelValue == 0xFF)
            {
                return backColor;
            }
            else
            {
                float alpha = pixelValue / 255.0f;

                byte r = GetColorValue(foreColor.R, backColor.R, pixelValue, alpha);
                byte g = GetColorValue(foreColor.G, backColor.G, pixelValue, alpha);
                byte b = GetColorValue(foreColor.B, backColor.B, pixelValue, alpha);

                return Color.FromArgb(r, g, b);
            }
        }

        private byte GetColorValue(byte fore, byte back, byte pixelVal, float alpha)
        {
            byte colorValue;

            if (fore < back)
            {
                if ((fore == 0) && (back == 0xFF))
                {
                    colorValue = pixelVal;
                }
                else
                {
                    colorValue = (byte)(fore + (byte)((back - fore) * alpha));
                }
            }
            else
            {
                colorValue = (byte)(fore - (byte)((fore - back) * alpha));
            }

            return colorValue;
        }
    }
}
