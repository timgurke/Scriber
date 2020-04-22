﻿using System.Collections.Generic;
using Tex.Net.Layout;
using Tex.Net.Layout.Document;
using Tex.Net.Text;

namespace Tex.Net.Engine
{
    public class Environment
    {
        public string Name { get; set; }
        public List<object> Objects { get; } = new List<object>();

        public Environment(string name)
        {
            Name = name;
        }
    }
}
