using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELibra.DBModel.ModelsTimeString
{
    [Table(Name ="Cars")]
    public class Cars : BaseModelUpdtString
    {
        private string _number;
        private int _markId;
        private double? _weight;
        private int? _clientId;
        private string _driverId;

        [Column(DbType = "TEXT", CanBeNull = false)]
        public string number
        {
            get
            {
                return _number;
            }
            set
            {
                if (_number != value)
                {
                    _number = value;
                }
            }
        }

        [Column(DbType ="INTEGER", CanBeNull = false)]
        public int markId
        {
            get
            {
                return _markId;
            }
            set
            {
                if (_markId != value)
                {
                    _markId = value;
                }
            }
        }

        [Column(DbType ="REAL", CanBeNull = true)]
        public double? weight
        {
            get
            {
                return _weight;
            }
            set
            {
                if (_weight != value)
                {
                    _weight = value;
                }
            }
        }

        [Column(DbType ="INTEGER", CanBeNull = true)]
        public int? clientId
        {
            get
            {
                return _clientId;
            }
            set
            {
                if (_clientId != value)
                {
                    _clientId = value;
                }
            }
        }

        [Column(DbType ="TEXT", CanBeNull = true)]
        public string driverId
        {
            get
            {
                return _driverId;
            }
            set
            {
                if (_driverId != value)
                {
                    _driverId = value;
                }
            }
        }
    }
}
