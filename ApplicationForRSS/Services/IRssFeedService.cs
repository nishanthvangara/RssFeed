using ApplicationForRSS.Models;
using System.Collections.Generic;

namespace ApplicationForRSS.Services
{
    public interface IRssFeedService
    {
        IEnumerable<RssFeedDto> GetRssFeed();
        void CreateRssFeed(RssFeedDto feedToBeCreatedDto);
        void DeleteRssFeed(int id);
        bool FeedExists(int id);
        void UpdateRssFeed(int id, RssFeedDto feedToBeUpdatedDto);
        void PartiallyUpdateRssFeed(int id, RssFeedDto feedToPatch);
        void StoreFeed(string data);
    }
}
