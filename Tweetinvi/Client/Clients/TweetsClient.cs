﻿using System.Linq;
using System.Threading.Tasks;
using Tweetinvi.Client.Requesters;
using Tweetinvi.Core.Client.Validators;
using Tweetinvi.Core.Iterators;
using Tweetinvi.Core.Web;
using Tweetinvi.Exceptions;
using Tweetinvi.Iterators;
using Tweetinvi.Models;
using Tweetinvi.Models.DTO;
using Tweetinvi.Models.DTO.QueryDTO;
using Tweetinvi.Parameters;

namespace Tweetinvi.Client
{
    public class TweetsClient : ITweetsClient
    {
        private readonly ITwitterClient _client;
        private readonly ITweetsRequester _tweetsRequester;

        public TweetsClient(ITwitterClient client)
        {
            _client = client;
            _tweetsRequester = client.RequestExecutor.Tweets;
        }

        public ITweetsClientParametersValidator ParametersValidator => _client.ParametersValidator;

        // Tweets

        public Task<ITweet> GetTweet(long? tweetId)
        {
            return GetTweet(new GetTweetParameters(tweetId));
        }

        public async Task<ITweet> GetTweet(IGetTweetParameters parameters)
        {
            var twitterResult = await _tweetsRequester.GetTweet(parameters).ConfigureAwait(false);
            return twitterResult?.Result;
        }

        public Task<ITweet[]> GetTweets(long[] tweetIds)
        {
            return GetTweets(new GetTweetsParameters(tweetIds));
        }

        public Task<ITweet[]> GetTweets(long?[] tweetIds)
        {
            return GetTweets(new GetTweetsParameters(tweetIds));
        }

        public Task<ITweet[]> GetTweets(ITweetIdentifier[] tweets)
        {
            return GetTweets(new GetTweetsParameters(tweets));
        }

        public async Task<ITweet[]> GetTweets(IGetTweetsParameters parameters)
        {
            var requestResult = await _tweetsRequester.GetTweets(parameters).ConfigureAwait(false);
            return requestResult?.Result;
        }

        // Tweets - Publish

        public Task<ITweet> PublishTweet(string text)
        {
            return PublishTweet(new PublishTweetParameters(text));
        }

        public async Task<ITweet> PublishTweet(IPublishTweetParameters parameters)
        {
            var requestResult = await _tweetsRequester.PublishTweet(parameters).ConfigureAwait(false);
            return requestResult?.Result;
        }

        // Tweets - Destroy

        public Task DestroyTweet(long? tweetId)
        {
            return DestroyTweet(new DestroyTweetParameters(tweetId));
        }

        public Task DestroyTweet(ITweetIdentifier tweet)
        {
            return DestroyTweet(new DestroyTweetParameters(tweet));
        }

        public Task DestroyTweet(ITweet tweet)
        {
            return DestroyTweet(tweet.TweetDTO);
        }

        public async Task DestroyTweet(ITweetDTO tweet)
        {
            await DestroyTweet(new DestroyTweetParameters(tweet)).ConfigureAwait(false);
            tweet.IsTweetDestroyed = true;
        }

        public async Task DestroyTweet(IDestroyTweetParameters parameters)
        {
            await _tweetsRequester.DestroyTweet(parameters).ConfigureAwait(false);
        }

        // Retweets

        public Task<ITweet[]> GetRetweets(long? tweetId)
        {
            return GetRetweets(new GetRetweetsParameters(tweetId));
        }

        public Task<ITweet[]> GetRetweets(ITweetIdentifier tweet)
        {
            return GetRetweets(new GetRetweetsParameters(tweet));
        }

        public async Task<ITweet[]> GetRetweets(IGetRetweetsParameters parameters)
        {
            var requestResult = await _tweetsRequester.GetRetweets(parameters).ConfigureAwait(false);
            return requestResult?.Result;
        }

        public Task<ITweet> PublishRetweet(long? tweetId)
        {
            return PublishRetweet(new PublishRetweetParameters(tweetId));
        }

        public Task<ITweet> PublishRetweet(ITweetIdentifier tweet)
        {
            return PublishRetweet(new PublishRetweetParameters(tweet));
        }

        public async Task<ITweet> PublishRetweet(IPublishRetweetParameters parameters)
        {
            var requestResult = await _tweetsRequester.PublishRetweet(parameters).ConfigureAwait(false);
            return requestResult?.Result;
        }

        public Task DestroyRetweet(long? retweetId)
        {
            return DestroyRetweet(new DestroyRetweetParameters(retweetId));
        }

        public Task DestroyRetweet(ITweetIdentifier retweet)
        {
            return DestroyRetweet(new DestroyRetweetParameters(retweet));
        }

        public async Task DestroyRetweet(IDestroyRetweetParameters parameters)
        {
            await _tweetsRequester.DestroyRetweet(parameters).ConfigureAwait(false);
        }

        public ITwitterIterator<long> GetRetweeterIdsIterator(long? tweetId)
        {
            return GetRetweeterIdsIterator(new GetRetweeterIdsParameters(tweetId));
        }

        public ITwitterIterator<long> GetRetweeterIdsIterator(ITweetIdentifier tweet)
        {
            return GetRetweeterIdsIterator(new GetRetweeterIdsParameters(tweet));
        }

        public ITwitterIterator<long> GetRetweeterIdsIterator(IGetRetweeterIdsParameters parameters)
        {
            var twitterResultIterator = _tweetsRequester.GetRetweeterIdsIterator(parameters);
            return new TwitterIteratorProxy<ITwitterResult<IIdsCursorQueryResultDTO>, long>(twitterResultIterator, dto => dto.DataTransferObject.Ids);
        }

        #region Favourite Tweets

        public ITwitterIterator<ITweet, long?> GetFavoriteTweets(long? userId)
        {
            return GetFavoriteTweets(new GetFavoriteTweetsParameters(userId));
        }

        public ITwitterIterator<ITweet, long?> GetFavoriteTweets(string username)
        {
            return GetFavoriteTweets(new GetFavoriteTweetsParameters(username));
        }

        public ITwitterIterator<ITweet, long?> GetFavoriteTweets(IUserIdentifier user)
        {
            return GetFavoriteTweets(new GetFavoriteTweetsParameters(user));
        }

        public ITwitterIterator<ITweet, long?> GetFavoriteTweets(IGetFavoriteTweetsParameters parameters)
        {
            var favoriteTweetsIterator = _tweetsRequester.GetFavoriteTweets(parameters);
            return new TwitterIteratorProxy<ITwitterResult<ITweetDTO[]>, ITweet, long?>(favoriteTweetsIterator,
                twitterResult =>
                {
                    return twitterResult.DataTransferObject.Select(x => _client.Factories.CreateTweet(x)).ToArray();
                });
        }

        public Task FavoriteTweet(long? tweetId)
        {
            return FavoriteTweet(new FavoriteTweetParameters(tweetId));
        }

        public Task FavoriteTweet(ITweetIdentifier tweet)
        {
            return FavoriteTweet(new FavoriteTweetParameters(tweet));
        }

        public Task FavoriteTweet(ITweet tweet)
        {
            return FavoriteTweet(tweet.TweetDTO);
        }

        public async Task FavoriteTweet(ITweetDTO tweet)
        {
            try
            {
                await FavoriteTweet(new FavoriteTweetParameters(tweet)).ConfigureAwait(false);
                tweet.Favorited = true;
            }
            catch (TwitterException ex)
            {
                var tweetWasAlreadyFavorited = ex.TwitterExceptionInfos != null && ex.TwitterExceptionInfos.Any() && ex.TwitterExceptionInfos.First().Code == 139;
                if (tweetWasAlreadyFavorited)
                {
                    tweet.Favorited = true;
                    return;
                }

                throw;
            }
        }

        public async Task FavoriteTweet(IFavoriteTweetParameters parameters)
        {
            await _tweetsRequester.FavoriteTweet(parameters).ConfigureAwait(false);
        }

        public Task UnFavoriteTweet(long? tweetId)
        {
            return UnFavoriteTweet(new UnFavoriteTweetParameters(tweetId));
        }

        public Task UnFavoriteTweet(ITweetIdentifier tweet)
        {
            return UnFavoriteTweet(new UnFavoriteTweetParameters(tweet));
        }

        public Task UnFavoriteTweet(ITweet tweet)
        {
            return UnFavoriteTweet(tweet.TweetDTO);
        }

        public async Task UnFavoriteTweet(ITweetDTO tweet)
        {
            await UnFavoriteTweet(new UnFavoriteTweetParameters(tweet)).ConfigureAwait(false);
            tweet.Favorited = false;
        }

        public async Task UnFavoriteTweet(IUnFavoriteTweetParameters parameters)
        {
            await _tweetsRequester.UnFavoriteTweet(parameters).ConfigureAwait(false);
        }

        public Task<IOEmbedTweet> GetOEmbedTweet(ITweetIdentifier tweet)
        {
            return GetOEmbedTweet(new GetOEmbedTweetParameters(tweet));
        }

        public Task<IOEmbedTweet> GetOEmbedTweet(long? tweetId)
        {
            return GetOEmbedTweet(new GetOEmbedTweetParameters(tweetId));
        }

        public async Task<IOEmbedTweet> GetOEmbedTweet(IGetOEmbedTweetParameters parameters)
        {
            var twitterResult = await _tweetsRequester.GetOEmbedTweet(parameters).ConfigureAwait(false);
            return _client.Factories.CreateOEmbedTweet(twitterResult?.DataTransferObject);
        }

        #endregion
    }
}
