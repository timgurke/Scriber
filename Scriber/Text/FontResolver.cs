﻿using PdfSharpCore.Fonts;
using System;
using System.IO;

namespace Scriber.Text
{
    public class FontResolver : IFontResolver
    {
        public string DefaultFontName => "CMU Serif";

        public byte[] GetFont(string faceName)
        {
            var asm = typeof(Font).Assembly;
            var fullName = $"Scriber.Resources.Fonts.{faceName}.ttf";
            using var stream = asm.GetManifestResourceStream(fullName);

            if (stream == null)
            {
                throw new IOException("Could not load font: " + faceName);
            }

            using var ms = new MemoryStream();
            stream.CopyTo(ms);
            return ms.ToArray();
        }

        public FontResolverInfo? ResolveTypeface(string familyName, bool isBold, bool isItalic)
        {
            if (familyName.Equals("CMU Serif", StringComparison.CurrentCultureIgnoreCase))
            {
                if (isBold && isItalic)
                {
                    return new FontResolverInfo("cmu.serif-bolditalic");
                }
                else if (isBold)
                {
                    return new FontResolverInfo("cmu.serif-bold");
                }
                else if (isItalic)
                {
                    return new FontResolverInfo("cmu.serif-italic");
                }
                else
                {
                    return new FontResolverInfo("cmu.serif-roman");
                }
            }
            return null;
        }
    }
}
