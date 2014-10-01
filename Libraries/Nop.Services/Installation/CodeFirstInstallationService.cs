using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Nop.Core;
using Nop.Core.Data;
using Nop.Core.Domain;
using Nop.Core.Domain.Cms;
using Nop.Core.Domain.Common;
using Nop.Core.Domain.Customers;
using Nop.Core.Domain.Directory;
using Nop.Core.Domain.Localization;
using Nop.Core.Domain.Logging;
using Nop.Core.Domain.Media;
using Nop.Core.Domain.Messages;
using Nop.Core.Domain.News;
using Nop.Core.Domain.Polls;
using Nop.Core.Domain.Security;
using Nop.Core.Domain.Seo;
using Nop.Core.Domain.Stores;
using Nop.Core.Domain.Tasks;
using Nop.Core.Domain.Topics;
using Nop.Core.Infrastructure;
using Nop.Services.Common;
using Nop.Services.Configuration;
using Nop.Services.Customers;
using Nop.Services.Helpers;
using Nop.Services.Localization;
using Nop.Services.Media;
using Nop.Services.Seo;

namespace Nop.Services.Installation
{
    public partial class CodeFirstInstallationService : IInstallationService
    {
        #region Fields

        private readonly IRepository<Store> _storeRepository;
        private readonly IRepository<Language> _languageRepository;
        private readonly IRepository<Customer> _customerRepository;
        private readonly IRepository<CustomerRole> _customerRoleRepository;
        private readonly IRepository<UrlRecord> _urlRecordRepository;
        private readonly IRepository<EmailAccount> _emailAccountRepository;
        private readonly IRepository<MessageTemplate> _messageTemplateRepository;
        private readonly IRepository<Country> _countryRepository;
        private readonly IRepository<StateProvince> _stateProvinceRepository;
        private readonly IRepository<Topic> _topicRepository;
        private readonly IRepository<NewsItem> _newsItemRepository;
        private readonly IRepository<Poll> _pollRepository;
        private readonly IRepository<ActivityLogType> _activityLogTypeRepository;
        private readonly IRepository<ScheduleTask> _scheduleTaskRepository;
        private readonly IGenericAttributeService _genericAttributeService;
        private readonly IWebHelper _webHelper;

        #endregion

        #region Ctor

        public CodeFirstInstallationService(IRepository<Store> storeRepository,
            IRepository<Language> languageRepository,
            IRepository<Customer> customerRepository,
            IRepository<CustomerRole> customerRoleRepository,
            IRepository<UrlRecord> urlRecordRepository,
            IRepository<EmailAccount> emailAccountRepository,
            IRepository<MessageTemplate> messageTemplateRepository,
            IRepository<Country> countryRepository,
            IRepository<StateProvince> stateProvinceRepository,
            IRepository<Topic> topicRepository,
            IRepository<NewsItem> newsItemRepository,
            IRepository<Poll> pollRepository,
            IRepository<ActivityLogType> activityLogTypeRepository,
            IRepository<ScheduleTask> scheduleTaskRepository,
            IGenericAttributeService genericAttributeService,
            IWebHelper webHelper)
        {
            this._storeRepository = storeRepository;
            this._languageRepository = languageRepository;
            this._customerRepository = customerRepository;
            this._customerRoleRepository = customerRoleRepository;
            this._urlRecordRepository = urlRecordRepository;
            this._emailAccountRepository = emailAccountRepository;
            this._messageTemplateRepository = messageTemplateRepository;
            this._countryRepository = countryRepository;
            this._stateProvinceRepository = stateProvinceRepository;
            this._topicRepository = topicRepository;
            this._newsItemRepository = newsItemRepository;
            this._pollRepository = pollRepository;
            this._activityLogTypeRepository = activityLogTypeRepository;
            this._scheduleTaskRepository = scheduleTaskRepository;
            this._genericAttributeService = genericAttributeService;
            this._webHelper = webHelper;
        }

        #endregion
        
        #region Utilities

        protected virtual void InstallStores()
        {
            //var storeUrl = "http://www.yourStore.com/";
            var storeUrl = _webHelper.GetStoreLocation(false);
            var stores = new List<Store>()
            {
                new Store()
                {
                    Name = "Your store name",
                    Url = storeUrl,
                    SslEnabled = false,
                    Hosts = "yourstore.com,www.yourstore.com",
                    DisplayOrder = 1,
                },
            };

            stores.ForEach(x => _storeRepository.Insert(x));
        }

        protected virtual void InstallLanguages()
        {
            var language = new Language
            {
                Name = "English",
                LanguageCulture = "en-US",
                UniqueSeoCode = "en",
                FlagImageFileName = "us.png",
                Published = true,
                DisplayOrder = 1
            };
            _languageRepository.Insert(language);
        }

        protected virtual void InstallLocaleResources()
        {
            //'English' language
            var language = _languageRepository.Table.Single(l => l.Name == "English");

            //save resources
            foreach (var filePath in System.IO.Directory.EnumerateFiles(_webHelper.MapPath("~/App_Data/Localization/"), "*.nopres.xml", SearchOption.TopDirectoryOnly))
            {
                var localesXml = File.ReadAllText(filePath);
                var localizationService = EngineContext.Current.Resolve<ILocalizationService>();
                localizationService.ImportResourcesFromXml(language, localesXml);
            }

        }

        protected virtual void InstallCountriesAndStates()
        {
            var cUsa = new Country
            {
                Name = "United States",
                TwoLetterIsoCode = "US",
                ThreeLetterIsoCode = "USA",
                NumericIsoCode = 840,
                SubjectToVat = false,
                DisplayOrder = 1,
                Published = true,
            };
            cUsa.StateProvinces.Add(new StateProvince()
            {
                Name = "AA (Armed Forces Americas)",
                Abbreviation = "AA",
                Published = true,
                DisplayOrder = 1,
            });
            cUsa.StateProvinces.Add(new StateProvince()
            {
                Name = "AE (Armed Forces Europe)",
                Abbreviation = "AE",
                Published = true,
                DisplayOrder = 1,
            });
            cUsa.StateProvinces.Add(new StateProvince()
            {
                Name = "Alabama",
                Abbreviation = "AL",
                Published = true,
                DisplayOrder = 1,
            });
            cUsa.StateProvinces.Add(new StateProvince()
            {
                Name = "Alaska",
                Abbreviation = "AK",
                Published = true,
                DisplayOrder = 1,
            });
            cUsa.StateProvinces.Add(new StateProvince()
            {
                Name = "American Samoa",
                Abbreviation = "AS",
                Published = true,
                DisplayOrder = 1,
            });
            cUsa.StateProvinces.Add(new StateProvince()
            {
                Name = "AP (Armed Forces Pacific)",
                Abbreviation = "AP",
                Published = true,
                DisplayOrder = 1,
            });
            cUsa.StateProvinces.Add(new StateProvince()
            {
                Name = "Arizona",
                Abbreviation = "AZ",
                Published = true,
                DisplayOrder = 1,
            });
            cUsa.StateProvinces.Add(new StateProvince()
            {
                Name = "Arkansas",
                Abbreviation = "AR",
                Published = true,
                DisplayOrder = 1,
            });
            cUsa.StateProvinces.Add(new StateProvince()
            {
                Name = "California",
                Abbreviation = "CA",
                Published = true,
                DisplayOrder = 1,
            });
            cUsa.StateProvinces.Add(new StateProvince()
            {
                Name = "Colorado",
                Abbreviation = "CO",
                Published = true,
                DisplayOrder = 1,
            });
            cUsa.StateProvinces.Add(new StateProvince()
            {
                Name = "Connecticut",
                Abbreviation = "CT",
                Published = true,
                DisplayOrder = 1,
            });
            cUsa.StateProvinces.Add(new StateProvince()
            {
                Name = "Delaware",
                Abbreviation = "DE",
                Published = true,
                DisplayOrder = 1,
            });
            cUsa.StateProvinces.Add(new StateProvince()
            {
                Name = "District of Columbia",
                Abbreviation = "DC",
                Published = true,
                DisplayOrder = 1,
            });
            cUsa.StateProvinces.Add(new StateProvince()
            {
                Name = "Federated States of Micronesia",
                Abbreviation = "FM",
                Published = true,
                DisplayOrder = 1,
            });
            cUsa.StateProvinces.Add(new StateProvince()
            {
                Name = "Florida",
                Abbreviation = "FL",
                Published = true,
                DisplayOrder = 1,
            });
            cUsa.StateProvinces.Add(new StateProvince()
            {
                Name = "Georgia",
                Abbreviation = "GA",
                Published = true,
                DisplayOrder = 1,
            });
            cUsa.StateProvinces.Add(new StateProvince()
            {
                Name = "Guam",
                Abbreviation = "GU",
                Published = true,
                DisplayOrder = 1,
            });
            cUsa.StateProvinces.Add(new StateProvince()
            {
                Name = "Hawaii",
                Abbreviation = "HI",
                Published = true,
                DisplayOrder = 1,
            });
            cUsa.StateProvinces.Add(new StateProvince()
            {
                Name = "Idaho",
                Abbreviation = "ID",
                Published = true,
                DisplayOrder = 1,
            });
            cUsa.StateProvinces.Add(new StateProvince()
            {
                Name = "Illinois",
                Abbreviation = "IL",
                Published = true,
                DisplayOrder = 1,
            });
            cUsa.StateProvinces.Add(new StateProvince()
            {
                Name = "Indiana",
                Abbreviation = "IN",
                Published = true,
                DisplayOrder = 1,
            });
            cUsa.StateProvinces.Add(new StateProvince()
            {
                Name = "Iowa",
                Abbreviation = "IA",
                Published = true,
                DisplayOrder = 1,
            });
            cUsa.StateProvinces.Add(new StateProvince()
            {
                Name = "Kansas",
                Abbreviation = "KS",
                Published = true,
                DisplayOrder = 1,
            });
            cUsa.StateProvinces.Add(new StateProvince()
            {
                Name = "Kentucky",
                Abbreviation = "KY",
                Published = true,
                DisplayOrder = 1,
            });
            cUsa.StateProvinces.Add(new StateProvince()
            {
                Name = "Louisiana",
                Abbreviation = "LA",
                Published = true,
                DisplayOrder = 1,
            });
            cUsa.StateProvinces.Add(new StateProvince()
            {
                Name = "Maine",
                Abbreviation = "ME",
                Published = true,
                DisplayOrder = 1,
            });
            cUsa.StateProvinces.Add(new StateProvince()
            {
                Name = "Marshall Islands",
                Abbreviation = "MH",
                Published = true,
                DisplayOrder = 1,
            });
            cUsa.StateProvinces.Add(new StateProvince()
            {
                Name = "Maryland",
                Abbreviation = "MD",
                Published = true,
                DisplayOrder = 1,
            });
            cUsa.StateProvinces.Add(new StateProvince()
            {
                Name = "Massachusetts",
                Abbreviation = "MA",
                Published = true,
                DisplayOrder = 1,
            });
            cUsa.StateProvinces.Add(new StateProvince()
            {
                Name = "Michigan",
                Abbreviation = "MI",
                Published = true,
                DisplayOrder = 1,
            });
            cUsa.StateProvinces.Add(new StateProvince()
            {
                Name = "Minnesota",
                Abbreviation = "MN",
                Published = true,
                DisplayOrder = 1,
            });
            cUsa.StateProvinces.Add(new StateProvince()
            {
                Name = "Mississippi",
                Abbreviation = "MS",
                Published = true,
                DisplayOrder = 1,
            });
            cUsa.StateProvinces.Add(new StateProvince()
            {
                Name = "Missouri",
                Abbreviation = "MO",
                Published = true,
                DisplayOrder = 1,
            });
            cUsa.StateProvinces.Add(new StateProvince()
            {
                Name = "Montana",
                Abbreviation = "MT",
                Published = true,
                DisplayOrder = 1,
            });
            cUsa.StateProvinces.Add(new StateProvince()
            {
                Name = "Nebraska",
                Abbreviation = "NE",
                Published = true,
                DisplayOrder = 1,
            });
            cUsa.StateProvinces.Add(new StateProvince()
            {
                Name = "Nevada",
                Abbreviation = "NV",
                Published = true,
                DisplayOrder = 1,
            });
            cUsa.StateProvinces.Add(new StateProvince()
            {
                Name = "New Hampshire",
                Abbreviation = "NH",
                Published = true,
                DisplayOrder = 1,
            });
            cUsa.StateProvinces.Add(new StateProvince()
            {
                Name = "New Jersey",
                Abbreviation = "NJ",
                Published = true,
                DisplayOrder = 1,
            });
            cUsa.StateProvinces.Add(new StateProvince()
            {
                Name = "New Mexico",
                Abbreviation = "NM",
                Published = true,
                DisplayOrder = 1,
            });
            cUsa.StateProvinces.Add(new StateProvince()
            {
                Name = "New York",
                Abbreviation = "NY",
                Published = true,
                DisplayOrder = 1,
            });
            cUsa.StateProvinces.Add(new StateProvince()
            {
                Name = "North Carolina",
                Abbreviation = "NC",
                Published = true,
                DisplayOrder = 1,
            });
            cUsa.StateProvinces.Add(new StateProvince()
            {
                Name = "North Dakota",
                Abbreviation = "ND",
                Published = true,
                DisplayOrder = 1,
            });
            cUsa.StateProvinces.Add(new StateProvince()
            {
                Name = "Northern Mariana Islands",
                Abbreviation = "MP",
                Published = true,
                DisplayOrder = 1,
            });
            cUsa.StateProvinces.Add(new StateProvince()
            {
                Name = "Ohio",
                Abbreviation = "OH",
                Published = true,
                DisplayOrder = 1,
            });
            cUsa.StateProvinces.Add(new StateProvince()
            {
                Name = "Oklahoma",
                Abbreviation = "OK",
                Published = true,
                DisplayOrder = 1,
            });
            cUsa.StateProvinces.Add(new StateProvince()
            {
                Name = "Oregon",
                Abbreviation = "OR",
                Published = true,
                DisplayOrder = 1,
            });
            cUsa.StateProvinces.Add(new StateProvince()
            {
                Name = "Palau",
                Abbreviation = "PW",
                Published = true,
                DisplayOrder = 1,
            });
            cUsa.StateProvinces.Add(new StateProvince()
            {
                Name = "Pennsylvania",
                Abbreviation = "PA",
                Published = true,
                DisplayOrder = 1,
            });
            cUsa.StateProvinces.Add(new StateProvince()
            {
                Name = "Puerto Rico",
                Abbreviation = "PR",
                Published = true,
                DisplayOrder = 1,
            });
            cUsa.StateProvinces.Add(new StateProvince()
            {
                Name = "Rhode Island",
                Abbreviation = "RI",
                Published = true,
                DisplayOrder = 1,
            });
            cUsa.StateProvinces.Add(new StateProvince()
            {
                Name = "South Carolina",
                Abbreviation = "SC",
                Published = true,
                DisplayOrder = 1,
            });
            cUsa.StateProvinces.Add(new StateProvince()
            {
                Name = "South Dakota",
                Abbreviation = "SD",
                Published = true,
                DisplayOrder = 1,
            });
            cUsa.StateProvinces.Add(new StateProvince()
            {
                Name = "Tennessee",
                Abbreviation = "TN",
                Published = true,
                DisplayOrder = 1,
            });
            cUsa.StateProvinces.Add(new StateProvince()
            {
                Name = "Texas",
                Abbreviation = "TX",
                Published = true,
                DisplayOrder = 1,
            });
            cUsa.StateProvinces.Add(new StateProvince()
            {
                Name = "Utah",
                Abbreviation = "UT",
                Published = true,
                DisplayOrder = 1,
            });
            cUsa.StateProvinces.Add(new StateProvince()
            {
                Name = "Vermont",
                Abbreviation = "VT",
                Published = true,
                DisplayOrder = 1,
            });
            cUsa.StateProvinces.Add(new StateProvince()
            {
                Name = "Virgin Islands",
                Abbreviation = "VI",
                Published = true,
                DisplayOrder = 1,
            });
            cUsa.StateProvinces.Add(new StateProvince()
            {
                Name = "Virginia",
                Abbreviation = "VA",
                Published = true,
                DisplayOrder = 1,
            });
            cUsa.StateProvinces.Add(new StateProvince()
            {
                Name = "Washington",
                Abbreviation = "WA",
                Published = true,
                DisplayOrder = 1,
            });
            cUsa.StateProvinces.Add(new StateProvince()
            {
                Name = "West Virginia",
                Abbreviation = "WV",
                Published = true,
                DisplayOrder = 1,
            });
            cUsa.StateProvinces.Add(new StateProvince()
            {
                Name = "Wisconsin",
                Abbreviation = "WI",
                Published = true,
                DisplayOrder = 1,
            });
            cUsa.StateProvinces.Add(new StateProvince()
            {
                Name = "Wyoming",
                Abbreviation = "WY",
                Published = true,
                DisplayOrder = 1,
            });
            var cCanada = new Country
            {
                Name = "Canada",
                
                
                TwoLetterIsoCode = "CA",
                ThreeLetterIsoCode = "CAN",
                NumericIsoCode = 124,
                SubjectToVat = false,
                DisplayOrder = 2,
                Published = true,
            };
            cCanada.StateProvinces.Add(new StateProvince()
            {
                Name = "Alberta",
                Abbreviation = "AB",
                Published = true,
                DisplayOrder = 1,
            });
            cCanada.StateProvinces.Add(new StateProvince()
            {
                Name = "British Columbia",
                Abbreviation = "BC",
                Published = true,
                DisplayOrder = 1,
            });
            cCanada.StateProvinces.Add(new StateProvince()
            {
                Name = "Manitoba",
                Abbreviation = "MB",
                Published = true,
                DisplayOrder = 1,
            });
            cCanada.StateProvinces.Add(new StateProvince()
            {
                Name = "New Brunswick",
                Abbreviation = "NB",
                Published = true,
                DisplayOrder = 1,
            });
            cCanada.StateProvinces.Add(new StateProvince()
            {
                Name = "Newfoundland and Labrador",
                Abbreviation = "NL",
                Published = true,
                DisplayOrder = 1,
            });
            cCanada.StateProvinces.Add(new StateProvince()
            {
                Name = "Northwest Territories",
                Abbreviation = "NT",
                Published = true,
                DisplayOrder = 1,
            });
            cCanada.StateProvinces.Add(new StateProvince()
            {
                Name = "Nova Scotia",
                Abbreviation = "NS",
                Published = true,
                DisplayOrder = 1,
            });
            cCanada.StateProvinces.Add(new StateProvince()
            {
                Name = "Nunavut",
                Abbreviation = "NU",
                Published = true,
                DisplayOrder = 1,
            });
            cCanada.StateProvinces.Add(new StateProvince()
            {
                Name = "Ontario",
                Abbreviation = "ON",
                Published = true,
                DisplayOrder = 1,
            });
            cCanada.StateProvinces.Add(new StateProvince()
            {
                Name = "Prince Edward Island",
                Abbreviation = "PE",
                Published = true,
                DisplayOrder = 1,
            });
            cCanada.StateProvinces.Add(new StateProvince()
            {
                Name = "Quebec",
                Abbreviation = "QC",
                Published = true,
                DisplayOrder = 1,
            });
            cCanada.StateProvinces.Add(new StateProvince()
            {
                Name = "Saskatchewan",
                Abbreviation = "SK",
                Published = true,
                DisplayOrder = 1,
            });
            cCanada.StateProvinces.Add(new StateProvince()
            {
                Name = "Yukon Territory",
                Abbreviation = "YU",
                Published = true,
                DisplayOrder = 1,
            });
            var countries = new List<Country>
                                {
                                    cUsa,
                                    cCanada,
                                    //other countries
                                    new Country
                                    {
	                                    Name = "Argentina",
	                                    
	                                    
	                                    TwoLetterIsoCode = "AR",
	                                    ThreeLetterIsoCode = "ARG",
	                                    NumericIsoCode = 32,
	                                    SubjectToVat = false,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "Armenia",
	                                    
	                                    
	                                    TwoLetterIsoCode = "AM",
	                                    ThreeLetterIsoCode = "ARM",
	                                    NumericIsoCode = 51,
	                                    SubjectToVat = false,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "Aruba",
	                                    
	                                    
	                                    TwoLetterIsoCode = "AW",
	                                    ThreeLetterIsoCode = "ABW",
	                                    NumericIsoCode = 533,
	                                    SubjectToVat = false,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "Australia",
	                                    
	                                    
	                                    TwoLetterIsoCode = "AU",
	                                    ThreeLetterIsoCode = "AUS",
	                                    NumericIsoCode = 36,
	                                    SubjectToVat = false,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "Austria",
	                                    
	                                    
	                                    TwoLetterIsoCode = "AT",
	                                    ThreeLetterIsoCode = "AUT",
	                                    NumericIsoCode = 40,
	                                    SubjectToVat = true,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "Azerbaijan",
	                                    
	                                    
	                                    TwoLetterIsoCode = "AZ",
	                                    ThreeLetterIsoCode = "AZE",
	                                    NumericIsoCode = 31,
	                                    SubjectToVat = false,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "Bahamas",
	                                    
	                                    
	                                    TwoLetterIsoCode = "BS",
	                                    ThreeLetterIsoCode = "BHS",
	                                    NumericIsoCode = 44,
	                                    SubjectToVat = false,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "Bangladesh",
	                                    
	                                    
	                                    TwoLetterIsoCode = "BD",
	                                    ThreeLetterIsoCode = "BGD",
	                                    NumericIsoCode = 50,
	                                    SubjectToVat = false,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "Belarus",
	                                    
	                                    
	                                    TwoLetterIsoCode = "BY",
	                                    ThreeLetterIsoCode = "BLR",
	                                    NumericIsoCode = 112,
	                                    SubjectToVat = false,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "Belgium",
	                                    
	                                    
	                                    TwoLetterIsoCode = "BE",
	                                    ThreeLetterIsoCode = "BEL",
	                                    NumericIsoCode = 56,
	                                    SubjectToVat = true,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "Belize",
	                                    
	                                    
	                                    TwoLetterIsoCode = "BZ",
	                                    ThreeLetterIsoCode = "BLZ",
	                                    NumericIsoCode = 84,
	                                    SubjectToVat = false,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "Bermuda",
	                                    
	                                    
	                                    TwoLetterIsoCode = "BM",
	                                    ThreeLetterIsoCode = "BMU",
	                                    NumericIsoCode = 60,
	                                    SubjectToVat = false,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "Bolivia",
	                                    
	                                    
	                                    TwoLetterIsoCode = "BO",
	                                    ThreeLetterIsoCode = "BOL",
	                                    NumericIsoCode = 68,
	                                    SubjectToVat = false,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "Bosnia and Herzegowina",
	                                    
	                                    
	                                    TwoLetterIsoCode = "BA",
	                                    ThreeLetterIsoCode = "BIH",
	                                    NumericIsoCode = 70,
	                                    SubjectToVat = false,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "Brazil",
	                                    
	                                    
	                                    TwoLetterIsoCode = "BR",
	                                    ThreeLetterIsoCode = "BRA",
	                                    NumericIsoCode = 76,
	                                    SubjectToVat = false,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "Bulgaria",
	                                    
	                                    
	                                    TwoLetterIsoCode = "BG",
	                                    ThreeLetterIsoCode = "BGR",
	                                    NumericIsoCode = 100,
	                                    SubjectToVat = true,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "Cayman Islands",
	                                    
	                                    
	                                    TwoLetterIsoCode = "KY",
	                                    ThreeLetterIsoCode = "CYM",
	                                    NumericIsoCode = 136,
	                                    SubjectToVat = false,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "Chile",
	                                    
	                                    
	                                    TwoLetterIsoCode = "CL",
	                                    ThreeLetterIsoCode = "CHL",
	                                    NumericIsoCode = 152,
	                                    SubjectToVat = false,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "China",
	                                    
	                                    
	                                    TwoLetterIsoCode = "CN",
	                                    ThreeLetterIsoCode = "CHN",
	                                    NumericIsoCode = 156,
	                                    SubjectToVat = false,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "Colombia",
	                                    
	                                    
	                                    TwoLetterIsoCode = "CO",
	                                    ThreeLetterIsoCode = "COL",
	                                    NumericIsoCode = 170,
	                                    SubjectToVat = false,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "Costa Rica",
	                                    
	                                    
	                                    TwoLetterIsoCode = "CR",
	                                    ThreeLetterIsoCode = "CRI",
	                                    NumericIsoCode = 188,
	                                    SubjectToVat = false,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "Croatia",
	                                    
	                                    
	                                    TwoLetterIsoCode = "HR",
	                                    ThreeLetterIsoCode = "HRV",
	                                    NumericIsoCode = 191,
	                                    SubjectToVat = false,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "Cuba",
	                                    
	                                    
	                                    TwoLetterIsoCode = "CU",
	                                    ThreeLetterIsoCode = "CUB",
	                                    NumericIsoCode = 192,
	                                    SubjectToVat = false,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "Cyprus",
	                                    
	                                    
	                                    TwoLetterIsoCode = "CY",
	                                    ThreeLetterIsoCode = "CYP",
	                                    NumericIsoCode = 196,
	                                    SubjectToVat = true,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "Czech Republic",
	                                    
	                                    
	                                    TwoLetterIsoCode = "CZ",
	                                    ThreeLetterIsoCode = "CZE",
	                                    NumericIsoCode = 203,
	                                    SubjectToVat = true,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "Denmark",
	                                    
	                                    
	                                    TwoLetterIsoCode = "DK",
	                                    ThreeLetterIsoCode = "DNK",
	                                    NumericIsoCode = 208,
	                                    SubjectToVat = true,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "Dominican Republic",
	                                    
	                                    
	                                    TwoLetterIsoCode = "DO",
	                                    ThreeLetterIsoCode = "DOM",
	                                    NumericIsoCode = 214,
	                                    SubjectToVat = false,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "Ecuador",
	                                    
	                                    
	                                    TwoLetterIsoCode = "EC",
	                                    ThreeLetterIsoCode = "ECU",
	                                    NumericIsoCode = 218,
	                                    SubjectToVat = false,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "Egypt",
	                                    
	                                    
	                                    TwoLetterIsoCode = "EG",
	                                    ThreeLetterIsoCode = "EGY",
	                                    NumericIsoCode = 818,
	                                    SubjectToVat = false,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "Finland",
	                                    
	                                    
	                                    TwoLetterIsoCode = "FI",
	                                    ThreeLetterIsoCode = "FIN",
	                                    NumericIsoCode = 246,
	                                    SubjectToVat = true,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "France",
	                                    
	                                    
	                                    TwoLetterIsoCode = "FR",
	                                    ThreeLetterIsoCode = "FRA",
	                                    NumericIsoCode = 250,
	                                    SubjectToVat = true,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "Georgia",
	                                    
	                                    
	                                    TwoLetterIsoCode = "GE",
	                                    ThreeLetterIsoCode = "GEO",
	                                    NumericIsoCode = 268,
	                                    SubjectToVat = false,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "Germany",
	                                    
	                                    
	                                    TwoLetterIsoCode = "DE",
	                                    ThreeLetterIsoCode = "DEU",
	                                    NumericIsoCode = 276,
	                                    SubjectToVat = true,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "Gibraltar",
	                                    
	                                    
	                                    TwoLetterIsoCode = "GI",
	                                    ThreeLetterIsoCode = "GIB",
	                                    NumericIsoCode = 292,
	                                    SubjectToVat = false,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "Greece",
	                                    
	                                    
	                                    TwoLetterIsoCode = "GR",
	                                    ThreeLetterIsoCode = "GRC",
	                                    NumericIsoCode = 300,
	                                    SubjectToVat = true,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "Guatemala",
	                                    
	                                    
	                                    TwoLetterIsoCode = "GT",
	                                    ThreeLetterIsoCode = "GTM",
	                                    NumericIsoCode = 320,
	                                    SubjectToVat = false,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "Hong Kong",
	                                    
	                                    
	                                    TwoLetterIsoCode = "HK",
	                                    ThreeLetterIsoCode = "HKG",
	                                    NumericIsoCode = 344,
	                                    SubjectToVat = false,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "Hungary",
	                                    
	                                    
	                                    TwoLetterIsoCode = "HU",
	                                    ThreeLetterIsoCode = "HUN",
	                                    NumericIsoCode = 348,
	                                    SubjectToVat = true,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "India",
	                                    
	                                    
	                                    TwoLetterIsoCode = "IN",
	                                    ThreeLetterIsoCode = "IND",
	                                    NumericIsoCode = 356,
	                                    SubjectToVat = false,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "Indonesia",
	                                    
	                                    
	                                    TwoLetterIsoCode = "ID",
	                                    ThreeLetterIsoCode = "IDN",
	                                    NumericIsoCode = 360,
	                                    SubjectToVat = false,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "Ireland",
	                                    
	                                    
	                                    TwoLetterIsoCode = "IE",
	                                    ThreeLetterIsoCode = "IRL",
	                                    NumericIsoCode = 372,
	                                    SubjectToVat = true,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "Israel",
	                                    
	                                    
	                                    TwoLetterIsoCode = "IL",
	                                    ThreeLetterIsoCode = "ISR",
	                                    NumericIsoCode = 376,
	                                    SubjectToVat = false,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "Italy",
	                                    
	                                    
	                                    TwoLetterIsoCode = "IT",
	                                    ThreeLetterIsoCode = "ITA",
	                                    NumericIsoCode = 380,
	                                    SubjectToVat = true,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "Jamaica",
	                                    
	                                    
	                                    TwoLetterIsoCode = "JM",
	                                    ThreeLetterIsoCode = "JAM",
	                                    NumericIsoCode = 388,
	                                    SubjectToVat = false,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "Japan",
	                                    
	                                    
	                                    TwoLetterIsoCode = "JP",
	                                    ThreeLetterIsoCode = "JPN",
	                                    NumericIsoCode = 392,
	                                    SubjectToVat = false,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "Jordan",
	                                    
	                                    
	                                    TwoLetterIsoCode = "JO",
	                                    ThreeLetterIsoCode = "JOR",
	                                    NumericIsoCode = 400,
	                                    SubjectToVat = false,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "Kazakhstan",
	                                    
	                                    
	                                    TwoLetterIsoCode = "KZ",
	                                    ThreeLetterIsoCode = "KAZ",
	                                    NumericIsoCode = 398,
	                                    SubjectToVat = false,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "Korea, Democratic People's Republic of",
	                                    
	                                    
	                                    TwoLetterIsoCode = "KP",
	                                    ThreeLetterIsoCode = "PRK",
	                                    NumericIsoCode = 408,
	                                    SubjectToVat = false,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "Kuwait",
	                                    
	                                    
	                                    TwoLetterIsoCode = "KW",
	                                    ThreeLetterIsoCode = "KWT",
	                                    NumericIsoCode = 414,
	                                    SubjectToVat = false,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "Malaysia",
	                                    
	                                    
	                                    TwoLetterIsoCode = "MY",
	                                    ThreeLetterIsoCode = "MYS",
	                                    NumericIsoCode = 458,
	                                    SubjectToVat = false,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "Mexico",
	                                    
	                                    
	                                    TwoLetterIsoCode = "MX",
	                                    ThreeLetterIsoCode = "MEX",
	                                    NumericIsoCode = 484,
	                                    SubjectToVat = false,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "Netherlands",
	                                    
	                                    
	                                    TwoLetterIsoCode = "NL",
	                                    ThreeLetterIsoCode = "NLD",
	                                    NumericIsoCode = 528,
	                                    SubjectToVat = true,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "New Zealand",
	                                    
	                                    
	                                    TwoLetterIsoCode = "NZ",
	                                    ThreeLetterIsoCode = "NZL",
	                                    NumericIsoCode = 554,
	                                    SubjectToVat = false,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "Norway",
	                                    
	                                    
	                                    TwoLetterIsoCode = "NO",
	                                    ThreeLetterIsoCode = "NOR",
	                                    NumericIsoCode = 578,
	                                    SubjectToVat = false,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "Pakistan",
	                                    
	                                    
	                                    TwoLetterIsoCode = "PK",
	                                    ThreeLetterIsoCode = "PAK",
	                                    NumericIsoCode = 586,
	                                    SubjectToVat = false,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "Paraguay",
	                                    
	                                    
	                                    TwoLetterIsoCode = "PY",
	                                    ThreeLetterIsoCode = "PRY",
	                                    NumericIsoCode = 600,
	                                    SubjectToVat = false,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "Peru",
	                                    
	                                    
	                                    TwoLetterIsoCode = "PE",
	                                    ThreeLetterIsoCode = "PER",
	                                    NumericIsoCode = 604,
	                                    SubjectToVat = false,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "Philippines",
	                                    
	                                    
	                                    TwoLetterIsoCode = "PH",
	                                    ThreeLetterIsoCode = "PHL",
	                                    NumericIsoCode = 608,
	                                    SubjectToVat = false,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "Poland",
	                                    
	                                    
	                                    TwoLetterIsoCode = "PL",
	                                    ThreeLetterIsoCode = "POL",
	                                    NumericIsoCode = 616,
	                                    SubjectToVat = true,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "Portugal",
	                                    
	                                    
	                                    TwoLetterIsoCode = "PT",
	                                    ThreeLetterIsoCode = "PRT",
	                                    NumericIsoCode = 620,
	                                    SubjectToVat = true,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "Puerto Rico",
	                                    
	                                    
	                                    TwoLetterIsoCode = "PR",
	                                    ThreeLetterIsoCode = "PRI",
	                                    NumericIsoCode = 630,
	                                    SubjectToVat = false,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "Qatar",
	                                    
	                                    
	                                    TwoLetterIsoCode = "QA",
	                                    ThreeLetterIsoCode = "QAT",
	                                    NumericIsoCode = 634,
	                                    SubjectToVat = false,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "Romania",
	                                    
	                                    
	                                    TwoLetterIsoCode = "RO",
	                                    ThreeLetterIsoCode = "ROM",
	                                    NumericIsoCode = 642,
	                                    SubjectToVat = true,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "Russia",
	                                    
	                                    
	                                    TwoLetterIsoCode = "RU",
	                                    ThreeLetterIsoCode = "RUS",
	                                    NumericIsoCode = 643,
	                                    SubjectToVat = false,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "Saudi Arabia",
	                                    
	                                    
	                                    TwoLetterIsoCode = "SA",
	                                    ThreeLetterIsoCode = "SAU",
	                                    NumericIsoCode = 682,
	                                    SubjectToVat = false,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "Singapore",
	                                    
	                                    
	                                    TwoLetterIsoCode = "SG",
	                                    ThreeLetterIsoCode = "SGP",
	                                    NumericIsoCode = 702,
	                                    SubjectToVat = false,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "Slovakia (Slovak Republic)",
	                                    
	                                    
	                                    TwoLetterIsoCode = "SK",
	                                    ThreeLetterIsoCode = "SVK",
	                                    NumericIsoCode = 703,
	                                    SubjectToVat = true,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "Slovenia",
	                                    
	                                    
	                                    TwoLetterIsoCode = "SI",
	                                    ThreeLetterIsoCode = "SVN",
	                                    NumericIsoCode = 705,
	                                    SubjectToVat = true,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "South Africa",
	                                    
	                                    
	                                    TwoLetterIsoCode = "ZA",
	                                    ThreeLetterIsoCode = "ZAF",
	                                    NumericIsoCode = 710,
	                                    SubjectToVat = false,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "Spain",
	                                    
	                                    
	                                    TwoLetterIsoCode = "ES",
	                                    ThreeLetterIsoCode = "ESP",
	                                    NumericIsoCode = 724,
	                                    SubjectToVat = true,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "Sweden",
	                                    
	                                    
	                                    TwoLetterIsoCode = "SE",
	                                    ThreeLetterIsoCode = "SWE",
	                                    NumericIsoCode = 752,
	                                    SubjectToVat = true,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "Switzerland",
	                                    
	                                    
	                                    TwoLetterIsoCode = "CH",
	                                    ThreeLetterIsoCode = "CHE",
	                                    NumericIsoCode = 756,
	                                    SubjectToVat = false,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "Taiwan",
	                                    
	                                    
	                                    TwoLetterIsoCode = "TW",
	                                    ThreeLetterIsoCode = "TWN",
	                                    NumericIsoCode = 158,
	                                    SubjectToVat = false,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "Thailand",
	                                    
	                                    
	                                    TwoLetterIsoCode = "TH",
	                                    ThreeLetterIsoCode = "THA",
	                                    NumericIsoCode = 764,
	                                    SubjectToVat = false,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "Turkey",
	                                    
	                                    
	                                    TwoLetterIsoCode = "TR",
	                                    ThreeLetterIsoCode = "TUR",
	                                    NumericIsoCode = 792,
	                                    SubjectToVat = false,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "Ukraine",
	                                    
	                                    
	                                    TwoLetterIsoCode = "UA",
	                                    ThreeLetterIsoCode = "UKR",
	                                    NumericIsoCode = 804,
	                                    SubjectToVat = false,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "United Arab Emirates",
	                                    
	                                    
	                                    TwoLetterIsoCode = "AE",
	                                    ThreeLetterIsoCode = "ARE",
	                                    NumericIsoCode = 784,
	                                    SubjectToVat = false,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "United Kingdom",
	                                    
	                                    
	                                    TwoLetterIsoCode = "GB",
	                                    ThreeLetterIsoCode = "GBR",
	                                    NumericIsoCode = 826,
	                                    SubjectToVat = true,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "United States minor outlying islands",
	                                    
	                                    
	                                    TwoLetterIsoCode = "UM",
	                                    ThreeLetterIsoCode = "UMI",
	                                    NumericIsoCode = 581,
	                                    SubjectToVat = false,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "Uruguay",
	                                    
	                                    
	                                    TwoLetterIsoCode = "UY",
	                                    ThreeLetterIsoCode = "URY",
	                                    NumericIsoCode = 858,
	                                    SubjectToVat = false,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "Uzbekistan",
	                                    
	                                    
	                                    TwoLetterIsoCode = "UZ",
	                                    ThreeLetterIsoCode = "UZB",
	                                    NumericIsoCode = 860,
	                                    SubjectToVat = false,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "Venezuela",
	                                    
	                                    
	                                    TwoLetterIsoCode = "VE",
	                                    ThreeLetterIsoCode = "VEN",
	                                    NumericIsoCode = 862,
	                                    SubjectToVat = false,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "Serbia",
	                                    
	                                    
	                                    TwoLetterIsoCode = "RS",
	                                    ThreeLetterIsoCode = "SRB",
	                                    NumericIsoCode = 688,
	                                    SubjectToVat = false,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "Afghanistan",
	                                    
	                                    
	                                    TwoLetterIsoCode = "AF",
	                                    ThreeLetterIsoCode = "AFG",
	                                    NumericIsoCode = 4,
	                                    SubjectToVat = false,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "Albania",
	                                    
	                                    
	                                    TwoLetterIsoCode = "AL",
	                                    ThreeLetterIsoCode = "ALB",
	                                    NumericIsoCode = 8,
	                                    SubjectToVat = false,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "Algeria",
	                                    
	                                    
	                                    TwoLetterIsoCode = "DZ",
	                                    ThreeLetterIsoCode = "DZA",
	                                    NumericIsoCode = 12,
	                                    SubjectToVat = false,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "American Samoa",
	                                    
	                                    
	                                    TwoLetterIsoCode = "AS",
	                                    ThreeLetterIsoCode = "ASM",
	                                    NumericIsoCode = 16,
	                                    SubjectToVat = false,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "Andorra",
	                                    
	                                    
	                                    TwoLetterIsoCode = "AD",
	                                    ThreeLetterIsoCode = "AND",
	                                    NumericIsoCode = 20,
	                                    SubjectToVat = false,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "Angola",
	                                    
	                                    
	                                    TwoLetterIsoCode = "AO",
	                                    ThreeLetterIsoCode = "AGO",
	                                    NumericIsoCode = 24,
	                                    SubjectToVat = false,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "Anguilla",
	                                    
	                                    
	                                    TwoLetterIsoCode = "AI",
	                                    ThreeLetterIsoCode = "AIA",
	                                    NumericIsoCode = 660,
	                                    SubjectToVat = false,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "Antarctica",
	                                    
	                                    
	                                    TwoLetterIsoCode = "AQ",
	                                    ThreeLetterIsoCode = "ATA",
	                                    NumericIsoCode = 10,
	                                    SubjectToVat = false,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "Antigua and Barbuda",
	                                    
	                                    
	                                    TwoLetterIsoCode = "AG",
	                                    ThreeLetterIsoCode = "ATG",
	                                    NumericIsoCode = 28,
	                                    SubjectToVat = false,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "Bahrain",
	                                    
	                                    
	                                    TwoLetterIsoCode = "BH",
	                                    ThreeLetterIsoCode = "BHR",
	                                    NumericIsoCode = 48,
	                                    SubjectToVat = false,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "Barbados",
	                                    
	                                    
	                                    TwoLetterIsoCode = "BB",
	                                    ThreeLetterIsoCode = "BRB",
	                                    NumericIsoCode = 52,
	                                    SubjectToVat = false,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "Benin",
	                                    
	                                    
	                                    TwoLetterIsoCode = "BJ",
	                                    ThreeLetterIsoCode = "BEN",
	                                    NumericIsoCode = 204,
	                                    SubjectToVat = false,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "Bhutan",
	                                    
	                                    
	                                    TwoLetterIsoCode = "BT",
	                                    ThreeLetterIsoCode = "BTN",
	                                    NumericIsoCode = 64,
	                                    SubjectToVat = false,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "Botswana",
	                                    
	                                    
	                                    TwoLetterIsoCode = "BW",
	                                    ThreeLetterIsoCode = "BWA",
	                                    NumericIsoCode = 72,
	                                    SubjectToVat = false,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "Bouvet Island",
	                                    
	                                    
	                                    TwoLetterIsoCode = "BV",
	                                    ThreeLetterIsoCode = "BVT",
	                                    NumericIsoCode = 74,
	                                    SubjectToVat = false,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "British Indian Ocean Territory",
	                                    
	                                    
	                                    TwoLetterIsoCode = "IO",
	                                    ThreeLetterIsoCode = "IOT",
	                                    NumericIsoCode = 86,
	                                    SubjectToVat = false,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "Brunei Darussalam",
	                                    
	                                    
	                                    TwoLetterIsoCode = "BN",
	                                    ThreeLetterIsoCode = "BRN",
	                                    NumericIsoCode = 96,
	                                    SubjectToVat = false,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "Burkina Faso",
	                                    
	                                    
	                                    TwoLetterIsoCode = "BF",
	                                    ThreeLetterIsoCode = "BFA",
	                                    NumericIsoCode = 854,
	                                    SubjectToVat = false,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "Burundi",
	                                    
	                                    
	                                    TwoLetterIsoCode = "BI",
	                                    ThreeLetterIsoCode = "BDI",
	                                    NumericIsoCode = 108,
	                                    SubjectToVat = false,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "Cambodia",
	                                    
	                                    
	                                    TwoLetterIsoCode = "KH",
	                                    ThreeLetterIsoCode = "KHM",
	                                    NumericIsoCode = 116,
	                                    SubjectToVat = false,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "Cameroon",
	                                    
	                                    
	                                    TwoLetterIsoCode = "CM",
	                                    ThreeLetterIsoCode = "CMR",
	                                    NumericIsoCode = 120,
	                                    SubjectToVat = false,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "Cape Verde",
	                                    
	                                    
	                                    TwoLetterIsoCode = "CV",
	                                    ThreeLetterIsoCode = "CPV",
	                                    NumericIsoCode = 132,
	                                    SubjectToVat = false,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "Central African Republic",
	                                    
	                                    
	                                    TwoLetterIsoCode = "CF",
	                                    ThreeLetterIsoCode = "CAF",
	                                    NumericIsoCode = 140,
	                                    SubjectToVat = false,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "Chad",
	                                    
	                                    
	                                    TwoLetterIsoCode = "TD",
	                                    ThreeLetterIsoCode = "TCD",
	                                    NumericIsoCode = 148,
	                                    SubjectToVat = false,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "Christmas Island",
	                                    
	                                    
	                                    TwoLetterIsoCode = "CX",
	                                    ThreeLetterIsoCode = "CXR",
	                                    NumericIsoCode = 162,
	                                    SubjectToVat = false,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "Cocos (Keeling) Islands",
	                                    
	                                    
	                                    TwoLetterIsoCode = "CC",
	                                    ThreeLetterIsoCode = "CCK",
	                                    NumericIsoCode = 166,
	                                    SubjectToVat = false,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "Comoros",
	                                    
	                                    
	                                    TwoLetterIsoCode = "KM",
	                                    ThreeLetterIsoCode = "COM",
	                                    NumericIsoCode = 174,
	                                    SubjectToVat = false,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "Congo",
	                                    
	                                    
	                                    TwoLetterIsoCode = "CG",
	                                    ThreeLetterIsoCode = "COG",
	                                    NumericIsoCode = 178,
	                                    SubjectToVat = false,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "Cook Islands",
	                                    
	                                    
	                                    TwoLetterIsoCode = "CK",
	                                    ThreeLetterIsoCode = "COK",
	                                    NumericIsoCode = 184,
	                                    SubjectToVat = false,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "Cote D'Ivoire",
	                                    
	                                    
	                                    TwoLetterIsoCode = "CI",
	                                    ThreeLetterIsoCode = "CIV",
	                                    NumericIsoCode = 384,
	                                    SubjectToVat = false,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "Djibouti",
	                                    
	                                    
	                                    TwoLetterIsoCode = "DJ",
	                                    ThreeLetterIsoCode = "DJI",
	                                    NumericIsoCode = 262,
	                                    SubjectToVat = false,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "Dominica",
	                                    
	                                    
	                                    TwoLetterIsoCode = "DM",
	                                    ThreeLetterIsoCode = "DMA",
	                                    NumericIsoCode = 212,
	                                    SubjectToVat = false,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "El Salvador",
	                                    
	                                    
	                                    TwoLetterIsoCode = "SV",
	                                    ThreeLetterIsoCode = "SLV",
	                                    NumericIsoCode = 222,
	                                    SubjectToVat = false,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "Equatorial Guinea",
	                                    
	                                    
	                                    TwoLetterIsoCode = "GQ",
	                                    ThreeLetterIsoCode = "GNQ",
	                                    NumericIsoCode = 226,
	                                    SubjectToVat = false,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "Eritrea",
	                                    
	                                    
	                                    TwoLetterIsoCode = "ER",
	                                    ThreeLetterIsoCode = "ERI",
	                                    NumericIsoCode = 232,
	                                    SubjectToVat = false,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "Estonia",
	                                    
	                                    
	                                    TwoLetterIsoCode = "EE",
	                                    ThreeLetterIsoCode = "EST",
	                                    NumericIsoCode = 233,
	                                    SubjectToVat = true,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "Ethiopia",
	                                    
	                                    
	                                    TwoLetterIsoCode = "ET",
	                                    ThreeLetterIsoCode = "ETH",
	                                    NumericIsoCode = 231,
	                                    SubjectToVat = false,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "Falkland Islands (Malvinas)",
	                                    
	                                    
	                                    TwoLetterIsoCode = "FK",
	                                    ThreeLetterIsoCode = "FLK",
	                                    NumericIsoCode = 238,
	                                    SubjectToVat = false,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "Faroe Islands",
	                                    
	                                    
	                                    TwoLetterIsoCode = "FO",
	                                    ThreeLetterIsoCode = "FRO",
	                                    NumericIsoCode = 234,
	                                    SubjectToVat = false,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "Fiji",
	                                    
	                                    
	                                    TwoLetterIsoCode = "FJ",
	                                    ThreeLetterIsoCode = "FJI",
	                                    NumericIsoCode = 242,
	                                    SubjectToVat = false,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "French Guiana",
	                                    
	                                    
	                                    TwoLetterIsoCode = "GF",
	                                    ThreeLetterIsoCode = "GUF",
	                                    NumericIsoCode = 254,
	                                    SubjectToVat = false,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "French Polynesia",
	                                    
	                                    
	                                    TwoLetterIsoCode = "PF",
	                                    ThreeLetterIsoCode = "PYF",
	                                    NumericIsoCode = 258,
	                                    SubjectToVat = false,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "French Southern Territories",
	                                    
	                                    
	                                    TwoLetterIsoCode = "TF",
	                                    ThreeLetterIsoCode = "ATF",
	                                    NumericIsoCode = 260,
	                                    SubjectToVat = false,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "Gabon",
	                                    
	                                    
	                                    TwoLetterIsoCode = "GA",
	                                    ThreeLetterIsoCode = "GAB",
	                                    NumericIsoCode = 266,
	                                    SubjectToVat = false,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "Gambia",
	                                    
	                                    
	                                    TwoLetterIsoCode = "GM",
	                                    ThreeLetterIsoCode = "GMB",
	                                    NumericIsoCode = 270,
	                                    SubjectToVat = false,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "Ghana",
	                                    
	                                    
	                                    TwoLetterIsoCode = "GH",
	                                    ThreeLetterIsoCode = "GHA",
	                                    NumericIsoCode = 288,
	                                    SubjectToVat = false,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "Greenland",
	                                    
	                                    
	                                    TwoLetterIsoCode = "GL",
	                                    ThreeLetterIsoCode = "GRL",
	                                    NumericIsoCode = 304,
	                                    SubjectToVat = false,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "Grenada",
	                                    
	                                    
	                                    TwoLetterIsoCode = "GD",
	                                    ThreeLetterIsoCode = "GRD",
	                                    NumericIsoCode = 308,
	                                    SubjectToVat = false,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "Guadeloupe",
	                                    
	                                    
	                                    TwoLetterIsoCode = "GP",
	                                    ThreeLetterIsoCode = "GLP",
	                                    NumericIsoCode = 312,
	                                    SubjectToVat = false,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "Guam",
	                                    
	                                    
	                                    TwoLetterIsoCode = "GU",
	                                    ThreeLetterIsoCode = "GUM",
	                                    NumericIsoCode = 316,
	                                    SubjectToVat = false,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "Guinea",
	                                    
	                                    
	                                    TwoLetterIsoCode = "GN",
	                                    ThreeLetterIsoCode = "GIN",
	                                    NumericIsoCode = 324,
	                                    SubjectToVat = false,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "Guinea-bissau",
	                                    
	                                    
	                                    TwoLetterIsoCode = "GW",
	                                    ThreeLetterIsoCode = "GNB",
	                                    NumericIsoCode = 624,
	                                    SubjectToVat = false,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "Guyana",
	                                    
	                                    
	                                    TwoLetterIsoCode = "GY",
	                                    ThreeLetterIsoCode = "GUY",
	                                    NumericIsoCode = 328,
	                                    SubjectToVat = false,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "Haiti",
	                                    
	                                    
	                                    TwoLetterIsoCode = "HT",
	                                    ThreeLetterIsoCode = "HTI",
	                                    NumericIsoCode = 332,
	                                    SubjectToVat = false,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "Heard and Mc Donald Islands",
	                                    
	                                    
	                                    TwoLetterIsoCode = "HM",
	                                    ThreeLetterIsoCode = "HMD",
	                                    NumericIsoCode = 334,
	                                    SubjectToVat = false,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "Honduras",
	                                    
	                                    
	                                    TwoLetterIsoCode = "HN",
	                                    ThreeLetterIsoCode = "HND",
	                                    NumericIsoCode = 340,
	                                    SubjectToVat = false,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "Iceland",
	                                    
	                                    
	                                    TwoLetterIsoCode = "IS",
	                                    ThreeLetterIsoCode = "ISL",
	                                    NumericIsoCode = 352,
	                                    SubjectToVat = false,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "Iran (Islamic Republic of)",
	                                    
	                                    
	                                    TwoLetterIsoCode = "IR",
	                                    ThreeLetterIsoCode = "IRN",
	                                    NumericIsoCode = 364,
	                                    SubjectToVat = false,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "Iraq",
	                                    
	                                    
	                                    TwoLetterIsoCode = "IQ",
	                                    ThreeLetterIsoCode = "IRQ",
	                                    NumericIsoCode = 368,
	                                    SubjectToVat = false,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "Kenya",
	                                    
	                                    
	                                    TwoLetterIsoCode = "KE",
	                                    ThreeLetterIsoCode = "KEN",
	                                    NumericIsoCode = 404,
	                                    SubjectToVat = false,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "Kiribati",
	                                    
	                                    
	                                    TwoLetterIsoCode = "KI",
	                                    ThreeLetterIsoCode = "KIR",
	                                    NumericIsoCode = 296,
	                                    SubjectToVat = false,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "Korea",
	                                    
	                                    
	                                    TwoLetterIsoCode = "KR",
	                                    ThreeLetterIsoCode = "KOR",
	                                    NumericIsoCode = 410,
	                                    SubjectToVat = false,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "Kyrgyzstan",
	                                    
	                                    
	                                    TwoLetterIsoCode = "KG",
	                                    ThreeLetterIsoCode = "KGZ",
	                                    NumericIsoCode = 417,
	                                    SubjectToVat = false,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "Lao People's Democratic Republic",
	                                    
	                                    
	                                    TwoLetterIsoCode = "LA",
	                                    ThreeLetterIsoCode = "LAO",
	                                    NumericIsoCode = 418,
	                                    SubjectToVat = false,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "Latvia",
	                                    
	                                    
	                                    TwoLetterIsoCode = "LV",
	                                    ThreeLetterIsoCode = "LVA",
	                                    NumericIsoCode = 428,
	                                    SubjectToVat = true,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "Lebanon",
	                                    
	                                    
	                                    TwoLetterIsoCode = "LB",
	                                    ThreeLetterIsoCode = "LBN",
	                                    NumericIsoCode = 422,
	                                    SubjectToVat = false,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "Lesotho",
	                                    
	                                    
	                                    TwoLetterIsoCode = "LS",
	                                    ThreeLetterIsoCode = "LSO",
	                                    NumericIsoCode = 426,
	                                    SubjectToVat = false,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "Liberia",
	                                    
	                                    
	                                    TwoLetterIsoCode = "LR",
	                                    ThreeLetterIsoCode = "LBR",
	                                    NumericIsoCode = 430,
	                                    SubjectToVat = false,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "Libyan Arab Jamahiriya",
	                                    
	                                    
	                                    TwoLetterIsoCode = "LY",
	                                    ThreeLetterIsoCode = "LBY",
	                                    NumericIsoCode = 434,
	                                    SubjectToVat = false,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "Liechtenstein",
	                                    
	                                    
	                                    TwoLetterIsoCode = "LI",
	                                    ThreeLetterIsoCode = "LIE",
	                                    NumericIsoCode = 438,
	                                    SubjectToVat = false,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "Lithuania",
	                                    
	                                    
	                                    TwoLetterIsoCode = "LT",
	                                    ThreeLetterIsoCode = "LTU",
	                                    NumericIsoCode = 440,
	                                    SubjectToVat = true,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "Luxembourg",
	                                    
	                                    
	                                    TwoLetterIsoCode = "LU",
	                                    ThreeLetterIsoCode = "LUX",
	                                    NumericIsoCode = 442,
	                                    SubjectToVat = true,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "Macau",
	                                    
	                                    
	                                    TwoLetterIsoCode = "MO",
	                                    ThreeLetterIsoCode = "MAC",
	                                    NumericIsoCode = 446,
	                                    SubjectToVat = false,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "Macedonia",
	                                    
	                                    
	                                    TwoLetterIsoCode = "MK",
	                                    ThreeLetterIsoCode = "MKD",
	                                    NumericIsoCode = 807,
	                                    SubjectToVat = false,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "Madagascar",
	                                    
	                                    
	                                    TwoLetterIsoCode = "MG",
	                                    ThreeLetterIsoCode = "MDG",
	                                    NumericIsoCode = 450,
	                                    SubjectToVat = false,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "Malawi",
	                                    
	                                    
	                                    TwoLetterIsoCode = "MW",
	                                    ThreeLetterIsoCode = "MWI",
	                                    NumericIsoCode = 454,
	                                    SubjectToVat = false,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "Maldives",
	                                    
	                                    
	                                    TwoLetterIsoCode = "MV",
	                                    ThreeLetterIsoCode = "MDV",
	                                    NumericIsoCode = 462,
	                                    SubjectToVat = false,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "Mali",
	                                    
	                                    
	                                    TwoLetterIsoCode = "ML",
	                                    ThreeLetterIsoCode = "MLI",
	                                    NumericIsoCode = 466,
	                                    SubjectToVat = false,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "Malta",
	                                    
	                                    
	                                    TwoLetterIsoCode = "MT",
	                                    ThreeLetterIsoCode = "MLT",
	                                    NumericIsoCode = 470,
	                                    SubjectToVat = true,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "Marshall Islands",
	                                    
	                                    
	                                    TwoLetterIsoCode = "MH",
	                                    ThreeLetterIsoCode = "MHL",
	                                    NumericIsoCode = 584,
	                                    SubjectToVat = false,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "Martinique",
	                                    
	                                    
	                                    TwoLetterIsoCode = "MQ",
	                                    ThreeLetterIsoCode = "MTQ",
	                                    NumericIsoCode = 474,
	                                    SubjectToVat = false,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "Mauritania",
	                                    
	                                    
	                                    TwoLetterIsoCode = "MR",
	                                    ThreeLetterIsoCode = "MRT",
	                                    NumericIsoCode = 478,
	                                    SubjectToVat = false,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "Mauritius",
	                                    
	                                    
	                                    TwoLetterIsoCode = "MU",
	                                    ThreeLetterIsoCode = "MUS",
	                                    NumericIsoCode = 480,
	                                    SubjectToVat = false,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "Mayotte",
	                                    
	                                    
	                                    TwoLetterIsoCode = "YT",
	                                    ThreeLetterIsoCode = "MYT",
	                                    NumericIsoCode = 175,
	                                    SubjectToVat = false,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "Micronesia",
	                                    
	                                    
	                                    TwoLetterIsoCode = "FM",
	                                    ThreeLetterIsoCode = "FSM",
	                                    NumericIsoCode = 583,
	                                    SubjectToVat = false,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "Moldova",
	                                    
	                                    
	                                    TwoLetterIsoCode = "MD",
	                                    ThreeLetterIsoCode = "MDA",
	                                    NumericIsoCode = 498,
	                                    SubjectToVat = false,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "Monaco",
	                                    
	                                    
	                                    TwoLetterIsoCode = "MC",
	                                    ThreeLetterIsoCode = "MCO",
	                                    NumericIsoCode = 492,
	                                    SubjectToVat = false,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "Mongolia",
	                                    
	                                    
	                                    TwoLetterIsoCode = "MN",
	                                    ThreeLetterIsoCode = "MNG",
	                                    NumericIsoCode = 496,
	                                    SubjectToVat = false,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "Montenegro",
	                                    
	                                    
	                                    TwoLetterIsoCode = "ME",
	                                    ThreeLetterIsoCode = "MNE",
	                                    NumericIsoCode = 499,
	                                    SubjectToVat = false,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "Montserrat",
	                                    
	                                    
	                                    TwoLetterIsoCode = "MS",
	                                    ThreeLetterIsoCode = "MSR",
	                                    NumericIsoCode = 500,
	                                    SubjectToVat = false,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "Morocco",
	                                    
	                                    
	                                    TwoLetterIsoCode = "MA",
	                                    ThreeLetterIsoCode = "MAR",
	                                    NumericIsoCode = 504,
	                                    SubjectToVat = false,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "Mozambique",
	                                    
	                                    
	                                    TwoLetterIsoCode = "MZ",
	                                    ThreeLetterIsoCode = "MOZ",
	                                    NumericIsoCode = 508,
	                                    SubjectToVat = false,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "Myanmar",
	                                    
	                                    
	                                    TwoLetterIsoCode = "MM",
	                                    ThreeLetterIsoCode = "MMR",
	                                    NumericIsoCode = 104,
	                                    SubjectToVat = false,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "Namibia",
	                                    
	                                    
	                                    TwoLetterIsoCode = "NA",
	                                    ThreeLetterIsoCode = "NAM",
	                                    NumericIsoCode = 516,
	                                    SubjectToVat = false,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "Nauru",
	                                    
	                                    
	                                    TwoLetterIsoCode = "NR",
	                                    ThreeLetterIsoCode = "NRU",
	                                    NumericIsoCode = 520,
	                                    SubjectToVat = false,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "Nepal",
	                                    
	                                    
	                                    TwoLetterIsoCode = "NP",
	                                    ThreeLetterIsoCode = "NPL",
	                                    NumericIsoCode = 524,
	                                    SubjectToVat = false,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "Netherlands Antilles",
	                                    
	                                    
	                                    TwoLetterIsoCode = "AN",
	                                    ThreeLetterIsoCode = "ANT",
	                                    NumericIsoCode = 530,
	                                    SubjectToVat = false,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "New Caledonia",
	                                    
	                                    
	                                    TwoLetterIsoCode = "NC",
	                                    ThreeLetterIsoCode = "NCL",
	                                    NumericIsoCode = 540,
	                                    SubjectToVat = false,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "Nicaragua",
	                                    
	                                    
	                                    TwoLetterIsoCode = "NI",
	                                    ThreeLetterIsoCode = "NIC",
	                                    NumericIsoCode = 558,
	                                    SubjectToVat = false,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "Niger",
	                                    
	                                    
	                                    TwoLetterIsoCode = "NE",
	                                    ThreeLetterIsoCode = "NER",
	                                    NumericIsoCode = 562,
	                                    SubjectToVat = false,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "Nigeria",
	                                    
	                                    
	                                    TwoLetterIsoCode = "NG",
	                                    ThreeLetterIsoCode = "NGA",
	                                    NumericIsoCode = 566,
	                                    SubjectToVat = false,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "Niue",
	                                    
	                                    
	                                    TwoLetterIsoCode = "NU",
	                                    ThreeLetterIsoCode = "NIU",
	                                    NumericIsoCode = 570,
	                                    SubjectToVat = false,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "Norfolk Island",
	                                    
	                                    
	                                    TwoLetterIsoCode = "NF",
	                                    ThreeLetterIsoCode = "NFK",
	                                    NumericIsoCode = 574,
	                                    SubjectToVat = false,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "Northern Mariana Islands",
	                                    
	                                    
	                                    TwoLetterIsoCode = "MP",
	                                    ThreeLetterIsoCode = "MNP",
	                                    NumericIsoCode = 580,
	                                    SubjectToVat = false,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "Oman",
	                                    
	                                    
	                                    TwoLetterIsoCode = "OM",
	                                    ThreeLetterIsoCode = "OMN",
	                                    NumericIsoCode = 512,
	                                    SubjectToVat = false,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "Palau",
	                                    
	                                    
	                                    TwoLetterIsoCode = "PW",
	                                    ThreeLetterIsoCode = "PLW",
	                                    NumericIsoCode = 585,
	                                    SubjectToVat = false,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "Panama",
	                                    
	                                    
	                                    TwoLetterIsoCode = "PA",
	                                    ThreeLetterIsoCode = "PAN",
	                                    NumericIsoCode = 591,
	                                    SubjectToVat = false,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "Papua New Guinea",
	                                    
	                                    
	                                    TwoLetterIsoCode = "PG",
	                                    ThreeLetterIsoCode = "PNG",
	                                    NumericIsoCode = 598,
	                                    SubjectToVat = false,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "Pitcairn",
	                                    
	                                    
	                                    TwoLetterIsoCode = "PN",
	                                    ThreeLetterIsoCode = "PCN",
	                                    NumericIsoCode = 612,
	                                    SubjectToVat = false,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "Reunion",
	                                    
	                                    
	                                    TwoLetterIsoCode = "RE",
	                                    ThreeLetterIsoCode = "REU",
	                                    NumericIsoCode = 638,
	                                    SubjectToVat = false,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "Rwanda",
	                                    
	                                    
	                                    TwoLetterIsoCode = "RW",
	                                    ThreeLetterIsoCode = "RWA",
	                                    NumericIsoCode = 646,
	                                    SubjectToVat = false,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "Saint Kitts and Nevis",
	                                    
	                                    
	                                    TwoLetterIsoCode = "KN",
	                                    ThreeLetterIsoCode = "KNA",
	                                    NumericIsoCode = 659,
	                                    SubjectToVat = false,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "Saint Lucia",
	                                    
	                                    
	                                    TwoLetterIsoCode = "LC",
	                                    ThreeLetterIsoCode = "LCA",
	                                    NumericIsoCode = 662,
	                                    SubjectToVat = false,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "Saint Vincent and the Grenadines",
	                                    
	                                    
	                                    TwoLetterIsoCode = "VC",
	                                    ThreeLetterIsoCode = "VCT",
	                                    NumericIsoCode = 670,
	                                    SubjectToVat = false,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "Samoa",
	                                    
	                                    
	                                    TwoLetterIsoCode = "WS",
	                                    ThreeLetterIsoCode = "WSM",
	                                    NumericIsoCode = 882,
	                                    SubjectToVat = false,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "San Marino",
	                                    
	                                    
	                                    TwoLetterIsoCode = "SM",
	                                    ThreeLetterIsoCode = "SMR",
	                                    NumericIsoCode = 674,
	                                    SubjectToVat = false,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "Sao Tome and Principe",
	                                    
	                                    
	                                    TwoLetterIsoCode = "ST",
	                                    ThreeLetterIsoCode = "STP",
	                                    NumericIsoCode = 678,
	                                    SubjectToVat = false,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "Senegal",
	                                    
	                                    
	                                    TwoLetterIsoCode = "SN",
	                                    ThreeLetterIsoCode = "SEN",
	                                    NumericIsoCode = 686,
	                                    SubjectToVat = false,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "Seychelles",
	                                    
	                                    
	                                    TwoLetterIsoCode = "SC",
	                                    ThreeLetterIsoCode = "SYC",
	                                    NumericIsoCode = 690,
	                                    SubjectToVat = false,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "Sierra Leone",
	                                    
	                                    
	                                    TwoLetterIsoCode = "SL",
	                                    ThreeLetterIsoCode = "SLE",
	                                    NumericIsoCode = 694,
	                                    SubjectToVat = false,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "Solomon Islands",
	                                    
	                                    
	                                    TwoLetterIsoCode = "SB",
	                                    ThreeLetterIsoCode = "SLB",
	                                    NumericIsoCode = 90,
	                                    SubjectToVat = false,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "Somalia",
	                                    
	                                    
	                                    TwoLetterIsoCode = "SO",
	                                    ThreeLetterIsoCode = "SOM",
	                                    NumericIsoCode = 706,
	                                    SubjectToVat = false,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "South Georgia & South Sandwich Islands",
	                                    
	                                    
	                                    TwoLetterIsoCode = "GS",
	                                    ThreeLetterIsoCode = "SGS",
	                                    NumericIsoCode = 239,
	                                    SubjectToVat = false,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "Sri Lanka",
	                                    
	                                    
	                                    TwoLetterIsoCode = "LK",
	                                    ThreeLetterIsoCode = "LKA",
	                                    NumericIsoCode = 144,
	                                    SubjectToVat = false,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "St. Helena",
	                                    
	                                    
	                                    TwoLetterIsoCode = "SH",
	                                    ThreeLetterIsoCode = "SHN",
	                                    NumericIsoCode = 654,
	                                    SubjectToVat = false,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "St. Pierre and Miquelon",
	                                    
	                                    
	                                    TwoLetterIsoCode = "PM",
	                                    ThreeLetterIsoCode = "SPM",
	                                    NumericIsoCode = 666,
	                                    SubjectToVat = false,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "Sudan",
	                                    
	                                    
	                                    TwoLetterIsoCode = "SD",
	                                    ThreeLetterIsoCode = "SDN",
	                                    NumericIsoCode = 736,
	                                    SubjectToVat = false,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "Suriname",
	                                    
	                                    
	                                    TwoLetterIsoCode = "SR",
	                                    ThreeLetterIsoCode = "SUR",
	                                    NumericIsoCode = 740,
	                                    SubjectToVat = false,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "Svalbard and Jan Mayen Islands",
	                                    
	                                    
	                                    TwoLetterIsoCode = "SJ",
	                                    ThreeLetterIsoCode = "SJM",
	                                    NumericIsoCode = 744,
	                                    SubjectToVat = false,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "Swaziland",
	                                    
	                                    
	                                    TwoLetterIsoCode = "SZ",
	                                    ThreeLetterIsoCode = "SWZ",
	                                    NumericIsoCode = 748,
	                                    SubjectToVat = false,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "Syrian Arab Republic",
	                                    
	                                    
	                                    TwoLetterIsoCode = "SY",
	                                    ThreeLetterIsoCode = "SYR",
	                                    NumericIsoCode = 760,
	                                    SubjectToVat = false,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "Tajikistan",
	                                    
	                                    
	                                    TwoLetterIsoCode = "TJ",
	                                    ThreeLetterIsoCode = "TJK",
	                                    NumericIsoCode = 762,
	                                    SubjectToVat = false,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "Tanzania",
	                                    
	                                    
	                                    TwoLetterIsoCode = "TZ",
	                                    ThreeLetterIsoCode = "TZA",
	                                    NumericIsoCode = 834,
	                                    SubjectToVat = false,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "Togo",
	                                    
	                                    
	                                    TwoLetterIsoCode = "TG",
	                                    ThreeLetterIsoCode = "TGO",
	                                    NumericIsoCode = 768,
	                                    SubjectToVat = false,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "Tokelau",
	                                    
	                                    
	                                    TwoLetterIsoCode = "TK",
	                                    ThreeLetterIsoCode = "TKL",
	                                    NumericIsoCode = 772,
	                                    SubjectToVat = false,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "Tonga",
	                                    
	                                    
	                                    TwoLetterIsoCode = "TO",
	                                    ThreeLetterIsoCode = "TON",
	                                    NumericIsoCode = 776,
	                                    SubjectToVat = false,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "Trinidad and Tobago",
	                                    
	                                    
	                                    TwoLetterIsoCode = "TT",
	                                    ThreeLetterIsoCode = "TTO",
	                                    NumericIsoCode = 780,
	                                    SubjectToVat = false,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "Tunisia",
	                                    
	                                    
	                                    TwoLetterIsoCode = "TN",
	                                    ThreeLetterIsoCode = "TUN",
	                                    NumericIsoCode = 788,
	                                    SubjectToVat = false,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "Turkmenistan",
	                                    
	                                    
	                                    TwoLetterIsoCode = "TM",
	                                    ThreeLetterIsoCode = "TKM",
	                                    NumericIsoCode = 795,
	                                    SubjectToVat = false,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "Turks and Caicos Islands",
	                                    
	                                    
	                                    TwoLetterIsoCode = "TC",
	                                    ThreeLetterIsoCode = "TCA",
	                                    NumericIsoCode = 796,
	                                    SubjectToVat = false,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "Tuvalu",
	                                    
	                                    
	                                    TwoLetterIsoCode = "TV",
	                                    ThreeLetterIsoCode = "TUV",
	                                    NumericIsoCode = 798,
	                                    SubjectToVat = false,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "Uganda",
	                                    
	                                    
	                                    TwoLetterIsoCode = "UG",
	                                    ThreeLetterIsoCode = "UGA",
	                                    NumericIsoCode = 800,
	                                    SubjectToVat = false,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "Vanuatu",
	                                    
	                                    
	                                    TwoLetterIsoCode = "VU",
	                                    ThreeLetterIsoCode = "VUT",
	                                    NumericIsoCode = 548,
	                                    SubjectToVat = false,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "Vatican City State (Holy See)",
	                                    
	                                    
	                                    TwoLetterIsoCode = "VA",
	                                    ThreeLetterIsoCode = "VAT",
	                                    NumericIsoCode = 336,
	                                    SubjectToVat = false,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "Viet Nam",
	                                    
	                                    
	                                    TwoLetterIsoCode = "VN",
	                                    ThreeLetterIsoCode = "VNM",
	                                    NumericIsoCode = 704,
	                                    SubjectToVat = false,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "Virgin Islands (British)",
	                                    
	                                    
	                                    TwoLetterIsoCode = "VG",
	                                    ThreeLetterIsoCode = "VGB",
	                                    NumericIsoCode = 92,
	                                    SubjectToVat = false,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "Virgin Islands (U.S.)",
	                                    
	                                    
	                                    TwoLetterIsoCode = "VI",
	                                    ThreeLetterIsoCode = "VIR",
	                                    NumericIsoCode = 850,
	                                    SubjectToVat = false,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "Wallis and Futuna Islands",
	                                    
	                                    
	                                    TwoLetterIsoCode = "WF",
	                                    ThreeLetterIsoCode = "WLF",
	                                    NumericIsoCode = 876,
	                                    SubjectToVat = false,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "Western Sahara",
	                                    
	                                    
	                                    TwoLetterIsoCode = "EH",
	                                    ThreeLetterIsoCode = "ESH",
	                                    NumericIsoCode = 732,
	                                    SubjectToVat = false,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "Yemen",
	                                    
	                                    
	                                    TwoLetterIsoCode = "YE",
	                                    ThreeLetterIsoCode = "YEM",
	                                    NumericIsoCode = 887,
	                                    SubjectToVat = false,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "Zambia",
	                                    
	                                    
	                                    TwoLetterIsoCode = "ZM",
	                                    ThreeLetterIsoCode = "ZMB",
	                                    NumericIsoCode = 894,
	                                    SubjectToVat = false,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                    new Country
                                    {
	                                    Name = "Zimbabwe",
	                                    
	                                    
	                                    TwoLetterIsoCode = "ZW",
	                                    ThreeLetterIsoCode = "ZWE",
	                                    NumericIsoCode = 716,
	                                    SubjectToVat = false,
	                                    DisplayOrder = 100,
	                                    Published = true
                                    },
                                };
            countries.ForEach(c => _countryRepository.Insert(c));
        }

        protected virtual void InstallCustomersAndUsers(string defaultUserEmail, string defaultUserPassword)
        {
            var crAdministrators = new CustomerRole
            {
                Name = "Administrators",
                Active = true,
                IsSystemRole = true,
                SystemName = SystemCustomerRoleNames.Administrators,
            };
            var crRegistered = new CustomerRole
            {
                Name = "Registered",
                Active = true,
                IsSystemRole = true,
                SystemName = SystemCustomerRoleNames.Registered,
            };
            var crGuests = new CustomerRole
            {
                Name = "Guests",
                Active = true,
                IsSystemRole = true,
                SystemName = SystemCustomerRoleNames.Guests,
            };
            var customerRoles = new List<CustomerRole>
                                {
                                    crAdministrators,
                                    crRegistered,
                                    crGuests
                                };
            customerRoles.ForEach(cr => _customerRoleRepository.Insert(cr));

            //admin user
            var adminUser = new Customer()
            {
                CustomerGuid = Guid.NewGuid(),
                Email = defaultUserEmail,
                Username = defaultUserEmail,
                Password = defaultUserPassword,
                PasswordFormat = PasswordFormat.Clear,
                PasswordSalt = "",
                Active = true,
                CreatedOnUtc = DateTime.UtcNow,
                LastActivityDateUtc= DateTime.UtcNow,
            };
            var defaultAdminUserAddress = new Address()
            {
                FirstName = "John",
                LastName = "Smith",
                PhoneNumber = "12345678",
                Email = "admin@yourStore.com",
                FaxNumber = "",
                Company = "Nop Solutions",
                Address1 = "21 West 52nd Street",
                Address2 = "",
                City = "New York",
                StateProvince = _stateProvinceRepository.Table.FirstOrDefault(sp => sp.Name == "New York"),
                Country = _countryRepository.Table.FirstOrDefault(c => c.ThreeLetterIsoCode == "USA"),
                ZipPostalCode = "10021",
                CreatedOnUtc = DateTime.UtcNow,
            };
            adminUser.Addresses.Add(defaultAdminUserAddress);
            adminUser.CustomerRoles.Add(crAdministrators);
            adminUser.CustomerRoles.Add(crRegistered);
            _customerRepository.Insert(adminUser);
            //set default customer name
            _genericAttributeService.SaveAttribute(adminUser, SystemCustomerAttributeNames.FirstName, "John");
            _genericAttributeService.SaveAttribute(adminUser, SystemCustomerAttributeNames.LastName, "Smith");


            //search engine (crawler) built-in user
            var searchEngineUser = new Customer()
            {
                Email = "builtin@search_engine_record.com",
                CustomerGuid = Guid.NewGuid(),
                PasswordFormat = PasswordFormat.Clear,
                AdminComment = "Built-in system guest record used for requests from search engines.",
                Active = true,
                IsSystemAccount = true,
                SystemName = SystemCustomerNames.SearchEngine,
                CreatedOnUtc = DateTime.UtcNow,
                LastActivityDateUtc = DateTime.UtcNow,
            };
            searchEngineUser.CustomerRoles.Add(crGuests);
            _customerRepository.Insert(searchEngineUser);


            //built-in user for background tasks
            var backgroundTaskUser = new Customer()
            {
                Email = "builtin@background-task-record.com",
                CustomerGuid = Guid.NewGuid(),
                PasswordFormat = PasswordFormat.Clear,
                AdminComment = "Built-in system record used for background tasks.",
                Active = true,
                IsSystemAccount = true,
                SystemName = SystemCustomerNames.BackgroundTask,
                CreatedOnUtc = DateTime.UtcNow,
                LastActivityDateUtc = DateTime.UtcNow,
            };
            backgroundTaskUser.CustomerRoles.Add(crGuests);
            _customerRepository.Insert(backgroundTaskUser);
        }

        protected virtual void HashDefaultCustomerPassword(string defaultUserEmail, string defaultUserPassword)
        {
            var customerRegistrationService = EngineContext.Current.Resolve<ICustomerRegistrationService>();
            customerRegistrationService.ChangePassword(new ChangePasswordRequest(defaultUserEmail, false,
                 PasswordFormat.Hashed, defaultUserPassword));
        }

        protected virtual void InstallEmailAccounts()
        {
            var emailAccounts = new List<EmailAccount>
                               {
                                   new EmailAccount
                                       {
                                           Email = "test@mail.com",
                                           DisplayName = "Store name",
                                           Host = "smtp.mail.com",
                                           Port = 25,
                                           Username = "123",
                                           Password = "123",
                                           EnableSsl = false,
                                           UseDefaultCredentials = false
                                       },
                               };
            emailAccounts.ForEach(ea => _emailAccountRepository.Insert(ea));

        }

        protected virtual void InstallMessageTemplates()
        {
            var eaGeneral = _emailAccountRepository.Table.FirstOrDefault();
            if (eaGeneral == null)
                throw new Exception("Default email account cannot be loaded");
            var messageTemplates = new List<MessageTemplate>
                               {
                                   new MessageTemplate
                                       {
                                           Name = "Customer.EmailValidationMessage",
                                           Subject = "%Store.Name%. Email validation",
                                           Body = "<a href=\"%Store.URL%\">%Store.Name%</a>  <br />  <br />  To activate your account <a href=\"%Customer.AccountActivationURL%\">click here</a>.     <br />  <br />  %Store.Name%",
                                           IsActive = true,
                                           EmailAccountId = eaGeneral.Id,
                                       },
                                   new MessageTemplate
                                       {
                                           Name = "Customer.PasswordRecovery",
                                           Subject = "%Store.Name%. Password recovery",
                                           Body = "<a href=\"%Store.URL%\">%Store.Name%</a>  <br />  <br />  To change your password <a href=\"%Customer.PasswordRecoveryURL%\">click here</a>.     <br />  <br />  %Store.Name%",
                                           IsActive = true,
                                           EmailAccountId = eaGeneral.Id,
                                       },
                                   new MessageTemplate
                                       {
                                           Name = "Customer.WelcomeMessage",
                                           Subject = "Welcome to %Store.Name%",
                                           Body = "We welcome you to <a href=\"%Store.URL%\"> %Store.Name%</a>.<br /><br />You can now take part in the various services we have to offer you. Some of these services include:<br /><br />Permanent Cart - Any products added to your online cart remain there until you remove them, or check them out.<br />Address Book - We can now deliver your products to another address other than yours! This is perfect to send birthday gifts direct to the birthday-person themselves.<br />Order History - View your history of purchases that you have made with us.<br />Products Reviews - Share your opinions on products with our other customers.<br /><br />For help with any of our online services, please email the store-owner: <a href=\"mailto:%Store.Email%\">%Store.Email%</a>.<br /><br />Note: This email address was provided on our registration page. If you own the email and did not register on our site, please send an email to <a href=\"mailto:%Store.Email%\">%Store.Email%</a>.",
                                           IsActive = true,
                                           EmailAccountId = eaGeneral.Id,
                                       },
                                   new MessageTemplate
                                       {
                                           Name = "NewCustomer.Notification",
                                           Subject = "%Store.Name%. New customer registration",
                                           Body = "<p><a href=\"%Store.URL%\">%Store.Name%</a> <br /><br />A new customer registered with your store. Below are the customer's details:<br />Full name: %Customer.FullName%<br />Email: %Customer.Email%</p>",
                                           IsActive = true,
                                           EmailAccountId = eaGeneral.Id,
                                       },
                                   new MessageTemplate
                                       {
                                           Name = "NewReturnRequest.StoreOwnerNotification",
                                           Subject = "%Store.Name%. New return request.",
                                           Body = "<p><a href=\"%Store.URL%\">%Store.Name%</a> <br /><br />%Customer.FullName% has just submitted a new return request. Details are below:<br />Request ID: %ReturnRequest.ID%<br />Product: %ReturnRequest.Product.Quantity% x Product: %ReturnRequest.Product.Name%<br />Reason for return: %ReturnRequest.Reason%<br />Requested action: %ReturnRequest.RequestedAction%<br />Customer comments:<br />%ReturnRequest.CustomerComment%</p>",
                                           IsActive = true,
                                           EmailAccountId = eaGeneral.Id,
                                       },
                                   new MessageTemplate
                                       {
                                           Name = "News.NewsComment",
                                           Subject = "%Store.Name%. New news comment.",
                                           Body = "<p><a href=\"%Store.URL%\">%Store.Name%</a> <br /><br />A new news comment has been created for news \"%NewsComment.NewsTitle%\".</p>",
                                           IsActive = true,
                                           EmailAccountId = eaGeneral.Id,
                                       },
                                   new MessageTemplate
                                       {
                                           Name = "NewsLetterSubscription.ActivationMessage",
                                           Subject = "%Store.Name%. Subscription activation message.",
                                           Body = "<p><a href=\"%NewsLetterSubscription.ActivationUrl%\">Click here to confirm your subscription to our list.</a></p><p>If you received this email by mistake, simply delete it.</p>",
                                           IsActive = true,
                                           EmailAccountId = eaGeneral.Id,
                                       },
                                   new MessageTemplate
                                       {
                                           Name = "NewVATSubmitted.StoreOwnerNotification",
                                           Subject = "%Store.Name%. New VAT number is submitted.",
                                           Body = "<p><a href=\"%Store.URL%\">%Store.Name%</a> <br /><br />%Customer.FullName% (%Customer.Email%) has just submitted a new VAT number. Details are below:<br />VAT number: %Customer.VatNumber%<br />VAT number status: %Customer.VatNumberStatus%<br />Received name: %VatValidationResult.Name%<br />Received address: %VatValidationResult.Address%</p>",
                                           IsActive = true,
                                           EmailAccountId = eaGeneral.Id,
                                       },                                  
                                   new MessageTemplate
                                       {
                                           Name = "QuantityBelow.StoreOwnerNotification",
                                           Subject = "%Store.Name%. Quantity below notification. %Product.Name%",
                                           Body = "<p><a href=\"%Store.URL%\">%Store.Name%</a> <br /><br />%Product.Name% (ID: %Product.ID%) low quantity. <br /><br />Quantity: %Product.StockQuantity%<br /></p>",
                                           IsActive = true,
                                           EmailAccountId = eaGeneral.Id,
                                       },
                               };
            messageTemplates.ForEach(mt => _messageTemplateRepository.Insert(mt));

        }

        protected virtual void InstallTopics()
        {
            var topics = new List<Topic>
                               {
                                   new Topic
                                       {
                                           SystemName = "AboutUs",
                                           IncludeInSitemap = false,
                                           IsPasswordProtected = false,
                                           Title = "About Us",
                                           Body = "<p>Put your &quot;About Us&quot; information here. You can edit this in the admin site.</p>"
                                       },
                                   new Topic
                                       {
                                           SystemName = "ConditionsOfUse",
                                           IncludeInSitemap = false,
                                           IsPasswordProtected = false,
                                           Title = "Conditions of use",
                                           Body = "<p>Put your conditions of use information here. You can edit this in the admin site.</p>"
                                       },
                                   new Topic
                                       {
                                           SystemName = "ContactUs",
                                           IncludeInSitemap = false,
                                           IsPasswordProtected = false,
                                           Title = "",
                                           Body = "<p>Put your contact information here. You can edit this in the admin site.</p>"
                                       },
                                   new Topic
                                       {
                                           SystemName = "HomePageText",
                                           IncludeInSitemap = false,
                                           IsPasswordProtected = false,
                                           Title = "Welcome to our store",
                                           Body = "<p>Online shopping is the process consumers go through to purchase products or services over the Internet. You can edit this in the admin site.</p><p>If you have questions, see the <a href=\"http://www.nopcommerce.com/documentation.aspx\">Documentation</a>, or post in the <a href=\"http://www.nopcommerce.com/boards/\">Forums</a> at <a href=\"http://www.nopcommerce.com\">nopCommerce.com</a></p>"
                                       },
                                   new Topic
                                       {
                                           SystemName = "LoginRegistrationInfo",
                                           IncludeInSitemap = false,
                                           IsPasswordProtected = false,
                                           Title = "About login / registration",
                                           Body = "<p>Put your login / registration information here. You can edit this in the admin site.</p>"
                                       },
                                   new Topic
                                       {
                                           SystemName = "PrivacyInfo",
                                           IncludeInSitemap = false,
                                           IsPasswordProtected = false,
                                           Title = "Privacy policy",
                                           Body = "<p>Put your privacy policy information here. You can edit this in the admin site.</p>"
                                       },
                                   new Topic
                                       {
                                           SystemName = "PageNotFound",
                                           IncludeInSitemap = false,
                                           IsPasswordProtected = false,
                                           Title = "",
                                           Body = "<p><strong>The page you requested was not found, and we have a fine guess why.</strong></p><ul><li>If you typed the URL directly, please make sure the spelling is correct.</li><li>The page no longer exists. In this case, we profusely apologize for the inconvenience and for any damage this may cause.</li></ul>"
                                       },
                               };
            topics.ForEach(t => _topicRepository.Insert(t));


            //search engine names
            foreach (var topic in topics)
            {
                _urlRecordRepository.Insert(new UrlRecord()
                {
                    EntityId = topic.Id,
                    EntityName = "Topic",
                    LanguageId = 0,
                    IsActive = true,
                    Slug = topic.ValidateSeName("", !String.IsNullOrEmpty(topic.Title) ? topic.Title : topic.SystemName, true)
                });
            }

        }

        protected virtual void InstallSettings()
        {
            var settingService = EngineContext.Current.Resolve<ISettingService>();
            settingService.SaveSetting(new PdfSettings()
                {
                    LogoPictureId = 0,
                    LetterPageSizeEnabled = false,
                    RenderOrderNotes = true,
                    FontFileName = "FreeSerif.ttf",
                    InvoiceFooterTextColumn1 = null,
                    InvoiceFooterTextColumn2 = null,
                });

            settingService.SaveSetting(new CommonSettings()
                {
                    UseSystemEmailForContactUsForm = true,
                    UseStoredProceduresIfSupported = true,
                    SitemapEnabled = true,
                    SitemapIncludeTopics = true,
                    DisplayJavaScriptDisabledWarning = false,
                    UseFullTextSearch = false,
                    FullTextMode = FulltextSearchMode.ExactMatch,
                    Log404Errors = true,
                    BreadcrumbDelimiter = "/",
                    RenderXuaCompatible = false,
                    XuaCompatibleValue = "IE=edge"
                });

            settingService.SaveSetting(new SeoSettings()
                {
                    PageTitleSeparator = ". ",
                    PageTitleSeoAdjustment = PageTitleSeoAdjustment.PagenameAfterStorename,
                    DefaultTitle = "Your store",
                    DefaultMetaKeywords = "",
                    DefaultMetaDescription = "",
                    ConvertNonWesternChars = false,
                    AllowUnicodeCharsInUrls = true,
                    CanonicalUrlsEnabled = false,
                    WwwRequirement = WwwRequirement.NoMatter,
                    //we disable bundling out of the box because it requires a lot of server resources
                    EnableJsBundling = false,
                    EnableCssBundling = false,
                    TwitterMetaTags = true,
                    OpenGraphMetaTags = true,
                    ReservedUrlRecordSlugs = new List<string>()
                    {
                        "admin", 
                        "install", 
                        "recentlyviewedproducts", 
                        "newproducts",
                        "compareproducts", 
                        "clearcomparelist",
                        "setproductreviewhelpfulness",
                        "login", 
                        "register", 
                        "logout", 
                        "cart",
                        "wishlist", 
                        "emailwishlist", 
                        "checkout", 
                        "onepagecheckout", 
                        "contactus", 
                        "passwordrecovery", 
                        "subscribenewsletter",
                        "blog", 
                        "boards", 
                        "inboxupdate",
                        "sentupdate", 
                        "news", 
                        "sitemap", 
                        "search",
                        "config", 
                        "eucookielawaccept", 
                        "page-not-found"
                    },
                });

            settingService.SaveSetting(new AdminAreaSettings()
                {
                    DefaultGridPageSize = 15,
                    GridPageSizes = "10, 15, 20, 50, 100",
                });
            
            settingService.SaveSetting(new LocalizationSettings()
                {
                    DefaultAdminLanguageId = _languageRepository.Table.Single(l => l.Name == "English").Id,
                    UseImagesForLanguageSelection = false,
                    SeoFriendlyUrlsForLanguagesEnabled = false,
                    AutomaticallyDetectLanguage = false,
                    LoadAllLocaleRecordsOnStartup = true,
                    LoadAllLocalizedPropertiesOnStartup = true,
                    LoadAllUrlRecordsOnStartup = false,
                    IgnoreRtlPropertyForAdminArea = false,
                });

            settingService.SaveSetting(new CustomerSettings()
                {
                    UsernamesEnabled = false,
                    CheckUsernameAvailabilityEnabled = false,
                    AllowUsersToChangeUsernames = false,
                    DefaultPasswordFormat = PasswordFormat.Hashed,
                    HashedPasswordFormat = "SHA1",
                    PasswordMinLength = 6,
                    UserRegistrationType = UserRegistrationType.Standard,
                    AllowCustomersToUploadAvatars = false,
                    AvatarMaximumSizeBytes = 20000,
                    DefaultAvatarEnabled = true,
                    ShowCustomersLocation = false,
                    ShowCustomersJoinDate = false,
                    AllowViewingProfiles = false,
                    NotifyNewCustomerRegistration = false,
                    CustomerNameFormat = CustomerNameFormat.ShowFirstName,
                    GenderEnabled = true,
                    DateOfBirthEnabled = true,
                    CompanyEnabled = true,
                    StreetAddressEnabled = false,
                    StreetAddress2Enabled = false,
                    ZipPostalCodeEnabled = false,
                    CityEnabled = false,
                    CountryEnabled = false,
                    StateProvinceEnabled = false,
                    PhoneEnabled = false,
                    FaxEnabled = false,
                    AcceptPrivacyPolicyEnabled = false,
                    NewsletterEnabled = true,
                    NewsletterTickedByDefault = true,
                    HideNewsletterBlock = false,
                    OnlineCustomerMinutes = 20,
                    StoreLastVisitedPage = false,
                    SuffixDeletedCustomers = false,
                });

            settingService.SaveSetting(new AddressSettings()
                {
                    CompanyEnabled = true,
                    StreetAddressEnabled = true,
                    StreetAddressRequired = true,
                    StreetAddress2Enabled = true,
                    ZipPostalCodeEnabled = true,
                    ZipPostalCodeRequired = true,
                    CityEnabled = true,
                    CityRequired = true,
                    CountryEnabled = true,
                    StateProvinceEnabled = true,
                    PhoneEnabled = true,
                    PhoneRequired = true,
                    FaxEnabled = true,
                });

            settingService.SaveSetting(new MediaSettings()
                {
                    AvatarPictureSize = 85,
                    MaximumImageSize = 1280,
                    DefaultPictureZoomEnabled = false,
                    DefaultImageQuality = 80,
                    MultipleThumbDirectories = false
                });

            settingService.SaveSetting(new StoreInformationSettings()
                {
                    StoreClosed = false,
                    StoreClosedAllowForAdmins = false,
                    DefaultStoreTheme = "DefaultClean",
                    AllowCustomerToSelectTheme = true,
                    ResponsiveDesignSupported = true,
                    DisplayMiniProfilerInPublicStore = false,
                    DisplayEuCookieLawWarning = false,
                    FacebookLink = "http://www.facebook.com/nopCommerce",
                    TwitterLink = "https://twitter.com/nopCommerce",
                    YoutubeLink = "http://www.youtube.com/user/nopCommerce",
                    GooglePlusLink = "https://plus.google.com/+nopcommerce",
                });

            settingService.SaveSetting(new ExternalAuthenticationSettings()
                {
                    AutoRegisterEnabled = true,
                });

            settingService.SaveSetting(new RewardPointsSettings()
                {
                    Enabled = true,
                    ExchangeRate = 1,
                    PointsForRegistration = 0,
                    PointsForPurchases_Amount = 10,
                    PointsForPurchases_Points = 1,
                });

            settingService.SaveSetting(new MessageTemplatesSettings()
                {
                    CaseInvariantReplacement = false,
                    Color1 = "#b9babe",
                    Color2 = "#ebecee",
                    Color3 = "#dde2e6",
                });

            settingService.SaveSetting(new SecuritySettings()
                {
                    ForceSslForAllPages = false,
                    EncryptionKey = CommonHelper.GenerateRandomDigitCode(16),
                    AdminAreaAllowedIpAddresses = null,
                });

            settingService.SaveSetting(new DateTimeSettings()
                {
                    DefaultStoreTimeZoneId = "",
                    AllowCustomersToSetTimeZone = false
                });

            settingService.SaveSetting(new NewsSettings()
                {
                    Enabled = true,
                    AllowNotRegisteredUsersToLeaveComments = true,
                    NotifyAboutNewNewsComments = false,
                    ShowNewsOnMainPage = false,
                    MainPageNewsCount = 3,
                    NewsArchivePageSize = 10,
                    ShowHeaderRssUrl = false,
                });

            var eaGeneral = _emailAccountRepository.Table.FirstOrDefault();
            if (eaGeneral == null)
                throw new Exception("Default email account cannot be loaded");
            settingService.SaveSetting(new EmailAccountSettings()
                {
                    DefaultEmailAccountId = eaGeneral.Id
                });

            settingService.SaveSetting(new WidgetSettings()
                {
                    ActiveWidgetSystemNames = new List<string>() { "Widgets.NivoSlider" },
                });
        }

        protected virtual void InstallNews()
        {
            var defaultLanguage = _languageRepository.Table.FirstOrDefault();
            var news = new List<NewsItem>
                                {
                                    new NewsItem
                                        {
                                             AllowComments = true,
                                             Language = defaultLanguage,
                                             Title = "nopCommerce new release!",
                                             Short = "nopCommerce includes everything you need to begin your e-commerce online store. We have thought of everything and it's all included!<br /><br />nopCommerce is a fully customizable shopping cart. It's stable and highly usable. From downloads to documentation, www.nopCommerce.com offers a comprehensive base of information, resources, and support to the nopCommerce community.",
                                             Full = "<p>nopCommerce includes everything you need to begin your e-commerce online store. We have thought of everything and it's all included!</p><p>For full feature list go to <a href=\"http://www.nopCommerce.com\">nopCommerce.com</a></p><p>Providing outstanding custom search engine optimization, web development services and e-commerce development solutions to our clients at a fair price in a professional manner.</p>",
                                             Published  = true,
                                             CreatedOnUtc = DateTime.UtcNow,
                                        },
                                    new NewsItem
                                        {
                                             AllowComments = true,
                                             Language = defaultLanguage,
                                             Title = "New online store is open!",
                                             Short = "The new nopCommerce store is open now! We are very excited to offer our new range of products. We will be constantly adding to our range so please register on our site, this will enable you to keep up to date with any new products.",
                                             Full = "<p>Our online store is officially up and running. Stock up for the holiday season! We have a great selection of items. We will be constantly adding to our range so please register on our site, this will enable you to keep up to date with any new products.</p><p>All shipping is worldwide and will leave the same day an order is placed! Happy Shopping and spread the word!!</p>",
                                             Published  = true,
                                             CreatedOnUtc = DateTime.UtcNow.AddSeconds(1),
                                        },
                                };
            news.ForEach(n => _newsItemRepository.Insert(n));

            //search engine names
            foreach (var newsItem in news)
            {
                _urlRecordRepository.Insert(new UrlRecord()
                {
                    EntityId = newsItem.Id,
                    EntityName = "NewsItem",
                    LanguageId = newsItem.LanguageId,
                    IsActive = true,
                    Slug = newsItem.ValidateSeName("", newsItem.Title, true)
                });
            }
        }

        protected virtual void InstallPolls()
        {
            var defaultLanguage = _languageRepository.Table.FirstOrDefault();
            var poll1 = new Poll
            {
                Language = defaultLanguage,
                Name = "Do you like nopCommerce?",
                SystemKeyword = "RightColumnPoll",
                Published = true,
                DisplayOrder = 1,
            };
            poll1.PollAnswers.Add(new PollAnswer()
            {
                Name = "Excellent",
                DisplayOrder = 1,
            });
            poll1.PollAnswers.Add(new PollAnswer()
            {
                Name = "Good",
                DisplayOrder = 2,
            });
            poll1.PollAnswers.Add(new PollAnswer()
            {
                Name = "Poor",
                DisplayOrder = 3,
            });
            poll1.PollAnswers.Add(new PollAnswer()
            {
                Name = "Very bad",
                DisplayOrder = 4,
            });
            _pollRepository.Insert(poll1);
        }

        protected virtual void InstallActivityLogTypes()
        {
            var activityLogTypes = new List<ActivityLogType>()
                                      {
                                          //admin area activities
                                          new ActivityLogType
                                              {
                                                  SystemKeyword = "AddNewCustomer",
                                                  Enabled = true,
                                                  Name = "Add a new customer"
                                              },
                                          new ActivityLogType
                                              {
                                                  SystemKeyword = "AddNewCustomerRole",
                                                  Enabled = true,
                                                  Name = "Add a new customer role"
                                              },
                                          new ActivityLogType
                                              {
                                                  SystemKeyword = "AddNewSetting",
                                                  Enabled = true,
                                                  Name = "Add a new setting"
                                              },
                                          new ActivityLogType
                                              {
                                                  SystemKeyword = "AddNewWidget",
                                                  Enabled = true,
                                                  Name = "Add a new widget"
                                              },
                                          new ActivityLogType
                                              {
                                                  SystemKeyword = "DeleteCustomer",
                                                  Enabled = true,
                                                  Name = "Delete a customer"
                                              },
                                          new ActivityLogType
                                              {
                                                  SystemKeyword = "DeleteCustomerRole",
                                                  Enabled = true,
                                                  Name = "Delete a customer role"
                                              },
                                          new ActivityLogType
                                              {
                                                  SystemKeyword = "DeleteSetting",
                                                  Enabled = true,
                                                  Name = "Delete a setting"
                                              },
                                          new ActivityLogType
                                              {
                                                  SystemKeyword = "DeleteWidget",
                                                  Enabled = true,
                                                  Name = "Delete a widget"
                                              },
                                          new ActivityLogType
                                              {
                                                  SystemKeyword = "EditCustomer",
                                                  Enabled = true,
                                                  Name = "Edit a customer"
                                              },
                                          new ActivityLogType
                                              {
                                                  SystemKeyword = "EditCustomerRole",
                                                  Enabled = true,
                                                  Name = "Edit a customer role"
                                              },
                                          new ActivityLogType
                                              {
                                                  SystemKeyword = "EditSettings",
                                                  Enabled = true,
                                                  Name = "Edit setting(s)"
                                              },
                                          new ActivityLogType
                                              {
                                                  SystemKeyword = "EditWidget",
                                                  Enabled = true,
                                                  Name = "Edit a widget"
                                              },
                                              //public store activities
                                          new ActivityLogType
                                              {
                                                  SystemKeyword = "PublicStore.ContactUs",
                                                  Enabled = false,
                                                  Name = "Public store. Use contact us form"
                                              },
                                          new ActivityLogType
                                              {
                                                  SystemKeyword = "PublicStore.Login",
                                                  Enabled = false,
                                                  Name = "Public store. Login"
                                              },
                                          new ActivityLogType
                                              {
                                                  SystemKeyword = "PublicStore.Logout",
                                                  Enabled = false,
                                                  Name = "Public store. Logout"
                                              },
                                          new ActivityLogType
                                              {
                                                  SystemKeyword = "PublicStore.AddNewsComment",
                                                  Enabled = false,
                                                  Name = "Public store. Add news comment"
                                              },
                                      };
            activityLogTypes.ForEach(alt => _activityLogTypeRepository.Insert(alt));
        }

        protected virtual void InstallScheduleTasks()
        {
            var tasks = new List<ScheduleTask>()
            {
                new ScheduleTask()
                {
                    Name = "Send emails",
                    Seconds = 60,
                    Type = "Nop.Services.Messages.QueuedMessagesSendTask, Nop.Services",
                    Enabled = true,
                    StopOnError = false,
                },
                new ScheduleTask()
                {
                    Name = "Keep alive",
                    Seconds = 300,
                    Type = "Nop.Services.Common.KeepAliveTask, Nop.Services",
                    Enabled = true,
                    StopOnError = false,
                },
                new ScheduleTask()
                {
                    Name = "Delete guests",
                    Seconds = 600,
                    Type = "Nop.Services.Customers.DeleteGuestsTask, Nop.Services",
                    Enabled = true,
                    StopOnError = false,
                },
                new ScheduleTask()
                {
                    Name = "Clear cache",
                    Seconds = 600,
                    Type = "Nop.Services.Caching.ClearCacheTask, Nop.Services",
                    Enabled = false,
                    StopOnError = false,
                },
                new ScheduleTask()
                {
                    Name = "Clear log",
                    //60 minutes
                    Seconds = 3600,
                    Type = "Nop.Services.Logging.ClearLogTask, Nop.Services",
                    Enabled = false,
                    StopOnError = false,
                },
            };

            tasks.ForEach(x => _scheduleTaskRepository.Insert(x));
        }

        #endregion

        #region Methods

        public virtual void InstallData(string defaultUserEmail,
            string defaultUserPassword, bool installSampleData = true)
        {
            InstallStores();
            InstallLanguages();
            InstallCountriesAndStates();
            InstallCustomersAndUsers(defaultUserEmail, defaultUserPassword);
            InstallEmailAccounts();
            InstallMessageTemplates();
            InstallSettings();
            InstallTopics();
            InstallLocaleResources();
            InstallActivityLogTypes();
            HashDefaultCustomerPassword(defaultUserEmail, defaultUserPassword);
            InstallScheduleTasks();

            if (installSampleData)
            {
                InstallNews();
                InstallPolls();
            }
        }

        #endregion
    }
}