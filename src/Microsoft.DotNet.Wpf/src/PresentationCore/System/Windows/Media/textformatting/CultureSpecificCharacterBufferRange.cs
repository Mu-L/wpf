// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

//
// 
// Description: CultureSpecificCharacterBufferRange class
//

using System.Globalization;

namespace System.Windows.Media.TextFormatting
{
    /// <summary>
    /// Represents a range of characters that are associated with a culture
    /// </summary>
    public class CultureSpecificCharacterBufferRange
    {
        private CultureInfo _culture;
        private CharacterBufferRange _characterBufferRange;

        /// <summary>
        /// Construct a CultureSpecificCharacterBufferRange class
        /// </summary>
        public CultureSpecificCharacterBufferRange(CultureInfo culture, CharacterBufferRange characterBufferRange)
        {        
            _culture = culture;
            _characterBufferRange = characterBufferRange;
        }

        /// <summary>
        /// Culture of the containing range of characters 
        /// </summary>
        public CultureInfo CultureInfo 
        {
            get { return _culture; }
        }

        /// <summary>
        /// The character range
        /// </summary>
        public CharacterBufferRange CharacterBufferRange
        {
            get { return _characterBufferRange; }
        }
    }
}
  
