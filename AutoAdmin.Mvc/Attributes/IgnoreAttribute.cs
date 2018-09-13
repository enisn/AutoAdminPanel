using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoAdmin.Mvc.Attributes
{
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    sealed class IgnoreAttribute : Attribute
    {
        /// <summary>
        /// AutoAdmin.Mvc.Attributes.IgnoreState is not implemented yet! It works as IgnoreState.All the type of System.Type is imp
        /// </summary>
        /// <param name="state"></param>
        public IgnoreAttribute(IgnoreState state = IgnoreState.All)
        {
            
        }

        public enum IgnoreState
        {
            All,
            Index,
            Create,
            Edit,
        }
    }
}
