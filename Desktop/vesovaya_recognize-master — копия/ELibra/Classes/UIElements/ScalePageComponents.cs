using System.Windows.Forms;

namespace ELibra.Classes.UIElements
{
    class ScalePageComponents
    {
        public TabPage scalesPage { get; set; }
        public RadioButton rBtnMetra { get; set; }
        //public RadioButton rBtnTurc { get; set; }
        public RadioButton rBtnIP65 { get; set; }
        public RadioButton rBtnCAS { get; set; }
        public TextBox tMinWeightVideo { get; set; }
        public ComboBox cmbPortName { get; set; }
        public ComboBox cmbBaudRate { get; set; }
        public ComboBox cmbDataBits { get; set; }
        public ComboBox cmbParity { get; set; }
        public ComboBox cmbStopBits { get; set; }
        public ComboBox cmbHandshake { get; set; }
        public CheckBox cBoxRTS { get; set; }
        public ListBox listScaleCameras { get; set; }
        public ToolTip cameraToolTip { get; set; }
        public ComboBox cmbScaleCamerasURL { get; set; }
        public Button btnScaleCameraAdd { get; set; }
        public TextBox tScaleName { get; set; }
        public NumericUpDown numDelay { get; set; }
        public Label lblMinWeightVideo { get; set; }
        public Label lblScalesDelay { get; set; }
        public Label lblScaleCameras { get; set; }
        public Label lblScales { get; set; }
        public Label lblThread { get; set; }
        public Label lblStop { get; set; }
        public Label lblParity { get; set; }
        public Label lblBits { get; set; }
        public Label lblSpeed { get; set; }
        public Label lblNamePort { get; set; }
    }
}
