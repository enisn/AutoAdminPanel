using System;

namespace AutoAdmin.Mvc.Attributes
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
