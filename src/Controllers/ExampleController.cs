using Demo.Api.Options;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Demo.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ExampleController : ControllerBase
{
    private readonly IOptions<ExampleOptions> _options;

    public ExampleController(IOptions<ExampleOptions> options)
        => _options = options;

    [HttpGet]
    public IActionResult Get()
        => Ok(new
        {
            _options.Value.MY_COUNTRY,
            _options.Value.MyDetails,
        });
}
