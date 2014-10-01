using AutoMapper;
using Nop.Admin.Models.Cms;
using Nop.Admin.Models.Common;
using Nop.Admin.Models.Customers;
using Nop.Admin.Models.Directory;
using Nop.Admin.Models.ExternalAuthentication;
using Nop.Admin.Models.Localization;
using Nop.Admin.Models.Logging;
using Nop.Admin.Models.Messages;
using Nop.Admin.Models.News;
using Nop.Admin.Models.Plugins;
using Nop.Admin.Models.Polls;
using Nop.Admin.Models.Settings;
using Nop.Admin.Models.Stores;
using Nop.Admin.Models.Topics;
using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Common;
using Nop.Core.Domain.Customers;
using Nop.Core.Domain.Directory;
using Nop.Core.Domain.Localization;
using Nop.Core.Domain.Logging;
using Nop.Core.Domain.Media;
using Nop.Core.Domain.Messages;
using Nop.Core.Domain.News;
using Nop.Core.Domain.Polls;
using Nop.Core.Domain.Stores;
using Nop.Core.Domain.Topics;
using Nop.Core.Plugins;
using Nop.Services.Authentication.External;
using Nop.Services.Cms;

namespace Nop.Admin
{
    public static class MappingExtensions
    {
        #region Customer attributes

        //attributes
        public static CustomerAttributeModel ToModel(this CustomerAttribute entity)
        {
            return Mapper.Map<CustomerAttribute, CustomerAttributeModel>(entity);
        }

        public static CustomerAttribute ToEntity(this CustomerAttributeModel model)
        {
            return Mapper.Map<CustomerAttributeModel, CustomerAttribute>(model);
        }

        public static CustomerAttribute ToEntity(this CustomerAttributeModel model, CustomerAttribute destination)
        {
            return Mapper.Map(model, destination);
        }

        #endregion

        #region Languages

        public static LanguageModel ToModel(this Language entity)
        {
            return Mapper.Map<Language, LanguageModel>(entity);
        }

        public static Language ToEntity(this LanguageModel model)
        {
            return Mapper.Map<LanguageModel, Language>(model);
        }

        public static Language ToEntity(this LanguageModel model, Language destination)
        {
            return Mapper.Map(model, destination);
        }
        
        #endregion

        #region Email account

        public static EmailAccountModel ToModel(this EmailAccount entity)
        {
            return Mapper.Map<EmailAccount, EmailAccountModel>(entity);
        }

        public static EmailAccount ToEntity(this EmailAccountModel model)
        {
            return Mapper.Map<EmailAccountModel, EmailAccount>(model);
        }

        public static EmailAccount ToEntity(this EmailAccountModel model, EmailAccount destination)
        {
            return Mapper.Map(model, destination);
        }

        #endregion

        #region Message templates

        public static MessageTemplateModel ToModel(this MessageTemplate entity)
        {
            return Mapper.Map<MessageTemplate, MessageTemplateModel>(entity);
        }

        public static MessageTemplate ToEntity(this MessageTemplateModel model)
        {
            return Mapper.Map<MessageTemplateModel, MessageTemplate>(model);
        }

        public static MessageTemplate ToEntity(this MessageTemplateModel model, MessageTemplate destination)
        {
            return Mapper.Map(model, destination);
        }

        #endregion

        #region Queued email

        public static QueuedEmailModel ToModel(this QueuedEmail entity)
        {
            return Mapper.Map<QueuedEmail, QueuedEmailModel>(entity);
        }

        public static QueuedEmail ToEntity(this QueuedEmailModel model)
        {
            return Mapper.Map<QueuedEmailModel, QueuedEmail>(model);
        }

        public static QueuedEmail ToEntity(this QueuedEmailModel model, QueuedEmail destination)
        {
            return Mapper.Map(model, destination);
        }

        #endregion

        #region Topics

        public static TopicModel ToModel(this Topic entity)
        {
            return Mapper.Map<Topic, TopicModel>(entity);
        }

        public static Topic ToEntity(this TopicModel model)
        {
            return Mapper.Map<TopicModel, Topic>(model);
        }

        public static Topic ToEntity(this TopicModel model, Topic destination)
        {
            return Mapper.Map(model, destination);
        }

        #endregion

        #region Log

        public static LogModel ToModel(this Log entity)
        {
            return Mapper.Map<Log, LogModel>(entity);
        }

        public static Log ToEntity(this LogModel model)
        {
            return Mapper.Map<LogModel, Log>(model);
        }

        public static Log ToEntity(this LogModel model, Log destination)
        {
            return Mapper.Map(model, destination);
        }

        public static ActivityLogTypeModel ToModel(this ActivityLogType entity)
        {
            return Mapper.Map<ActivityLogType, ActivityLogTypeModel>(entity);
        }

        public static ActivityLogModel ToModel(this ActivityLog entity)
        {
            return Mapper.Map<ActivityLog, ActivityLogModel>(entity);
        }

        #endregion

        #region External authentication methods

        public static AuthenticationMethodModel ToModel(this IExternalAuthenticationMethod entity)
        {
            return Mapper.Map<IExternalAuthenticationMethod, AuthenticationMethodModel>(entity);
        }

        #endregion
        
        #region Widgets

        public static WidgetModel ToModel(this IWidgetPlugin entity)
        {
            return Mapper.Map<IWidgetPlugin, WidgetModel>(entity);
        }

        #endregion

        #region Address

        public static AddressModel ToModel(this Address entity)
        {
            return Mapper.Map<Address, AddressModel>(entity);
        }

        public static Address ToEntity(this AddressModel model)
        {
            return Mapper.Map<AddressModel, Address>(model);
        }

        public static Address ToEntity(this AddressModel model, Address destination)
        {
            return Mapper.Map(model, destination);
        }

        #endregion

        #region NewsLetter subscriptions

        public static NewsLetterSubscriptionModel ToModel(this NewsLetterSubscription entity)
        {
            return Mapper.Map<NewsLetterSubscription, NewsLetterSubscriptionModel>(entity);
        }

        public static NewsLetterSubscription ToEntity(this NewsLetterSubscriptionModel model)
        {
            return Mapper.Map<NewsLetterSubscriptionModel, NewsLetterSubscription>(model);
        }

        public static NewsLetterSubscription ToEntity(this NewsLetterSubscriptionModel model, NewsLetterSubscription destination)
        {
            return Mapper.Map(model, destination);
        }

        #endregion

        #region News

        //news items
        public static NewsItemModel ToModel(this NewsItem entity)
        {
            return Mapper.Map<NewsItem, NewsItemModel>(entity);
        }

        public static NewsItem ToEntity(this NewsItemModel model)
        {
            return Mapper.Map<NewsItemModel, NewsItem>(model);
        }

        public static NewsItem ToEntity(this NewsItemModel model, NewsItem destination)
        {
            return Mapper.Map(model, destination);
        }

        #endregion

        #region Polls

        //news items
        public static PollModel ToModel(this Poll entity)
        {
            return Mapper.Map<Poll, PollModel>(entity);
        }

        public static Poll ToEntity(this PollModel model)
        {
            return Mapper.Map<PollModel, Poll>(model);
        }

        public static Poll ToEntity(this PollModel model, Poll destination)
        {
            return Mapper.Map(model, destination);
        }

        #endregion

        #region Customers/users/customer roles
        //customer roles
        public static CustomerRoleModel ToModel(this CustomerRole entity)
        {
            return Mapper.Map<CustomerRole, CustomerRoleModel>(entity);
        }

        public static CustomerRole ToEntity(this CustomerRoleModel model)
        {
            return Mapper.Map<CustomerRoleModel, CustomerRole>(model);
        }

        public static CustomerRole ToEntity(this CustomerRoleModel model, CustomerRole destination)
        {
            return Mapper.Map(model, destination);
        }

        #endregion

        #region Countries / states

        public static CountryModel ToModel(this Country entity)
        {
            return Mapper.Map<Country, CountryModel>(entity);
        }

        public static Country ToEntity(this CountryModel model)
        {
            return Mapper.Map<CountryModel, Country>(model);
        }

        public static Country ToEntity(this CountryModel model, Country destination)
        {
            return Mapper.Map(model, destination);
        }

        public static StateProvinceModel ToModel(this StateProvince entity)
        {
            return Mapper.Map<StateProvince, StateProvinceModel>(entity);
        }

        public static StateProvince ToEntity(this StateProvinceModel model)
        {
            return Mapper.Map<StateProvinceModel, StateProvince>(model);
        }

        public static StateProvince ToEntity(this StateProvinceModel model, StateProvince destination)
        {
            return Mapper.Map(model, destination);
        }


        #endregion

        #region Settings


        public static NewsSettingsModel ToModel(this NewsSettings entity)
        {
            return Mapper.Map<NewsSettings, NewsSettingsModel>(entity);
        }
        public static NewsSettings ToEntity(this NewsSettingsModel model)
        {
            return Mapper.Map<NewsSettingsModel, NewsSettings>(model);
        }
        public static NewsSettings ToEntity(this NewsSettingsModel model, NewsSettings destination)
        {
            return Mapper.Map(model, destination);
        }



        public static RewardPointsSettingsModel ToModel(this RewardPointsSettings entity)
        {
            return Mapper.Map<RewardPointsSettings, RewardPointsSettingsModel>(entity);
        }
        public static RewardPointsSettings ToEntity(this RewardPointsSettingsModel model)
        {
            return Mapper.Map<RewardPointsSettingsModel, RewardPointsSettings>(model);
        }
        public static RewardPointsSettings ToEntity(this RewardPointsSettingsModel model, RewardPointsSettings destination)
        {
            return Mapper.Map(model, destination);
        }




        public static MediaSettingsModel ToModel(this MediaSettings entity)
        {
            return Mapper.Map<MediaSettings, MediaSettingsModel>(entity);
        }
        public static MediaSettings ToEntity(this MediaSettingsModel model)
        {
            return Mapper.Map<MediaSettingsModel, MediaSettings>(model);
        }
        public static MediaSettings ToEntity(this MediaSettingsModel model, MediaSettings destination)
        {
            return Mapper.Map(model, destination);
        }

        //customer/user settings
        public static CustomerUserSettingsModel.CustomerSettingsModel ToModel(this CustomerSettings entity)
        {
            return Mapper.Map<CustomerSettings, CustomerUserSettingsModel.CustomerSettingsModel>(entity);
        }
        public static CustomerSettings ToEntity(this CustomerUserSettingsModel.CustomerSettingsModel model)
        {
            return Mapper.Map<CustomerUserSettingsModel.CustomerSettingsModel, CustomerSettings>(model);
        }
        public static CustomerSettings ToEntity(this CustomerUserSettingsModel.CustomerSettingsModel model, CustomerSettings destination)
        {
            return Mapper.Map(model, destination);
        }
        public static CustomerUserSettingsModel.AddressSettingsModel ToModel(this AddressSettings entity)
        {
            return Mapper.Map<AddressSettings, CustomerUserSettingsModel.AddressSettingsModel>(entity);
        }
        public static AddressSettings ToEntity(this CustomerUserSettingsModel.AddressSettingsModel model)
        {
            return Mapper.Map<CustomerUserSettingsModel.AddressSettingsModel, AddressSettings>(model);
        }
        public static AddressSettings ToEntity(this CustomerUserSettingsModel.AddressSettingsModel model, AddressSettings destination)
        {
            return Mapper.Map(model, destination);
        }
        #endregion

        #region Plugins

        public static PluginModel ToModel(this PluginDescriptor entity)
        {
            return Mapper.Map<PluginDescriptor, PluginModel>(entity);
        }

        #endregion

        #region Stores

        public static StoreModel ToModel(this Store entity)
        {
            return Mapper.Map<Store, StoreModel>(entity);
        }

        public static Store ToEntity(this StoreModel model)
        {
            return Mapper.Map<StoreModel, Store>(model);
        }

        public static Store ToEntity(this StoreModel model, Store destination)
        {
            return Mapper.Map(model, destination);
        }

        #endregion

        #region Templates

        #endregion

    }
}