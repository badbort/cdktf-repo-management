using HashiCorp.Cdktf.Providers.Github;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CdktfTest.Models
{
    public class RACBranchProtectionConfig : BranchProtectionConfig
    {
        /// <summary>
        /// Backward compatible support for misspelled <see cref="BranchProtectionConfig.RequiredPullRequestReviews"/>
        /// </summary>
        public BranchProtectionRequiredPullRequestReviews? require_pull_request_reviews { get; set; }

        /// <summary>
        /// Backward compatible way of setting <see cref="BranchProtectionRequiredPullRequestReviews.RequireCodeOwnerReviews"/>
        /// </summary>
        //public bool? require_code_owner_reviews { get; set; }

        //public double? required_approving_review_count { get; set; }
    }
}
