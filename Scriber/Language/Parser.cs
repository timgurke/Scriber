﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Scriber.Language
{
    using Function = Func<ParserContext, Token, Element, Element?>;

    public static class Parser
    {
        //private readonly static ParserState StateGraph = new ParserState(null, null);
        private readonly static Dictionary<TokenType, Function> ParseActions = new Dictionary<TokenType, Function>();

        static Parser()
        {
            ParseActions[TokenType.Text] = ParseText;
            ParseActions[TokenType.At] = ParseCommand;
            ParseActions[TokenType.Quotation] = ParseQuotation;
            ParseActions[TokenType.CurlyOpen] = ParseCurlyOpen;
            ParseActions[TokenType.CurlyClose] = ParseCurlyClose;
            ParseActions[TokenType.BracketOpen] = ParseBracketOpen;
            ParseActions[TokenType.BracketClose] = ParseBracketClose;
            ParseActions[TokenType.Newline] = ParseNewline;
            ParseActions[TokenType.Percent] = ParsePercent;
            ParseActions[TokenType.ParenthesesOpen] = ParseParenthesisOpen;
            ParseActions[TokenType.ParenthesesClose] = ParseParenthesisClose;
            ParseActions[TokenType.Comma] = ParseComma;
            ParseActions[TokenType.Colon] = ParseColon;
        }

        private static Element ParsePercent(ParserContext context, Token token, Element previous)
        {
            context.Comment = true;
            return new Element(context.Parents.Peek(), ElementType.Comment, token.Index, context.Line) { StringBuilder = new StringBuilder() };
        }

        private static Element ParseText(ParserContext context, Token token, Element previous)
        {
            return AppendText(context, previous, context.Parents.Peek(), token);
        }

        private static Element? ParseNewline(ParserContext context, Token token, Element previous)
        {
            context.Line++;
            context.Comment = false;
            context.LastLine.Clear();
            context.LastLine.AddRange(context.CurrentLine);
            context.CurrentLine.Clear();
            if (IsLineMixed(context.LastLine) && !context.Newline)
            {
                context.Newline = true;
                return AppendText(context, previous, context.Parents.Peek(), new Token(" ", token.Index));
            }
            else if (context.Newline)
            {
                context.Newline = false;
                return new Element(context.Parents.Peek(), ElementType.Paragraph, token.Index, context.Line);
            }
            else
            {
                return null;
            }
        }

        public static Element? ParseQuotation(ParserContext context, Token token, Element previous)
        {
            if (context.Quotation)
            {
                context.Quotation = false;
                return null;
            }
            else
            {
                context.Quotation = true;
                return new Element(context.Parents.Peek(), ElementType.Quotation, token.Index, context.Line) { StringBuilder = new StringBuilder() };
            }
        }

        public static Element? ParseComma(ParserContext context, Token token, Element previous)
        {
            var secondParent = context.PeekParent(1);

            if (secondParent.Type == ElementType.Command)
            {
                context.Parents.Pop();
                var element = new Element(secondParent, ElementType.Block, token.Index, context.Line);
                context.Parents.Push(element);
                return element;
            }
            else if (secondParent.Type == ElementType.ObjectField)
            {
                context.Parents.Pop();
                context.Parents.Pop();
                return null;
            }
            else if (secondParent.Type == ElementType.ObjectArray)
            {
                context.Parents.Pop();
                var block = new Element(context.Parents.Peek(), ElementType.Block, token.Index, context.Line);
                context.Parents.Push(block);
                return block;
            }
            else
            {
                return AppendText(context, previous, context.Parents.Peek(), token);
            }
        }

        private static Element ParseCommand(ParserContext context, Token token, Element previous)
        {
            var command = new Element(context.Parents.Peek(), ElementType.Command, token.Index, context.Line)
            {
                Content = token.Content.Substring(1)
            };
            return command;
        }

        private static Element ParseParenthesisOpen(ParserContext context, Token token, Element previous)
        {
            if (previous.Type == ElementType.Command)
            {
                context.Parents.Push(previous);
                var element = new Element(previous, ElementType.Block, token.Index, context.Line);
                context.Parents.Push(element);
                return element;
            }
            else
            {
                return AppendText(context, previous, context.Parents.Peek(), token);
            }
        }

        private static Element? ParseParenthesisClose(ParserContext context, Token token, Element previous)
        {
            if (context.InCommand())
            {
                context.Parents.Pop();
                context.Parents.Pop();
                return null;
            }
            else
            {
                return AppendText(context, previous, context.Parents.Peek(), token);
            }
        }

        private static Element ParseCurlyOpen(ParserContext context, Token token, Element previous)
        {
            Element parent;

            if (previous.Type == ElementType.Command && context.Parents.Peek().Type != ElementType.Command)
            {
                context.Parents.Push(previous);
            }

            parent = context.Parents.Peek();
            var element = new Element(parent, ElementType.ExpliciteBlock, token.Index, context.Line);
            context.Parents.Push(element);
            return element;
        }

        private static Element? ParseCurlyClose(ParserContext context, Token token, Element previous)
        {
            if (context.Parents.Count > 1)
            {
                if (context.PeekParent(1).Type == ElementType.ObjectField)
                {
                    context.Parents.Pop();
                    context.Parents.Pop();
                }

                context.Parents.Pop();
                return null;
            }
            else
            {
                // Too many closing brackets
                throw new ParserException("");
            }
        }

        private static Element ParseBracketOpen(ParserContext context, Token token, Element previous)
        {
            var commandParent = context.PeekParent(1);
            if (commandParent.Type == ElementType.Command && previous.Type == ElementType.Block && previous.Children.Count == 0 || previous.Type == ElementType.ObjectCreation || commandParent.Type == ElementType.ObjectCreation)
            {
                if (commandParent.Type == ElementType.ObjectCreation)
                {
                    previous = context.Parents.Peek();
                }

                var array = new Element(previous, ElementType.ObjectArray, token.Index, context.Line);
                context.Parents.Push(array);
                var block = new Element(array, ElementType.Block, token.Index, context.Line);
                context.Parents.Push(block);
                return block;
            }
            else
            {
                return AppendText(context, previous, context.Parents.Peek(), token);
            }
        }

        private static Element? ParseBracketClose(ParserContext context, Token token, Element previous)
        {
            if (context.PeekParent(1).Type == ElementType.ObjectArray)
            {
                var block = context.Parents.Peek();
                if (block.Type == ElementType.Block && block.Children.Count == 0)
                {
                    if (context.PeekParent(1).Children.Count > 1)
                    {
                        // 
                        throw new ParserException();
                    }

                    block.Detach();
                }

                context.Parents.Pop();
                context.Parents.Pop();
                return null;
            }
            else
            {
                return AppendText(context, previous, context.Parents.Peek(), token);
            }
        }

        private static Element? ParseColon(ParserContext context, Token token, Element previous)
        {
            var parent = context.Parents.Peek();
            if (context.InCommand() && (parent.Type == ElementType.ExpliciteBlock || parent.Type == ElementType.ObjectCreation))
            {
                parent.Type = ElementType.ObjectCreation;
                previous.Type = ElementType.ObjectField;
                context.Parents.Push(previous);
                var block = new Element(previous, ElementType.Block, token.Index, context.Line);
                context.Parents.Push(block);
                return block;
            }
            else
            {
                return AppendText(context, previous, parent, token);
            }
        }

        public static Element[] Parse(IEnumerable<Token> tokens)
        {
            return ParseInternal(tokens).ToArray();
        }

        public static Element[] ParseFromString(string text)
        {
            return Parse(Lexer.Tokenize(text));
        }

        private static IEnumerable<Element> ParseInternal(IEnumerable<Token> tokens)
        {
            if (tokens == null)
                throw new ArgumentNullException(nameof(tokens));

            var items = tokens.ToArray();

            if (items.Length == 0)
                yield break;

            Element cur = new Element(null, ElementType.Block, 0, 0);
            var context = new ParserContext();
            context.Parents.Push(cur);

            foreach (var token in items)
            {
                var action = ParseActions[token.Type];
                bool isNewline = token.Type == TokenType.Newline;
                bool isCurlyOpen = token.Type == TokenType.CurlyOpen;
                bool isBracketOpen = token.Type == TokenType.BracketOpen;

                if (token.Type != TokenType.Quotation && context.Quotation)
                {
                    cur.StringBuilder?.Append(token.Content);
                    continue;
                }

                if (!isNewline && context.Comment)
                {
                    cur.StringBuilder?.Append(token.Content);
                    continue;
                }
                else if (!isNewline)
                {
                    context.Newline = false;
                }

                var element = action(context, token, cur);

                if (element == null)
                {
                    if (!context.Parents.TryPeek(out var top))
                    {
                        cur = TopParent(cur);
                        BuildContent(cur);
                        yield return cur;
                        cur = new Element(null, ElementType.Block, token.Index, context.Line);
                        context.Parents.Push(cur);
                    }
                    else
                    {
                        cur = top;
                    }
                }
                else
                {
                    // if at top parent
                    if (context.Parents.Count == 1)
                    {
                        context.CurrentLine.Add(element);
                    }
                    cur = element;
                }
            }

            // If blocks or commands where left hanging open
            if (context.Parents.Count > 2)
            {
                throw new ParserException("");
            }

            cur = TopParent(cur);
            BuildContent(cur);
            yield return cur;
        }

        private static Element TopParent(Element element)
        {
            if (element.Parent == null)
            {
                return element;
            }
            else
            {
                return TopParent(element.Parent);
            }
        }

        private static void BuildContent(Element element)
        {
            SetContent(element);

            foreach (var inline in element.Children.ToArray())
            {
                BuildContent(inline);
            }

            if (element.Type == ElementType.Text && string.IsNullOrEmpty(element.Content))
            {
                element.Parent?.Children.Remove(element);
            }
        }

        private static void SetContent(Element element)
        {
            if (element.StringBuilder != null)
            {
                var text = element.StringBuilder.ToString();

                if (element.Type == ElementType.ObjectField)
                {
                    text = text.Trim();
                }
                else if (element.Type == ElementType.Text)
                {
                    element.Siblings(out var previous, out var next);

                    if (previous == null || CutOffType(previous.Type))
                    {
                        text = text.TrimStart();
                    }
                    if (next == null || CutOffType(next.Type))
                    {
                        text = text.TrimEnd();
                    }

                    if (previous == null && next == null && text == "null")
                    {
                        element.Type = ElementType.Null;
                    }
                }
                element.Content = text;
                element.StringBuilder = null;
                element.Length = element.Content.Length;
            }
        }

        private static bool CutOffType(ElementType type)
        {
            return !(type == ElementType.Text || type == ElementType.Quotation || type == ElementType.Command); 
        }

        private static Element AppendText(ParserContext context, Element previous, Element parent, Token token)
        {
            var textItem = previous.Type == ElementType.Text ? previous : new Element(parent, ElementType.Text, token.Index, context.Line);

            if (textItem.StringBuilder == null)
            {
                textItem.StringBuilder = new StringBuilder(token.Content);
            }
            else
            {
                textItem.StringBuilder.Append(token.Content);
            }

            return textItem;
        }

        private static bool IsLineMixed(IEnumerable<Element> elements)
        {
            return elements.Any(e => e.Type == ElementType.Text);
        }
    }
}
