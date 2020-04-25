﻿using Tex.Net.Drawing;

using Img = SixLabors.ImageSharp.Image<SixLabors.ImageSharp.PixelFormats.Rgba32>;

namespace Tex.Net.Layout.Document
{
    public class ImageElement : Block
    {
        private readonly byte[] imageData;
        public Img Image { get; }

        public Unit Height { get; set; }
        public Unit Width { get; set; }
        public double Scale { get; set; } = 1.0;
        public double Angle { get; set; } = 0.0;
        public Position Origin { get; set; }

        public string FileName { get; }

        public bool KeepAspectRatio { get; set; } = true;
        public bool Draft { get; set; } = false;

        public ImageElement(byte[] imageData, string fileName) : this(null, imageData, fileName)
        {
            
        }

        private ImageElement(Img image, byte[] imageData, string fileName)
        {
            FileName = fileName;
            this.imageData = imageData;
            Image = image ?? SixLabors.ImageSharp.Image.Load(imageData);
            Height = new Unit(Image.Height, UnitType.Presentation);
            Width = new Unit(Image.Width, UnitType.Presentation);
            Origin = new Position(Width.Presentation / 2, Height.Presentation / 2);
        }

        public override void OnRender(IDrawingContext drawingContext, Measurement measurement)
        {
            var image = new Image(imageData, Image);
            drawingContext.DrawImage(image, new Rectangle(measurement.Position, measurement.Size));
        }

        protected override DocumentElement CloneInternal()
        {
            var image = new ImageElement(Image, imageData, FileName)
            {
                Height = Height,
                Width = Width,
                Scale = Scale,
                Angle = Angle,
                Origin = Origin,
                KeepAspectRatio = KeepAspectRatio,
                Draft = Draft
            };
            return image;
        }

        protected override Measurements MeasureOverride(Size availableSize)
        {
            var size = new Size(Width.Presentation * Scale, Height.Presentation * Scale);

            var ms = new Measurements();
            var m = new Measurement(this, size, Margin);
            ms.Add(m);
            return ms;
        }
    }
}