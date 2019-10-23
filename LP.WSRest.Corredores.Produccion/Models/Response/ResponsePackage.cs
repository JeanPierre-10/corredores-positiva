using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace LP.WSRest.Corredores.Produccion.Models.Response
{
    public class ResponsePackage
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<String> Errors { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public object Data { get; set; }
        public ResponsePackage(object result, List<String> errors)
        {
            Data = result;
            if (errors.Count == 0) { errors = null; }

            Errors = errors;
        }

    }
    public class ResponseWrappingHandler : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            //Step 1: Wait for the Response
            var response = await base.SendAsync(request, cancellationToken);

            return BuildApiResponse(request, response);
        }

        private HttpResponseMessage BuildApiResponse(HttpRequestMessage request, HttpResponseMessage response)
        {
            object content;
            List<String> modelStateErrors = new List<String>();

            //Step 2: Get the Response Content
            if (response.TryGetContentValue(out content) && !response.IsSuccessStatusCode)
            {
                HttpError error = content as HttpError;
                if (error != null)
                {
                    //Step 3: If content is an error, return nothing for the Result.
                    content = null; //We have errors, so don't return any content
                                    //Step 4: Insert the ModelState errors              
                    if (error.ModelState != null)
                    {
                        //Read as String
                        var httpErrorObject = response.Content.ReadAsStringAsync().Result;

                        //Convert to anonymous object
                        var anonymousErrorObject = new { message = "", ModelState = new Dictionary<String, String[]>() };

                        // Deserialize anonymous object
                        var deserializedErrorObject = JsonConvert.DeserializeAnonymousType(httpErrorObject, anonymousErrorObject);

                        // Get error messages from ModelState object
                        var modelStateValues = deserializedErrorObject.ModelState.Select(kvp => String.Join(", ", kvp.Value));

                        for (int i = 0; i < modelStateValues.Count(); i++)
                        {
                            modelStateErrors.Add(modelStateValues.ElementAt(i));
                        }
                    }
                }
            }
            if (response.StatusCode.Equals(System.Net.HttpStatusCode.InternalServerError)) { modelStateErrors.Add("A ocurrido un error al intentar procesar la informacion."); }
            //Step 5: Create a new response
            var newResponse = request.CreateResponse(response.StatusCode, new ResponsePackage(content, modelStateErrors));

            //Step 6: Add Back the Response Headers
            foreach (var header in response.Headers) //Add back the response headers
            {
                newResponse.Headers.Add(header.Key, header.Value);
            }

            return newResponse;
        }
    }
}