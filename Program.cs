using HashiCorp.Cdktf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CdktfTest.Models;
using CdktfTest.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Schema;
using Newtonsoft.Json.Schema.Generation;
using Newtonsoft.Json.Serialization;

namespace CdktfTest;

internal class Program
{
    public static void Main(string[] args)
    {
        JSchemaGenerator generator = new JSchemaGenerator();

        // change contract resolver so property names are camel case
        generator.ContractResolver = new PropertyNamesContractResolver(new SnakeCaseNamingStrategy());
        generator.DefaultRequired = Required.Default;
        
        JSchema schema = generator.Generate(typeof(Dictionary<string,RACRepositoryConfig>));
        File.WriteAllText(@"schema.json", schema.ToString());

        // App app = new App();
        // new MainStack(app, "github-repo-management-team");
        // app.Synth();
    }
}
