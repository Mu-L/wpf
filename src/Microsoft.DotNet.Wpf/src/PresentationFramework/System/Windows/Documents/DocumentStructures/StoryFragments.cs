﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.


using System.Collections;
using System.Windows.Markup;

[assembly: XmlnsDefinition(
    "http://schemas.microsoft.com/xps/2005/06/documentstructure",
    "System.Windows.Documents.DocumentStructures")]

namespace System.Windows.Documents.DocumentStructures
{
    /// <summary>
    ///
    /// </summary>
    [ContentProperty("StoryFragmentList")]
    public class StoryFragments : IAddChild, IEnumerable<StoryFragment>, IEnumerable
    {
        /// <summary>
        ///
        /// </summary>
        public StoryFragments()
        {
            _elementList = new List<StoryFragment>();
        }

        public void Add(StoryFragment storyFragment)
        {
            ArgumentNullException.ThrowIfNull(storyFragment);

            ((IAddChild) this).AddChild(storyFragment);
        }

        void IAddChild.AddChild(object value)
        {
            //
            // Only the StoryFragment type are accepted. 
            //
            if (value is StoryFragment)
            {
                _elementList.Add( (StoryFragment) value);
                return;
            }

            throw new ArgumentException(SR.Format(SR.UnexpectedParameterType, value.GetType(), typeof(StoryFragment)), nameof(value));
        }
        
        void IAddChild.AddText(string text) { } 
        
        IEnumerator<StoryFragment> IEnumerable<StoryFragment>.GetEnumerator()
        {
            throw new NotSupportedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<StoryFragment>)this).GetEnumerator();
        }
        
        internal List<StoryFragment> StoryFragmentList
        {
            get
            {
                return _elementList;
            }
        }

        private List<StoryFragment> _elementList;
    }
    
    /// <summary>
    ///
    /// </summary>
    [ContentProperty("BlockElementList")]
    public class StoryFragment : IAddChild, IEnumerable<BlockElement>, IEnumerable
    {
        /// <summary>
        ///
        /// </summary>
        public StoryFragment()
        {
            _elementList = new List<BlockElement>();
        }

        public void Add(BlockElement element)
        {
            ArgumentNullException.ThrowIfNull(element);
            ((IAddChild) this).AddChild(element);
        }
        
        void IAddChild.AddChild(object value)
        {
            //
            // Only the following type are accepted. 
            // Section|Paragraph|Inline(Bold|Italic|Underline)|Floater|Figure|List
            // |Table|StoryBreak
            //
            if (value is SectionStructure || value is ParagraphStructure || value is FigureStructure 
                || value is ListStructure || value is TableStructure || value is StoryBreak)
            {
                _elementList.Add( (BlockElement) value);
                return;
            }

            throw new ArgumentException(SR.Format(SR.DocumentStructureUnexpectedParameterType6, value.GetType(),
                typeof(SectionStructure), typeof(ParagraphStructure), typeof(FigureStructure), typeof(ListStructure), typeof(TableStructure), typeof(StoryBreak)),
                nameof(value));
        }
        void IAddChild.AddText(string text) { }

        IEnumerator<BlockElement> IEnumerable<BlockElement>.GetEnumerator()
        {
            throw new NotSupportedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<BlockElement>)this).GetEnumerator();
        }

        /// <summary>
        /// The element name
        /// </summary>
        public string StoryName
        {
            get
            {
                return _storyName;
            }
            set
            {
                _storyName = value;
            }
        }

        /// <summary>
        /// The element name
        /// </summary>
        public string FragmentName
        {
            get
            {
                return _fragmentName;
            }
            set
            {
                _fragmentName = value;
            }
        }

        /// <summary>
        /// The element name
        /// </summary>
        public String FragmentType
        {
            get
            {
                return _fragmentType;
            }
            set
            {
                _fragmentType = value;
            }
        }

        internal List<BlockElement> BlockElementList
        {
            get
            {
                return _elementList;
            }
        }
        
        private List<BlockElement> _elementList;
        private String _storyName;
        private String _fragmentName;
        private String _fragmentType;
    }
}

