﻿using System.Threading.Tasks;
using FakeItEasy;
using Tweetinvi;
using Tweetinvi.Controllers.Account;
using Tweetinvi.Core.Web;
using Tweetinvi.Models;
using Tweetinvi.Models.DTO;
using Tweetinvi.Models.DTO.QueryDTO;
using Tweetinvi.Parameters;
using Xunit;
using xUnitinvi.TestHelpers;

namespace xUnitinvi.ClientActions.AccountsClient
{
    public class AccountQueryExecutorTests
    {
        public AccountQueryExecutorTests()
        {
            _fakeBuilder = new FakeClassBuilder<AccountQueryExecutor>();
            _fakeAccountQueryGenerator = _fakeBuilder.GetFake<IAccountQueryGenerator>().FakedObject;
            _fakeTwitterAccessor = _fakeBuilder.GetFake<ITwitterAccessor>().FakedObject;
        }

        private readonly FakeClassBuilder<AccountQueryExecutor> _fakeBuilder;
        private readonly IAccountQueryGenerator _fakeAccountQueryGenerator;
        private readonly ITwitterAccessor _fakeTwitterAccessor;

        private AccountQueryExecutor CreateAccountQueryExecutor()
        {
            return _fakeBuilder.GenerateClass();
        }

        [Fact]
        public async Task GetAuthenticatedUser_ReturnsUserDTO()
        {
            // Arrange
            var queryExecutor = CreateAccountQueryExecutor();

            var url = TestHelper.GenerateString();
            var parameters = new GetAuthenticatedUserParameters();
            var request = A.Fake<ITwitterRequest>();
            var expectedResult = A.Fake<ITwitterResult<IUserDTO>>();

            request.ExecutionContext.TweetMode = TweetMode.Extended;

            A.CallTo(() => _fakeAccountQueryGenerator.GetAuthenticatedUserQuery(parameters, TweetMode.Extended)).Returns(url);
            A.CallTo(() => _fakeTwitterAccessor.ExecuteRequest<IUserDTO>(request)).Returns(expectedResult);

            // Act
            var result = await queryExecutor.GetAuthenticatedUser(parameters, request);

            // Assert
            Assert.Equal(result, expectedResult);
            Assert.Equal(request.Query.Url, url);
            Assert.Equal(HttpMethod.GET, request.Query.HttpMethod);
        }
        
        // BLOCK
        [Fact]
        public async Task BlockUser_ReturnsUserDTO()
        {
            // Arrange
            var queryExecutor = CreateAccountQueryExecutor();
            var userDTO = A.Fake<IUserDTO>();

            var url = TestHelper.GenerateString();
            var parameters = new BlockUserParameters(userDTO);
            var request = A.Fake<ITwitterRequest>();
            var expectedResult = A.Fake<ITwitterResult<IUserDTO>>();

            A.CallTo(() => _fakeAccountQueryGenerator.GetBlockUserQuery(parameters)).Returns(url);
            A.CallTo(() => _fakeTwitterAccessor.ExecuteRequest<IUserDTO>(request)).Returns(expectedResult);

            // Act
            var result = await queryExecutor.BlockUser(parameters, request);

            // Assert
            Assert.Equal(result, expectedResult);
            Assert.Equal(request.Query.Url, url);
            Assert.Equal(HttpMethod.POST, request.Query.HttpMethod);
        }

        [Fact]
        public async Task UnblockUser_ReturnsUserDTO()
        {
            // Arrange
            var queryExecutor = CreateAccountQueryExecutor();
            var userDTO = A.Fake<IUserDTO>();

            var url = TestHelper.GenerateString();
            var parameters = new UnblockUserParameters(userDTO);
            var request = A.Fake<ITwitterRequest>();
            var expectedResult = A.Fake<ITwitterResult<IUserDTO>>();

            A.CallTo(() => _fakeAccountQueryGenerator.GetUnblockUserQuery(parameters)).Returns(url);
            A.CallTo(() => _fakeTwitterAccessor.ExecuteRequest<IUserDTO>(request)).Returns(expectedResult);

            // Act
            var result = await queryExecutor.UnblockUser(parameters, request);

            // Assert
            Assert.Equal(result, expectedResult);
            Assert.Equal(request.Query.Url, url);
            Assert.Equal(HttpMethod.POST, request.Query.HttpMethod);
        }
        
        [Fact]
        public async Task GetBlockedUserIds_ReturnsUserDTO()
        {
            // Arrange
            var queryExecutor = CreateAccountQueryExecutor();

            var url = TestHelper.GenerateString();
            var parameters = new GetBlockedUserIdsParameters();
            var request = A.Fake<ITwitterRequest>();
            var expectedResult = A.Fake<ITwitterResult<IIdsCursorQueryResultDTO>>();

            A.CallTo(() => _fakeAccountQueryGenerator.GetBlockedUserIdsQuery(parameters)).Returns(url);
            A.CallTo(() => _fakeTwitterAccessor.ExecuteRequest<IIdsCursorQueryResultDTO>(request)).Returns(expectedResult);

            // Act
            var result = await queryExecutor.GetBlockedUserIds(parameters, request);

            // Assert
            Assert.Equal(result, expectedResult);
            Assert.Equal(request.Query.Url, url);
            Assert.Equal(HttpMethod.GET, request.Query.HttpMethod);
        }
       
        // FOLLOWERS
        [Fact]
        public async Task FollowUser_ReturnsUserDTO()
        {
            // Arrange
            var queryExecutor = CreateAccountQueryExecutor();
            var userDTO = A.Fake<IUserDTO>();

            var url = TestHelper.GenerateString();
            var parameters = new FollowUserParameters(userDTO);
            var request = A.Fake<ITwitterRequest>();
            var expectedResult = A.Fake<ITwitterResult<IUserDTO>>();

            A.CallTo(() => _fakeAccountQueryGenerator.GetFollowUserQuery(parameters)).Returns(url);
            A.CallTo(() => _fakeTwitterAccessor.ExecuteRequest<IUserDTO>(request)).Returns(expectedResult);

            // Act
            var result = await queryExecutor.FollowUser(parameters, request);

            // Assert
            Assert.Equal(result, expectedResult);
            Assert.Equal(request.Query.Url, url);
            Assert.Equal(HttpMethod.POST, request.Query.HttpMethod);
        }
        [Fact]
        public async Task UnFollowUser_ReturnsUserDTO()
        {
            // Arrange
            var queryExecutor = CreateAccountQueryExecutor();
            var userDTO = A.Fake<IUserDTO>();

            var url = TestHelper.GenerateString();
            var parameters = new UnFollowUserParameters(userDTO);
            var request = A.Fake<ITwitterRequest>();
            var expectedResult = A.Fake<ITwitterResult<IUserDTO>>();

            A.CallTo(() => _fakeAccountQueryGenerator.GetUnFollowUserQuery(parameters)).Returns(url);
            A.CallTo(() => _fakeTwitterAccessor.ExecuteRequest<IUserDTO>(request)).Returns(expectedResult);

            // Act
            var result = await queryExecutor.UnFollowUser(parameters, request);

            // Assert
            Assert.Equal(result, expectedResult);
            Assert.Equal(request.Query.Url, url);
            Assert.Equal(HttpMethod.POST, request.Query.HttpMethod);
        }

        [Fact]
        public async Task GetUserIdsRequestingFriendship_ReturnsPendingFollowers()
        {
            // Arrange
            var queryExecutor = CreateAccountQueryExecutor();
            var parameters = new GetUserIdsRequestingFriendshipParameters();

            var url = TestHelper.GenerateString();
            var request = A.Fake<ITwitterRequest>();
            var expectedResult = A.Fake<ITwitterResult<IIdsCursorQueryResultDTO>>();

            A.CallTo(() => _fakeAccountQueryGenerator.GetUserIdsRequestingFriendshipQuery(parameters)).Returns(url);
            A.CallTo(() => _fakeTwitterAccessor.ExecuteRequest<IIdsCursorQueryResultDTO>(request)).Returns(expectedResult);

            // Act
            var result = await queryExecutor.GetUserIdsRequestingFriendship(parameters, request);

            // Assert
            Assert.Equal(result, expectedResult);
            Assert.Equal(request.Query.Url, url);
            Assert.Equal(HttpMethod.GET, request.Query.HttpMethod);
        }
        
        [Fact]
        public async Task GetUserIdsYouRequestedToFollow_ReturnsPendingFollowers()
        {
            // Arrange
            var queryExecutor = CreateAccountQueryExecutor();
            var parameters = new GetUserIdsYouRequestedToFollowParameters();

            var url = TestHelper.GenerateString();
            var request = A.Fake<ITwitterRequest>();
            var expectedResult = A.Fake<ITwitterResult<IIdsCursorQueryResultDTO>>();

            A.CallTo(() => _fakeAccountQueryGenerator.GetUserIdsYouRequestedToFollowQuery(parameters)).Returns(url);
            A.CallTo(() => _fakeTwitterAccessor.ExecuteRequest<IIdsCursorQueryResultDTO>(request)).Returns(expectedResult);

            // Act
            var result = await queryExecutor.GetUserIdsYouRequestedToFollow(parameters, request);

            // Assert
            Assert.Equal(result, expectedResult);
            Assert.Equal(request.Query.Url, url);
            Assert.Equal(HttpMethod.GET, request.Query.HttpMethod);
        }
        
        [Fact]
        public async Task UpdateRelationship_ReturnsRelationships()
        {
            // Arrange
            var queryExecutor = CreateAccountQueryExecutor();
            var parameters = new UpdateRelationshipParameters(42);

            var url = TestHelper.GenerateString();
            var request = A.Fake<ITwitterRequest>();
            var expectedResult = A.Fake<ITwitterResult<IRelationshipDetailsDTO>>();

            A.CallTo(() => _fakeAccountQueryGenerator.GetUpdateRelationshipQuery(parameters)).Returns(url);
            A.CallTo(() => _fakeTwitterAccessor.ExecuteRequest<IRelationshipDetailsDTO>(request)).Returns(expectedResult);

            // Act
            var result = await queryExecutor.UpdateRelationship(parameters, request);

            // Assert
            Assert.Equal(result, expectedResult);
            Assert.Equal(request.Query.Url, url);
            Assert.Equal(HttpMethod.POST, request.Query.HttpMethod);
        }

        [Fact]
        public async Task GetRelationshipsWith_ReturnsRelationships()
        {
            // Arrange
            var queryExecutor = CreateAccountQueryExecutor();
            var parameters = new GetRelationshipsWithParameters(new long[] { 42 });

            var url = TestHelper.GenerateString();
            var request = A.Fake<ITwitterRequest>();
            var expectedResult = A.Fake<ITwitterResult<IRelationshipStateDTO[]>>();

            A.CallTo(() => _fakeAccountQueryGenerator.GetRelationshipsWithQuery(parameters)).Returns(url);
            A.CallTo(() => _fakeTwitterAccessor.ExecuteRequest<IRelationshipStateDTO[]>(request)).Returns(expectedResult);

            // Act
            var result = await queryExecutor.GetRelationshipsWith(parameters, request);

            // Assert
            Assert.Equal(result, expectedResult);
            Assert.Equal(request.Query.Url, url);
            Assert.Equal(HttpMethod.GET, request.Query.HttpMethod);
        }
    }
}