﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MS.Internal.TextFormatting;

namespace System.Windows.Media.TextFormatting
{
    /// <summary>
    /// Generic implementation of text marker properties
    /// </summary>
    public class TextSimpleMarkerProperties : TextMarkerProperties
    {
        private double          _offset;
        private TextSource      _textSource;

        /// <summary>
        /// Construct a text marker object
        /// </summary>
        /// <param name="style">marker style</param>
        /// <param name="offset">distance from line start to the end of the marker symbol</param>
        /// <param name="autoNumberingIndex">autonumbering counter of counter-style marker</param>
        /// <param name="textParagraphProperties">text paragraph properties</param>
        public TextSimpleMarkerProperties(
            TextMarkerStyle             style,
            double                      offset,
            int                         autoNumberingIndex,
            TextParagraphProperties     textParagraphProperties
            ) 
        {
            ArgumentNullException.ThrowIfNull(textParagraphProperties);

            _offset = offset;

            if (style != TextMarkerStyle.None)
            {
                if (TextMarkerSource.IsKnownSymbolMarkerStyle(style))
                {
                    // autoNumberingIndex is ignored
                }
                else if (TextMarkerSource.IsKnownIndexMarkerStyle(style))
                {
                    // validate autoNumberingIndex
                    ArgumentOutOfRangeException.ThrowIfNegativeOrZero(autoNumberingIndex);
                }
                else
                {
                    // invalid style
                    throw new ArgumentException(SR.Format(SR.Enum_Invalid, typeof(TextMarkerStyle)), nameof(style));
                }

                _textSource = new TextMarkerSource(
                    textParagraphProperties, 
                    style, 
                    autoNumberingIndex
                    );
            }
        }


        /// <summary>
        /// Distance from line start to the end of the marker symbol
        /// </summary>
        public sealed override double Offset
        {
            get { return _offset; }
        }


        /// <summary>
        /// Source of text runs used for text marker
        /// </summary>
        public sealed override TextSource TextSource
        {
            get { return _textSource; }
        }
    }
}

