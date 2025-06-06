﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Windows.Controls;

namespace System.Windows.Automation.Peers
{
    /// 
    public class LabelAutomationPeer : FrameworkElementAutomationPeer
    {
        ///
        public LabelAutomationPeer(Label owner): base(owner)
        {}
    
        ///
        protected override string GetClassNameCore()
        {
            return "Text";
        }

        ///
        protected override AutomationControlType GetAutomationControlTypeCore()
        {
            return AutomationControlType.Text;
        }

        // Return the base without the AccessKey character
        ///
        protected override string GetNameCore()
        {
            string result = base.GetNameCore();
            if (!string.IsNullOrEmpty(result))
            {
                Label label = (Label)Owner;
                if (label.Content is string)
                {
                    return AccessText.RemoveAccessKeyMarker(result);
                }
            }

            return result;
        }
    }
}

