using CdktfTest.Models;
using HashiCorp.Cdktf;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace CdktfTest.Serialization;

public static class YamlTool
{
    public static Dictionary<string, RACRepositoryConfig> GetRepositories(string yamlContents)
    {
        var deserializer = new DeserializerBuilder()
            .WithNamingConvention(SnakeCaseNamingConvention.Instance)
            .WithTypeResolver(new TypeResolver())
            .WithNodeTypeResolver(new NodeResolver())
            .WithTypeInspector(t => new TypeInspector(t))
            .Build();
        return deserializer.Deserialize<Dictionary<string, RACRepositoryConfig>>(yamlContents);
    }

    /// <summary>
    /// Parses the repos yaml file
    /// </summary>
    public static Dictionary<string, RACRepositoryConfig> GetRepositoriesFromFile(string filePath)
    {
        //var a = Fn.Yamldecode(filePath);
        var yamlContents = File.ReadAllText(filePath);
        return GetRepositories(yamlContents);
    }
}

public class SnakeCaseNamingConvention : INamingConvention
{
    public static readonly INamingConvention Instance = new SnakeCaseNamingConvention();
    private INamingConvention _underScore;

    public SnakeCaseNamingConvention()
    {
        _underScore = UnderscoredNamingConvention.Instance;
    }

    public string Apply(string value) => _underScore.Apply(value).ToLower();
}