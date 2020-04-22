﻿using System;
using System.Collections.Generic;
using System.Text;
using Tex.Net.Layout.Document;

namespace Tex.Net.Engine.Commands
{
    [Package]
    public static class Pagination
    {
        [Command("pagebreak")]
        public static Pagebreak Pagebreak()
        {
            return new Pagebreak();
        }

        [Command("thepage")]
        public static PageReference Thepage()
        {
            // Create self referencing element
            return new PageReference();
        }
    }
}
