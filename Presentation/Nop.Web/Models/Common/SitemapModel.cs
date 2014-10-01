using System.Collections.Generic;
using Nop.Web.Framework.Mvc;
using Nop.Web.Models.Topics;

namespace Nop.Web.Models.Common
{
    public partial class SitemapModel : BaseNopModel
    {
        public SitemapModel()
        {

        }

        public IList<TopicModel> Topics { get; set; }

        public bool NewsEnabled { get; set; }
    }
}