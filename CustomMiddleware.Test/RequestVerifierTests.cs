using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace CustomMiddleware.Test
{
    public class RequestVerifierTests
    {
       /// <summary>
       /// This test will test when method is not GET/POST/DELETE
       /// </summary>
       /// <returns>Should Accept the test with message method not implemented</returns>
        [Fact]
        public async Task WhenMethodNot_GETPOSTDELTE()
        {
            // Arrange
            var context = new DefaultHttpContext();
            var middleware = new RequestVerifier((innerHttpContext) =>
            {
                throw new NotImplementedException("// Some implementation goes here; it's not important for this exercise");


            });
            middleware.Requestdate = new DateTime(2019,4,1); // Date is April 1 //CHANGE THIS DATE TO ANY OTHER DATE 
            context.Request.Method = "PUT"; // REQUEST METHOD IS PUT AND NOT MENTIONED IN REQUIREMENT CHANGE THIS METHOD TO TEST OTHER
            context.Request.Path = "/api/1"; // CHANGE THIS PATH TO ANY OTHER like /Coffee/api/1
            context.Response.Body = new MemoryStream();

            await middleware.Invoke(context);

            
            context.Response.Body.Seek(0, SeekOrigin.Begin);
            var reader = new StreamReader(context.Response.Body);
            var streamText = reader.ReadToEnd();
            var objResponse = JsonConvert.DeserializeObject<object>(streamText);          
           
            Assert.Equal((int)HttpStatusCode.OK, context.Response.StatusCode); 
        }

        /// <summary>
        /// This test will test when method is GET
        /// </summary>
        /// <returns>Return Status Code 418</returns>
        [Fact]
        public async Task WhenMethodGET()
        {
            // Arrange
            var context = new DefaultHttpContext();
            var middleware = new RequestVerifier((innerHttpContext) =>
            {
                throw new NotImplementedException("// Some implementation goes here; it's not important for this exercise");


            });
            middleware.Requestdate = new DateTime(2019, 4, 1); // Passing date April 1
            context.Request.Method = "GET"; // Method GET
            context.Request.Path = "/Coffee/api/1"; // REQUEST PATH Starts with /Coffeee (case-insensitive)
            context.Response.Body = new MemoryStream();

            await middleware.Invoke(context);


            context.Response.Body.Seek(0, SeekOrigin.Begin);
            var reader = new StreamReader(context.Response.Body);
            var streamText = reader.ReadToEnd();
            var objResponse = JsonConvert.DeserializeObject<object>(streamText);
            
            Assert.Equal(418, context.Response.StatusCode);
        }

        /// <summary>
        /// This test will test when method is POST
        /// </summary>
        /// <returns>Return Status Code 418</returns>
        [Fact]
        public async Task WhenMethodPOST()
        {
            // Arrange
            var context = new DefaultHttpContext();
            var middleware = new RequestVerifier((innerHttpContext) =>
            {
                throw new NotImplementedException("// Some implementation goes here; it's not important for this exercise");


            });
            middleware.Requestdate = new DateTime(2019, 4, 1); // Passing date April 1
            context.Request.Method = "POST"; // Method POST
            context.Request.Path = "/Coffee/api/1"; // REQUEST PATH Starts with /Coffeee (case-insensitive)
            context.Response.Body = new MemoryStream();

            await middleware.Invoke(context);


            context.Response.Body.Seek(0, SeekOrigin.Begin);
            var reader = new StreamReader(context.Response.Body);
            var streamText = reader.ReadToEnd();
            var objResponse = JsonConvert.DeserializeObject<object>(streamText);

            Assert.Equal(418, context.Response.StatusCode);
        }

        /// <summary>
        /// This test will test when method is DELETE
        /// </summary>
        /// <returns>Return Status Code 418</returns>
        [Fact]
        public async Task WhenMethodDELETE()
        {
            // Arrange
            var context = new DefaultHttpContext();
            var middleware = new RequestVerifier((innerHttpContext) =>
            {
                throw new NotImplementedException("// Some implementation goes here; it's not important for this exercise");


            });
            middleware.Requestdate = new DateTime(2019, 4, 1); // Passing date April 1
            context.Request.Method = "DELETE"; // Method DELETE
            context.Request.Path = "/Coffee/api/1"; // REQUEST PATH Starts with /Coffeee (case-insensitive)
            context.Response.Body = new MemoryStream();

            await middleware.Invoke(context);


            context.Response.Body.Seek(0, SeekOrigin.Begin);
            var reader = new StreamReader(context.Response.Body);
            var streamText = reader.ReadToEnd();
            var objResponse = JsonConvert.DeserializeObject<object>(streamText);

            Assert.Equal(418, context.Response.StatusCode);
        }


    }
}
