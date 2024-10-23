using Microsoft.Extensions.Configuration;

namespace App.Services.BlockChainExplorer.Options;

public class BlockChainExplorerProvider
{
    public const string SETTING_NAME = nameof(BlockChainExplorer);

    public static BlockChainExplorerConfig Get(IConfiguration configuration) {
        var section = configuration.GetSection(SETTING_NAME);
        var options = section.Get<BlockChainExplorerConfig>();
        try
        {
            var val = GetByEnv(configuration);
        }
        catch (Exception e)
        {
            
        }
        return options;
    }
    public static BlockChainExplorerConfig GetByEnv(IConfiguration configuration)
    {
        var vars = Environment.GetEnvironmentVariables();
        var setting = vars[SETTING_NAME];
        var section = configuration.GetSection(SETTING_NAME);
        var options = section.Get<BlockChainExplorerConfig>();
        return options;
    }
}