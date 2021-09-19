using System;
using DSharpPlus.Entities;

namespace masz.InviteTracking
{
    /*
        Helper class to combine default invites and vanity url.
    */
    public class TrackedInvite
    {
        public string Code { get; set; }
        public ulong CreatorId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public int Uses { get; set; }
        public int? MaxUses { get; set; }
        public ulong TargetChannelId { get; set; }
        public ulong GuildId { get; set; }
        public DateTime? ExpiresAt { get; set; }

        public bool IsExpired()
        {
            if (ExpiresAt == null) return false;
            return ExpiresAt < DateTime.UtcNow;
        }

        public bool HasNewUses(int currentUses)
        {
            return currentUses != Uses;
        }

        public TrackedInvite(DiscordInvite invite, ulong guildId)
        {
            GuildId = guildId;
            Code = invite.Code;
            try
            {
                CreatorId = invite.Inviter.Id;
            } catch (NullReferenceException)
            {
                CreatorId = 0;
            }
            CreatedAt = invite.CreatedAt.UtcDateTime;
            Uses = invite.Uses;
            MaxUses = invite.MaxUses;
            try
            {
                TargetChannelId = invite.Channel.Id;
            } catch (NullReferenceException)
            {
                TargetChannelId = 0;
            }
            ExpiresAt = invite.CreatedAt.UtcDateTime;
        }

        public TrackedInvite(ulong guildId, string vanityUrl, int uses)
        {
            GuildId = guildId;
            Code = vanityUrl;
            Uses = uses;
            CreatorId = 0;
            CreatedAt = null;
            MaxUses = null;
            TargetChannelId = 0;
            ExpiresAt = null;
        }
    }
}