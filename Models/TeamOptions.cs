namespace CdktfTest.Models
{
    public class TeamOptions
    {
        /// <summary>
        /// One or more yaml files that contain a dictionary of string to <see cref="RACRepositoryConfig"/> which is used to manage the repositories owned
        /// by the team identified by <see cref="TeamSlug"/>.
        /// </summary>
        public List<string> RepositoryFiles { get; set; }

        /// <summary>
        /// The team slug which also matches the name of the Azure AD Group
        /// </summary>
        public string TeamSlug { get; set; }
    }
}
