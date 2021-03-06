﻿using Scriber.Layout;
using Scriber.Layout.Document;

namespace Scriber.Engine.Commands
{
    [Package]
    public static class Layouts
    {
        [Command("VerticalSpace")]
        public static Box VSpace(string vertical)
        {
            double value = double.Parse(vertical);
            return new Box(new Layout.Size(0, value));
        }

        [Command("Centering")]
        public static CallbackArrangingBlock Centering()
        {
            return new CallbackArrangingBlock(Center);

            static void Center(DocumentElement block)
            {
                if (block.Parent != null)
                {
                    block.Parent.HorizontalAlignment = HorizontalAlignment.Center;
                }
            }
        }

        [Command("SetLength")]
        public static void SetLength(CompilerState state, string lengthName, string length)
        {
            var l = double.Parse(length);
            state.Document.Variables[DocumentVariables.Length][lengthName].SetValue(l);
        }

        [Command("SetCounter")]
        public static CallbackArrangingBlock SetCounter(CompilerState state, string counter, string count)
        {
            int intCount = int.Parse(count);
            return new CallbackArrangingBlock(Set);

            void Set(DocumentElement element)
            {
                element.Document?.PageNumbering.Set(intCount);
            }
        }
    }
}
