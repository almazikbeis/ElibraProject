using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Linq.Mapping;

namespace ELibra.DBModel.ModelsTimeString
{
    [Table(Name = "CarMarks")]
    public class CarMarks : BaseModelUpdtString
    {
        private string _name;

        [Column(DbType ="TEXT", CanBeNull = false)]
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
