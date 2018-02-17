using System;

namespace ApplicationForRSS.DomainObjects
{
    public class RssFeedDomainObj
    {
        public string Source { get; set; }

        public string Header { get; set; }

        public string Description { get; set; }

        public DateTime DateTime { get; set; }
    }
}
