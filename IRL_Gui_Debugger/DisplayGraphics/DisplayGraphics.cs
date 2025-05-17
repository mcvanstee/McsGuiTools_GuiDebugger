using Gui_Debug_Tool.DisplayInstructions;
using IRL_Gui_Debugger.Logging;
using KGySoft;
using KGySoft.Drawing;
using System.Collections.Concurrent;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace Gui_Debug_Tool.DisplaySimulator
{
    public class DisplayGraphics
    {
        public static ConcurrentQueue<Image> GraphicsCache = new ConcurrentQueue<Image>();

        private Bitmap? m_displayBitmap = null;    
        private byte[] m_pixelFileData = Array.Empty<byte>();

        public DisplayGraphics()
        {
        }

        public void CreateDisplayBitmap(int width, int height)
        {
            m_displayBitmap = new Bitmap(width, height, PixelFormat.Format16bppRgb565);
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
                BinaryReader binaryReader = new BinaryReader(File.Open(filePath, FileMode.Open));
                m_pixelFileData = binaryReader.ReadBytes((int)binaryReader.BaseStream.Length);
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
            if (m_pixelFileData.Length == 0 || m_displayBitmap == null)
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
                    DrawRoundedRectangle(g, pen, rectangle, rbInstruction.Radius);
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
                DrawImageInBitmap(imageInstuction);
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
            byte[] pixelDataBytes = new byte[width * height * 2];
            byte[] colorBytes = BitConverter.GetBytes(GetRGB565(color));

            for (int i = 0; i < pixelDataBytes.Length; i += 2)
            {
                pixelDataBytes[i] = colorBytes[0];
                pixelDataBytes[i + 1] = colorBytes[1];
            }

            CopyPixelsToBitmap(pixelDataBytes, width, height, point.X, point.Y);
        }

        private void DrawImageInBitmap(ImageInstruction instruction)
        {
            try
            {
                int width = instruction.Size.Width;

                byte[] pixelDataBytes = new byte[instruction.DataSize];
                Array.Copy(m_pixelFileData, instruction.DataOffset, pixelDataBytes, 0, instruction.DataSize);

                for (int i = 0; i < instruction.DataSize; i += 2)
                {
                    byte temp = pixelDataBytes[i];
                    pixelDataBytes[i] = pixelDataBytes[i + 1];
                    pixelDataBytes[i + 1] = temp;
                }

                CopyPixelsToBitmap(pixelDataBytes, width, instruction.Size.Height, instruction.Point.X, instruction.Point.Y);
            }
            catch (Exception e)
            {
                Logger.Error($"Draw Image in Bitmap: {e.Message}");
            }

        }

        static ushort GetRGB565(Color color)
        {
            return GetRGB565(color.R, color.G, color.B);
        }

        static ushort GetRGB565(int r, int g, int b)
        {
            return (ushort)(((r >> 3) << 11) + ((g >> 2) << 5) + (b >> 3));
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
                    Marshal.Copy(pixelDataBytes, i * width * 2, bmd.Scan0 + bmd.Stride * i, width * 2);
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

        public static void DrawRoundedRectangle(Graphics graphics, Pen pen, Rectangle bounds, int cornerRadius)
        {
            if (graphics == null)
                throw new ArgumentNullException(nameof(graphics), PublicResources.ArgumentNull);
            if (pen == null)
                throw new ArgumentNullException(nameof(pen), PublicResources.ArgumentNull);

            using (GraphicsPath path = CreateRoundedRectangle(bounds, pen, cornerRadius))
            {
                graphics.DrawPath(pen, path);
            }
        }

        private static GraphicsPath CreateRoundedRectangle(Rectangle bounds, Pen pen, int radius)
        {
            var path = new GraphicsPath();
            if (radius == 0)
            {
                path.AddRectangle(bounds);
                return path;
            }

            int diameter = (radius * 2);
            var size = new Size(diameter, diameter);
            var arc = new Rectangle(bounds.Location, size);

            // top left arc
            path.AddArc(arc, 180, 90);

            // top right arc
            arc.X = bounds.Right - diameter;
            path.AddArc(arc, 270, 90);

            // bottom right arc
            arc.Y = bounds.Bottom - diameter;
            path.AddArc(arc, 0, 90);

            // bottom left arc
            arc.X = bounds.Left;
            path.AddArc(arc, 90, 90);

            path.CloseFigure();
            return path;
        }

    }
}
