using MASZ.Enums;

namespace MASZ.Models
{
    public class GuildLevelAuditLogConfigView
    {
        public int Id { get; set; }
        public string GuildId { get; set; }
        public GuildAuditLogEvent GuildAuditLogEvent { get; set; }
        public string ChannelId { get; set; }
        public string[] PingRoles { get; set; }
        public GuildLevelAuditLogConfigView(GuildLevelAuditLogConfig config)
        {
            Id = config.Id;
            GuildId = config.GuildId.ToString();
            GuildAuditLogEvent = config.GuildAuditLogEvent;
            ChannelId = config.ChannelId.ToString();
            PingRoles = config.PingRoles.Select(x => x.ToString()).ToArray();
        }
    }
}
