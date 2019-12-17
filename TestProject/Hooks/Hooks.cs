using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using TestProject.Services;

namespace TestProject.Hooks
{
    [Binding]
    public class Hooks
    {
        public const string BaseUrlUnsecured = "http://jsonplaceholder.typicode.com/";
        public const string BaseUrlSecured = "https://jsonplaceholder.typicode.com/";
        private readonly RequestData requestData;

        public Hooks(RequestData requestData)
        {
            this.requestData = requestData;
        }

        [BeforeScenario]
        public void BeforeScenario1()
        {
            requestData.BaseUrlUnsecured = BaseUrlUnsecured;
            requestData.BaseUrlSecured = BaseUrlSecured;

            TestData.RemoveAll();
        }
    }
}
