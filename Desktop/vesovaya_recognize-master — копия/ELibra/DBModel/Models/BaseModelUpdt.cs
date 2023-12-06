using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELibra.DBModel.Models
{
    public class BaseModelUpdt : BaseModel
    {
        private DateTime _updated;

        [Column(DbType = "TEXT")]
        public DateTime updated
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
