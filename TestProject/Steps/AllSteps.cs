using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using TestProject.Models;
using TestProject.Services;

namespace TestProject.Steps
{
    [Binding]
    class AllSteps
    {
        private readonly RequestData requestData;
        private IRestResponse<List<UserData>> userDataResponse;
        private IRestResponse<PostData> postDataResponse;
        private IRestResponse<PhotoData> photoDataResponse;
        private ExpectedPostData expectedPostData;
        UserData anyUser;
        private RequestServices requestServices;

        public AllSteps(RequestData requestData)
        {
            requestServices = new RequestServices();
            expectedPostData = new ExpectedPostData();
            this.requestData = requestData;
        }

        [Given(@"I add post with parameters: userId '(.*)', title '(.*)', body '(.*)'")]
        public void GivenIAddPostWithParametersUserIdTitleBody(string userId, string title, string body)
        {
            var usersList = GetUserDataById(userId);
            if (userId == "Any")
            {
                var random = new Random();
                int index = random.Next(usersList.Count);
                anyUser = usersList[index];
            }
            else
            {
                anyUser = usersList.Find(x => x.Id.ToString() == userId);
            }
            var jsonData = new AddPostJsonData();
            jsonData.UserId = anyUser.Id;
            jsonData.Title = title;
            jsonData.Body = body;

            requestData.JsonBody = jsonData;
            postDataResponse = requestServices.AddPost<PostData>(requestData);

            expectedPostData.UserId = anyUser.Id;
            expectedPostData.Title = title;
            expectedPostData.Body = body;
            TestData.Add(TestData.ExpectedPostData, expectedPostData);
            TestData.Add(TestData.PostDataResponse, postDataResponse);
        }

        [Given(@"Get user data by id '(.*)'")]
        public List<UserData> GetUserDataById(string userId)
        {          
            userDataResponse = requestServices.GetUsers<List<UserData>>(requestData);
            return userDataResponse.Data;
        }

        [Given(@"I compare expected post data with created data")]
        [Then(@"I compare expected post data with created data")]

        public void ThenICompareInputDataWithResponse()
        {
            var expectedData = TestData.Get<ExpectedPostData>(TestData.ExpectedPostData);
            var actualData = TestData.Get<RestResponse<PostData>>(TestData.PostDataResponse);

            Assert.AreEqual(expectedData.UserId, actualData.Data.UserId, "User id is incorrect");
            Assert.AreEqual(expectedData.Title, actualData.Data.Title, "Title is incorrect");
            Assert.AreEqual(expectedData.Body, actualData.Data.Body, "Body is incorrect");
        }

        [Given(@"I check status '(.*)' when post is added")]
        [Then(@"I check status '(.*)' when post is added")]
        public void ThenICheckStatus(string status)
        {
            var actualData = TestData.Get<RestResponse<PostData>>(TestData.PostDataResponse);
            Assert.AreEqual(actualData.StatusCode.ToString(), status, "Create response status is wrong");
        }

        [When(@"I update title '(.*)', body '(.*)' for post")]
        public void WhenIUpdateTitleBodyForPost(string title, string body)
        {
            var createdPost = TestData.Get<RestResponse<PostData>>(TestData.PostDataResponse);
            requestData.AddToUrl = createdPost.Data.Id;
            var jsonData = new AddPostJsonData();
            jsonData.UserId = createdPost.Data.UserId;
            jsonData.Title = title;
            jsonData.Body = body;
            requestData.JsonBody = jsonData;
            
            postDataResponse = requestServices.UpdatePost<PostData>(requestData);

            expectedPostData.Title = title;
            expectedPostData.Body = body;
            TestData.Add(TestData.UpdatedPost, postDataResponse);
            TestData.Add(TestData.ExpectedPostData, expectedPostData);
        }

        [Then(@"I check updated post data")]
        public void ThenICheckUpdatedPostData()
        {
            var expectedData = TestData.Get<ExpectedPostData>(TestData.ExpectedPostData);
            var actualData = TestData.Get<RestResponse<PostData>>(TestData.UpdatedPost);

            Assert.AreEqual(expectedData.UserId, actualData.Data.UserId, "User id is incorrect");
            Assert.AreEqual(expectedData.Title, actualData.Data.Title, "Title is incorrect");
            Assert.AreEqual(expectedData.Body, actualData.Data.Body, "Body is incorrect");
        }

        [Then(@"I check status '(.*)' when post is updated")]
        public void ThenICheckStatusWhenPostIsUpdated(string status)
        {
            var actualData = TestData.Get<RestResponse<PostData>>(TestData.UpdatedPost);
            Assert.AreEqual(status, actualData.StatusCode.ToString(), "Update response status is wrong");
        }

        [Given(@"Get photo by id '(.*)'")]
        public void GivenGetPhotoById(string idPhoto)
        {
            requestData.AddToUrl = idPhoto;
            photoDataResponse = requestServices.GetPhoto<PhotoData>(requestData);

            TestData.Add(TestData.PhotoData, photoDataResponse);
        }

        [Then(@"Check if image of photo with id '(.*)' isn't corrupted\.")]
        public void ThenCheckIfImageOfPhotoWithIdIsnTCorrupted_(string idPhoto)
        {
            var photolData = TestData.Get<RestResponse<PhotoData>>(TestData.PhotoData);
            Assert.IsTrue(photolData.Data.Id == idPhoto, "Id photo or image is different as expected");

            byte[]  actualImage = requestServices.GetImageByUrl(photolData.Data.Url);
            byte[] expectedlImage = ImageToByte(Resources.Resource.d32776);

            Assert.IsTrue(expectedlImage == actualImage, $"Actual image bytes: {actualImage.Length.ToString()} is different as expected image bytes: {expectedlImage.Length.ToString()}");
        }

        public static byte[] ImageToByte(Image img)
        {
            ImageConverter converter = new ImageConverter();
            return (byte[])converter.ConvertTo(img, typeof(byte[]));
        }
    }
}
