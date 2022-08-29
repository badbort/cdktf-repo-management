using HashiCorp.Cdktf;
using Newtonsoft.Json.Schema.Generation;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CdktfTest;

internal class Program
{
    public static void Main(string[] args)
    {
        JSchemaGenerator generator = new JSchemaGenerator();

        // change contract resolver so property names are camel case
        generator.ContractResolver = new CamelCasePropertyNamesContractResolver();     


        App app = new App();
        new MainStack(app, "github-repo-management-team");
        app.Synth();
    }
}
