using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
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
            throw new NotImplementedException();
        }

        public void StoreFeed(string data)
        {

           
            //var rssFeedDto = Mapper.Map<RssFeedDto>(data);
            dynamic stuff = JsonConvert.DeserializeObject(data);

            string name = stuff;
            string address = stuff.Address.City;
            using (var ms = new MemoryStream(Encoding.Unicode.GetBytes(data)))
            {
                // Deserialization from JSON  
                DataContractJsonSerializer deserializer = new DataContractJsonSerializer(typeof(MyClass));
                MyClass bsObj2 = (MyClass)deserializer.ReadObject(ms);
            }
            _rssFeedRepository.CreateRssFeed(null);
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
    }

    public class MyClass
    {
        // extra fields
        [JsonExtensionData]
        private IDictionary<string, JToken> _extraStuff;
    }
}
