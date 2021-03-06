﻿using PdfSharpCore.Fonts;
using SixLabors.Fonts;
using System.IO;

namespace Scriber.Text
{
    public class Font
    {
        private static readonly FontCollection fonts = new FontCollection();

        private static readonly FontFamily serif = fonts.Install(LoadFont("cmu.serif-roman"));
        private static readonly FontFamily serifItalic = fonts.Install(LoadFont("cmu.serif-italic"));
        private static readonly FontFamily serifBold = fonts.Install(LoadFont("cmu.serif-bold"));
        private static readonly FontFamily serifBoldItalic = fonts.Install(LoadFont("cmu.serif-bolditalic"));

        private const float Dpi = 72;

        public static Font Serif { get; } = new Font(serif);
        public static Font SerifItalic { get; } = new Font(serifItalic);
        public static Font SerifBold { get; } = new Font(serifBold);
        public static Font SerifBoldItalic { get; } = new Font(serifBoldItalic);

        static Font()
        {
            GlobalFontSettings.FontResolver = new FontResolver();
        }

        internal FontFamily FontFamily { get; set; }

        public string Name { get; }

        private Font(FontFamily fontFamily)
        {
            Name = fontFamily.Name;
            FontFamily = fontFamily;
        }

        public double GetWidth(string text, double size, FontWeight weight)
        {
            var realizedFont = FontFamily.CreateFont((float)size, (SixLabors.Fonts.FontStyle)(int)weight);
            return TextMeasurer.Measure(text, new RendererOptions(realizedFont, Dpi)).Width;
        }

        private static Stream LoadFont(string name)
        {
            var asm = typeof(Font).Assembly;
            var fullName = $"Scriber.Resources.Fonts.{name}.ttf";
            using var stream = asm.GetManifestResourceStream(fullName);

            if (stream == null)
            {
                throw new IOException("Could not load font: " + name);
            }

            using var ms = new MemoryStream();
            stream.CopyTo(ms);
            ms.Position = 0;
            return new MemoryStream(ms.ToArray());
        }
    }
}
