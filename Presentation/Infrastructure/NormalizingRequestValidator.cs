using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Util;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AV.Development.Web.Infrastructure
{
    public class NormalizingRequestValidator : RequestValidator
    {
        protected override bool IsValidRequestString(HttpContext context, string value, RequestValidationSource requestValidationSource, string collectionKey, out int validationFailureIndex)
        {

            var data = new StreamReader(HttpContext.Current.Request.InputStream).ReadToEnd();

            data = "{\"PageNo\":1,\"PageSize\":50,\"script\":\"<script>alert('hi')</script>\"}";

            string json = @"
                    {
                        ""routes"": [
                            {
                                ""bounds"": {
                                    ""northeast"": {
                                        ""lat"": 50.4639653,
                                        ""lng"": 30.6325177
                                    },
                                    ""southwest"": {
                                        ""lat"": 50.4599625,
                                        ""lng"": 30.6272425
                                    }
                                },
                                ""legs"": [
                                    {
                                        ""distance"": {
                                            ""text"": ""1.7 km"",
                                            ""message"": ""<script>alert('Hi gentleman')</script>"",
                                            ""value"": 1729
                                        },
                                        ""duration"": {
                                            ""text"": ""4 mins"",
                                            ""message"": ""<script>alert('Hi gentleman')</script>"",
                                            ""value"": 223
                                        }
                                    },
                                    {
                                        ""distance"": {
                                            ""text"": ""2.3 km"",
                                            ""message"": ""<script>alert('Hi gentleman')</script>"",
                                            ""value"": 2301
                                        },
                                        ""duration"": {
                                            ""text"": ""5 mins"",
                                            ""message"": ""<script>alert('Hi gentleman')</script>"",
                                            ""value"": 305
                                        }
                                    }
                                ]
                            }
                        ],
            ""TestObject"":""viniston""
                    }";
            if (data != null && data != "")
            {

                JObject jo = JObject.Parse(data);



                foreach (var token in jo)
                {

                    if (jo[token.Key].Type == JTokenType.Object)
                    {

                        formatobj(token.Value as JObject);

                    }
                    else if (jo[token.Key].Type == JTokenType.Array)
                    {
                        foreach (var child in token.Value.Children())
                        {
                            //do something with the JSON array items
                            formatobj(child as JObject);
                        }
                    }
                    else if (jo[token.Key].Type == JTokenType.String)
                    {
                        jo[token.Key] = HttpUtility.HtmlEncode(token.Value);

                    }
                    else
                    {
                        //do something with a JSON value
                    }

                }

                Console.WriteLine(jo);

            }

            return base.IsValidRequestString(context, value.Normalize(NormalizationForm.FormKC), requestValidationSource, collectionKey, out validationFailureIndex);
        }

        public void formatobj(JObject jobj)
        {
            foreach (var token in jobj)
            {

                if (jobj[token.Key].Type == JTokenType.Object)
                {
                    foreach (var pair in token.Value as JObject)
                    {
                        string name = pair.Key;
                        JToken childobj = pair.Value;
                        //do something with the JSON properties
                        FindString(pair.Value, pair.Key, token.Value as JObject);


                    }
                }
                else if (jobj[token.Key].Type == JTokenType.Array)
                {
                    foreach (var child in token.Value.Children())
                    {
                        //do something with the JSON array items

                        formatobj(child as JObject);

                    }
                }
                else if (jobj[token.Key].Type == JTokenType.String)
                {
                    jobj[token.Key] = HttpUtility.HtmlEncode(token.Value);
                }
                else
                {
                    //do something with a JSON value
                }
            }

        }

        private void FindString(JToken containerToken, string key, JObject parent)
        {

            if (containerToken.Type == JTokenType.Object)
            {
                foreach (JProperty child in containerToken.Children<JProperty>())
                {

                    FindString(child.Value, key, parent);
                }
            }

            else if (containerToken.Type == JTokenType.String)
            {
                parent[key] = HttpUtility.HtmlEncode(containerToken.ToString());

            }
            else if (containerToken.Type == JTokenType.Array)
            {
                foreach (JToken child in containerToken.Children())
                {
                    formatobj(child as JObject);
                }
            }

        }

    }



}