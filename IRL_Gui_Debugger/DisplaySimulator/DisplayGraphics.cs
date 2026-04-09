using Gui_Debug_Tool.DisplayInstructions;
using IRL_Gui_Debugger.DisplayInstructions;
using IRL_Gui_Debugger.DisplaySimulator;
using IRL_Gui_Debugger.Logging;
using KGySoft.Drawing;
using System.Collections.Concurrent;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace Gui_Debug_Tool.DisplaySimulator
{
    public class DisplayGraphics
    {
        public static ConcurrentQueue<Image> GraphicsCache = new();

        private Bitmap? m_displayBitmap = null;  
        private GuiImageFile m_guiImage = new();

        public DisplayGraphics()
        {
        }

        public void CreateDisplayBitmap(int width, int height)
        {
            m_displayBitmap = new Bitmap(width, height, PixelFormat.Format24bppRgb);
            Clear();
        }

        public void Clear()
        {
            if (m_displayBitmap == null)
            {
                return;
            }

            using Graphics g = Graphics.FromImage(m_displayBitmap);
            g.Clear(Color.White);
        }

        public void QueueBitmapToCache()
        {
            if (m_displayBitmap == null)
            {
                return;
            }

            Bitmap bitmap = (Bitmap)m_displayBitmap.Clone();
            GraphicsCache.Enqueue(bitmap);
        }

        public bool OpenImageFile(string filePath)
        {
            bool result = true;

            try
            {
                m_guiImage = new();

                BinaryReader binaryReader = new(File.Open(filePath, FileMode.Open));
                m_guiImage.ReadGuiImageFile(binaryReader);
                binaryReader.Close();
            }
            catch 
            {
                result = false;
                Logger.Error($"Open Image file: {filePath}");
            }

            return result;
        }

        public void ProcessDisplayInstructions(Queue<DisplayInstruction> instructionsQueue)
        {
            if (m_guiImage.IsEmpty || m_displayBitmap == null)
            {
                Logger.Error("Process Display Instruction");

                return;
            }

            using Graphics graphics = Graphics.FromImage(m_displayBitmap);

            int instructionsToDraw = instructionsQueue.Count;

            for (int i = 0; i < instructionsToDraw; i++)
            {
                DisplayInstruction instruction = instructionsQueue.Dequeue();
                DrawInstruction(instruction, graphics);
            }
        }

        public DataLocation GetDataLocation(int dataLocationId)
        {
            return m_guiImage.GetDataLocation(dataLocationId);
        }

        private void DrawInstruction(DisplayInstruction displayInstuction, Graphics g)
        {
            if (displayInstuction is RectangleFillInstruction rfInstruction)
            {
                if (rfInstruction.Radius == 0)
                {
                    DrawRectFill(rfInstruction);
                }
                else
                {
                    GraphicsExtensions.FillRoundedRectangle(g, new SolidBrush(rfInstruction.Color), new Rectangle(rfInstruction.Point, rfInstruction.Size), rfInstruction.Radius);
                }
                
            }
            else if (displayInstuction is RectangleBorderInstruction rbInstruction)
            {
                Pen pen = new(rbInstruction.BorderColor, rbInstruction.BorderThickness);
                
                Size size = rbInstruction.Size;
                size.Width -= rbInstruction.BorderThickness;
                size.Height -= rbInstruction.BorderThickness;

                Point point = rbInstruction.Point;
                point.X += rbInstruction.BorderThickness / 2;
                point.Y += rbInstruction.BorderThickness / 2;

                Rectangle rectangle = new(point, size);

                if (rbInstruction.Radius == 0)
                {
                    g.DrawRectangle(pen, rectangle);
                }
                else
                {
                    GraphicsExtensions.DrawRoundedRectangle(g, pen, rectangle, rbInstruction.Radius);
                }
            }
            else if (displayInstuction is RectangleFillBorderInstruction rfbInstruction)
            {
                Pen pen = new(rfbInstruction.BorderColor, rfbInstruction.BorderThickness);

                Size size = rfbInstruction.Size;
                size.Width -= rfbInstruction.BorderThickness;
                size.Height -= rfbInstruction.BorderThickness;

                Point point = rfbInstruction.Point;
                point.X += rfbInstruction.BorderThickness / 2;
                point.Y += rfbInstruction.BorderThickness / 2;

                Rectangle rectangle = new(point, size);

                if (rfbInstruction.Radius == 0)
                {
                    DrawRectFill(rfbInstruction.Point, rfbInstruction.Size, rfbInstruction.FillColor);
                    g.DrawRectangle(pen, rectangle);
                }
                else
                {
                    GraphicsExtensions.DrawRoundedRectangle(g, pen, rectangle, rfbInstruction.Radius);
                    GraphicsExtensions.FillRoundedRectangle(g, new SolidBrush(rfbInstruction.FillColor), rectangle, rfbInstruction.Radius);
                }
            }
            else if (displayInstuction is ImageInstruction imageInstuction)
            {
                DrawImageInstructionRLE(imageInstuction);
            }
            else if (displayInstuction is OptimizedImageInstruction optimizedImageInstruction)
            {
                DrawImageInstructionRLE_A(optimizedImageInstruction);
            }
            else
            {
                Logger.Error("Can not draw Fill instrcution, no background and border");
            }
        }

        private void DrawRectFill(RectangleFillInstruction instruction)
        {
            DrawRectFill(instruction.Point, instruction.Size, instruction.Color);
        }

        private void DrawRectFill(Point point, Size size, Color color)
        {
            int width = size.Width;
            int height = size.Height;
            byte[] pixelDataBytes = new byte[width * height * GuiImageFile.BytesPerPixel];

            for (int i = 0; i < pixelDataBytes.Length; i += 3)
            {
                pixelDataBytes[i] = color.B;
                pixelDataBytes[i + 1] = color.G;
                pixelDataBytes[i + 2] = color.R;
            }

            CopyPixelsToBitmap(pixelDataBytes, width, height, point.X, point.Y);
        }

        private void DrawImageInstructionRLE_A(OptimizedImageInstruction instruction)
        {
            try
            {
                int width = instruction.Size.Width;
                int height = instruction.Size.Height;
                byte[] pixelDataBytes;

                if (instruction.Type == ImageType.Bitmap)
                {
                    pixelDataBytes = m_guiImage.GetPixelDataImageRLE_A(width, height, instruction.BmpKey, instruction.Properties, instruction.ForeColor, instruction.BackColor);
                }
                else
                {
                    pixelDataBytes = m_guiImage.GetPixelDataFontRLE_A(width, height, instruction.Character, instruction.FontId, instruction.ForeColor, instruction.BackColor);
                }

                CopyPixelsToBitmap(pixelDataBytes, width, instruction.Size.Height, instruction.Point.X, instruction.Point.Y);
            }
            catch (Exception e)
            {
                Logger.Error($"Draw Image in Bitmap: {e.Message}");
            }
        }

        private void DrawImageInstructionRLE(ImageInstruction instruction)
        {
            try
            {
                int width = instruction.Size.Width;
                int height = instruction.Size.Height;
                byte[] pixelDataBytes;

                if (instruction.Type == ImageType.Bitmap)
                {
                    pixelDataBytes = m_guiImage.GetPixelDataImageRLE(width, height, instruction.BmpKey, instruction.Properties);
                }
                else
                {
                    pixelDataBytes = m_guiImage.GetPixelDataFontRLE(width, height, instruction.Character, instruction.FontId);
                }

                CopyPixelsToBitmap(pixelDataBytes, width, instruction.Size.Height, instruction.Point.X, instruction.Point.Y);
            }
            catch (Exception e)
            {
                Logger.Error($"Draw Compressed Image in Bitmap: {e.Message}");
            }
        }

        private void CopyPixelsToBitmap(byte[] pixelDataBytes, int width, int height, int x, int y)
        {
            BitmapData? bmd = null;

            try
            {
                bmd = m_displayBitmap.LockBits(
                    new Rectangle(x, y, width, height),
                        ImageLockMode.ReadOnly, m_displayBitmap.PixelFormat);

                for (int i = 0; i < height; i++)
                {
                    Marshal.Copy(pixelDataBytes, i * width * 3, bmd.Scan0 + bmd.Stride * i, width * 3);
                }

                m_displayBitmap.UnlockBits(bmd);
            }
            catch (Exception e)
            {
                if (bmd != null)
                {
                    m_displayBitmap.UnlockBits(bmd);
                }

                Logger.Error($"Copy Pixels to Bitmap: {e.Message}");
            }
        }
    }
}
