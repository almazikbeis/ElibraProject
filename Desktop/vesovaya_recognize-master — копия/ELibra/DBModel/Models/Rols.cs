using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELibra.DBModel.Models
{
    [Table(Name = "Rols")]
    public class Rols : BaseModel
    {
        private string _name;

        [Column(DbType = "TEXT", CanBeNull = true)]
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
