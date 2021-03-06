﻿using ApplicationForRSS.DomainObjects;
using ApplicationForRSS.Models;
using ApplicationForRSS.Services;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace ApplicationForRSS.Controllers
{
    [Route("api/rssfeed")]
    public class RssFeedController : Controller
    {
        private IRssFeedService _rssFeedService;
        private ILogger<RssFeedController> _logger;

        public RssFeedController(IRssFeedService rssFeedService, ILogger<RssFeedController> logger)
        {
            _rssFeedService = rssFeedService;
            _logger = logger;
        }

        [HttpGet("GetRssFeed")]
        public IActionResult GetRssFeed()
        {
            try
            {
                var data = _rssFeedService.GetRssFeed();
                var results = Mapper.Map<IEnumerable<RssFeedDomainObj>>(data);
                return Ok(results);
            }
            catch (Exception exception)
            {
                _logger.LogCritical($"Exception while getting Rss Feed", exception);
                return StatusCode(500, "A problem happened while handling your request.");
            }
        }

        [HttpPost()]
        public IActionResult CreateRssFeed([FromBody] RssFeedDomainObj feedToBeCreated)
        {
            if (feedToBeCreated == null)
            {
                return BadRequest();
            }
            if (feedToBeCreated.Source == feedToBeCreated.Description)
            {
                ModelState.AddModelError("Data", "The provided Data cannot be same as source");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var feedToBeCreatedDto = Mapper.Map<Models.RssFeedDto>(feedToBeCreated);
                _rssFeedService.CreateRssFeed(feedToBeCreatedDto);
                return Ok();
            }
            catch (Exception exception)
            {
                _logger.LogCritical($"Exception while saving Rss Feed", exception);
                return StatusCode(500, "A problem happened while handling your request.");
            }
        }

        [HttpPut()]
        public IActionResult UpdateRssFeed(int id, [FromBody] RssFeedDomainObj feedToBeUpdated)
        {
            if (feedToBeUpdated == null)
            {
                return BadRequest();
            }

            if (feedToBeUpdated.Description == feedToBeUpdated.Source)
            {
                ModelState.AddModelError("Description", "The provided description should be different from the Source.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_rssFeedService.FeedExists(id))
            {
                return NotFound();
            }
            var feedToBeUpdatedDto = Mapper.Map<RssFeedDto>(feedToBeUpdated);
            _rssFeedService.UpdateRssFeed(id, feedToBeUpdatedDto);
            return NoContent();
        }

        [HttpPatch()]
        public IActionResult PartiallyUpdateRssFeed(int id, [FromBody] JsonPatchDocument<RssFeedDomainObj> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest();
            }

            if (!_rssFeedService.FeedExists(id))
            {
                return NotFound();
            }
            var feedToPatch = Mapper.Map<RssFeedDto>(patchDoc);
            _rssFeedService.PartiallyUpdateRssFeed(id, feedToPatch);
            return NoContent();
        }

        [HttpDelete()]
        public IActionResult DeleteRssFeed(int Id)
        {
            if (!_rssFeedService.FeedExists(Id))
            {
                return NotFound();
            }
            _rssFeedService.DeleteRssFeed(Id);
            return NoContent();
        }

        [HttpGet("GetRssFeedFromarAlikatte")]
        public IActionResult GetRssFeedFromAlikatte()
        {
            string baseUrl = "http://www.aralikatte.com/feed/";
            _rssFeedService.GetRssFeedFromAlikatte(baseUrl);
            _rssFeedService.Save();
            return Ok();
        }        
    }
}
