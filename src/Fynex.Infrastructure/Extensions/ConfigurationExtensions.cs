using Microsoft.Extensions.Configuration;

namespace Fynex.Infrastructure.Extensions;

public static class ConfigurationExtensions
{
    public static bool IsTestEnviroment(this IConfiguration configuration)
    {
        return configuration.GetValue<bool>("InMemoryTest");
    }
}
