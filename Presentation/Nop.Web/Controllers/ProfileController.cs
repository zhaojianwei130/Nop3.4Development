using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Nop.Core.Domain.Customers;
using Nop.Core.Domain.Media;
using Nop.Services.Common;
using Nop.Services.Customers;
using Nop.Services.Directory;
using Nop.Services.Helpers;
using Nop.Services.Localization;
using Nop.Services.Media;
using Nop.Services.Seo;
using Nop.Web.Framework;
using Nop.Web.Framework.Security;
using Nop.Web.Models.Common;
using Nop.Web.Models.Profile;

namespace Nop.Web.Controllers
{
    [NopHttpsRequirement(SslRequirement.No)]
    public partial class ProfileController : BasePublicController
    {
        private readonly ILocalizationService _localizationService;
        private readonly IPictureService _pictureService;
        private readonly ICountryService _countryService;
        private readonly ICustomerService _customerService;
        private readonly IDateTimeHelper _dateTimeHelper;
        private readonly CustomerSettings _customerSettings;
        private readonly MediaSettings _mediaSettings;

        public ProfileController(ILocalizationService localizationService,
            IPictureService pictureService,
            ICountryService countryService,
            ICustomerService customerService,
            IDateTimeHelper dateTimeHelper,
            CustomerSettings customerSettings,
            MediaSettings mediaSettings)
        {
            this._localizationService = localizationService;
            this._pictureService = pictureService;
            this._countryService = countryService;
            this._customerService = customerService;
            this._dateTimeHelper = dateTimeHelper;
            this._customerSettings = customerSettings;
            this._mediaSettings = mediaSettings;
        }

        public ActionResult Index(int? id, int? page)
        {
            if (!_customerSettings.AllowViewingProfiles)
            {
                return RedirectToRoute("HomePage");
            }

            var customerId = 0;
            if (id.HasValue)
            {
                customerId = id.Value;
            }

            var customer = _customerService.GetCustomerById(customerId);
            if (customer == null || customer.IsGuest())
            {
                return RedirectToRoute("HomePage");
            }

            bool pagingPosts = false;
            int postsPage = 0;

            if (page.HasValue)
            {
                postsPage = page.Value;
                pagingPosts = true;
            }

            var name = customer.FormatUserName();
            var title = string.Format(_localizationService.GetResource("Profile.ProfileOf"), name);

            var model = new ProfileIndexModel()
            {
                ProfileTitle = title,
                PostsPage = postsPage,
                PagingPosts = pagingPosts,
                CustomerProfileId = customer.Id
            };

            return View(model);
        }

        //profile info tab
        [ChildActionOnly]
        public ActionResult Info(int customerProfileId)
        {
            var customer = _customerService.GetCustomerById(customerProfileId);
            if (customer == null)
            {
                return RedirectToRoute("HomePage");
            }

            //avatar
            var avatarUrl = "";
            if (_customerSettings.AllowCustomersToUploadAvatars)
            {
                avatarUrl =_pictureService.GetPictureUrl(
                 customer.GetAttribute<int>(SystemCustomerAttributeNames.AvatarPictureId),
                 _mediaSettings.AvatarPictureSize,
                 _customerSettings.DefaultAvatarEnabled,
                 defaultPictureType: PictureType.Avatar);
            }

            //location
            bool locationEnabled = false;
            string location = string.Empty;
            if (_customerSettings.ShowCustomersLocation)
            {
                locationEnabled = true;

                var countryId = customer.GetAttribute<int>(SystemCustomerAttributeNames.CountryId);
                var country = _countryService.GetCountryById(countryId);
                if (country != null)
                {
                    location = country.GetLocalized(x => x.Name);
                }
                else
                {
                    locationEnabled = false;
                }
            }

            //registration date
            bool joinDateEnabled = false;
            string joinDate = string.Empty;

            if (_customerSettings.ShowCustomersJoinDate)
            {
                joinDateEnabled = true;
                joinDate = _dateTimeHelper.ConvertToUserTime(customer.CreatedOnUtc, DateTimeKind.Utc).ToString("f");
            }

            //birth date
            bool dateOfBirthEnabled = false;
            string dateOfBirth = string.Empty;
            if (_customerSettings.DateOfBirthEnabled)
            {
                var dob = customer.GetAttribute<DateTime?>(SystemCustomerAttributeNames.DateOfBirth);
                if (dob.HasValue)
                {
                    dateOfBirthEnabled = true;
                    dateOfBirth = dob.Value.ToString("D");
                }
            }

            var model = new ProfileInfoModel()
            {
                CustomerProfileId = customer.Id,
                AvatarUrl = avatarUrl,
                LocationEnabled = locationEnabled,
                Location = location,
                JoinDateEnabled = joinDateEnabled,
                JoinDate = joinDate,
                DateOfBirthEnabled = dateOfBirthEnabled,
                DateOfBirth = dateOfBirth,
            };

            return PartialView(model);
        }
    }
}
