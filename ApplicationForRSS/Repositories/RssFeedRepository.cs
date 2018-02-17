using System.Collections.Generic;
using System.Linq;
using ApplicationForRSS.Entities;
using ApplicationForRSS.Models;
using AutoMapper;

namespace ApplicationForRSS.Repositories
{
    public class RssFeedRepository : IRssFeedRepository
    {
        private RssFeedContext _context;

        public RssFeedRepository(RssFeedContext context)
        {
            _context = context;
        }

        public void CreateRssFeed(RssFeedDto feedToBeCreatedDto)
        {
            var rssFeedEntity = Mapper.Map<RssFeed>(feedToBeCreatedDto);
            _context.RssFeed.Add(rssFeedEntity);
            _context.SaveChanges();
        }

        public void DeleteRssFeed(RssFeedDto feedToBeDelete)
        {
            var rssFeedEntity = Mapper.Map<RssFeed>(feedToBeDelete);
            _context.RssFeed.Remove(rssFeedEntity);
            _context.SaveChanges();
        }

        public bool FeedExists(int id)
        {
            var rssFeedEntity = _context.RssFeed.Where(feed => feed.Id == id).FirstOrDefault();
            if (rssFeedEntity != null) return true;
            return false;
        }

        public IEnumerable<RssFeedDto> GetRssFeed()
        {
            var dbRecords = _context.RssFeed.OrderBy(dbRecord => dbRecord.Id).ToList();
            var listofRssFeedDto = Mapper.Map<IEnumerable<RssFeedDto>>(dbRecords);
            return listofRssFeedDto;
        }

        public RssFeedDto GetRssFeedPassingId(int id)
        {
            var dbRecord = _context.RssFeed.Where(record => record.Id == id).FirstOrDefault();
            var feed = Mapper.Map<RssFeedDto>(dbRecord);
            return feed;
        }

        public void UpDateRssFeed(RssFeedDto feedDto)
        {
            var rssFeedEntity = Mapper.Map<RssFeed>(feedDto);
            _context.RssFeed.Remove(rssFeedEntity);
            _context.SaveChanges();
        }
    }
}
