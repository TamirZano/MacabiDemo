using RestSharp;
using System.Xml.Linq;

namespace MacabiDemo
{
    public class calculatorapitests
    {
        private RestClient _client;

        [SetUp]
        public void Setup()
        {
            _client = new RestClient("http://localhost:5000");
        }

        private static object[] DivideTestData =
        {
            new object[] { 10, 2, 12 },
            new object[] { -10, 2, -8 },
            new object[] { 10, -2, 8 },
        };

        [TestCase(10, 2, 12)]
        [TestCase(-10, 2, -8)]
        [TestCase(10, -2, 8)]
        public void add_validinputs_API(int num1, int num2, int expectedsum)
        {
            // arrange
            var request = new RestRequest("http://10.100.102.252:3001/sum", Method.Post);
            request.AddJsonBody(new { a = num1, b = num2 });

            // act
            var response = _client.Execute(request);

            // assert
            Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));
            Assert.That(response.Content, Is.EqualTo("{\"result\":" + expectedsum.ToString() + "}"));
        }

        [TestCase(10, 2, 8)]
        [TestCase(-10, 2, -12)]
        [TestCase(10, -2, 12)]
        public void subtract_validinputs_returnscorrectdifference(int num1, int num2, int expectedsum)
        {
            // arrange
            var request = new RestRequest("http://10.100.102.252:3001/subtract", Method.Post);
            request.AddJsonBody(new { a = num1, b = num2 });

            // act
            var response = _client.Execute(request);

            // assert
            Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));
            Assert.That(response.Content, Is.EqualTo("{\"result\":" + expectedsum.ToString() + "}"));
        }

        [TestCase(10, 2, 20)]
        [TestCase(-10, 2, -20)]
        [TestCase(10, -2, -20)]
        public void multiply_validinputs_returnscorrectproduct(int num1, int num2, int expectedsum)
        {
            // arrange
            var request = new RestRequest("http://10.100.102.252:3001/multiply", Method.Post);
            request.AddJsonBody(new { a = num1, b = num2 });

            // act
            var response = _client.Execute(request);

            // assert
            Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));
            Assert.That(response.Content, Is.EqualTo("{\"result\":" + expectedsum.ToString() + "}"));
        }

        [TestCase(10, 2, 5)]
        [TestCase(-10, 2, -5)]
        [TestCase(10, -2, -5)]
        public void divide_validinputs_returnscorrectquotient(int num1, int num2, int expectedsum)
        {
            // arrange
            var request = new RestRequest("http://10.100.102.252:3001/divide", Method.Post);
            request.AddJsonBody(new { a = num1, b = num2 });

            // act
            var response = _client.Execute(request);

            // assert
            Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));
            Assert.That(response.Content, Is.EqualTo("{\"result\":" + expectedsum.ToString() + "}"));
        }

        [TestCase(10, 0)]
        [TestCase(-10, 0)]
        public void divide_byzero_returnsbadrequest(int num1, int num2)
        {
            // arrange
            var request = new RestRequest("http://10.100.102.252:3001/divide", Method.Post);
            request.AddJsonBody(new { a = num1, b = num2 });

            // act
            var response = _client.Execute(request);

            // assert
            Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.BadRequest));
        }
    }
}