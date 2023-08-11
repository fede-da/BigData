using FrontEndServer.Server.Data.Service.RabbitMq;
using FrontEndServer.Server.Data.Services.GuidGenerator;
using FrontEndServer.Shared;
using FrontEndServer.Shared.Dtos;
using Microsoft.AspNetCore.Mvc;

[Controller]
[Route(ApiConstants.RabbitMqControllerConstants.BaseUrl)]
public class RabbitMqController : Controller
{
    private readonly IRabbitMqService _rabbitMqService;
    private readonly ILogger<RabbitMqController> _logger;
    private readonly IGuidGenerator _guidGenerator;
    public RabbitMqController(IRabbitMqService rabbitMqService,IGuidGenerator generator ,ILogger<RabbitMqController> logger)
    {
        _rabbitMqService = rabbitMqService;
        _logger = logger;
        _guidGenerator = generator;
    }

    [HttpPost]
    [Route(ApiConstants.RabbitMqControllerConstants.Send)]
    public IActionResult SendMessage([FromBody] MessageDTO dto)
    {
        Console.WriteLine($"Received message on RabbitMqController: {dto.Message}");
        string _tmpGuid = _guidGenerator.ReadValidateAndSetGuidCookie(Response.Cookies);
        _logger.LogInformation($"Generated Guid: {_tmpGuid}");
        dto.Guid = _tmpGuid;
        _rabbitMqService.SendMessage(dto);
        return Ok();
    }

}