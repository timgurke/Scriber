﻿using PdfSharpCore.Drawing;
using System;
using Scriber.Layout;
using Scriber.Text;

namespace Scriber.Drawing
{
    public class PdfDrawingContext : IDrawingContext
    {
        public XGraphics? Graphics { get; set; }
        public Position Offset { get; set; }

        private XGraphics G => Graphics ?? throw new InvalidOperationException("Cannot use pdf drawing context without setting the Graphics property");

        public void DrawImage(Image image, Rectangle rectangle)
        {
            G.DrawImage(XImage.FromStream(() => image.GetStream()), new XRect(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height));
        }

        public void DrawText(TextRun run, Color color)
        {
            double yOffset = FontStyler.ScaleOffset(run.Typeface.Style, run.Typeface.Size);
            double size = FontStyler.ScaleSize(run.Typeface.Style, run.Typeface.Size);
            G.DrawString(run.Text, ToXFont(run.Typeface.Font, size, run.Typeface.Weight), ToXBrush(color), new XPoint(Offset.X, Offset.Y + yOffset));
        }

        private XFont ToXFont(Font font, double size, FontWeight weight)
        {
            // Quick convert via integer casting
            var xWeight = (XFontStyle)(int)weight;
            var xFont = new XFont(font.Name, size, xWeight);
            return xFont;
        }

        private XBrush ToXBrush(Color color)
        {
            return new XSolidBrush(XColor.FromArgb(color.Argb));
        }
    }
}
