using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELibra.DBModel.Models
{
    [Table(Name = "Scales")]
    public class Scales : BaseModel
    {
        private string _name;
        private string _nameUser;
        private string _Model;
        private string _PortName;
        private string _BaudRate;
        private string _DataBits;
        private string _Parity;
        private string _RtsEnable;
        private string _StopBits;
        private string _Handshake;
        private string _MaxWeight;
        private string _MinWeightVideo;
        private string _linkedCameras;
        private string _isSave;
        private double? _RecognizeDelay;



        [Column(DbType = "TEXT", CanBeNull = true)]
        public string name 
        { 
            get => this._name;
            set 
            {
                if (_name != value)
                {
                    _name = value;
                }
            }
        }

        [Column(DbType = "TEXT", CanBeNull = true)]
        public string nameUser
        {
            get => this._nameUser;
            set
            {
                if (_nameUser != value)
                {
                    _nameUser = value;
                }
            }
        }

        [Column(DbType = "TEXT", CanBeNull = true)]
        public string Model 
        {
            get => _Model;
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
            get => _PortName;
            set
            {
                if (_PortName != value)
                {
                    _PortName = value;
                }
            }
        }

        [Column(DbType = "TEXT", CanBeNull = true)]
        public string BaudRate 
        { 
            get => _BaudRate;
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
            get => _DataBits;
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
            get => _Parity;
            set
            {
                if (_Parity != value)
                {
                    _Parity = value;
                }
            }
        }

        [Column(DbType = "TEXT", CanBeNull = true)]
        public string RtsEnable
        {
            get => _RtsEnable;
            set
            {
                if (_RtsEnable != value)
                {
                    _RtsEnable = value;
                }
            }
        }

        [Column(DbType = "TEXT", CanBeNull = true)]
        public string StopBits 
        { 
            get => _StopBits;
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
            get => _Handshake;
            set
            {
                if (_Handshake != value)
                {
                    _Handshake = value;
                }
            }
        }

        [Column(DbType = "TEXT", CanBeNull = true)]
        public string MaxWeight 
        { 
            get => _MaxWeight;
            set
            {
                if (_MaxWeight != value)
                {
                    _MaxWeight = value;
                }
            }
        }

        [Column(DbType = "TEXT", CanBeNull = true)]
        public string MinWeightVideo 
        {
            get => _MinWeightVideo;
            set
            {
                if (_MinWeightVideo != value)
                {
                    _MinWeightVideo = value;
                }
            }
        }

        [Column(DbType = "TEXT", CanBeNull = true)]
        public string LinkedCameras 
        { 
            get => _linkedCameras;
            set
            {
                if (_linkedCameras != value)
                {
                    _linkedCameras = value;
                }
            }
        }

        [Column(DbType = "TEXT", CanBeNull = true)]
        public string isSave
        {
            get => _isSave;
            set
            {
                if (_isSave != value)
                {
                    _isSave = value;
                }
            }
        }

        [Column(DbType = "REAL", CanBeNull = true)]
        public double? RecognizeDelay 
        { 
            get => _RecognizeDelay;
            set
            {
                if (_RecognizeDelay != value)
                {
                    _RecognizeDelay = value;
                }
            }
        }
    }
}
