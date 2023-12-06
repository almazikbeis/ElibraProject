using ELibra.Classes.UIElements;

namespace ELibra.Classes.Structures
{
    public class scaleMainPanel
    {
        public string Name { get; set; }
        public string NameUser { get; set; }
        public ScalesMainForm ScalePanel { get; set; }
        public WeightingBase Libra { get; set; }
        public string[] Cameras { get; set; }
        public bool Active { get; set; }
        public string Model { get; set; }
    }
}
