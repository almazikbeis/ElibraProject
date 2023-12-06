using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELibra.DBModel.ModelsTimeString
{
    [Table(Name = "Goods")]
    public class Goods : BaseModelUpdtString
    {
        private string _name;
        private double? _price;

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

        [Column(DbType = "REAL", CanBeNull = false)]
        public double? price
        {
            get
            {
                return _price;
            }
            set
            {
                if (_price != value)
                {
                    _price = value;
                }
            }
        }
    }
}
