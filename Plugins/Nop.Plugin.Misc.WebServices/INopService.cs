//Contributor: Nicolas Muniere

using System.Collections.Generic;
using System.Data;
using System.ServiceModel;

namespace Nop.Plugin.Misc.WebServices
{

    [ServiceContract]
    public interface INopService
    {
        [OperationContract]
        DataSet ExecuteDataSet(string[] sqlStatements, string usernameOrEmail, string userPassword);
        [OperationContract]
        void ExecuteNonQuery(string sqlStatement, string usernameOrEmail, string userPassword);
        [OperationContract]
        object ExecuteScalar(string sqlStatement, string usernameOrEmail, string userPassword);
    }
}
