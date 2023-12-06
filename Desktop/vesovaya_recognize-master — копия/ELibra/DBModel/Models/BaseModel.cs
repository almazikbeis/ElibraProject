using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELibra.DBModel.Models
{
    public class BaseModel
    {
        private int _id;

        [Column(IsDbGenerated = true, IsPrimaryKey = true, CanBeNull = false, DbType = "INTEGER")]
        public int id
        {
            get
            {
                return _id;
            }
            set
            {
                if (_id != value)
                {
                    _id = value;
                }
            }
        }
    }
}
