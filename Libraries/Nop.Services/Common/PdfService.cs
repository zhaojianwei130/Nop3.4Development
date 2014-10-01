// RTL Support provided by Credo inc (www.credo.co.il  ||   info@credo.co.il)

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Nop.Core;
using Nop.Core.Domain.Common;
using Nop.Core.Domain.Directory;
using Nop.Core.Domain.Localization;
using Nop.Core.Html;
using Nop.Services.Configuration;
using Nop.Services.Directory;
using Nop.Services.Helpers;
using Nop.Services.Localization;
using Nop.Services.Media;
using Nop.Services.Stores;

namespace Nop.Services.Common
{
    /// <summary>
    /// PDF service
    /// </summary>
    public partial class PdfService : IPdfService
    {
        #region Fields

        private readonly ILocalizationService _localizationService;
        private readonly ILanguageService _languageService;
        private readonly IWorkContext _workContext;
        private readonly IDateTimeHelper _dateTimeHelper;
        private readonly IPictureService _pictureService;
        private readonly IStoreService _storeService;
        private readonly IStoreContext _storeContext;
        private readonly ISettingService _settingContext;
        private readonly IWebHelper _webHelper;

        private readonly PdfSettings _pdfSettings;
        private readonly AddressSettings _addressSettings;

        #endregion

        #region Ctor

        public PdfService(ILocalizationService localizationService, 
            ILanguageService languageService,
            IWorkContext workContext,
            IDateTimeHelper dateTimeHelper,
            IPictureService pictureService,
            IStoreService storeService,
            IStoreContext storeContext,
            ISettingService settingContext,
            IWebHelper webHelper, 
            PdfSettings pdfSettings,
            AddressSettings addressSettings)
        {
            this._localizationService = localizationService;
            this._languageService = languageService;
            this._workContext = workContext;
            this._dateTimeHelper = dateTimeHelper;
            this._pictureService = pictureService;
            this._storeService = storeService;
            this._storeContext = storeContext;
            this._settingContext = settingContext;
            this._webHelper = webHelper;
            this._pdfSettings = pdfSettings;
            this._addressSettings = addressSettings;
        }

        #endregion

        #region Utilities

        protected virtual Font GetFont()
        {
            //nopCommerce supports unicode characters
            //nopCommerce uses Free Serif font by default (~/App_Data/Pdf/FreeSerif.ttf file)
            //It was downloaded from http://savannah.gnu.org/projects/freefont
            string fontPath = Path.Combine(_webHelper.MapPath("~/App_Data/Pdf/"), _pdfSettings.FontFileName);
            var baseFont = BaseFont.CreateFont(fontPath, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
            var font = new Font(baseFont, 10, Font.NORMAL);
            return font;
        }

        /// <summary>
        /// Get font direction
        /// </summary>
        /// <param name="lang">Language</param>
        /// <returns>Font direction</returns>
        protected virtual int GetDirection(Language lang)
        {
            return lang.Rtl ? PdfWriter.RUN_DIRECTION_RTL : PdfWriter.RUN_DIRECTION_LTR;
        }

        /// <summary>
        /// Get element alignment
        /// </summary>
        /// <param name="lang">Language</param>
        /// <param name="isOpposite">Is opposite?</param>
        /// <returns>Element alignment</returns>
        protected virtual int GetAlignment(Language lang, bool isOpposite = false)
        {
            //if we need the element to be opposite, like logo etc`.
            if (!isOpposite)
                return lang.Rtl ? Element.ALIGN_RIGHT : Element.ALIGN_LEFT;
            else
                return lang.Rtl ? Element.ALIGN_LEFT : Element.ALIGN_RIGHT;
        }

        #endregion

        #region Methods

        #endregion
    }
}