using Bogus;
using GitHubTop5.Controllers;
using GitHubTop5.DomainModels;
using GitHubTop5.Services.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Threading.Tasks;

namespace GitHubTop5.Services.UnitTests
{
    [TestClass]
    public class GitHubSearchRepositoryServiceTests
    {
        private Mock<IGitHubSearchRepositoryService> _mockGitHubSearchRepositoryService;
        
        private GitHubSearchRepositoryController _target;
        
        [TestInitialize]
        public void Initialize()
        {
            _mockGitHubSearchRepositoryService = new Mock<IGitHubSearchRepositoryService>();
            _target = new GitHubSearchRepositoryController(_mockGitHubSearchRepositoryService.Object);
        }

        [TestMethod]
        public async Task GetTop5Starred_ShouldReturnRootWhenLanguageIsNotNull()
        {
            const string expectedLanguage = "CSharp";

            var expectedItems = new Faker<Item>()
                .RuleFor(x => x.Language, expectedLanguage)
                .RuleFor(x => x.StargazersCount, f => f.Random.Int(0, 10000))
                .GenerateBetween(3, 10);
            
            var expectedVal = new Faker<Root>()
                .RuleFor(x => x.Items, expectedItems)
                .RuleFor(x => x.TotalCount, expectedItems.Count)
                .Generate();

            _mockGitHubSearchRepositoryService.Setup(x => x.GetTop5Starred(It.IsAny<string>()))
               .Returns(Task.FromResult(expectedVal));

            var ret = await _target.GetTop5Starred(expectedLanguage);

            Assert.AreEqual(ret.TotalCount, expectedVal.TotalCount);

            _mockGitHubSearchRepositoryService.Verify();
           
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException), "Invalid language")]
        public async Task GetTop5Starred_ShouldNotReturnRootWhenLanguageIsNull()
        {
            const string expectedLanguage = null;

            var expectedItems = new Faker<Item>()
                .RuleFor(x => x.Language, (string)null)
                .RuleFor(x => x.StargazersCount, f => f.Random.Int(0, 10000))
                .GenerateBetween(3, 10);

            var expectedVal = new Faker<Root>()
                .RuleFor(x => x.Items, expectedItems)
                .RuleFor(x => x.TotalCount, expectedItems.Count)
                .Generate();

            _mockGitHubSearchRepositoryService.Setup(x => x.GetTop5Starred(It.IsAny<string>()))
               .Returns((Task<Root>)null);

            var ret = await _target.GetTop5Starred(expectedLanguage);

            Assert.AreEqual(ret.TotalCount, expectedVal.TotalCount);

            _mockGitHubSearchRepositoryService.Verify();

        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException), "Invalid language")]
        public async Task GetTop5Starred_ShouldNotReturnRootWhenLanguageIsNullOrWhitespace()
        {
            var expectedLanguage = string.Empty;

            var expectedItems = new Faker<Item>()
                .RuleFor(x => x.Language, (string)null)
                .RuleFor(x => x.StargazersCount, f => f.Random.Int(0, 10000))
                .GenerateBetween(3, 10);

            var expectedVal = new Faker<Root>()
                .RuleFor(x => x.Items, expectedItems)
                .RuleFor(x => x.TotalCount, expectedItems.Count)
                .Generate();

            _mockGitHubSearchRepositoryService.Setup(x => x.GetTop5Starred(It.IsAny<string>()))
               .Returns((Task<Root>)null);

            var ret = await _target.GetTop5Starred(expectedLanguage);

            Assert.AreEqual(ret.TotalCount, expectedVal.TotalCount);

            _mockGitHubSearchRepositoryService.Verify();

        }
    }
}
