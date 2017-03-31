namespace ProcurmentKanbanBoard
{
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Drawing.Imaging;

    internal class HelperClass
    {
        public static Image CreateButtonImage(Bitmap original, Color backColor, Color foreColor, int imageSize)
        {
            Bitmap image = new Bitmap(imageSize, imageSize);
            image.SetResolution(original.HorizontalResolution, original.VerticalResolution);
            Graphics graphics = Graphics.FromImage(image);
            ColorMap[] map = new ColorMap[0x100];
            for (int i = 0; i < 0x100; i++)
            {
                map[i] = new ColorMap();
                map[i].OldColor = Color.FromArgb(i, i, i);
                map[i].NewColor = Color.FromArgb((((0xff - i) * foreColor.R) / 0xff) + ((i * backColor.R) / 0xff), (((0xff - i) * foreColor.G) / 0xff) + ((i * backColor.G) / 0xff), (((0xff - i) * foreColor.B) / 0xff) + ((i * backColor.B) / 0xff));
            }
            ImageAttributes imageAttr = new ImageAttributes();
            imageAttr.SetRemapTable(map);
            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
            graphics.SmoothingMode = SmoothingMode.HighQuality;
            graphics.DrawImage(original, new Rectangle(0, 0, imageSize, imageSize), 0, 0, original.Width, original.Height, GraphicsUnit.Pixel, imageAttr);
            graphics.Dispose();
            return image;
        }
    }
}

