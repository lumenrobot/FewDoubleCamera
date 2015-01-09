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
        [JsonProperty("contentUrl")]
        public string ContentUrl { get; set; }
    }
}
