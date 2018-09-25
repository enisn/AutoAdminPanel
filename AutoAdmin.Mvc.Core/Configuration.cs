using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoAdmin.Mvc.Core
{
    public class Configuration
    {
        public static void Init(Type dbContextType)
        {
            ctxType = dbContextType;
            Context = (DbContext)Activator.CreateInstance(ctxType);
        }

        internal static Type ctxType;

        #region DoNotTouch
        public static DbContext NewContext() => (DbContext)Activator.CreateInstance(ctxType);
        public static DbContext Context { get; private set; }
        #endregion
    }
}
