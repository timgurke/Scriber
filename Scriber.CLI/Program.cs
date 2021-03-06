﻿using System.Diagnostics;
using System.Linq;
using System.Text;
using Scriber.Bibliography;
using Scriber.Bibliography.BibTex.Language;
using Scriber.Bibliography.Styling;
using Scriber.Bibliography.Styling.Specification;
using Scriber.Engine;
using Scriber.Language;
using Scriber.Language.Antlr;
using Scriber.Logging;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;

namespace Scriber.CLI
{
    class Program
    {
        static void Main()
        {
            var style = File.Load<StyleFile>("ieee.csl");
            var processor = new Processor(style, LocaleFile.Defaults);

            var bibEntries = BibParser.Parse(System.IO.File.ReadAllText("lib.bib"), out var _);

            var citations = bibEntries.Select(e => e.ToCitation()).ToArray();

            for (int i = 0; i < citations.Length; i++)
            {
                citations[i]["citation-number"] = new TextVariable(i.ToString());
            }

            var runs = processor.Bibliography(new Localization.Culture("de-DE"), citations);


            Stopwatch watch = Stopwatch.StartNew();

            var context = new Context();
            context.Logger.Level = LogLevel.Debug;
            new ReflectionLoader().Discover(context, typeof(TestPackage).Assembly);

            StringBuilder sb = new StringBuilder();

            //sb.AppendLine("\\setlength[baselinestretch]{2}");
            //sb.AppendLine("\\cfooter{Page \\thepage}");

            //sb.AppendLine("\\section{This is a section}");

            //sb.AppendLine("\\begin{figure}");
            //sb.AppendLine("\\centering");
            //sb.AppendLine("\\includegraphics{test-image.png}");
            //sb.AppendLine("\\caption{First Test Image}");
            //sb.AppendLine("\\end{figure}");

            sb.AppendLine("// Das ist ein Test");
            sb.AppendLine("/*");
            sb.AppendLine("Das hier ist ein zweiter Test");
            sb.AppendLine("*/");
            sb.AppendLine("But I must explain to you how all this mistaken idea of denouncing pleasure and praising pain was born and I will give you a complete account of the system, and expound the actual teachings of the great explorer of the truth, the master-builder of human happiness. No one rejects, dislikes, or avoids pleasure itself, because it is pleasure, but because those who do not know how to pursue pleasure rationally encounter consequences that are extremely painful. Nor again is there anyone who loves or pursues or desires to obtain pain of itself, because it is pain, but because occasionally circumstances occur in which toil and pain can procure him some great pleasure. To take a trivial example, which of us ever undertakes laborious physical exercise, except to obtain some advantage from it? But who has any right to find fault with a man who chooses to enjoy a pleasure that has no annoying consequences, or one who avoids a pain that produces no resultant pleasure? On the other hand, we denounce with righteous indignation and dislike men who are so beguiled and demoralized by the charms of pleasure of the moment, so blinded by desire, that they cannot foresee the pain and trouble that are bound to ensue; and equal blame belongs to those who fail in their duty through weakness of will, which is the same as saying through shrinking from toil and pain. These cases are perfectly simple and easy to distinguish. In a free hour, when our power of choice is untrammelled and when nothing prevents our being able to do what we like best, every pleasure is to be welcomed and every pain avoided. But in certain circumstances and owing to the claims of duty or the obligations of business it will frequently occur that ");
            sb.AppendLine("Das ist ein toller @b()");
            sb.AppendLine("{");
            sb.AppendLine("Hier! das ist echt @bold() {test}");
            sb.AppendLine("}");

            sb.AppendLine("@test({a: d, key: value, enum: One}, {a: xD, key: value, enum: One})");

            sb.AppendLine("@BibliographyStyle(ieee.csl)");
            sb.AppendLine("@Bibliography(lib.bib)");

            sb.AppendLine();
            sb.AppendLine();
            sb.AppendLine();
            sb.AppendLine("@Section(This is a section)");
            sb.AppendLine();
            sb.AppendLine("Aft\\-er the @Color(blue, first) section [follows]@Footnote(Some Paragraph Content, \"1\") a @Bold(pagebreak).@Cite(Zanoni.2015)");
            sb.AppendLine("The following is a citation: @Cite(\"Zanoni.2015\")");
            sb.AppendLine();
            sb.AppendLine();
            sb.AppendLine();
            sb.AppendLine("But I must explain to you how all this mistaken idea of denouncing pleasure and praising pain was born and I will give you a complete account of the system, and expound the actual teachings of the great explorer of the truth, the master-builder of human happiness. No one rejects, dislikes, or avoids pleasure itself, because it is pleasure, but because those who do not know how to pursue pleasure rationally encounter consequences that are extremely painful. Nor again is there anyone who loves or pursues or desires to obtain pain of itself, because it is pain, but because occasionally circumstances occur in which toil and pain can procure him some great pleasure. To take a trivial example, which of us ever undertakes laborious physical exercise, except to obtain some advantage from it? But who has any right to find fault with a man who chooses to enjoy a pleasure that has no annoying consequences, or one who avoids a pain that produces no resultant pleasure? On the other hand, we denounce with righteous indignation and dislike men who are so beguiled and demoralized by the charms of pleasure of the moment, so blinded by desire, that they cannot foresee the pain and trouble that are bound to ensue; and equal blame belongs to those who fail in their duty through weakness of will, which is the same as saying through shrinking from toil and pain. These cases are perfectly simple and easy to distinguish. In a free hour, when our power of choice is untrammelled and when nothing prevents our being able to do what we like best, every pleasure is to be welcomed and every pain avoided. But in certain circumstances and owing to the claims of duty or the obligations of business it will frequently occur that ");
            //sb.AppendLine();
            //sb.AppendLine();
            //sb.AppendLine();
            //sb.AppendLine();
            //sb.AppendLine();
            //sb.AppendLine();
            //sb.AppendLine();
            //sb.AppendLine();
            //sb.AppendLine("\\begin{figure}");
            //sb.AppendLine();
            //sb.AppendLine();
            //sb.AppendLine("\\centering");
            //sb.AppendLine();
            //sb.AppendLine();
            //sb.AppendLine("\\includegraphics{test-image.png}");
            //sb.AppendLine();
            //sb.AppendLine();
            //sb.AppendLine("\\caption{Test Image}");
            //sb.AppendLine();
            //sb.AppendLine();
            //sb.AppendLine("\\end{figure}");

            //sb.AppendLine("\\begin{figure}");
            //sb.AppendLine("\\centering");
            //sb.AppendLine("\\includegraphics{test-image.png}");
            //sb.AppendLine("\\caption{Second Test Image}");
            //sb.AppendLine("\\end{figure}");

            //sb.AppendLine("\\begin{figure}");
            //sb.AppendLine("\\centering");
            //sb.AppendLine("\\includegraphics{test-image.png}");
            //sb.AppendLine("\\caption{Second Test Image}");
            //sb.AppendLine("\\end{figure}");

            sb.AppendLine("@Figure() {");
            sb.AppendLine("@Centering()");
            sb.AppendLine("@IncludeGraphics(test-image.png, { Draft: true })");
            sb.AppendLine("@Caption(Second Test Image)");
            sb.AppendLine("}");

            sb.AppendLine();
            sb.AppendLine("@PrintBibliography()");

            //sb.AppendLine();
            //sb.AppendLine("1 1 1 1 1 1 1 1 1 1");

            //sb.AppendLine();
            //sb.AppendLine();

            //for (int i = 0; i < 100; i++)
            //{
            //    sb.Append("1 ");
            //}
            //sb.AppendLine("\\begin{figure}");
            //sb.AppendLine("\\centering");
            //sb.AppendLine("\\includegraphics{test-image.png}");
            //sb.AppendLine("\\caption{Third Test Image}");
            //sb.AppendLine("\\end{figure}");

            //sb.AppendLine();
            ////sb.AppendLine("\\pagebreak");

            //for (int i = 0; i < 98; i++)
            //{
            //    sb.AppendLine("\\section{This is a section}");
            //    sb.AppendLine();
            //}
            
            sb.AppendLine("@figure()\n{ }");
            sb.AppendLine("@test(null)");
            sb.Append("@Figure() { }");
            sb.AppendLine("@includegraphics({Some, Text @Command()})");
            
            ScriberLexer lexer = new ScriberLexer(CharStreams.fromstring(sb.ToString()));
            ScriberParser parser = new ScriberParser(new CommonTokenStream(lexer));
            var root = parser.root();
            ScriberVisitor visitor = new ScriberVisitor();
            visitor.Visit(root);
            var result = Compiler.Compile(context, visitor.Elements);

            var document = result.Document;
            document.Run(context.Logger);

            var bytes = document.ToPdf();


            System.IO.File.WriteAllBytes("test.pdf", bytes);

            watch.Stop();
            Debug.WriteLine($"Created document in {watch.ElapsedMilliseconds}ms");
        }

        private static void Logger_Logged(Log log)
        {
            Debug.WriteLine(string.Join("", log.GetFullMessageParts()));
        }
    }
}
