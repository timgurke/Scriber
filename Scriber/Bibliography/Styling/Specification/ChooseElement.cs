﻿using Scriber.Bibliography.Styling.Formatting;
using System.Linq;
using System.Xml.Serialization;

namespace Scriber.Bibliography.Styling.Specification
{
    /// <summary>
    /// The cs:choose rendering element allows for conditional rendering of rendering elements.
    /// </summary>
    public class ChooseElement : RenderingElement
    {
        /// <summary>
        /// Represents the first, 'if', conditional branch.
        /// </summary>
        [XmlElement("if")]
        public IfElement? If
        {
            get;
            set;
        }
        /// <summary>
        /// Represents the subsequent, 'else if' conditional branches.
        /// </summary>
        [XmlElement("else-if")]
        public IfElement[]? ElseIf
        {
            get;
            set;
        }
        /// <summary>
        /// Represents the last, 'else' conditional branch.
        /// </summary>
        [XmlElement("else")]
        public ElseElement? Else
        {
            get;
            set;
        }

        public override void EvaluateOverride(Interpreter interpreter, Citation citation)
        {
            ElseElement? renderingElement = null;

            if (If != null && If.Matches(citation))
            {
                renderingElement = If;
            }
            else if (ElseIf != null)
            {
                foreach (var elseIf in ElseIf)
                {
                    if (elseIf.Matches(citation))
                    {
                        renderingElement = elseIf;
                        break;
                    }
                }
            }

            if (renderingElement == null)
            {
                renderingElement = Else;
            }

            if (renderingElement != null)
            {
                renderingElement.Evaluate(interpreter, citation);
            }
        }

        public override bool HasVariableDefined(Interpreter interpreter, Citation citation)
        {
            return (If != null && If.HasVariableDefined(interpreter, citation)) || (ElseIf != null && ElseIf.Any(e => e.HasVariableDefined(interpreter, citation))) || (Else != null && Else.HasVariableDefined(interpreter, citation));
        }
    }
}