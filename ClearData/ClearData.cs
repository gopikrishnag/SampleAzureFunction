using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.IO;
using System.Threading.Tasks;
using ClearData.Service;

namespace ClearRegistrationData
{
    public class ClearData
    {
        private readonly IDatabaseService _databaseService;

        public ClearData(IDatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        [FunctionName("ClearRegData")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)]
            HttpRequest req,
            ILogger log)
        {
            log.LogInformation("User data clean-up request has raised");

            var result = 0;
            string email = req.Query["email"];

            var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            email = email ?? data?.email;

          
            if (email != null && email.IndexOf('@') > 2)
            {
                result = await _databaseService.CleanDatabaseTables(email);
            }
            else
            {
                log.LogInformation("Email is not supplied or not valid - {email} ");
                return new BadRequestObjectResult("Please pass a email on the query string or in the request body");
            }

            if (result != 1) return new BadRequestObjectResult("Something not correct");
            log.LogInformation($"Successfully cleaned the records from repo, email - {email} ");
            return new OkObjectResult($"Successfully has cleaned the records for email -, {email} ");

        }
    }

}
 
