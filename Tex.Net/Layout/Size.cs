﻿namespace Tex.Net.Layout
{
    public struct Size
    {
        public double Width;
        public double Height;

        public Size(double width, double height)
        {
            Width = width;
            Height = height;
        }

        public override bool Equals(object obj)
        {
            if (obj is Size size)
            {
                return Width == size.Width && Height == size.Height;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return Width.GetHashCode() + Height.GetHashCode() * 13;
        }

        public override string ToString()
        {
            return $"Width: {Width.ToString("N4")} Height: {Height.ToString("N4")}";
        }

        public static bool operator ==(Size left, Size right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Size left, Size right)
        {
            return !(left == right);
        }
    }
}
