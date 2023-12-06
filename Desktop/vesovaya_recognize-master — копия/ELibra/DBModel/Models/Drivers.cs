using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELibra.DBModel.Models
{
    [Table(Name = "Drivers")]
    public class Drivers : BaseModelUpdt
    {
        private string _surname;
        private string _name;
        private string _patronimyc;
        private string _udl;

        [Column(DbType = "TEXT", CanBeNull = false)]
        public string surname
        {
            get
            {
                return _surname;
            }
            set
            {
                if (_surname != value)
                {
                    _surname = value;
                }
            }
        }

        [Column(DbType ="TEXT", CanBeNull = true)]
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
        public string patronimyc
        {
            get
            {
                return _patronimyc;
            }
            set
            {
                if (_patronimyc != value)
                {
                    _patronimyc = value;
                }
            }
        }

        [Column(DbType = "TEXT", CanBeNull = true)]
        public string udl
        {
            get
            {
                return _udl;
            }
            set
            {
                if (_udl != value)
                {
                    _udl = value;
                }
            }
        }
    }
}
