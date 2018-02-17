using System;

namespace ApplicationForRSS.Models
{
    public class RssFeedDto
    {
        public int Id;

        public string Source { get; set; }

        public string Header { get; set; }

        public string Description { get; set; }

        public DateTime DateTime { get; set; }

        public DateTime CreateDateTime { get; set; }
    }
}
