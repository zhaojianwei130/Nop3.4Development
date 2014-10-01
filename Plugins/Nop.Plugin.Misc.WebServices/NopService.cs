//Contributor: Nicolas Muniere


using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.ServiceModel.Activation;
using Nop.Core;
using Nop.Core.Domain.Customers;
using Nop.Core.Domain.Directory;
using Nop.Core.Infrastructure;
using Nop.Core.Plugins;
using Nop.Plugin.Misc.WebServices.Security;
using Nop.Services.Authentication;
using Nop.Services.Common;
using Nop.Services.Customers;
using Nop.Services.Directory;
using Nop.Services.Security;

namespace Nop.Plugin.Misc.WebServices
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
    public class NopService : INopService
    {
        #region Fields

        private readonly IAddressService _addressService;
        private readonly ICountryService _countryService;
        private readonly IStateProvinceService _stateProvinceService;
        private readonly ICustomerService _customerService;
        private readonly ICustomerRegistrationService _customerRegistrationService;
        private readonly CustomerSettings _customerSettings;
        private readonly IPermissionService _permissionSettings;
        private readonly IAuthenticationService _authenticationService;
        private readonly IWorkContext _workContext;
        private readonly IPluginFinder _pluginFinder;
        private readonly IStoreContext _storeContext;
        
        #endregion 

        #region Ctor

        public NopService()
        {
            _addressService = EngineContext.Current.Resolve<IAddressService>();
            _countryService = EngineContext.Current.Resolve<ICountryService>();
            _stateProvinceService = EngineContext.Current.Resolve<IStateProvinceService>();
            _customerService = EngineContext.Current.Resolve<ICustomerService>();
            _customerRegistrationService = EngineContext.Current.Resolve<ICustomerRegistrationService>();
            _customerSettings = EngineContext.Current.Resolve<CustomerSettings>();
            _permissionSettings = EngineContext.Current.Resolve<IPermissionService>();
            _authenticationService = EngineContext.Current.Resolve<IAuthenticationService>();
            _workContext = EngineContext.Current.Resolve<IWorkContext>();
            _pluginFinder = EngineContext.Current.Resolve<IPluginFinder>();
            _storeContext = EngineContext.Current.Resolve<IStoreContext>();
        }

        #endregion 

        #region Utilities

        protected void CheckAccess(string usernameOrEmail, string userPassword)
        {
            //check whether web service plugin is installed
            var pluginDescriptor = _pluginFinder.GetPluginDescriptorBySystemName("Misc.WebServices");
            if (pluginDescriptor == null)
                throw new ApplicationException("Web services plugin cannot be loaded");
            if (!_pluginFinder.AuthenticateStore(pluginDescriptor, _storeContext.CurrentStore.Id))
                throw new ApplicationException("Web services plugin is not available in this store");

            if (_customerRegistrationService.ValidateCustomer(usernameOrEmail, userPassword)!= CustomerLoginResults.Successful)
                    throw new ApplicationException("Not allowed");
            
            var customer = _customerSettings.UsernamesEnabled ? _customerService.GetCustomerByUsername(usernameOrEmail) : _customerService.GetCustomerByEmail(usernameOrEmail);

            _workContext.CurrentCustomer = customer;
            _authenticationService.SignIn(customer, true);

            //valdiate whether we can access this web service
            if (!_permissionSettings.Authorize(WebServicePermissionProvider.AccessWebService))
                throw new ApplicationException("Not allowed to access web service");
        }

        #endregion

        #region Orders

        public DataSet ExecuteDataSet(string[] sqlStatements, string usernameOrEmail, string userPassword)
        {
            //uncomment lines below in order to allow execute any SQL
            //CheckAccess(usernameOrEmail, userPassword);

            //if (!_permissionSettings.Authorize(StandardPermissionProvider.ManageOrders))
            //    throw new ApplicationException("Not allowed to manage orders");

            //var dataset = new DataSet();

            //var dataSettingsManager = new DataSettingsManager();
            //var dataProviderSettings = dataSettingsManager.LoadSettings();
            //using (SqlConnection connection = new SqlConnection(dataProviderSettings.DataConnectionString))
            //{
            //    foreach (var sqlStatement in sqlStatements)
            //    {
            //        DataTable dt = dataset.Tables.Add();
            //        SqlDataAdapter adapter = new SqlDataAdapter();
            //        adapter.SelectCommand = new SqlCommand(sqlStatement, connection);
            //        adapter.Fill(dt);

            //    }
            //}

            //return dataset;




            return null;
        }
        
        public Object ExecuteScalar(string sqlStatement, string usernameOrEmail, string userPassword)
        {
            //uncomment lines below in order to allow execute any SQL
            //CheckAccess(usernameOrEmail, userPassword);
            //if (!_permissionSettings.Authorize(StandardPermissionProvider.ManageOrders))
            //    throw new ApplicationException("Not allowed to manage orders");

            //Object result;
            //var dataSettingsManager = new DataSettingsManager();
            //var dataProviderSettings = dataSettingsManager.LoadSettings();
            //using (SqlConnection connection = new SqlConnection(dataProviderSettings.DataConnectionString))
            //{
            //    SqlCommand cmd = new SqlCommand(sqlStatement, connection);
            //    connection.Open();
            //    result = cmd.ExecuteScalar();

            //}

            //return result;



            return null;
        }
        
        public void ExecuteNonQuery(string sqlStatement, string usernameOrEmail, string userPassword)
        {
            //uncomment lines below in order to allow execute any SQL
            //CheckAccess(usernameOrEmail, userPassword);
            //if (!_permissionSettings.Authorize(StandardPermissionProvider.ManageOrders))
            //    throw new ApplicationException("Not allowed to manage orders");

            //var dataSettingsManager = new DataSettingsManager();
            //var dataProviderSettings = dataSettingsManager.LoadSettings();
            //using (SqlConnection connection = new SqlConnection(dataProviderSettings.DataConnectionString))
            //{
            //    SqlCommand cmd = new SqlCommand(sqlStatement, connection);
            //    connection.Open();
            //    cmd.ExecuteScalar();
            //}
        }
        
        #endregion
    }
}
