using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELibra.DBModel.Models
{
    [Table(Name = "Weighings")]
    public class Weighings : BaseModelUpdt
    {
        private int _clientId;
        private string _material;
        private double? _carWeight;
        private double? _brutto;
        private DateTime _dateWeight;
        private int _userId;
        private string _carNumber;
        private string _carMark;
        private double? _docWeight;
        private string _consignee;
        private int? _talonNum;
        private string _point1;
        private string _point2;
        private string _operationType;
        private int? _invoiceNum;
        private double? _factorWeight;
        private int? _invNumLoading;
        private double? _volume;
        private string _driver;
        private string _seal;
        private int? _driverId;
        private int? _storageId;
        private int? _carrierid;
        private DateTime? _dateOut;
        private int? _invoiceNum2;
        private double? _price;
        private string _orderNumber;
        private string _orderPositionNumber;
        private string _consignor;
        private double? _adjustment;
        private string _description;
        private string _taraScale;
        private string _fullWeightScale;
        private DateTime? _edited;
        private string _redactor;


        [Column(DbType = "INTEGER", CanBeNull = false)]
        public int clientId
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

        [Column(DbType = "TEXT", CanBeNull = true)]
        public string material
        {
            get
            {
                return _material;
            }
            set
            {
                if (_material != value)
                {
                    _material = value;
                }
            }
        }

        [Column(DbType = "REAL", CanBeNull = true)]
        public double? carWeight
        {
            get
            {
                return _carWeight;
            }
            set
            {
                if (_carWeight != value)
                {
                    _carWeight = value;
                }
            }
        }

        [Column(DbType = "REAL", CanBeNull = true)]
        public double? brutto
        {
            get
            {
                return _brutto;
            }
            set
            {
                if (_brutto != value)
                {
                    _brutto = value;
                }
            }
        }

        [Column(DbType = "TEXT", CanBeNull = false)]
        public DateTime dateWeight
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

        [Column(DbType = "INTEGER", CanBeNull = false)]
        public int userId
        {
            get
            {
                return _userId;
            }
            set
            {
                if (_userId != value)
                {
                    _userId = value;
                }
            }
        }

        [Column(DbType = "TEXT", CanBeNull = false)]
        public string carNumber
        {
            get
            {
                return _carNumber;
            }
            set
            {
                if (_carNumber != value)
                {
                    _carNumber = value;
                }
            }
        }

        [Column(DbType = "TEXT", CanBeNull = false)]
        public string carMark
        {
            get
            {
                return _carMark;
            }
            set
            {
                if (_carMark != value)
                {
                    _carMark = value;
                }
            }
        }

        [Column(DbType = "REAL", CanBeNull = true)]
        public double? docWeight
        {
            get
            {
                return _docWeight;
            }
            set
            {
                if (_docWeight != value)
                {
                    _docWeight = value;
                }
            }
        }

        [Column(DbType = "TEXT", CanBeNull = true)]
        public string consignee
        {
            get
            {
                return _consignee;
            }
            set
            {
                if (_consignee != value)
                {
                    _consignee = value;
                }
            }
        }

        [Column(DbType = "INTEGER", CanBeNull = true)]
        public int? talonNum
        {
            get
            {
                return _talonNum;
            }
            set
            {
                if (_talonNum != value)
                {
                    _talonNum = value;
                }
            }
        }

        [Column(DbType = "TEXT", CanBeNull = true)]
        public string point1
        {
            get
            {
                return _point1;
            }
            set
            {
                if (_point1 != value)
                {
                    _point1 = value;
                }
            }
        }

        [Column(DbType = "TEXT", CanBeNull = true)]
        public string point2
        {
            get
            {
                return _point2;
            }
            set
            {
                if (_point2 != value)
                {
                    _point2 = value;
                }
            }
        }

        [Column(DbType = "TEXT", CanBeNull = true)]
        public string operationType
        {
            get
            {
                return _operationType;
            }
            set
            {
                if (_operationType != value)
                {
                    _operationType = value;
                }
            }
        }

        [Column(DbType = "INTEGER", CanBeNull = true)]
        public int? invoiceNum
        {
            get
            {
                return _invoiceNum;
            }
            set
            {
                if (_invoiceNum != value)
                {
                    _invoiceNum = value;
                }
            }
        }

        [Column(DbType = "REAL", CanBeNull = true)]
        public double? factorWeight
        {
            get
            {
                return _factorWeight;
            }
            set
            {
                if (_factorWeight != value)
                {
                    _factorWeight = value;
                }
            }
        }

        [Column(DbType = "INTEGER", CanBeNull = true)]
        public int? invNumLoading
        {
            get
            {
                return _invNumLoading;
            }
            set
            {
                if (_invNumLoading != value)
                {
                    _invNumLoading = value;
                }
            }
        }

        [Column(DbType = "REAL", CanBeNull = true)]
        public double? volume
        {
            get
            {
                return _volume;
            }
            set
            {
                if (_volume != value)
                {
                    _volume = value;
                }
            }
        }

        [Column(DbType = "TEXT", CanBeNull = true)]
        public string driver
        {
            get
            {
                return _driver;
            }
            set
            {
                if (_driver != value)
                {
                    _driver = value;
                }
            }
        }

        [Column(DbType = "TEXT", CanBeNull = true)]
        public string seal
        {
            get
            {
                return _seal;
            }
            set
            {
                if (_seal != value)
                {
                    _seal = value;
                }
            }
        }

        [Column(DbType = "INTEGER", CanBeNull = true)]
        public int? driverId
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

        [Column(DbType = "INTEGER", CanBeNull = true)]
        public int? storageId
        {
            get
            {
                return _storageId;
            }
            set
            {
                if (_storageId != value)
                {
                    _storageId = value;
                }
            }
        }

        [Column(DbType = "INTEGER", CanBeNull = true)]
        public int? carrierid
        {
            get
            {
                return _carrierid;
            }
            set
            {
                if (_carrierid != value)
                {
                    _carrierid = value;
                }
            }
        }

        [Column(DbType = "TEXT", CanBeNull = true)]
        public DateTime? dateOut
        {
            get
            {
                return _dateOut;
            }
            set
            {
                if (_dateOut != value)
                {
                    _dateOut = value;
                }
            }
        }

        [Column(DbType = "INTEGER", CanBeNull = true)]
        public int? invoiceNum2
        {
            get
            {
                return _invoiceNum2;
            }
            set
            {
                if (_invoiceNum2 != value)
                {
                    _invoiceNum2 = value;
                }
            }
        }


        [Column(DbType = "REAL", CanBeNull = true)]
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

        [Column(DbType = "TEXT", CanBeNull = true)]
        public string orderNumber
        {
            get
            {
                return _orderNumber;
            }
            set
            {
                if (_orderNumber != value)
                {
                    _orderNumber = value;
                }
            }
        }

        [Column(DbType = "TEXT", CanBeNull = true)]
        public string orderPositionNumber
        {
            get
            {
                return _orderPositionNumber;
            }
            set
            {
                if (_orderPositionNumber != value)
                {
                    _orderPositionNumber = value;
                }
            }
        }

        [Column(DbType = "TEXT", CanBeNull = true)]
        public string consignor
        {
            get
            {
                return _consignor;
            }
            set
            {
                if (_consignor != value)
                {
                    _consignor = value;
                }
            }
        }

        [Column(DbType = "REAL", CanBeNull = true)]
        public double? adjustment
        {
            get
            {
                return _adjustment;
            }
            set
            {
                if (_adjustment != value)
                {
                    _adjustment = value;
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

        [Column(DbType = "TEXT", CanBeNull = true)]
        public string taraScale
        {
            get
            {
                return _taraScale;
            }
            set
            {
                if (_taraScale != value)
                {
                    _taraScale = value;
                }
            }
        }

        [Column(DbType = "TEXT", CanBeNull = true)]
        public string fullWeightScale
        {
            get
            {
                return _fullWeightScale;
            }
            set
            {
                if (_fullWeightScale != value)
                {
                    _fullWeightScale = value;
                }
            }
        }
        [Column(DbType ="TEXT", CanBeNull = true)]
        public DateTime? edited 
        {
            get 
            {
                return _edited;
            }
            set
            {
                if (_edited != value)
                {
                    _edited = value;
                }
            }
        }

        [Column(DbType = "TEXT", CanBeNull = true)]
        public string redactor
        {
            get
            {
                return _redactor;
            }
            set
            {
                if (_redactor != value)
                {
                    _redactor = value;
                }
            }
        }
    }
}
