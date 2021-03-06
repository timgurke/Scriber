﻿using System;
using Scriber.Drawing;

namespace Scriber.Layout.Document
{
    [Flags]
    public enum FixedPosition
    {
        Top = 1,
        Bottom = 2,

        Left = 4,
        Center = 8,
        Right = 16,

        TopLeft = 5,
        BottomLeft = 6,
        TopCenter = 9,
        BottomCenter = 10,
        TopRight = 17,
        BottomRight = 18
    }

    public class FixedBlock : DocumentElement
    {
        public DocumentElement Child { get; }
        public FixedPosition Position { get; set; } = FixedPosition.TopLeft;

        public FixedBlock(DocumentElement child)
        {
            Child = child;
            Child.Parent = this;
        }

        public override void OnRender(IDrawingContext drawingContext, Measurement measurement)
        {
            Child.OnRender(drawingContext, measurement);
        }

        protected override AbstractElement CloneInternal()
        {
            return new FixedBlock(Child.Clone())
            {
                Position = Position
            };
        }

        public override void Interlude()
        {
            Child.Interlude();
        }

        protected override Measurements MeasureOverride(Size availableSize)
        {
            var measurements = Child.Measure(availableSize);

            foreach (var measurement in measurements)
            {
                measurement.Position = new Position(10, 10);
            }

            return measurements;
        }
    }
}
