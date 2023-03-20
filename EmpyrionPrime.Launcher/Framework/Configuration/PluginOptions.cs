using EmpyrionPrime.ModFramework.Configuration;
using Microsoft.Extensions.Configuration;

namespace EmpyrionPrime.Launcher.Framework.Configuration;

internal interface IPluginOptionsChangable
{
    void HandleSourceChange();
}

internal class PluginOptions<TOptions> : IPluginOptions<TOptions>, IPluginOptionsChangable where TOptions : class, new()
{
    private readonly IConfiguration _configuration;
    private readonly string _sectionName;
    private readonly TOptions _options = new();

    public TOptions Value => _options;

    public event Action? OptionsChanged;

    public PluginOptions(IConfiguration configuration, string sectionName)
    {
        _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        _sectionName = sectionName ?? throw new ArgumentNullException(nameof(sectionName));

        _configuration.Bind(_sectionName, _options);
    }

    public void HandleSourceChange()
    {
        _configuration.Bind(_sectionName, _options);
        OptionsChanged?.Invoke();
    }
}
