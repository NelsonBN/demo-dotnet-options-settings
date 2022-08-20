using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Demo.Api.Options;

public class ExampleOptions
{
    public string MyDetails { get; set; }
    public string MY_COUNTRY { get; set; }
}

public class ExampleOptionsSetup : IConfigureOptions<ExampleOptions>
{
    private const string PROPERTY_NAME = "MY_COUNTRY";

    public readonly IConfiguration _configuration;
    public readonly IOptions<DemoOptions> _demoOptions;

    public ExampleOptionsSetup(
        IConfiguration configuration,
        IOptions<DemoOptions> demoOptions
    )
    {
        _configuration = configuration;
        _demoOptions = demoOptions;
    }

    public void Configure(ExampleOptions options)
    {
        options.MY_COUNTRY = _configuration.GetValue<string>(PROPERTY_NAME);

        options.MyDetails = $"{_demoOptions.Value.MY_NAME} from {options.MY_COUNTRY}";
    }
}
