using System;

namespace R5T.Nykoping.Database.Entities
{
    public class EmailEndpoint
    {
        public int ID { get; set; }

        public Guid EndpointGUID { get; set; }

        /// <summary>
        /// Not a unique key. There can be multiple email endpoints with the same email address.
        /// </summary>
        public string EmailAddress { get; set; }
    }
}
