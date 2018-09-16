using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace AutoAdmin.Mvc.Attributes
{
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public sealed partial class IgnoreAttribute : Attribute, Abstraction.IModelAttribute
    {
        /// <summary>
        /// AutoAdmin.Mvc.Attributes.IgnoreState is not implemented yet! It works as IgnoreState.All the type of System.Type is imp
        /// </summary>
        /// <param name="state"></param>
        public IgnoreAttribute(IgnoreState state = IgnoreState.All)
        {
            
        }

        public string EditorClass { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string DisplayClass { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool IsValidated { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public Func<dynamic, bool> ValidationExpression { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public MvcHtmlString GetDisplayHtml()
        {
            throw new NotImplementedException();
        }

        public MvcHtmlString GetEditorHtml()
        {
            throw new NotImplementedException();
        }

        public MvcHtmlString GetValidationHtml()
        {
            throw new NotImplementedException();
        }
    }
}
