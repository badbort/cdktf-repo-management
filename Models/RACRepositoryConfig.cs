using HashiCorp.Cdktf.Providers.Github;

namespace CdktfTest.Models;

#pragma warning disable IDE1006 // Naming Styles
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

public class RACRepositoryConfig
{
    /// <seealso cref="RepositoryConfig.Description"/>
    public string description { get; set; }

    /// <seealso cref="RepositoryConfig.Visibility"/>
    public string visibility { get; set; }

    /// <seealso cref="RepositoryConfig.HasIssues"/>
    public bool has_issues { get; set; }

    /// <seealso cref="RepositoryConfig.HasProjects"/>
    public bool has_projects { get; set; }

    /// <seealso cref="RepositoryConfig.HasWiki"/>
    public bool has_wiki { get; set; }

    /// <seealso cref="RepositoryConfig.IsTemplate"/>
    public bool is_template { get; set; }

    /// <seealso cref="RepositoryConfig.AllowMergeCommit"/>
    public bool allow_merge_commit { get; set; }

    /// <seealso cref="RepositoryConfig.AllowSquashMerge"/>
    public bool allow_squash_merge { get; set; }

    /// <seealso cref="RepositoryConfig.AllowRebaseMerge"/>
    public bool allow_rebase_merge { get; set; }

    /// <seealso cref="RepositoryConfig.AllowAutoMerge"/>
    public bool allow_auto_merge { get; set; }

    /// <seealso cref="RepositoryConfig.DeleteBranchOnMerge"/>
    public bool delete_branch_on_merge { get; set; }

    /// <seealso cref="RepositoryConfig.HasDownloads"/>
    public bool has_downloads { get; set; }

    /// <seealso cref="RepositoryConfig.AutoInit"/>
    public bool auto_init { get; set; }

    /// <seealso cref="RepositoryConfig.Archived"/>
    public bool archived { get; set; }

    /// <seealso cref="RepositoryConfig.ArchiveOnDestroy"/>
    public bool archive_on_destroy { get; set; }

    /// <seealso cref="RepositoryConfig.VulnerabilityAlerts"/>
    public bool vulnerability_alerts { get; set; }

    /// <seealso cref="RepositoryConfig.DefaultBranch"/>
    public string default_branch { get; set; } = "main";

    /// <seealso cref="RepositoryConfig.HomepageUrl"/>
    public string homepage_url { get; set; }

    /// <seealso cref="RepositoryConfig.Topics"/>
    public List<string> topics { get; set; }

    /// <summary>
    /// Dictionary mapping team name to github access
    /// </summary>
    public Dictionary<string, string> teams { get; set; } = new()
    {
        { "default-access", "push" }
    };

    /// <summary>
    /// RACWA specific configuration
    /// </summary>
    public bool create_app_registration { get; set; }

    /// <summary>
    ///  **_Important_** <see cref="disable_default_write"/> property refers to the addition of the _Github Default Access_ team being added to the repo with 
    ///  write access. This is the preferred approach and should only be disabled under special circumstances where write access needs to be more refined. 
    ///  This default Organisation does not allow for repositories to not have the minimum base role of read access to all members of the Organisation
    /// </summary>
    public bool disable_default_write { get; set; }

    public List<RACBranchProtectionConfig> branch_protection { get; set; }

    /// <summary>
    /// Modifies any team derived default properties on this instance 
    /// </summary>
    public void ApplyDefaults(TeamOptions teamOptions)
    {
        // Always add team slug as a topic if it doesnt exist
        topics = (topics ?? Enumerable.Empty<string>()).Concat(new[] { teamOptions.TeamSlug }).Distinct().ToList();
    }
}

#pragma warning restore IDE1006 // Naming Styles
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.