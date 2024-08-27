namespace MacabiDemo.NessAutomationClient
{

    using Newtonsoft.Json.Linq;
    using RestSharp;
    using System.Runtime.CompilerServices;

    internal class CalculatorGUI
    {
        private RestClient _client;
        private string sessionId = "";
        private string server;

        public CalculatorGUI()
        {
            server = "http://10.100.102.252:3000";
            try
            {
                _client = new RestClient(server);
                var request = new RestRequest(server + "/NessAutomation/steps", Method.Post);
                var requestBody = new
                {
                    sid = "",
                    application = "web_calculator",
                    browser = "chrome",
                    steps = new[]
                    {
                    new
                    {
                        page = "browser",
                        method = new
                        {
                            name = "set_application_url",
                            parameters = new[] { "http://62.0.137.141:3002/" }
                        }
                    }
                }
                };

                request.AddJsonBody(requestBody);

                // act
                var response = _client.Execute(request);

                var jsonResponse = JObject.Parse(response.Content);
                sessionId = jsonResponse["message"]["sid"].ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int FirstNumber
        {
            get
            {
                //extract value from response
                var jsonResponse = SendStep("get_first_number");
                var returnValue = jsonResponse["message"]["steps"]
                            .FirstOrDefault(step => step["method"]?.ToString() == "get_first_number")?["return_value"]
                            ?.ToString();

                return int.Parse(returnValue);
            }
            set
            {
                //extract value from response
                var jsonResponse = SendStep("set_first_number", new string[] { value.ToString() });
                var returnValue = jsonResponse["message"]["steps"]
                            .FirstOrDefault(step => step["method"]?.ToString() == "get_first_number")?["return_value"]
                            ?.ToString();
            }
        }

        public int SecondNumber
        {
            get
            {
                //extract value from response
                var jsonResponse = SendStep("get_second_number");
                var returnValue = jsonResponse["message"]["steps"]
                            .FirstOrDefault(step => step["method"]?.ToString() == "get_second_number")?["return_value"]
                            ?.ToString();

                return int.Parse(returnValue);
            }
            set
            {
                //extract value from response
                var jsonResponse = SendStep("set_second_number", new string[] { value.ToString() });
                var returnValue = jsonResponse["message"]["steps"]
                            .FirstOrDefault(step => step["method"]?.ToString() == "get_second_number")?["return_value"]
                            ?.ToString();
            }
        }

        public double Result
        {
            get
            {
                //extract value from response
                var jsonResponse = SendStep("get_result");
                var returnValue = jsonResponse["message"]["steps"]
                            .FirstOrDefault(step => step["method"]?.ToString() == "get_result")?["return_value"]
                            ?.ToString();

                return double.Parse(returnValue);
            }
        }

        public bool SelectOperation(string opertion)
        {
            if (opertion != "+" && opertion != "-" && opertion != "/" && opertion != "*")
            {
                return false;
            }
            else
            {
                //validate response
                var jsonResponse = SendStep("select_operation", new string[] { opertion });
                var status = jsonResponse["message"]["steps"]
                            .FirstOrDefault(step => step["method"]?.ToString() == "select_operation")?["status"]
                            ?.ToString();

                if (status == "pass")
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
        }

        public bool Calculate()
        {
            //validate response
            var jsonResponse = SendStep("click_calculate");
            var status = jsonResponse["message"]["steps"]
                        .FirstOrDefault(step => step["method"]?.ToString() == "click_calculate")?["status"]
                        ?.ToString();

            if (status == "pass")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool ValidateResults(double expectedResults)
        {
            //validate response
            var jsonResponse = SendStep("validate_result", new string[] { "Result: " + expectedResults });
            var status = jsonResponse["message"]["steps"]
                        .FirstOrDefault(step => step["method"]?.ToString() == "validate_result")?["status"]
                        ?.ToString();

            if (status == "pass")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void CloseSession()
        {
            try
            {
                var request = new RestRequest(server + "/NessAutomation/steps", Method.Post);
                var requestBody = new
                {
                    sid = sessionId,
                    application = "web_calculator",
                    browser = "chrome",
                    steps = new[]
                    {
                    new
                    {
                        page = "browser",
                        method = new
                        {
                            name = "close"
                        }
                    }
                }
                };

                request.AddJsonBody(requestBody);

                // act
                var response = _client.Execute(request);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private JObject SendStep(string stepName, string[] parameters = null)
        {
            try
            {
                var request = new RestRequest(server + "/NessAutomation/steps", Method.Post);
                var requestBody = new
                {
                    sid = sessionId,
                    application = "web_calculator",
                    browser = "chrome",
                    steps = new[]
                    {
                        new
                        {
                            page = "main",
                            method = new
                            {
                                name = stepName,
                                parameters
                            }
                        }
                    }
                };

                request.AddJsonBody(requestBody);

                // act
                var response = _client.Execute(request);

                //parse response
                var jsonResponse = JObject.Parse(response.Content);

                return jsonResponse;

            }
            catch (Exception ex)
            {
                JObject errorResult = new JObject
                {
                    ["message"] = new JObject
                    {
                        ["steps"] = new JArray
                {
                    new JObject
                    {
                        ["page"] = "main",
                        ["method"] = stepName,
                        ["status"] = "fail",
                        ["error"] = "An error occurred while executing the step.",
                        ["exception_message"] = ex.Message
                    }
                }
                    }
                };
                return errorResult;
            }
        }
    }
}
