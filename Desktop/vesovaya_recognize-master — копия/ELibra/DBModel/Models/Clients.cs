using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELibra.DBModel.Models
{
    [Table(Name = "Clients")]
    public class Clients : BaseModelUpdt
    {
        private string _name;
        private string _RNN;
        private string _address;
        private string _phone;
        private string _description;
        private double? _factor;
        private DateTime _startDate;
        private DateTime _endDate;


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


        [Column(DbType = "TEXT", CanBeNull = true)]
        public string RNN
        {
            get
            {
                return _RNN;
            }
            set
            {
                if (_RNN != value)
                {
                    _RNN = value;
                }
            }
        }


        [Column(DbType = "TEXT", CanBeNull = true)]
        public string address
        {
            get
            {
                return _address;
            }
            set
            {
                if (_address != value)
                {
                    _address = value;
                }
            }
        }

        [Column(DbType = "TEXT", CanBeNull = true)]
        public string phone
        {
            get
            {
                return _phone;
            }
            set
            {
                if (_phone != value)
                {
                    _phone = value;
                }
            }
        }

        [Column(DbType = "TEXT", CanBeNull = true)]
        public string description
        {
            get
            {
                return _description;
            }
            set
            {
                if (_description != value)
                {
                    _description = value;
                }
            }
        }

        [Column(DbType = "REAL", CanBeNull = true)]
        public double? factor
        {
            get
            {
                return _factor;
            }
            set
            {
                if (_factor != value)
                {
                    _factor = value;
                }
            }
        }

        [Column(DbType = "TEXT")]
        public DateTime startDate
        {
            get
            {
                return _startDate;
            }
            set
            {
                if (_startDate != value)
                {
                    _startDate = value;
                }
            }
        }

        [Column(DbType = "datetime")]
        public DateTime endDate
        {
            get
            {
                return _endDate;
            }
            set
            {
                if (_endDate != value)
                {
                    _endDate = value;
                }
            }
        }
    }
}
