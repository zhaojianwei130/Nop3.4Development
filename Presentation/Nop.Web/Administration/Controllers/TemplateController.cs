using System;
using System.Linq;
using System.Web.Mvc;
using Nop.Core.Domain.Catalog;
using Nop.Services.Localization;
using Nop.Services.Security;
using Nop.Web.Framework.Kendoui;
using Nop.Web.Framework.Mvc;

namespace Nop.Admin.Controllers
{
    public partial class TemplateController : BaseAdminController
    {
        #region Fields

        private readonly IPermissionService _permissionService;
        private readonly ILocalizationService _localizationService;

        #endregion

        #region Constructors

        public TemplateController(IPermissionService permissionService,
            ILocalizationService localizationService)
        {
            this._permissionService = permissionService;
            this._localizationService = localizationService;
        }

        #endregion
    }
}
