using ELibra.DBModel.Models;
using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELibra.DBModel.ModelsTimeString
{
    [Table(Name = "Settings")]
    public class Settings : BaseModel
    {
        private string _Model;
        private string _PortName;
        private string _MaxWeight;
        private string _BaudRate;
        private string _DataBits;
        private string _Parity;
        private string _StopBits;
        private string _Handshake;
        private string _RtsEnable;
        private string _HostDB;
        private string _LoginDB;
        private string _PasswordDB;
        private string _NameCompany;
        private string _Consignee;
        private string _Point1;
        private string _Point2;
        private string _TypeTransaction;
        private string _Carrier;
        private string _Stock;
        private string _OrderNumber;
        private string _PositionStockNumber;
        private string _Snipper;
        private string _MinWeightVideo;
        private string _PathVideo;
        private string _UrlVideoCam;
        private string _UrlVideoCamNum;
        private string _cBoxExportFold;
        private string _tExportFold;
        private string _Role;
        private string _licenseId;
        private string _licenseDate;
        private string _licenseNumber;
        private string _UploadDelay;
        private string _lastUpload;
        private string _isSave;
        private string _fioOperator;
        private string _weightTalon;
        private string _ttn;
        private string _nakl1;
        private string _nakl2;
        private string _UrlVideoCamEnabled;
        private string _UrlVideoCamNumEnabled;

        [Column(DbType = "TEXT", CanBeNull = true)]
        public string Model
        {
            get
            {
                return _Model;
            }
            set
            {
                if (_Model != value)
                {
                    _Model = value;
                }
            }
        }

        [Column(DbType = "TEXT", CanBeNull = true)]
        public string PortName
        {
            get
            {
                return _PortName;
            }
            set
            {
                if (_PortName != value)
                {
                    _PortName = value;
                }
            }
        }

        [Column(DbType = "TEXT", CanBeNull = true)]
        public string MaxWeight
        {
            get
            {
                return _MaxWeight;
            }
            set
            {
                if (_MaxWeight != value)
                {
                    _MaxWeight = value;
                }
            }
        }

        [Column(DbType = "TEXT", CanBeNull = true)]
        public string BaudRate
        {
            get
            {
                return _BaudRate;
            }
            set
            {
                if (_BaudRate != value)
                {
                    _BaudRate = value;
                }
            }
        }

        [Column(DbType = "TEXT", CanBeNull = true)]
        public string DataBits
        {
            get
            {
                return _DataBits;
            }
            set
            {
                if (_DataBits != value)
                {
                    _DataBits = value;
                }
            }
        }

        [Column(DbType = "TEXT", CanBeNull = true)]
        public string Parity
        {
            get
            {
                return _Parity;
            }
            set
            {
                if (_Parity != value)
                {
                    _Parity = value;
                }
            }
        }

        [Column(DbType = "TEXT", CanBeNull = true)]
        public string StopBits
        {
            get
            {
                return _StopBits;
            }
            set
            {
                if (_StopBits != value)
                {
                    _StopBits = value;
                }
            }
        }

        [Column(DbType = "TEXT", CanBeNull = true)]
        public string Handshake
        {
            get
            {
                return _Handshake;
            }
            set
            {
                if (_Handshake != value)
                {
                    _Handshake = value;
                }
            }
        }

        [Column(DbType = "TEXT", CanBeNull = true)]
        public string RtsEnable
        {
            get
            {
                return _RtsEnable;
            }
            set
            {
                if (_RtsEnable != value)
                {
                    _RtsEnable = value;
                }
            }
        }

        [Column(DbType = "TEXT", CanBeNull = true)]
        public string HostDB
        {
            get
            {
                return _HostDB;
            }
            set
            {
                if (_HostDB != value)
                {
                    _HostDB = value;
                }
            }
        }

        [Column(DbType = "TEXT", CanBeNull = true)]
        public string LoginDB
        {
            get
            {
                return _LoginDB;
            }
            set
            {
                if (_LoginDB != value)
                {
                    _LoginDB = value;
                }
            }
        }

        [Column(DbType = "TEXT", CanBeNull = true)]
        public string PasswordDB
        {
            get
            {
                return _PasswordDB;
            }
            set
            {
                if (_PasswordDB != value)
                {
                    _PasswordDB = value;
                }
            }
        }

        [Column(DbType = "TEXT", CanBeNull = true)]
        public string NameCompany
        {
            get
            {
                return _NameCompany;
            }
            set
            {
                if (_NameCompany != value)
                {
                    _NameCompany = value;
                }
            }
        }

        [Column(DbType = "TEXT", CanBeNull = true)]
        public string Consignee
        {
            get
            {
                return _Consignee;
            }
            set
            {
                if (_Consignee != value)
                {
                    _Consignee = value;
                }
            }
        }

        [Column(DbType = "TEXT", CanBeNull = true)]
        public string Point1
        {
            get
            {
                return _Point1;
            }
            set
            {
                if (_Point1 != value)
                {
                    _Point1 = value;
                }
            }
        }

        [Column(DbType = "TEXT", CanBeNull = true)]
        public string Point2
        {
            get
            {
                return _Point2;
            }
            set
            {
                if (_Point2 != value)
                {
                    _Point2 = value;
                }
            }
        }

        [Column(DbType = "TEXT", CanBeNull = true)]
        public string TypeTransaction
        {
            get
            {
                return _TypeTransaction;
            }
            set
            {
                if (_TypeTransaction != value)
                {
                    _TypeTransaction = value;
                }
            }
        }

        [Column(DbType = "TEXT", CanBeNull = true)]
        public string Carrier
        {
            get
            {
                return _Carrier;
            }
            set
            {
                if (_Carrier != value)
                {
                    _Carrier = value;
                }
            }
        }

        [Column(DbType = "TEXT", CanBeNull = true)]
        public string Stock
        {
            get
            {
                return _Stock;
            }
            set
            {
                if (_Stock != value)
                {
                    _Stock = value;
                }
            }
        }

        [Column(DbType = "TEXT", CanBeNull = true)]
        public string OrderNumber
        {
            get
            {
                return _OrderNumber;
            }
            set
            {
                if (_OrderNumber != value)
                {
                    _OrderNumber = value;
                }
            }
        }

        [Column(DbType = "TEXT", CanBeNull = true)]
        public string PositionStockNumber
        {
            get
            {
                return _PositionStockNumber;
            }
            set
            {
                if (_PositionStockNumber != value)
                {
                    _PositionStockNumber = value;
                }
            }
        }

        [Column(DbType = "TEXT", CanBeNull = true)]
        public string Snipper
        {
            get
            {
                return _Snipper;
            }
            set
            {
                if (_Snipper != value)
                {
                    _Snipper = value;
                }
            }
        }

        [Column(DbType = "TEXT", CanBeNull = true)]
        public string MinWeightVideo
        {
            get
            {
                return _MinWeightVideo;
            }
            set
            {
                if (_MinWeightVideo != value)
                {
                    _MinWeightVideo = value;
                }
            }
        }

        [Column(DbType = "TEXT", CanBeNull = true)]
        public string PathVideo
        {
            get
            {
                return _PathVideo;
            }
            set
            {
                if (_PathVideo != value)
                {
                    _PathVideo = value;
                }
            }
        }

        [Column(DbType = "TEXT", CanBeNull = true)]
        public string UrlVideoCam
        {
            get
            {
                return _UrlVideoCam;
            }
            set
            {
                if (_UrlVideoCam != value)
                {
                    _UrlVideoCam = value;
                }
            }
        }

        [Column(DbType = "TEXT", CanBeNull = true)]
        public string UrlVideoCamNum
        {
            get
            {
                return _UrlVideoCamNum;
            }
            set
            {
                if (_UrlVideoCamNum != value)
                {
                    _UrlVideoCamNum = value;
                }
            }
        }

        [Column(DbType = "TEXT", CanBeNull = true)]
        public string cBoxExportFold
        {
            get
            {
                return _cBoxExportFold;
            }
            set
            {
                if (_cBoxExportFold != value)
                {
                    _cBoxExportFold = value;
                }
            }
        }

        [Column(DbType = "TEXT", CanBeNull = true)]
        public string tExportFold
        {
            get
            {
                return _tExportFold;
            }
            set
            {
                if (_tExportFold != value)
                {
                    _tExportFold = value;
                }
            }
        }

        [Column(DbType = "TEXT", CanBeNull = true)]
        public string Role
        {
            get
            {
                return _Role;
            }
            set
            {
                if (_Role != value)
                {
                    _Role = value;
                }
            }
        }

        [Column(DbType = "TEXT", CanBeNull = true)]
        public string licenseId
        {
            get
            {
                return _licenseId;
            }
            set
            {
                if (_licenseId != value)
                {
                    _licenseId = value;
                }
            }
        }

        [Column(DbType = "TEXT", CanBeNull = true)]
        public string licenseDate
        {
            get
            {
                return _licenseDate;
            }
            set
            {
                if (_licenseDate != value)
                {
                    _licenseDate = value;
                }
            }
        }

        [Column(DbType = "TEXT", CanBeNull = true)]
        public string licenseNumber
        {
            get
            {
                return _licenseNumber;
            }
            set
            {
                if (_licenseNumber != value)
                {
                    _licenseNumber = value;
                }
            }
        }

        [Column(DbType = "TEXT", CanBeNull = true)]
        public string UploadDelay
        {
            get
            {
                return _UploadDelay;
            }
            set
            {
                if (_UploadDelay != value)
                {
                    _UploadDelay = value;
                }
            }
        }

        [Column(DbType = "TEXT", CanBeNull = true)]
        public string lastUpload
        {
            get
            {
                return _lastUpload;
            }
            set
            {
                if (_lastUpload != value)
                {
                    _lastUpload = value;
                }
            }
        }

        [Column(DbType = "TEXT", CanBeNull = true)]
        public string isSave
        {
            get
            {
                return _isSave;
            }
            set
            {
                if (_isSave != value)
                {
                    _isSave = value;
                }
            }
        }

        [Column(DbType = "TEXT", CanBeNull = true)]
        public string fioOperator
        {
            get
            {
                return _fioOperator;
            }
            set
            {
                if (_fioOperator != value)
                {
                    _fioOperator = value;
                }
            }
        }

        [Column(DbType = "TEXT", CanBeNull = true)]
        public string weightTalon
        {
            get
            {
                return _weightTalon;
            }
            set
            {
                if (_weightTalon != value)
                {
                    _weightTalon = value;
                }
            }
        }

        [Column(DbType = "TEXT", CanBeNull = true)]
        public string ttn
        {
            get
            {
                return _ttn;
            }
            set
            {
                if (_ttn != value)
                {
                    _ttn = value;
                }
            }
        }

        [Column(DbType = "TEXT", CanBeNull = true)]
        public string nakl1
        {
            get
            {
                return _nakl1;
            }
            set
            {
                if (_nakl1 != value)
                {
                    _nakl1 = value;
                }
            }
        }

        [Column(DbType = "TEXT", CanBeNull = true)]
        public string nakl2
        {
            get
            {
                return _nakl2;
            }
            set
            {
                if (_nakl2 != value)
                {
                    _nakl2 = value;
                }
            }
        }

        [Column(DbType = "TEXT", CanBeNull = true)]
        public string UrlVideoCamEnabled
        {
            get
            {
                return _UrlVideoCamEnabled;
            }
            set
            {
                if (_UrlVideoCamEnabled != value)
                {
                    _UrlVideoCamEnabled = value;
                }
            }
        }

        [Column(DbType = "TEXT", CanBeNull = true)]
        public string UrlVideoCamNumEnabled
        {
            get
            {
                return _UrlVideoCamNumEnabled;
            }
            set
            {
                if (_UrlVideoCamNumEnabled != value)
                {
                    _UrlVideoCamNumEnabled = value;
                }
            }
        }
    }
}
