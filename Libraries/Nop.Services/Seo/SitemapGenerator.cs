using System;
using System.Linq;
using System.Web.Mvc;
using Nop.Core;
using Nop.Core.Domain.Common;
using Nop.Services.Topics;

namespace Nop.Services.Seo
{
    /// <summary>
    /// Represents a sitemap generator
    /// </summary>
    public partial class SitemapGenerator : BaseSitemapGenerator, ISitemapGenerator
    {
        private readonly IStoreContext _storeContext;
        private readonly ITopicService _topicService;
        private readonly CommonSettings _commonSettings;

        public SitemapGenerator(IStoreContext storeContext,
            ITopicService topicService, CommonSettings commonSettings)
        {
            this._storeContext = storeContext;
            this._topicService = topicService;
            this._commonSettings = commonSettings;
        }

        /// <summary>
        /// Method that is overridden, that handles creation of child urls.
        /// Use the method WriteUrlLocation() within this method.
        /// </summary>
        /// <param name="urlHelper">URL helper</param>
        protected override void GenerateUrlNodes(UrlHelper urlHelper)
        {
            if (_commonSettings.SitemapIncludeTopics)
            {
                WriteTopics(urlHelper);
            }
        }

        private void WriteTopics(UrlHelper urlHelper)
        {
            var topics = _topicService.GetAllTopics(_storeContext.CurrentStore.Id).ToList().FindAll(t => t.IncludeInSitemap);
            foreach (var topic in topics)
            {
                var url = urlHelper.RouteUrl("Topic", new { SeName = topic.GetSeName() }, "http");
                var updateFrequency = UpdateFrequency.Weekly;
                var updateTime = DateTime.UtcNow;
                WriteUrlLocation(url, updateFrequency, updateTime);
            }
        }
    }
}
