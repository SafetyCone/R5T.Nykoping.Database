using System;

namespace R5T.Nykoping.Database.Entities
{
    public class WebformEndpoint
    {
        public int ID { get; set; }

        public Guid EndpointGUID { get; set; }

        /// <summary>
        /// Not a unique key. There can be multiple endpoints with the same phone number.
        /// </summary>
        public string WebformUrl { get; set; }
    }
}
