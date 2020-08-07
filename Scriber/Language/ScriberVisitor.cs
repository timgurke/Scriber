namespace Scriber.Language {

    using System.Collections.Generic;
    using System.Diagnostics;
    using Scriber.Language.Antlr;

    public class ScriberVisitor : ScriberBaseVisitor<Element> {

        public IList<Element> Elements {get;}

        public ScriberVisitor() {
            Elements = new List<Element>();
        }
        
        public override Element VisitRoot(ScriberParser.RootContext context) {
            var lines = context.line();
            for (int i = 0; i < lines.Length; i++) {
                Elements.Add(Visit(lines[i]));
            }
            return VisitChildren(context);
        }

        public override Element VisitLine(ScriberParser.LineContext context) {
            var lineContent = context.lineContent();
            if (lineContent == null) {
                return VisitChildren(context);
            } else {
                return Visit(lineContent);
            }
        }

        public override Element VisitLineContent(ScriberParser.LineContentContext context) {
            var environment = context.environment();
            var quotation = context.quotation();
            var text = context.text();

            var element = new Element(null, ElementType.Paragraph);

            for (int i = 0; i < environment.Length; i++) {
                var environmentElement = Visit(environment[i]);
                element.Children.AddLast(environmentElement);
            }

            for (int i = 0; i < quotation.Length; i++) {
                var quotationElement = Visit(quotation[i]);
                element.Children.AddLast(quotationElement);
            }

            for (int i = 0; i < text.Length; i++) {
                var textElement = new Element(element, ElementType.Text);
                element.Content = text[i].GetText();
            }

            return element;
        }
                
    }
}