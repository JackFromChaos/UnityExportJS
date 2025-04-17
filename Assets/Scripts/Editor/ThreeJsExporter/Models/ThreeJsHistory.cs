using System.Collections.Generic;

namespace ThreeJsExporter.Models
{
    [System.Serializable]
    public class ThreeJsHistory
    {
        public List<object> undos { get; set; } = new List<object>();
        public List<object> redos { get; set; } = new List<object>();
        
        public ThreeJsHistory()
        {
            undos = new List<object>();
            redos = new List<object>();
        }
    }
}