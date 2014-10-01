using Nop.Web.Framework;
using Nop.Web.Framework.Mvc;

namespace Nop.Admin.Models.Settings
{
    public partial class MediaSettingsModel : BaseNopModel
    {
        public int ActiveStoreScopeConfiguration { get; set; }



        [NopResourceDisplayName("Admin.Configuration.Settings.Media.PicturesStoredIntoDatabase")]
        public bool PicturesStoredIntoDatabase { get; set; }

        [NopResourceDisplayName("Admin.Configuration.Settings.Media.AvatarPictureSize")]
        public int AvatarPictureSize { get; set; }
        public bool AvatarPictureSize_OverrideForStore { get; set; }

        [NopResourceDisplayName("Admin.Configuration.Settings.Media.MaximumImageSize")]
        public int MaximumImageSize { get; set; }
        public bool MaximumImageSize_OverrideForStore { get; set; }
    }
}