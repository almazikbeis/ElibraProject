using ELibra.DBModel;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELibra.Classes
{
    public class WeightingBase
    {
        public delegate void WeightChangeHandler(double weight, string scaleName);
        public SerialPort sp = new SerialPort();
        public bool isRecord = false;
        public List<Recorder> arrRec = new List<Recorder>();
        public List<string> arrVid = new List<string>();

        private double weight = 0;

        public double Weight
        {
            get { return weight; }
            set
            {
                weight = value;
            }
        }
    }
}
