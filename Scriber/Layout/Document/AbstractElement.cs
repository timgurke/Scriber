﻿using Scriber.Text;

namespace Scriber.Layout.Document
{
    public abstract class AbstractElement
    {
        public AbstractElement? Parent { get; set; }

        public Font? Font
        {
            get => font ?? Parent?.Font;
            set => font = value;
        }
        public double FontSize
        {
            get => fontSize ?? Parent?.FontSize ?? 0;
            set => fontSize = value;
        }
        public FontWeight FontWeight
        {
            get => fontWeight ?? Parent?.FontWeight ?? FontWeight.Normal;
            set => fontWeight = value;
        }

        public FontStyle FontStyle
        {
            get => fontStyle ?? Parent?.FontStyle ?? FontStyle.Normal;
            set => fontStyle = value;
        }

        public Color Foreground
        {
            get => foreground ?? Parent?.Foreground ?? Colors.Black;
            set => foreground = value;
        }

        public HorizontalAlignment HorizontalAlignment
        {
            get => horizontalAlignment ?? Parent?.HorizontalAlignment ?? 0;
            set => horizontalAlignment = value;
        }

        public VerticalAlignment VerticalAlignment
        {
            get => verticalAlignment ?? Parent?.VerticalAlignment ?? 0;
            set => verticalAlignment = value;
        }

        public DocumentPage? Page
        {
            get => page ?? Parent?.Page;
            set => page = value;
        }

        public Scriber.Document? Document
        {
            get => document ?? Parent?.Document;
            set => document = value;
        }

        public object? Tag { get; set; }

        public Thickness Margin { get; set; } = Thickness.Zero;
        
        private Font? font;
        private double? fontSize;
        private FontStyle? fontStyle;
        private Color? foreground;
        private FontWeight? fontWeight;
        private HorizontalAlignment? horizontalAlignment;
        private VerticalAlignment? verticalAlignment;
        private DocumentPage? page;
        private Scriber.Document? document;

        public AbstractElement Clone()
        {
            var clone = CloneInternal();
            clone.Margin = Margin;
            clone.font = font;
            clone.fontSize = fontSize;
            clone.foreground = foreground;
            clone.fontWeight = fontWeight;
            clone.fontStyle = fontStyle;
            clone.document = document;
            clone.horizontalAlignment = horizontalAlignment;
            return clone;
        }

        protected abstract AbstractElement CloneInternal();

        public virtual void Interlude()
        {

        }
    }
}
