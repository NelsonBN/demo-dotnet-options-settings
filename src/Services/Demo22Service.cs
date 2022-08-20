using Demo.Api.Controllers;
using Demo.Api.DTOs;
using Demo.Api.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Demo.Api.Services;

public interface IDemo22Service
{
    void Run();
}

public class Demo22Service : IDemo22Service
{
    private readonly ILogger<DemoController> _logger;

    private readonly IConfiguration _configuration;

    private readonly DemoOptions _options1;
    private readonly DemoOptions _optionsMonitor1;
    private readonly DemoOptions _optionsSnapshot1;

    private readonly IOptions<DemoOptions> _options2;
    private readonly IOptionsMonitor<DemoOptions> _optionsMonitor2;
    private readonly IOptionsSnapshot<DemoOptions> _optionsSnapshot2;

    public Demo22Service(
        ILogger<DemoController> logger,
        IConfiguration configuration,
        IOptions<DemoOptions> options,
        IOptionsMonitor<DemoOptions> optionsMonitor,
        IOptionsSnapshot<DemoOptions> optionsSnapshot
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
    }

    public void Run()
    {
        var response1 = new OptionsResponse
        {
            FromConfig = _configuration.GetValue<string>("MY_NAME"),
            FromArrayConfig = _configuration["MY_NAME"].ToString(),
            FromOption = _options1.MY_NAME,
            FromOptionMonitor = _optionsMonitor1.MY_NAME,
            FromOptionSnapshot = _optionsSnapshot1.MY_NAME
        };
        _logger.LogInformation($"[SERVICE 22][1] {response1}");


        var response2 = new OptionsResponse
        {
            FromConfig = _configuration.GetValue<string>("MY_NAME"),
            FromArrayConfig = _configuration["MY_NAME"].ToString(),
            FromOption = _options2.Value.MY_NAME,
            FromOptionMonitor = _optionsMonitor2.CurrentValue.MY_NAME,
            FromOptionSnapshot = _optionsSnapshot2.Value.MY_NAME
        };
        _logger.LogInformation($"[SERVICE 22][2] {response2}");
    }
}
