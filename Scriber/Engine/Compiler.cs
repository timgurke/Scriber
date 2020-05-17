﻿using System.Collections.Generic;
using System.Linq;
using Scriber.Language;
using Scriber.Layout.Document;
using Scriber.Util;

namespace Scriber.Engine
{
    public static class Compiler
    {
        public static CompilerResult Compile(IEnumerable<Element> elements)
        {
            var state = new CompilerState();

            foreach (var element in elements)
            {
                Execute(state, element);
            }

            state.Document.Elements.AddRange(state.Blocks.Current.Objects.OfType<DocumentElement>());

            ResolveCallbacks(state.Document.Elements);

            Document? doc = state.Document;

            if (state.Issues.Any(e => e.Type == CompilerIssueType.Error))
            {
                doc = null;
            }

            var result = new CompilerResult(doc);
            result.Issues.AddRange(state.Issues);
            return result;
        }

        private static void Execute(CompilerState state, Element element)
        {
            if (BlockElement(element))
            {
                state.Blocks.Push();
            }

            foreach (var inline in element.Children)
            {
                Execute(state, inline);
            }

            var obj = state.Execute(element, state.Blocks.Current.Objects.ToArray());

            if (BlockElement(element))
            {
                state.Blocks.Pop();
                // If we just created a new block via an environment
                // in the last instruction push it to the state.
                if (obj is Block env)
                {
                    state.Blocks.Push(env);
                }
            }

            if (obj != null && !(obj is Block))
            {
                // basically pass the returned object to the previous environment
                var flattened = obj.ConvertToFlatArray();
                state.Blocks.Current.Objects.AddRange(flattened);
            }
        }

        private static bool BlockElement(Element element)
        {
            return element.Type == ElementType.Block 
                || element.Type == ElementType.Command 
                || element.Type == ElementType.ExpliciteBlock 
                || element.Type == ElementType.ObjectArray 
                || element.Type == ElementType.ObjectCreation 
                || element.Type == ElementType.ObjectField;
        }

        private static void ResolveCallbacks(ElementCollection<DocumentElement> blocks)
        {
            for (int i = 0; i < blocks.Count; i++)
            {
                var block = blocks[i];
                if (block is CallbackBlock callbackBlock)
                {
                    blocks[i] = callbackBlock.Callback();
                }
            }
        }
    }
}