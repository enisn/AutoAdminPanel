using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace AutoAdmin
{
    public class Configuration
    {
        public readonly static Type ctxType = typeof(Models.NORTHWNDEntities);

        #region DoNotTouch
        public static DbContext NewContext() => (DbContext)Activator.CreateInstance(ctxType);
        public static DbContext Context { get; private set; } = (DbContext)Activator.CreateInstance(ctxType);
        #endregion
    }
}