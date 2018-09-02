using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoAdmin.Mvc
{
    public class Configuration
    {
        /// <summary>
        /// To init project
        /// </summary>
        /// <param name="dbContext"></param>
        public static void Init(Type dbContext)
        {
            ctxType = dbContext;
        }

        internal static Type ctxType;

        #region DoNotTouch
        public static DbContext NewContext() => (DbContext)Activator.CreateInstance(ctxType);
        public static DbContext Context { get; private set; } = (DbContext)Activator.CreateInstance(ctxType);
        #endregion
    }
}
