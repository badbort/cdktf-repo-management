using CdktfTest.Serialization;
using NUnit.Framework;
using Pulumi.Github;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CdktfTest.Tests;

[TestFixture]
internal class DeserializationTests
{
    [Test]
    public void Can_deserialize_branch_protection()
    {
        string yaml = @"
github-actions-sandbox:
  branch_protection:
    - require_conversation_resolution: true
      require_pull_request_reviews:
        require_code_owner_reviews: true
        required_approving_review_count: 2
      require_status_checks:
        strict: true
        contexts: [""Codacy Static Code Analysis/ Codacy Production""]";

        var repos = YamlTool.GetRepositories(yaml);

        

    }
}
