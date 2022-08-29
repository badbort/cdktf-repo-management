using CdktfTest.Models;
using CdktfTest.Serialization;
using Constructs;
using HashiCorp.Cdktf;
using HashiCorp.Cdktf.Providers.Azuread;
using HashiCorp.Cdktf.Providers.Azurerm;
using HashiCorp.Cdktf.Providers.Github;
using System.Dynamic;


namespace CdktfTest;

public class MainStack : TerraformStack
{
    public MainStack(Construct scope, string id) : base(scope, id)
    {
        // Variables
        TerraformVariable org = new(this, "github_organization", new TerraformVariableConfig
        {
            Default = "bortington"
        });

        TerraformVariable teamSlug = new(this, "team_slug", new TerraformVariableConfig
        {
            Type = "string"
        });

        TerraformVariable repoYamlFile = new(this, "repos_path", new TerraformVariableConfig
        {
            Type = "string",
             Nullable = true
        });

        TerraformVariable appRegistrationStorageContainerSubscriptionId = new(this, "app_registration_sub_id", new TerraformVariableConfig
        {
            Type = "string"
        });

        TerraformVariable githubToken = new(this, "github_token", new TerraformVariableConfig
        {
            Default = "asdfasdf"
        });


        // Providers
        var ghProvider = new GithubProvider(this, "github", new GithubProviderConfig
        {
            Organization = "racwa",
            Token = githubToken.StringValue
        });

        var adProvider = new AzureadProvider(this, "ad", new AzureadProviderConfig()
        {
        });

        AzurermProvider azureProvider = new AzurermProvider(this, "azurerm-racwa-management", new AzurermProviderConfig
        {
            SubscriptionId = Constants.Subscriptions.RacWaManagementID,
            Alias = "racwa-management",
            SkipProviderRegistration = true,
            Features = new AzurermProviderFeatures()
            {
            }
        } );

        var globalContext = new GlobalContext(this);
        //var yamlFile = repoYamlFile.StringValue;
        var yamlFile = Environment.GetEnvironmentVariable("REPOS_FILE_PATH");

        var repos = YamlTool.GetRepositoriesFromFile(yamlFile);
        //var repos = Fn.Yamldecode(yamlFile);
        //var iterator = TerraformIterator.FromMap(repos);



        foreach(var (repoName, options) in repos)
            CreateRepository(teamSlug.StringValue, repoName, options, globalContext);

        /*BranchProtectionConfig c = new()
        {
            RequiredPullRequestReviews = new BranchProtectionRequiredPullRequestReviews
            require_pull_request_reviews
        };*/
    }

    private RepositoryTfContext CreateRepository(string teamSlug, string repoSlug, RACRepositoryConfig repoConfig, GlobalContext teamContext)
    {
        var repoContext = new RepositoryTfContext();

        repoContext.Repository = new Repository(this, GetId("repo"), new RepositoryConfig()
        {
            Description = repoConfig.description,
            Visibility = repoConfig.visibility,
            HasIssues = repoConfig.has_issues,
            HasProjects = repoConfig.has_projects,
            HasWiki = repoConfig.has_wiki,
            IsTemplate = repoConfig.is_template,
            AllowMergeCommit = repoConfig.allow_merge_commit,
            AllowSquashMerge = repoConfig.allow_squash_merge,
            AllowRebaseMerge = repoConfig.allow_rebase_merge,
            AllowAutoMerge = repoConfig.allow_auto_merge,
            DeleteBranchOnMerge = repoConfig.delete_branch_on_merge,
            HasDownloads = repoConfig.has_downloads,
            AutoInit = repoConfig.auto_init,
            Archived = repoConfig.archived,
            ArchiveOnDestroy = repoConfig.archive_on_destroy,
            VulnerabilityAlerts = repoConfig.vulnerability_alerts,
            DefaultBranch = repoConfig.default_branch,
            HomepageUrl = repoConfig.homepage_url,
            Topics = repoConfig.topics.ToArray()
        });

        foreach (var (team, access) in repoConfig.teams)
        {
            var repoTeamSettings = new TeamRepository(this, GetId($"team-access-{team}"), new TeamRepositoryConfig()
            {
                Repository = repoContext.Repository.GetStringAttribute(nameof(RepositoryConfig.Name)),
                TeamId = teamContext.DataGithubTeams[team].GetStringAttribute(nameof(DataGithubTeamConfig.Id)),
                Permission = access
            });
        }

        return repoContext;

        string GetId(string resourceId) => $"{teamSlug}-{repoSlug}-{resourceId}";
    }
}