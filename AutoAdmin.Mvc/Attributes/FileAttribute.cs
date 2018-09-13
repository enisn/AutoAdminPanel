using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace AutoAdmin.Mvc.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class FileAttribute : Attribute
    {
        public FileAttribute()
        {
            
        }
    }
}
