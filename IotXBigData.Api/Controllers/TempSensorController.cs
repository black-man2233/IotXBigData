using System.Net.Mime;
using IotXBigData.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Confluent.Kafka;
using IotXBigData.Shared;
using Newtonsoft.Json;

namespace IotXBigData.Api.Controllers;

[ApiController]
[Route("[controller]")]
[Produces(MediaTypeNames.Application.Json)]
public class TempSensorController : Controller
{
    private string _messageKey = "kevin_test";
    private string _topic = "Kevin_Zilas_Mathias";

    private ProducerConfig _prodConfig = new()
    {
        BootstrapServers = "192.168.39.58"
    };

    private ProducerBuilder<string, string> producer { get; set; }

    JsonSerializerSettings _settings = new()
    {
        ContractResolver = new LowercaseContractResolver(),
        Formatting = Formatting.Indented
    };

    public TempSensorController()
    {
        producer = new ProducerBuilder<string, string>(_prodConfig);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Update(TempDTO temp)
    {
        using (var prodBuild = producer.Build())
        {
            string message = Newtonsoft.Json.JsonConvert.SerializeObject(temp, _settings);
            var result = await prodBuild.ProduceAsync(
                topic: _topic,
                message: new Message<string, string>
                    { Key = _messageKey, Value = message, }
            );

            prodBuild.Flush(TimeSpan.FromSeconds(5));
            return Ok(result);
        }
    }
}