using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ELibra.Classes.Structures
{
    public class JournalItem
    {
        public DateTime dateWeight { get; set; }
        public string name { get; set; }
        public string carNumber { get; set; }
        public string carMark { get; set; }
        public string material { get; set; }
        public string carWeight { get; set; }
        public string brutto { get; set; }
        public string factorWeight { get; set; }
        public string docWeight { get; set; }
        public double? volume { get; set; }
        public double adjustment { get; set; }
        public string scaleTara { get; set; }
        public string scaleBrutto { get; set; }
        public string edited { get; set; }
        public DataGridViewComboBoxColumn VideoFiles { get; set; }

    }



}
