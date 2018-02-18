using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Xml;
using ApplicationForRSS.Helpers;
using ApplicationForRSS.Models;
using ApplicationForRSS.Repositories;
using AutoMapper;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ApplicationForRSS.Services
{
    public class RssFeedService : IRssFeedService
    {
        private IRssFeedRepository _rssFeedRepository;

        public RssFeedService(IRssFeedRepository rssFeedRepository)
        {
            _rssFeedRepository = rssFeedRepository;
        }

        public void CreateRssFeed(RssFeedDto feedToBeCreatedDto)
        {
            _rssFeedRepository.CreateRssFeed(feedToBeCreatedDto);
        }

        public void DeleteRssFeed(int id)
        {
            var feed = _rssFeedRepository.GetRssFeedPassingId(id);
            _rssFeedRepository.DeleteRssFeed(feed);
        }

        public bool FeedExists(int id)
        {
            return _rssFeedRepository.FeedExists(id);
        }

        public IEnumerable<RssFeedDto> GetRssFeed()
        {
            return _rssFeedRepository.GetRssFeed();
        }

        public void PartiallyUpdateRssFeed(int id, RssFeedDto feedToPatch)
        {
            var feedDto = _rssFeedRepository.GetRssFeedPassingId(id);
            feedDto.Source = feedToPatch.Source;
            feedDto.Description = feedToPatch.Description;
            feedDto.DateTime = feedToPatch.DateTime;
            feedDto.CreateDateTime = DateTime.Now;

            _rssFeedRepository.UpDateRssFeed(feedDto);
        }

        public void UpdateRssFeed(int id, RssFeedDto feedToBeUpdatedDto)
        {
            var feedDto = _rssFeedRepository.GetRssFeedPassingId(id);
            feedDto.Source = feedToBeUpdatedDto.Source;
            feedDto.Description = feedToBeUpdatedDto.Description;
            feedDto.DateTime = feedToBeUpdatedDto.DateTime;
            feedDto.CreateDateTime = DateTime.Now;

            _rssFeedRepository.UpDateRssFeed(feedDto);
        }

        public async void GetRssFeedFromAlikatte(string url)
        {
            using (HttpClient client = new HttpClient())
            using (HttpResponseMessage res = await client.GetAsync(url))
                if (res.Content.Headers.ContentType.MediaType == "application/json")
                {
                    // parse json
                }
                else
                {
                    // parse XML
                    var obj = XmlToRSS.GetDataAndParseToDto(url);
                    CreateRssFeed(obj);
                }
        }

        public bool Save()
        {
            return (_rssFeedRepository.Save());
        }
    }
}
