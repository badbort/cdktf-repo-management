using HashiCorp.Cdktf.Providers.Github;

namespace CdktfTest.Models
{
    /// <summary>
    /// Contains tf resources associated with the creation and maintenance of a repository.
    /// </summary>
    internal class RepositoryTfContext
    {
        public Repository Repository { get; set; }

        public BranchDefault BranchDefault { get; set; }

        public DataGithubTeam DataTeam { get; set; }
    }
}
