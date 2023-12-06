using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELibra.DBModel.ModelsTimeString
{
    [Table(Name = "Weighings")]
    public class Weighings : BaseModelUpdtString
    {
        private string _clientId;
        private string _material;
        private string _carWeight;
        private string _brutto;
        private string _dateWeight;
        private string _userId;
        private string _carNumber;
        private string _carMark;
        private string _docWeight;
        private string _consignee;
        private string _talonNum;
        private string _point1;
        private string _point2;
        private string _operationType;
        private string _invoiceNum;
        private string _factorWeight;
        private string _invNumLoading;
        private string _volume;
        private string _driver;
        private string _seal;
        private string _driverId;
        private string _storageId;
        private string _carrierid;
        private string _dateOut;
        private string _invoiceNum2;
        private string _price;
        private string _orderNumber;
        private string _orderPositionNumber;
        private string _consignor;
        private string _adjustment;
        private string _description;

        [Column(DbType = "INTEGER", CanBeNull = false)]
        public Int64? clientId
        {            
            get
            {
                return Int32.Parse(_clientId);
            }
            set
            {
                try
                {
                    if (_clientId != value.ToString())
                    {
                        _clientId = value.ToString();
                    }
                }
                catch
                {
                    _clientId = null;
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
            //get
            //{
            //    return _carWeight;
            //}
            //set
            //{
            //    if (_carWeight != value)
            //    {
            //        _carWeight = value;
            //    }
            //}

            get
            {
                try
                {
                    return double.Parse(_carWeight);
                }
                catch
                {
                    return default;
                }
            }
            set
            {
                try
                {
                    if (_carWeight != value.ToString())
                    {
                        _carWeight = value.ToString();
                    }
                }
                catch
                {
                    _carWeight = null;
                }
            }
        }

        [Column(DbType = "REAL", CanBeNull = true)]
        public double? brutto
        {
            //get
            //{
            //    return _brutto;
            //}
            //set
            //{
            //    if (_brutto != value)
            //    {
            //        _brutto = value;
            //    }
            //}

            get
            {
                try
                {
                    return double.Parse(_brutto);
                }
                catch
                {
                    return default;
                }
            }
            set
            {
                try
                {
                    if (_brutto != value.ToString())
                    {
                        _brutto = value.ToString();
                    }
                }
                catch
                {
                    _brutto = null;
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

        [Column(DbType = "INTEGER", CanBeNull = false)]
        public int userId
        {
            //get
            //{
            //    return _userId;
            //}
            //set
            //{
            //    if (_userId != value)
            //    {
            //        _userId = value;
            //    }
            //}

            get
            {
                try
                {
                    return int.Parse(_userId);
                }
                catch
                {
                    return default;
                }
            }
            set
            {
                try
                {
                    if (_userId != value.ToString())
                    {
                        _userId = value.ToString();
                    }
                }
                catch
                {

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
            //get
            //{
            //    return _docWeight;
            //}
            //set
            //{
            //    if (_docWeight != value)
            //    {
            //        _docWeight = value;
            //    }
            //}

            get
            {
                try
                {
                    return double.Parse(_docWeight);
                }
                catch
                {
                    return default;
                }
            }
            set
            {
                try
                {
                    if (_docWeight != value.ToString())
                    {
                        _docWeight = value.ToString();
                    }
                }
                catch
                {
                    _docWeight = null;
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
        public string talonNum
        {
            //get
            //{
            //    //int result;
            //    //int.TryParse(_talonNum, out int result);
            //    if (int.TryParse(_talonNum, out int result))
            //    {
            //        return result;
            //    }
            //    else
            //    {
            //        return null;
            //    }
            //    //return result;
            //}
            //set
            //{
            //    //if (_talonNum != value)
            //    //{
            //    //    _talonNum = value;
            //    //}

            //    if (int.TryParse(_talonNum, out int result))
            //    {
            //        if (result != value)
            //        {
            //            _talonNum = value.ToString(); ;
            //        }
            //    }
            //    else
            //    {
            //        _talonNum = value.ToString();
            //    }
            //}



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
        public string invoiceNum
        {
            get
            {
                try
                {
                    return _invoiceNum;
                }
                catch 
                {
                    return null;
                }

            }
            set
            {
                try
                {                  
                    if (_invoiceNum != value)
                    {
                        _invoiceNum = value;
                    }

                }
                catch
                {
                    _invoiceNum = null;
                }
               
            }

            //get
            //{
            //    //int result;
            //    //int.TryParse(_clientId, out int result);
            //    if (int.TryParse(_invoiceNum, out int result))
            //    {
            //        return result;
            //    }
            //    else
            //    {
            //        return null;
            //    }
            //    //return result;
            //}
            //set
            //{
            //    //if (_clientId != value)
            //    //{
            //    //    _clientId = value;
            //    //}

            //    if (int.TryParse(_invoiceNum, out int result))
            //    {
            //        if (result != value)
            //        {
            //            _invoiceNum = value.ToString(); ;
            //        }
            //    }
            //    else
            //    {
            //        _invoiceNum = value.ToString();
            //    }
            //}
        }

        [Column(DbType = "REAL", CanBeNull = true)]
        public string factorWeight
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
        public string invNumLoading
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
        public string volume
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
            //get
            //{
            //    return _driverId;
            //}
            //set
            //{
            //    if (_driverId != value)
            //    {
            //        _driverId = value;
            //    }
            //}

            get
            {
                try
                {
                    return int.Parse(_driverId);
                }
                catch
                {
                    return default;
                }
            }
            set
            {
                try
                {
                    if (_driverId != value.ToString())
                    {
                        _driverId = value.ToString();
                    }
                }
                catch
                {
                    _driverId = null;
                }
            }
        }

        [Column(DbType = "INTEGER", CanBeNull = true)]
        public string storageId
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
        public string carrierid
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
        public string dateOut
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
        public string invoiceNum2
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
            //get
            //{
            //    return _price;
            //}
            //set
            //{
            //    if (_price != value)
            //    {
            //        _price = value;
            //    }
            //}

            get
            {
                try
                {
                    return double.Parse(_price);
                }
                catch
                {
                    return default;
                }
            }
            set
            {
                try
                {
                    if (_price != value.ToString())
                    {
                        _price = value.ToString();
                    }
                }
                catch
                {
                    _price = null;
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
            //get
            //{
            //    return _adjustment;
            //}
            //set
            //{
            //    if (_adjustment != value)
            //    {
            //        _adjustment = value;
            //    }
            //}

            get
            {
                try
                {
                    return double.Parse(_adjustment);
                }
                catch
                {
                    return default;
                }
            }
            set
            {
                try
                {
                    if (_adjustment != value.ToString())
                    {
                        _adjustment = value.ToString();
                    }
                }
                catch
                {
                    _adjustment = null;
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
    }
}
