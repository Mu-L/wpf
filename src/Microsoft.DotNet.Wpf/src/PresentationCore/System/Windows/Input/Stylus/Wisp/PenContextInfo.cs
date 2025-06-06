// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MS.Win32.Penimc;

namespace System.Windows.Input
{
    /////////////////////////////////////////////////////////////////////////
    /// <summary>
    ///     Struct used to store new PenContext information.
    /// </summary>
    internal struct PenContextInfo
    {
        public IPimcContext3 PimcContext;
        
        public IntPtr CommHandle;
        
        public int ContextId;

        /// <summary>
        /// The GIT key for a WISP context COM object.
        /// </summary>
        public UInt32 WispContextKey;
    }
}


