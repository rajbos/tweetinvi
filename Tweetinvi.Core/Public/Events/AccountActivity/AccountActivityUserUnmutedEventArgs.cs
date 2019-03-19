﻿using System;
using Tweetinvi.Models;

namespace Tweetinvi.Events
{
    public enum UserUnmutedRaisedInResultOf
    {
        /// <summary>
        /// The account user has Unmuted another user 
        /// </summary>
        AccountUserMutingAnotherUser,

        /// <summary>
        /// This case should not happen and is here in case Twitter changes when they trigger the Unmuted event.
        /// If you happen to receive this mode, please report to Tweetinvi your case ideally with the associated json.
        /// </summary>
        Unknown
    }

    public class AccountActivityUserUnmutedEventArgs : BaseAccountActivityEventArgs<UserUnmutedRaisedInResultOf>
    {
        public AccountActivityUserUnmutedEventArgs(AccountActivityEvent<Tuple<IUser, IUser>> eventInfo) : base(eventInfo)
        {
            UnmutedBy = eventInfo.Args.Item1;
            UnmutedUser = eventInfo.Args.Item2;

            InResultOf = GetInResultOf();
        }

        /// <summary>
        /// User who stopped being muted
        /// </summary>
        public IUser UnmutedUser { get; }

        /// <summary>
        /// User who performed the action of muting another user
        /// </summary>
        public IUser UnmutedBy { get; }

        private UserUnmutedRaisedInResultOf GetInResultOf()
        {
            if (UnmutedBy.Id == AccountUserId)
            {
                return UserUnmutedRaisedInResultOf.AccountUserMutingAnotherUser;
            }

            return UserUnmutedRaisedInResultOf.Unknown;
        }
    }
}