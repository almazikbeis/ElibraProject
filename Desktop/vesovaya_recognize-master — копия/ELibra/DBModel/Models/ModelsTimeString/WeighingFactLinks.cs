using ELibra.DBModel.Models;
using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELibra.DBModel.ModelsTimeString
{
    [Table(Name = "WeighingFactLinks")]
    public class WeighingFactLinks : BaseModel
    {
        private int _weighingId;
        private int _factId;
        private int _weightKind;
        private string _dateWeight;

        [Column(DbType = "INTEGER", CanBeNull = false)]
        public int weighingId
        {
            get
            {
                return _weighingId;
            }
            set
            {
                if (_weighingId != value)
                {
                    _weighingId = value;
                }
            }
        }

        [Column(DbType = "INTEGER", CanBeNull = false)]
        public int factId
        {
            get
            {
                return _factId;
            }
            set
            {
                if (_factId != value)
                {
                    _factId = value;
                }
            }
        }

        [Column(DbType = "INTEGER", CanBeNull = false)]
        public int weightKind
        {
            get
            {
                return _weightKind;
            }
            set
            {
                if (_weightKind != value)
                {
                    _weightKind = value;
                }
            }
        }

        [Column(DbType = "TEXT", CanBeNull = false)]
        public string dateWeight
        {
            get
            {
                return _dateWeight;
            }
            set
            {
                if (_dateWeight != value)
                {
                    _dateWeight = value;
                }
            }
        }
    }
}
