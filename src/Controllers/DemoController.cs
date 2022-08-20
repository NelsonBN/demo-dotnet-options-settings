using System.Threading;
using Demo.Api.DTOs;
using Demo.Api.Options;
using Demo.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Demo.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class DemoController : ControllerBase
{
    private readonly ILogger<DemoController> _logger;

    private readonly IConfiguration _configuration;
    private readonly DemoOptions _options1;
    private readonly DemoOptions _optionsMonitor1;
    private readonly DemoOptions _optionsSnapshot1;

    private readonly IOptions<DemoOptions> _options2;
    private readonly IOptionsMonitor<DemoOptions> _optionsMonitor2;
    private readonly IOptionsSnapshot<DemoOptions> _optionsSnapshot2;

    private readonly IDemo11Service _service11;
    private readonly IDemo12Service _service12;

    private readonly IDemo21Service _service21;
    private readonly IDemo22Service _service22;

    public DemoController(
        ILogger<DemoController> logger,

        IConfiguration configuration,
        IOptions<DemoOptions> options,
        IOptionsMonitor<DemoOptions> optionsMonitor,
        IOptionsSnapshot<DemoOptions> optionsSnapshot,

        IDemo11Service service11,
        IDemo12Service service12,

        IDemo21Service service21,
        IDemo22Service service22
    )
    {
        _logger = logger;

        _configuration = configuration;

        _options1 = options.Value;
        _optionsMonitor1 = optionsMonitor.CurrentValue;
        _optionsSnapshot1 = optionsSnapshot.Value;

        _options2 = options;
        _optionsMonitor2 = optionsMonitor;
        _optionsSnapshot2 = optionsSnapshot;

        _service11 = service11;
        _service12 = service12;

        _service21 = service21;
        _service22 = service22;

        _optionsMonitor2.OnChange((newValue)
            => _logger.LogInformation($"[CONTROLLER][OPTIONS MONITOR][ON CHANGE][1] {newValue.MY_NAME}")
        );
    }

    [HttpGet]
    public IActionResult Get()
    {
        var response1 = new OptionsResponse
        {
            FromConfig = _configuration.GetValue<string>("MY_NAME"),
            FromArrayConfig = _configuration["MY_NAME"].ToString(),
            FromOption = _options1.MY_NAME,
            FromOptionMonitor = _optionsMonitor1.MY_NAME,
            FromOptionSnapshot = _optionsSnapshot1.MY_NAME
        };

        _logger.LogInformation($"[CONTROLLER][1] {response1}");


        _service11.Run();
        _service21.Run();

        Thread.Sleep(5000);

        _service12.Run();
        _service22.Run();

        var response2 = new OptionsResponse
        {
            FromConfig = _configuration.GetValue<string>("MY_NAME"),
            FromArrayConfig = _configuration["MY_NAME"].ToString(),
            FromOption = _options2.Value.MY_NAME,
            FromOptionMonitor = _optionsMonitor2.CurrentValue.MY_NAME,
            FromOptionSnapshot = _optionsSnapshot2.Value.MY_NAME
        };
        _logger.LogInformation($"[CONTROLLER][2] {response2}");

        return Ok(new
        {
            response1,
            response2
        });
    }
}
