using ELibra.DBModel.Models;
using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELibra.DBModel.ModelsTimeString
{
    public class BaseModelUpdtString : BaseModel
    {
        private string _updated;

        [Column(DbType = "TEXT")]
        public string updated
        {
            get
            {
                return _updated;
            }
            set
            {
                if (_updated != value)
                {
                    _updated = value;
                }
            }
        }
    }
}
