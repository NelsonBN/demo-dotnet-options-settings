namespace Demo.Api.DTOs;

public record OptionsResponse
{
    public string FromConfig { get; set; }
    public string FromArrayConfig { get; set; }
    public string FromOption { get; set; }
    public string FromOptionMonitor { get; set; }
    public string FromOptionSnapshot { get; set; }
}
