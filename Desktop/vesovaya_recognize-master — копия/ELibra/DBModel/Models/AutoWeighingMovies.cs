using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Linq.Mapping;

namespace ELibra.DBModel.Models
{
    [Table(Name = "AutoWeighingMovies")]
    public class AutoWeighingMovies : BaseModel
    {
        private int _factId;
        private string _movieName;

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

        [Column(DbType = "TEXT", CanBeNull = false)]
        public string movieName
        {
            get
            {
                return _movieName;
            }

            set
            {
                if (_movieName != value)
                {
                    _movieName = value;
                }
            }
        }


    }
}
