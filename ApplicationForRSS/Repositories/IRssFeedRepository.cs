using ApplicationForRSS.Models;
using System.Collections.Generic;

namespace ApplicationForRSS.Repositories
{
    public interface IRssFeedRepository
    {
        IEnumerable<RssFeedDto> GetRssFeed();
        void CreateRssFeed(RssFeedDto feedToBeCreatedDto);
        void DeleteRssFeed(RssFeedDto feedToBeDelete);
        bool FeedExists(int id);
        RssFeedDto GetRssFeedPassingId(int id);
        void UpDateRssFeed(RssFeedDto feedDto);
        bool Save();
    }
}
