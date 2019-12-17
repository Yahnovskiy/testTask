using RestSharp;
using RestSharp.Extensions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TestProject.Services
{
    public class BaseService
    {
        protected IRestResponse<T> ExecuteWithDesirializedData<T>(RequestData requestData) where T : new()
        {
            var client = RestClient(requestData.FullEndpointPath);
            var request = RestJsonRequest(requestData);
            var response = client.Execute<T>(request);
            response.Data = JsonConverter.FromJson<T>(response.Content);

            return response;
        }

        protected byte[] RequestDownloadData(string url)
        {
            var client = new RestClient(url);
            var requestData = new RestRequest("GetImage", Method.GET);
            byte[] response = client.DownloadData(requestData);
            return response;
        }

        private RestClient RestClient(string fullEndpointPath)
        {
            return new RestClient(fullEndpointPath);
        }

        private IRestRequest RestJsonRequest(RequestData requestData)
        {
            var request = new RestRequest(requestData.Method);
            request.RequestFormat = DataFormat.Json;
            if (requestData.Headers != null)
            {
                foreach (var header in requestData.Headers)
                {
                    request.AddHeader(header.Key, header.Value);
                }
            }
            if (requestData.JsonBody != null)
            {
                request.AddJsonBody(requestData.JsonBody);
            }

            return request;
        }

        protected RequestData PrepareUnsecuredRequestData(RequestData requestData, EndpointName endpointName, EndpointAction endpointAction)
        {
            var endPoint = new Endpoint(endpointName, endpointAction);
            endPoint.BaseUrl = requestData.BaseUrlUnsecured;
            if (requestData.AddToUrl != null)
            {
                requestData.FullEndpointPath = endPoint.Url + "/" + requestData.AddToUrl;
            }
            else
            {
                requestData.FullEndpointPath = endPoint.Url;
            }
            requestData.Method = endPoint.Method;
            return requestData;
        }


        protected RequestData PrepareSecuredRequestData(RequestData requestData, EndpointName endpointName, EndpointAction endpointAction)
        {
            var endPoint = new Endpoint(endpointName, endpointAction);
            endPoint.BaseUrl = requestData.BaseUrlSecured;
            if (requestData.AddToUrl != null)
            {
                requestData.FullEndpointPath = endPoint.Url + "/" + requestData.AddToUrl;
            }
            else
            {
                requestData.FullEndpointPath = endPoint.Url;
            }
            requestData.Method = endPoint.Method;
            return requestData;
        }
    }
}
