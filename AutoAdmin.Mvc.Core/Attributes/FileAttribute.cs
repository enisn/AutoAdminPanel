using AutoAdmin.Mvc.Core.Abstraction;
using Microsoft.AspNetCore.Html;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoAdmin.Mvc.Core.Attributes
{
    [System.AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public sealed class FileAttribute : Attribute
    {
        public FileAttribute(string positionalString)
        {

        }
    }
}
