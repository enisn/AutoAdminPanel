using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoAdmin.Mvc.Configs
{
    public class IndexConfig
    {
        public IndexConfig()
        {
            this.TableClass = "table";
            this.EditClass = "btn btn-default";
            this.DetailClass = "btn btn-info";
            this.DeleteClass = "btn btn-danger";
            this.AddClass = "btn btn-primary";
        }
        public string TableClass { get; set; }
        public string EditClass { get; set; }
        public string DetailClass { get; set; }
        public string DeleteClass { get; set; }
        public string AddClass { get; set; }
        public int Id { get; set; }
    }
}
