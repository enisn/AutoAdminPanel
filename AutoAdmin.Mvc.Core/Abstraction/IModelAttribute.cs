using Microsoft.AspNetCore.Html;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoAdmin.Mvc.Core.Abstraction
{
    public interface IModelAttribute
    {
        IHtmlContent GetDisplayHtml();
        IHtmlContent GetEditorHtml();
        IHtmlContent GetValidationHtml();
        string EditorClass { get; set; }
        string DisplayClass { get; set; }
        bool IsValidated { get; set; }
        Func<dynamic, bool> ValidationExpression { get; set; }
    }
}
