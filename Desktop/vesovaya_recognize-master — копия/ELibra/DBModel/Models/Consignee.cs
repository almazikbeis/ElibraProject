using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELibra.DBModel.Models
{
    [Table(Name = "Consignee")]
    public class Consignee : BaseModelUpdt
    {
        private string _name;

        [Column(DbType = "TEXT", CanBeNull = false)]
        public string name
        {
            get
            {
                return _name;
            }
            set
            {
                if (_name != value)
                {
                    _name = value;
                }
            }
        }
    }
}
