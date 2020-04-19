﻿using System;

namespace Tex.Net.Engine
{
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class PackageAttribute : Attribute
    {
        public string Name { get; set; }

        public PackageAttribute() : this("_default")
        {

        }

        public PackageAttribute(string name)
        {
            Name = name;
        }
    }
}
