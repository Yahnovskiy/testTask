using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject.Services
{
    public class RequestData
    {
        public Dictionary<string, string> Headers { get; set; }
        public string AddToUrl { get; set; }
        public string FullEndpointPath { get; set; }
        public string BaseUrlUnsecured { get; set; }
        public string BaseUrlSecured { get; set; }
        public object JsonBody { get; set; }
        public Method Method { get; set; }
        public int UserId { get; set; }      
    }
}
