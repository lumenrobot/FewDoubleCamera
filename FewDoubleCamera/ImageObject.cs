using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace FewDoubleCamera
{
    /// <summary>
    /// To be read from AMQP topic.
    /// </summary>
    class ImageObject
    {
        [JsonProperty("name")]
        public String Name { get; set; }
        [JsonProperty("contentType")]
        public String ContentType { get; set; }
        [JsonProperty("contentSize")]
        public long ContentSize { get; set; }
        [JsonProperty("uploadDate")]
        public String UploadDate { get; set; }
        [JsonProperty("dateCreated")]
        public String DateCreated { get; set; }
        [JsonProperty("dateModified")]
        public String DateModified { get; set; }
        [JsonProperty("datePublished")]
        public String DatePublished { get; set; }
        [JsonProperty("contentUrl")]
        public string ContentUrl { get; set; }

        public override string ToString()
        {
            return Name + " (" + ContentSize + ") " + ContentUrl;
        }
    }
}
