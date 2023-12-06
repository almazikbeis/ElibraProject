//using LinqToDB.Mapping;
using ELibra.DBModel.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Linq.Mapping;

using System.Data.SQLite;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELibra.DBModel.ModelsTimeString
{
    [Table(Name = "AutoWeighingFacts") ] 
    
    public class AutoWeighingFacts : BaseModel
    {
        private string _startDate;

        private string _finishDate;

        private double _startWeight;

        private double _finishWeight;

        private double _maxWeight;
        
        [Column(DbType = "TEXT", CanBeNull = false)]
        public string startDate
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

        [Column(DbType = "TEXT", CanBeNull = false)]
        public string finishDate
        {
            get
            {
                return _finishDate;
            }
            set
            {
                if (_finishDate != value)
                {
                    _finishDate = value;
                }
            }
        }

        [Column(DbType = "REAL", CanBeNull = false)]
        public double startWeight
        {
            get
            {
                return _startWeight;
            }
            set
            {
                if (_startWeight != value)
                {
                    _startWeight = value;
                }
            }
        }

        [Column(DbType = "REAL", CanBeNull = false)]
        public double finishWeight
        {
            get
            {
                return _finishWeight;
            }
            set
            {
                if (_finishWeight != value)
                {
                    _finishWeight = value;
                }
            }
        }

        [Column(DbType = "REAL", CanBeNull = false)]
        public double maxWeight
        {
            get
            {
                return _maxWeight;
            }
            set
            {
                if (_maxWeight != value)
                {
                    _maxWeight = value;
                }
            }
        }
    }
}
