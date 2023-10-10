using Newtonsoft.Json;
using PowerPlantCC.Models.Request;
using PowerPlantCC.Models.Response;
using PowerPlantCC.Services;

namespace PowerPlantCC.Unit.Tests.Services
{
    public class ProductionPlanServiceTests
    {
        [Fact]
        public void CalculateProductionPlan_ForPayload3_ReturnsPayload3Response()
        {
            // Arrange
            ProductionPlanRequest productionPlanRequest = JsonConvert.DeserializeObject<ProductionPlanRequest>(ReadFromJsonExamplePayload("payload3.json"))!;            
            var ppService = new ProductionPlanService();
            ProductionPlanResponse[] productionPlanResponse = JsonConvert.DeserializeObject<ProductionPlanResponse[]>(ReadFromJsonExamplePayload("response3.json"))!;

            // Act
            var response = ppService.CalculateProductionPlan(productionPlanRequest);

            // Assert
            for(int i=0; i< response.Length; i++)
            {
                Assert.Equal(productionPlanResponse[i].Name, response[i].Name);
                Assert.Equal(productionPlanResponse[i].P, response[i].P);
            }
        }

        [Fact]
        public void CalculateProductionPlan_ForPayload1_ReturnsValidResponse()
        {
            // Arrange
            ProductionPlanRequest productionPlanRequest = JsonConvert.DeserializeObject<ProductionPlanRequest>(ReadFromJsonExamplePayload("payload1.json"))!;
            var ppService = new ProductionPlanService();
            var responseString = "[\r\n  {\r\n    \"name\": \"windpark1\",\r\n    \"p\": 90\r\n  },\r\n  {\r\n    \"name\": \"windpark2\",\r\n    \"p\": 21.6\r\n  },\r\n  {\r\n    \"name\": \"gasfiredbig1\",\r\n    \"p\": 368.4\r\n  },\r\n  {\r\n    \"name\": \"gasfiredbig2\",\r\n    \"p\": 0\r\n  },\r\n  {\r\n    \"name\": \"gasfiredsomewhatsmaller\",\r\n    \"p\": 0\r\n  },\r\n  {\r\n    \"name\": \"tj1\",\r\n    \"p\": 0\r\n  }\r\n]";
            ProductionPlanResponse[] productionPlanResponse = JsonConvert.DeserializeObject<ProductionPlanResponse[]>(responseString)!;

            // Act
            var response = ppService.CalculateProductionPlan(productionPlanRequest);

            // Assert
            for (int i = 0; i < response.Length; i++)
            {
                Assert.Equal(productionPlanResponse[i].Name, response[i].Name);
                Assert.Equal(productionPlanResponse[i].P, response[i].P);
            }
        }

        [Fact]
        public void CalculateProductionPlan_ForPayload2_ReturnsValidResponse()
        {
            // Arrange
            ProductionPlanRequest productionPlanRequest = JsonConvert.DeserializeObject<ProductionPlanRequest>(ReadFromJsonExamplePayload("payload2.json"))!;
            var ppService = new ProductionPlanService();
            var responseString = "[\r\n  {\r\n    \"name\": \"windpark1\",\r\n    \"p\": 0\r\n  },\r\n  {\r\n    \"name\": \"windpark2\",\r\n    \"p\": 0\r\n  },\r\n  {\r\n    \"name\": \"gasfiredbig1\",\r\n    \"p\": 380\r\n  },\r\n  {\r\n    \"name\": \"gasfiredbig2\",\r\n    \"p\": 100\r\n  },\r\n  {\r\n    \"name\": \"gasfiredsomewhatsmaller\",\r\n    \"p\": 0\r\n  },\r\n  {\r\n    \"name\": \"tj1\",\r\n    \"p\": 0\r\n  }\r\n]";
            ProductionPlanResponse[] productionPlanResponse = JsonConvert.DeserializeObject<ProductionPlanResponse[]>(responseString)!;

            // Act
            var response = ppService.CalculateProductionPlan(productionPlanRequest);

            // Assert
            for (int i = 0; i < response.Length; i++)
            {
                Assert.Equal(productionPlanResponse[i].Name, response[i].Name);
                Assert.Equal(productionPlanResponse[i].P, response[i].P);
            }
        }

        private string ReadFromJsonExamplePayload(string fileName)
        {
            string json = string.Empty;
            using (StreamReader r = new StreamReader($"../../../../../example_payloads/{fileName}"))
            {
                json = r.ReadToEnd();
            }
            return json;
        }
    }
}
