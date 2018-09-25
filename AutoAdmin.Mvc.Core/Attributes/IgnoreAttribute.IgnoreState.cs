using System;
using System.Collections.Generic;
using System.Text;

namespace AutoAdmin.Mvc.Core.Attributes
{
    public sealed partial class IgnoreAttribute
    {
        [Flags]
        public enum IgnoreState
        {
            All,
            Index = 1 << 0, // 00000001
            Create = 1 << 1, //00000010
            Edit = 1 << 2,   //00000100
        }
    }
}
