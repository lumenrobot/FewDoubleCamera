using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace FewDoubleCamera
{
    class HumanFaceRecognized
    {
        [JsonProperty("index")]
        public int Index { get; set; }
        [JsonProperty("name")]
        public String Name { get; set; }
        [JsonProperty("minPoint")]
        public Vector3 MinPoint { get; set; }
        [JsonProperty("maxPoint")]
        public Vector3 MaxPoint { get; set; }

        public HumanFaceRecognized(string name, Vector3 minPoint, Vector3 maxPoint)
        {
            this.Name = name;
            this.MinPoint = minPoint;
            this.MaxPoint = maxPoint;
        }
        
        public override string ToString()
        {
            return Name + " " + MinPoint + ".." + MaxPoint;
        }

    }
}
