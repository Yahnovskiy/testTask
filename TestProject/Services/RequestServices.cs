using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject.Services
{
    public class RequestServices: BaseService
    {
        internal IRestResponse<T> GetUsers<T>(RequestData requestData) where T : new()
        {
            return ExecuteWithDesirializedData<T>(PrepareSecuredRequestData(requestData, EndpointName.Users, EndpointAction.Get));
        }

        internal IRestResponse<T> UpdatePost<T>(RequestData requestData) where T : new()
        {
            return ExecuteWithDesirializedData<T>(PrepareUnsecuredRequestData(requestData, EndpointName.Posts, EndpointAction.Edit));
        }

        internal IRestResponse<T> AddPost<T>(RequestData requestData) where T : new()
        {
            return ExecuteWithDesirializedData<T>(PrepareUnsecuredRequestData(requestData, EndpointName.Posts, EndpointAction.Add));
        }

        internal IRestResponse<T> GetPhoto<T>(RequestData requestData) where T : new()
        {
            return ExecuteWithDesirializedData<T>(PrepareUnsecuredRequestData(requestData, EndpointName.Photos, EndpointAction.Get));
        }

        internal byte[] GetImageByUrl(string url)
        {
            return RequestDownloadData(url);
        }
    }
}
