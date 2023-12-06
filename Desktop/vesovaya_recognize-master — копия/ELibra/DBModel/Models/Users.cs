using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELibra.DBModel.Models
{
    [Table(Name = "Users")]
    public class Users : BaseModelUpdt
    {
        private string _FIO;
        private string _username;
        private string _password;
        private int _roleId;
        private string _post;
        private string _deleted;

        [Column(DbType = "TEXT", CanBeNull = false)]
        public string FIO
        {
            get
            {
                return _FIO;
            }
            set
            {
                if (_FIO != value)
                {
                    _FIO = value;
                }
            }
        }

        [Column(DbType = "TEXT", CanBeNull = false)]
        public string username
        {
            get
            {
                return _username;
            }
            set
            {
                if (_username != value)
                {
                    _username = value;
                }
            }
        }

        [Column(DbType = "TEXT", CanBeNull = false)]
        public string password
        {
            get
            {
                return _password;
            }
            set
            {
                if (_password != value)
                {
                    _password = value;
                }
            }
        }

        [Column(DbType = "INTEGER")]
        public int roleId
        {
            get
            {
                return _roleId;
            }
            set
            {
                if (_roleId != value)
                {
                    _roleId = value;
                }
            }
        }

        [Column(DbType = "TEXT", CanBeNull = true)]
        public string post
        {
            get
            {
                return _post;
            }
            set
            {
                if (_post != value)
                {
                    _post = value;
                }
            }
        }

        [Column(DbType = "TEXT", CanBeNull = false)]
        public string deleted
        {
            get
            {
                return _deleted;
            }
            set
            {
                if (_deleted != value)
                {
                    _deleted = value;
                }
            }
        }
    }
}
