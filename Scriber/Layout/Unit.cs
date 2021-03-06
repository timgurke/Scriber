﻿#region PDFsharp - A .NET library for processing PDF
//
// Authors:
//   Stefan Lange
//
// Copyright (c) 2005-2016 empira Software GmbH, Cologne Area (Germany)
//
// http://www.PdfSharpCore.com
// http://sourceforge.net/projects/pdfsharp
//
// Permission is hereby granted, free of charge, to any person obtaining a
// copy of this software and associated documentation files (the "Software"),
// to deal in the Software without restriction, including without limitation
// the rights to use, copy, modify, merge, publish, distribute, sublicense,
// and/or sell copies of the Software, and to permit persons to whom the
// Software is furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included
// in all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL
// THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER 
// DEALINGS IN THE SOFTWARE.
#endregion

using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace Scriber.Layout
{
    public enum UnitType
    {
        Point,
        Inch,
        Millimeter,
        Centimeter,
        Presentation
    }

    [DebuggerDisplay("{DebuggerDisplay}")]
    public struct Unit : IFormattable, IEquatable<Unit>
    {
        internal const double PointFactor = 1;
        internal const double InchFactor = 72;
        internal const double MillimeterFactor = 72 / 25.4;
        internal const double CentimeterFactor = 72 / 2.54;
        internal const double PresentationFactor = 72 / 96.0;

        internal const double PointFactorWpf = 96 / 72.0;
        internal const double InchFactorWpf = 96;
        internal const double MillimeterFactorWpf = 96 / 25.4;
        internal const double CentimeterFactorWpf = 96 / 2.54;
        internal const double PresentationFactorWpf = 1;

        /// <summary>
        /// Initializes a new instance of the XUnit class with type set to point.
        /// </summary>
        public Unit(double point)
        {
            Value = point;
            Type = UnitType.Point;
        }

        /// <summary>
        /// Initializes a new instance of the XUnit class.
        /// </summary>
        public Unit(double value, UnitType type)
        {
            if (!Enum.IsDefined(typeof(UnitType), type))
                throw new System.ComponentModel.InvalidEnumArgumentException("type");
            Value = value;
            Type = type;
        }

        /// <summary>
        /// Gets the raw value of the object without any conversion.
        /// To determine the XGraphicsUnit use property <code>Type</code>.
        /// To get the value in point use the implicit conversion to double.
        /// </summary>
        public double Value { get; private set; }

        /// <summary>
        /// Gets the unit of measure.
        /// </summary>
        public UnitType Type { get; private set; }

        /// <summary>
        /// Gets or sets the value in point.
        /// </summary>
        public double Point
        {
            get
            {
                return Type switch
                {
                    UnitType.Point => Value,
                    UnitType.Inch => Value * InchFactor,
                    UnitType.Millimeter => Value * MillimeterFactor,
                    UnitType.Centimeter => Value * CentimeterFactor,
                    UnitType.Presentation => Value * PresentationFactor,
                    _ => throw new InvalidCastException(),
                };
            }
            set
            {
                Value = value;
                Type = UnitType.Point;
            }
        }

        /// <summary>
        /// Gets or sets the value in inch.
        /// </summary>
        public double Inch
        {
            get
            {
                return Type switch
                {
                    UnitType.Point => Value / 72,
                    UnitType.Inch => Value,
                    UnitType.Millimeter => Value / 25.4,
                    UnitType.Centimeter => Value / 2.54,
                    UnitType.Presentation => Value / 96,
                    _ => throw new InvalidCastException(),
                };
            }
            set
            {
                Value = value;
                Type = UnitType.Inch;
            }
        }

        /// <summary>
        /// Gets or sets the value in millimeter.
        /// </summary>
        public double Millimeter
        {
            get
            {
                return Type switch
                {
                    UnitType.Point => Value * 25.4 / 72,
                    UnitType.Inch => Value * 25.4,
                    UnitType.Millimeter => Value,
                    UnitType.Centimeter => Value * 10,
                    UnitType.Presentation => Value * 25.4 / 96,
                    _ => throw new InvalidCastException(),
                };
            }
            set
            {
                Value = value;
                Type = UnitType.Millimeter;
            }
        }

        /// <summary>
        /// Gets or sets the value in centimeter.
        /// </summary>
        public double Centimeter
        {
            get
            {
                return Type switch
                {
                    UnitType.Point => Value * 2.54 / 72,
                    UnitType.Inch => Value * 2.54,
                    UnitType.Millimeter => Value / 10,
                    UnitType.Centimeter => Value,
                    UnitType.Presentation => Value * 2.54 / 96,
                    _ => throw new InvalidCastException(),
                };
            }
            set
            {
                Value = value;
                Type = UnitType.Centimeter;
            }
        }

        /// <summary>
        /// Gets or sets the value in presentation units (1/96 inch).
        /// </summary>
        public double Presentation
        {
            get
            {
                return Type switch
                {
                    UnitType.Point => Value * 96 / 72,
                    UnitType.Inch => Value * 96,
                    UnitType.Millimeter => Value * 96 / 25.4,
                    UnitType.Centimeter => Value * 96 / 2.54,
                    UnitType.Presentation => Value,
                    _ => throw new InvalidCastException(),
                };
            }
            set
            {
                Value = value;
                Type = UnitType.Point;
            }
        }

        /// <summary>
        /// Returns the object as string using the format information.
        /// The unit of measure is appended to the end of the string.
        /// </summary>
        public string ToString(IFormatProvider formatProvider)
        {
            string valuestring = Value.ToString(formatProvider) + GetSuffix();
            return valuestring;
        }

        /// <summary>
        /// Returns the object as string using the specified format and format information.
        /// The unit of measure is appended to the end of the string.
        /// </summary>
        string IFormattable.ToString(string? format, IFormatProvider? formatProvider)
        {
            string valuestring = Value.ToString(format, formatProvider) + GetSuffix();
            return valuestring;
        }

        /// <summary>
        /// Returns the object as string. The unit of measure is appended to the end of the string.
        /// </summary>
        public override string ToString()
        {
            string valuestring = Value.ToString(CultureInfo.InvariantCulture) + GetSuffix();
            return valuestring;
        }

        /// <summary>
        /// Returns the unit of measure of the object as a string like 'pt', 'cm', or 'in'.
        /// </summary>
        string GetSuffix()
        {
            return Type switch
            {
                UnitType.Point => "pt",
                UnitType.Inch => "in",
                UnitType.Millimeter => "mm",
                UnitType.Centimeter => "cm",
                UnitType.Presentation => "pu",
                _ => throw new InvalidCastException(),
            };
        }

        /// <summary>
        /// Returns an XUnit object. Sets type to point.
        /// </summary>
        public static Unit FromPoint(double value)
        {
            Unit unit = new Unit
            {
                Value = value,
                Type = UnitType.Point
            };
            return unit;
        }

        /// <summary>
        /// Returns an XUnit object. Sets type to inch.
        /// </summary>
        public static Unit FromInch(double value)
        {
            Unit unit = new Unit
            {
                Value = value,
                Type = UnitType.Inch
            };
            return unit;
        }

        /// <summary>
        /// Returns an XUnit object. Sets type to millimeters.
        /// </summary>
        public static Unit FromMillimeter(double value)
        {
            Unit unit = new Unit
            {
                Value = value,
                Type = UnitType.Millimeter
            };
            return unit;
        }

        /// <summary>
        /// Returns an XUnit object. Sets type to centimeters.
        /// </summary>
        public static Unit FromCentimeter(double value)
        {
            Unit unit = new Unit
            {
                Value = value,
                Type = UnitType.Centimeter
            };
            return unit;
        }

        /// <summary>
        /// Returns an XUnit object. Sets type to Presentation.
        /// </summary>
        public static Unit FromPresentation(double value)
        {
            Unit unit = new Unit
            {
                Value = value,
                Type = UnitType.Presentation
            };
            return unit;
        }

        /// <summary>
        /// Memberwise comparison. To compare by value, 
        /// use code like Math.Abs(a.Pt - b.Pt) &lt; 1e-5.
        /// </summary>
        public static bool operator ==(Unit value1, Unit value2)
        {
            return value1.Equals(value2);
        }

        /// <summary>
        /// Memberwise comparison. To compare by value, 
        /// use code like Math.Abs(a.Pt - b.Pt) &lt; 1e-5.
        /// </summary>
        public static bool operator !=(Unit value1, Unit value2)
        {
            return !(value1 == value2);
        }

        /// <summary>
        /// Calls base class Equals.
        /// </summary>
        public override bool Equals(object? obj) => obj is Unit unit && Equals(unit);

        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        public override int GetHashCode()
        {
            return HashCode.Combine(Value, Type);
        }

        /// <summary>
        /// Converts an existing object from one unit into another unit type.
        /// </summary>
        public void ConvertType(UnitType type)
        {
            if (Type == type)
                return;

            switch (type)
            {
                case UnitType.Point:
                    Value = Point;
                    Type = UnitType.Point;
                    break;

                case UnitType.Inch:
                    Value = Inch;
                    Type = UnitType.Inch;
                    break;

                case UnitType.Centimeter:
                    Value = Centimeter;
                    Type = UnitType.Centimeter;
                    break;

                case UnitType.Millimeter:
                    Value = Millimeter;
                    Type = UnitType.Millimeter;
                    break;

                case UnitType.Presentation:
                    Value = Presentation;
                    Type = UnitType.Presentation;
                    break;

                default:
                    throw new ArgumentException("Unknown unit type: '" + type + "'");
            }
        }

        public bool Equals([AllowNull] Unit other)
        {
            if (other == null)
            {
                return false;
            }

            return Type == other.Type && Value == other.Value;
        }

        /// <summary>
        /// Represents a unit with all values zero.
        /// </summary>
        public static readonly Unit Zero = new Unit();

        /// <summary>
        /// Gets the DebuggerDisplayAttribute text.
        /// </summary>
        /// <value>The debugger display.</value>
        string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture, "unit=({0:0000} {1})", Value, GetSuffix());
            }
        }
    }
}
