using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace AutoAdmin.Mvc.Abstraction
{
    public interface IModelAttribute
    {
        MvcHtmlString GetDisplayHtml();
        MvcHtmlString GetEditorHtml();
        MvcHtmlString GetValidationHtml();
        string EditorClass { get; set; }
        string DisplayClass { get; set; }
        bool IsValidated { get; set; }
        Func<dynamic, bool> ValidationExpression { get; set; }
    }
}
