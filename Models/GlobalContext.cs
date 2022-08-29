using Constructs;
using HashiCorp.Cdktf.Providers.Azuread;
using HashiCorp.Cdktf.Providers.Github;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CdktfTest.Models
{
    public class GlobalContext
    {
        /// <summary>
        /// Gets access to all teams
        /// </summary>
        public Dictionary<string, DataGithubTeam> DataGithubTeams { get; set; }

        public DataAzureadClientConfig DataAdClientConfig { get; set; }

        public DataAzureadGroup DataAzureadGroup { get; }

        public GlobalContext(Construct scope)
        {
            DataAzureadGroup = new DataAzureadGroup(scope, "github_actions_service_principals", new DataAzureadGroupConfig
            {
                DisplayName = "github-actions-service-principals"
            });
        }
    }
}
