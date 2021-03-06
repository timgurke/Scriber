﻿using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

namespace Scriber.Bibliography.Styling.Specification
{
    /// <summary>
    /// Represents localization data which can be included in a styles. Each locale element contains localization data for a single
    /// language dialect. This locale code is set on the required xml:lang attribute on the cs:locale root element.
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("{XmlLang}")]
    public class LocaleElement : Element, ILocale
    {
        /// <summary>
        /// The locale code of this locale.
        /// </summary>
        [XmlAttribute("xml:lang")]
        public string? XmlLang
        {
            get;
            set;
        }

        /// <summary>
        /// Style options of the locale.
        /// </summary>
        [XmlElement("style-options")]
        public StyleOptionsElement? StyleOptions
        {
            get;
            set;
        }

        /// <summary>
        /// Two localized date formats can be defined with cs:date elements: a “numeric” (e.g. “12-15-2005”) and a “text” format.
        /// </summary>
        [XmlElement("date")]
        public List<DateElement> Dates
        {
            get;
            set;
        } = new List<DateElement>();

        /// <summary>
        /// Terms are localized strings (e.g. by using the “and” term, “Doe and Smith” automatically becomes “Doe und Smith”
        /// when the style locale is switched from English to German). Terms are either directly defined in the content of cs:term,
        /// or, in cases where singular and plural variants are needed (e.g. “page” and “pages”), in the content of the child
        /// elements cs:single and cs:multiple, respectively.
        /// </summary>
        [XmlArray("terms")]
        [XmlArrayItem("term")]
        public List<TermElement> Terms
        {
            get;
            set;
        } = new List<TermElement>();
    }
}