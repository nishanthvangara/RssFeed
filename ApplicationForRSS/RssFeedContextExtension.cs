using ApplicationForRSS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApplicationForRSS
{
    public static class RssFeedContextExtension
    {
        public static void EnsureSeedDataForContext(this RssFeedContext context)
        {
            if (context.RssFeed.Any())
            {
                return;
            }

            var feed = new RssFeed()
            {
                //Id = 1,
                Header = "headerTest",
                Description = "Description test",
                Source = "source test",
                DateTime = DateTime.Now,
                CreateDateTime = DateTime.Now
            };

            context.Add(feed);
            context.SaveChanges();
        }
    }
}
