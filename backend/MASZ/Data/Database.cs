﻿using MASZ.Enums;
using MASZ.Models;
using Microsoft.EntityFrameworkCore;

namespace MASZ.Data
{
    public class Database
    {
        private readonly DataContext context;

        public Database() { }

        public Database(DataContext context)
        {
            this.context = context;
        }

        public async Task SaveChangesAsync()
        {
            await context.SaveChangesAsync();
        }

        public async Task<bool> CanConnectAsync()
        {
            return await context.Database.CanConnectAsync();
        }

        // ==================================================================================
        //
        // Guildconfig
        //
        // ==================================================================================
        public async Task<GuildConfig> SelectSpecificGuildConfig(ulong guildId)
        {
            return await context.GuildConfigs.AsQueryable().FirstOrDefaultAsync(x => x.GuildId == guildId);
        }

        public async Task<List<GuildConfig>> SelectAllGuildConfigs()
        {
            return await context.GuildConfigs.AsQueryable().ToListAsync();
        }

        public void DeleteSpecificGuildConfig(GuildConfig guildConfig)
        {
            context.GuildConfigs.Remove(guildConfig);
        }

        public void UpdateGuildConfig(GuildConfig guildConfig)
        {
            context.GuildConfigs.Update(guildConfig);
        }

        public async Task SaveGuildConfig(GuildConfig guildConfig)
        {
            await context.GuildConfigs.AddAsync(guildConfig);
        }
        public async Task<int> CountAllGuildConfigs()
        {
            return await context.GuildConfigs.AsQueryable().CountAsync();
        }

        // ==================================================================================
        //
        // ModCase
        //
        // ==================================================================================

        public async Task<List<ModCase>> SelectAllModcasesMarkedAsDeleted()
        {
            return await context.ModCases.AsQueryable().Where(x => x.MarkedToDeleteAt < DateTime.UtcNow).ToListAsync();
        }
        public async Task<ModCase> SelectSpecificModCase(ulong guildId, int modCaseId)
        {
            return await context.ModCases.Include(c => c.Comments).AsQueryable().FirstOrDefaultAsync(x => x.GuildId == guildId && x.CaseId == modCaseId);
        }

        public async Task<List<ModCase>> SelectAllModcasesForSpecificUserOnGuild(ulong guildId, ulong userId)
        {
            return await context.ModCases.AsQueryable().Where(x => x.GuildId == guildId && x.UserId == userId).OrderByDescending(x => x.CaseId).ToListAsync();
        }

        public async Task<List<ModCase>> SelectAllModcasesForSpecificUserOnGuild(ulong guildId, ulong userId, ModcaseTableType tableType)
        {
            return tableType switch
            {
                ModcaseTableType.OnlyPunishments => await context.ModCases.AsQueryable().Where(x => x.GuildId == guildId && x.UserId == userId && x.PunishmentActive).OrderByDescending(x => x.CaseId).ToListAsync(),
                ModcaseTableType.OnlyBin => await context.ModCases.AsQueryable().Where(x => x.GuildId == guildId && x.UserId == userId && x.MarkedToDeleteAt != null).OrderByDescending(x => x.CaseId).ToListAsync(),
                _ => await context.ModCases.AsQueryable().Where(x => x.GuildId == guildId && x.UserId == userId).OrderByDescending(x => x.CaseId).ToListAsync(),
            };
        }

        public async Task<List<ModCase>> SelectAllModCasesForGuild(ulong guildId)
        {
            return await context.ModCases.AsQueryable().Where(x => x.GuildId == guildId).OrderByDescending(x => x.CaseId).ToListAsync();
        }

        public async Task<List<ModCase>> SelectAllModCasesForGuild(ulong guildId, ModcaseTableType tableType)
        {
            return tableType switch
            {
                ModcaseTableType.OnlyPunishments => await context.ModCases.AsQueryable().Where(x => x.GuildId == guildId && x.PunishmentActive).OrderByDescending(x => x.CaseId).ToListAsync(),
                ModcaseTableType.OnlyBin => await context.ModCases.AsQueryable().Where(x => x.GuildId == guildId && x.MarkedToDeleteAt != null).OrderByDescending(x => x.CaseId).ToListAsync(),
                _ => await context.ModCases.AsQueryable().Where(x => x.GuildId == guildId).OrderByDescending(x => x.CaseId).ToListAsync(),
            };
        }

        public async Task<List<ModCase>> SelectAllModcasesForSpecificUserOnGuild(ulong guildId, ulong userId, int startPage, int pageSize)
        {
            return await context.ModCases.AsQueryable().Where(x => x.GuildId == guildId && x.UserId == userId).OrderByDescending(x => x.CaseId).Skip(startPage * pageSize).Take(pageSize).ToListAsync();
        }

        public async Task<List<ModCase>> SelectAllModcasesForSpecificUserOnGuild(ulong guildId, ulong userId, int startPage, int pageSize, ModcaseTableType tableType)
        {
            return tableType switch
            {
                ModcaseTableType.OnlyPunishments => await context.ModCases.AsQueryable().Where(x => x.GuildId == guildId && x.UserId == userId && x.PunishmentActive).OrderByDescending(x => x.CaseId).Skip(startPage * pageSize).Take(pageSize).ToListAsync(),
                ModcaseTableType.OnlyBin => await context.ModCases.AsQueryable().Where(x => x.GuildId == guildId && x.UserId == userId && x.MarkedToDeleteAt != null).OrderByDescending(x => x.CaseId).Skip(startPage * pageSize).Take(pageSize).ToListAsync(),
                _ => await context.ModCases.AsQueryable().Where(x => x.GuildId == guildId && x.UserId == userId).OrderByDescending(x => x.CaseId).Skip(startPage * pageSize).Take(pageSize).ToListAsync(),
            };
        }

        public async Task<List<ModCase>> SelectAllModCasesForGuild(ulong guildId, int startPage, int pageSize)
        {
            return await context.ModCases.AsQueryable().Where(x => x.GuildId == guildId).OrderByDescending(x => x.CaseId).Skip(startPage * pageSize).Take(pageSize).ToListAsync();
        }

        public async Task<List<ModCase>> SelectAllModCasesForGuild(ulong guildId, int startPage, int pageSize, ModcaseTableType tableType)
        {
            return tableType switch
            {
                ModcaseTableType.OnlyPunishments => await context.ModCases.AsQueryable().Where(x => x.GuildId == guildId && x.PunishmentActive).OrderByDescending(x => x.CaseId).Skip(startPage * pageSize).Take(pageSize).ToListAsync(),
                ModcaseTableType.OnlyBin => await context.ModCases.AsQueryable().Where(x => x.GuildId == guildId && x.MarkedToDeleteAt != null).OrderByDescending(x => x.CaseId).Skip(startPage * pageSize).Take(pageSize).ToListAsync(),
                _ => await context.ModCases.AsQueryable().Where(x => x.GuildId == guildId).OrderByDescending(x => x.CaseId).Skip(startPage * pageSize).Take(pageSize).ToListAsync(),
            };
        }

        public async Task<List<ModCase>> SelectAllModCasesWithActivePunishmentForGuild(ulong guildId)
        {
            return await context.ModCases.AsQueryable().Where(x => x.GuildId == guildId && x.PunishmentActive == true).ToListAsync();
        }

        public async Task<List<ModCase>> SelectAllModCasesWithActiveMuteForGuildAndUser(ulong guildId, ulong userId)
        {
            return await context.ModCases.AsQueryable().Where(x => x.GuildId == guildId && x.UserId == userId && x.PunishmentActive == true && x.PunishmentType == PunishmentType.Mute && x.MarkedToDeleteAt == null).ToListAsync();
        }

        public async Task<List<ModCase>> SelectAllModCasesThatHaveParallelPunishment(ModCase modCase)
        {
            return await context.ModCases.AsQueryable().Where(
                x =>
                  x.GuildId == modCase.GuildId &&
                  x.UserId == modCase.UserId &&
                  x.CaseId != modCase.CaseId &&
                  x.MarkedToDeleteAt == null &&
                  x.PunishmentType == modCase.PunishmentType &&
                  x.PunishmentActive == true &&
                  (
                      x.PunishedUntil == null ||
                      x.PunishedUntil > DateTime.UtcNow
                  )).ToListAsync();
        }

        public async Task<List<ModCase>> SelectAllModCasesWithActivePunishments()
        {
            return await context.ModCases.AsQueryable().Where(x => x.PunishmentActive == true && x.MarkedToDeleteAt == null).ToListAsync();
        }

        public async Task<List<ModCase>> SelectAllModCases()
        {
            return await context.ModCases.AsQueryable().ToListAsync();
        }

        public async Task<List<ModCase>> SelectAllModCasesForSpecificUser(ulong userId)
        {
            return await context.ModCases.AsQueryable().Where(x => x.UserId == userId).ToListAsync();
        }

        public async Task<List<ModCase>> SelectLatestModCases(DateTime timeLimit, int limit = 1000)
        {
            return await context.ModCases.AsQueryable().Where(x => x.CreatedAt >= timeLimit).OrderByDescending(x => x.CreatedAt).Take(limit).ToListAsync();
        }

        public async Task<int> CountAllModCases()
        {
            return await context.ModCases.AsQueryable().CountAsync();
        }

        public async Task<int> CountAllModCasesForGuild(ulong guildId)
        {
            return await context.ModCases.AsQueryable().Where(x => x.GuildId == guildId).CountAsync();
        }

        public async Task<int> CountAllActivePunishmentsForGuild(ulong guildId)
        {
            return await context.ModCases.AsQueryable().Where(x => x.GuildId == guildId && x.PunishmentActive == true).CountAsync();
        }

        public async Task<int> CountAllActivePunishmentsForGuild(ulong guildId, PunishmentType type)
        {
            return await context.ModCases.AsQueryable().Where(x => x.GuildId == guildId && x.PunishmentActive == true && x.PunishmentType == type).CountAsync();
        }

        public async Task<List<DbCount>> GetCaseCountGraph(ulong guildId, DateTime since)
        {
            return await context.ModCases.AsQueryable().Where(x => x.GuildId == guildId && x.OccuredAt > since)
            .GroupBy(x => new { x.OccuredAt.Month, x.OccuredAt.Year }).Select(x => new DbCount { Year = x.Key.Year, Month = x.Key.Month, Count = x.Count() }).OrderByDescending(x => x.Year).ThenByDescending(x => x.Month).ToListAsync();
        }

        public async Task<List<DbCount>> GetPunishmentCountGraph(ulong guildId, DateTime since)
        {
            return await context.ModCases.AsQueryable().Where(x => x.GuildId == guildId && x.OccuredAt > since && x.PunishmentType != PunishmentType.Warn)
            .GroupBy(x => new { x.OccuredAt.Month, x.OccuredAt.Year }).Select(x => new DbCount { Year = x.Key.Year, Month = x.Key.Month, Count = x.Count() }).OrderByDescending(x => x.Year).ThenByDescending(x => x.Month).ToListAsync();
        }

        public async Task DeleteAllModCasesForGuild(ulong guildId)
        {
            var cases = await context.ModCases.AsQueryable().Where(x => x.GuildId == guildId).ToListAsync();
            context.ModCases.RemoveRange(cases);
        }

        public void DeleteSpecificModCase(ModCase modCase)
        {
            context.ModCases.Remove(modCase);
        }

        public void UpdateModCase(ModCase modCase)
        {
            context.ModCases.Update(modCase);
        }

        public async Task SaveModCase(ModCase modCase)
        {
            await context.ModCases.AddAsync(modCase);
        }

        public async Task<int> GetHighestCaseIdForGuild(ulong guildId)
        {
            var query = context.ModCases.AsQueryable().Where(x => x.GuildId == guildId);
            if (!await query.AnyAsync())
            {
                return 0;
            }
            return await query.MaxAsync(p => p.CaseId);
        }

        // ==================================================================================
        //
        // AutoModerationEvents
        //
        // ==================================================================================

        public async Task<int> CountAllModerationEvents()
        {
            return await context.AutoModerationEvents.AsQueryable().CountAsync();
        }

        public async Task<List<DbCount>> GetModerationCountGraph(ulong guildId, DateTime since)
        {
            return await context.AutoModerationEvents.AsQueryable().Where(x => x.GuildId == guildId && x.CreatedAt > since)
                .GroupBy(x => new { x.CreatedAt.Month, x.CreatedAt.Year }).Select(x => new DbCount { Year = x.Key.Year, Month = x.Key.Month, Count = x.Count() }).OrderByDescending(x => x.Year).ThenByDescending(x => x.Month).ToListAsync();
        }
        public async Task<List<AutoModerationTypeSplit>> GetModerationSplitGraph(ulong guildId, DateTime since)
        {
            return await context.AutoModerationEvents.AsQueryable().Where(x => x.GuildId == guildId && x.CreatedAt > since)
                .GroupBy(x => new { Type = x.AutoModerationType }).Select(x => new AutoModerationTypeSplit { Type = x.Key.Type, Count = x.Count() }).ToListAsync();
        }

        public async Task<int> CountAllModerationEventsForGuild(ulong guildId)
        {
            return await context.AutoModerationEvents.AsQueryable().Where(x => x.GuildId == guildId).CountAsync();
        }

        public async Task<int> CountAllModerationEventsForSpecificUserOnGuild(ulong guildId, ulong userId)
        {
            return await context.AutoModerationEvents.AsQueryable().Where(x => x.GuildId == guildId && x.UserId == userId).CountAsync();
        }

        public async Task<List<AutoModerationEvent>> SelectAllModerationEvents()
        {
            return await context.AutoModerationEvents.AsQueryable().ToListAsync();
        }
        public async Task<List<AutoModerationEvent>> SelectAllModerationEventsForSpecificUser(ulong userId)
        {
            return await context.AutoModerationEvents.AsQueryable().Where(x => x.UserId == userId).ToListAsync();
        }
        public async Task<List<AutoModerationEvent>> SelectAllModerationEventsForSpecificUser(ulong userId, int minutes)
        {
            var since = DateTime.UtcNow.AddMinutes(-minutes);
            return await context.AutoModerationEvents.AsQueryable().Where(x => x.UserId == userId && x.CreatedAt > since).ToListAsync();
        }
        public async Task<List<AutoModerationEvent>> SelectAllModerationEventsForGuild(ulong guildId)
        {
            return await context.AutoModerationEvents.AsQueryable().Where(x => x.GuildId == guildId).OrderByDescending(x => x.CreatedAt).ToListAsync();
        }
        public async Task<List<AutoModerationEvent>> SelectAllModerationEventsForGuild(ulong guildId, int startPage, int pageSize)
        {
            return await context.AutoModerationEvents.AsQueryable().Where(x => x.GuildId == guildId).OrderByDescending(x => x.CreatedAt).Skip(startPage * pageSize).Take(pageSize).ToListAsync();
        }
        public async Task<List<AutoModerationEvent>> SelectAllModerationEventsForSpecificUserOnGuild(ulong guildId, ulong userId, int startPage, int pageSize)
        {
            return await context.AutoModerationEvents.AsQueryable().Where(x => x.GuildId == guildId && x.UserId == userId).OrderByDescending(x => x.CreatedAt).Skip(startPage * pageSize).Take(pageSize).ToListAsync();
        }

        public async Task DeleteAllModerationEventsForGuild(ulong guildId)
        {
            var events = await context.AutoModerationEvents.AsQueryable().Where(x => x.GuildId == guildId).ToListAsync();
            context.AutoModerationEvents.RemoveRange(events);
        }

        public async Task SaveModerationEvent(AutoModerationEvent modEvent)
        {
            await context.AutoModerationEvents.AddAsync(modEvent);
        }

        // ==================================================================================
        //
        // AutoModerationConfig
        //
        // ==================================================================================

        public async Task<List<AutoModerationConfig>> SelectAllModerationConfigsForGuild(ulong guildId)
        {
            return await context.AutoModerationConfigs.AsQueryable().Where(x => x.GuildId == guildId).ToListAsync();
        }
        public async Task<AutoModerationConfig> SelectModerationConfigForGuildAndType(ulong guildId, AutoModerationType type)
        {
            return await context.AutoModerationConfigs.AsQueryable().FirstOrDefaultAsync(x => x.GuildId == guildId && x.AutoModerationType == type);
        }
        public void PutModerationConfig(AutoModerationConfig modConfig)
        {
            context.AutoModerationConfigs.Update(modConfig);
        }
        public void DeleteSpecificModerationConfig(AutoModerationConfig modConfig)
        {
            context.AutoModerationConfigs.Remove(modConfig);
        }
        public async Task DeleteAllModerationConfigsForGuild(ulong guildId)
        {
            var events = await context.AutoModerationConfigs.AsQueryable().Where(x => x.GuildId == guildId).ToListAsync();
            context.AutoModerationConfigs.RemoveRange(events);
        }

        // ==================================================================================
        //
        // Comments
        //
        // ==================================================================================

        public async Task SaveModCaseComment(ModCaseComment comment)
        {
            await context.ModCaseComments.AddAsync(comment);
        }

        public void UpdateModCaseComment(ModCaseComment comment)
        {
            context.ModCaseComments.Update(comment);
        }

        public void DeleteSpecificModCaseComment(ModCaseComment comment)
        {
            context.ModCaseComments.Remove(comment);
        }

        public async Task<ModCaseComment> SelectSpecificModCaseComment(int commentId)
        {
            return await context.ModCaseComments.AsQueryable().FirstOrDefaultAsync(c => c.Id == commentId);
        }

        public async Task<List<ModCaseComment>> SelectLastModCaseCommentsByGuild(ulong guildId)
        {
            return await context.ModCaseComments.Include(x => x.ModCase).AsQueryable().Where(x => x.ModCase.GuildId == guildId).OrderByDescending(x => x.CreatedAt).Take(10).ToListAsync();
        }

        public async Task<int> CountCommentsForGuild(ulong guildId)
        {
            return await context.ModCaseComments.Include(x => x.ModCase).AsQueryable().Where(x => x.ModCase.GuildId == guildId).CountAsync();
        }

        // ==================================================================================
        //
        // CaseTemplates
        //
        // ==================================================================================

        public async Task SaveCaseTemplate(CaseTemplate template)
        {
            await context.CaseTemplates.AddAsync(template);
        }

        public void DeleteSpecificCaseTemplate(CaseTemplate template)
        {
            context.CaseTemplates.Remove(template);
        }

        public async Task<CaseTemplate> GetSpecificCaseTemplate(int templateId)
        {
            return await context.CaseTemplates.AsQueryable().FirstOrDefaultAsync(x => x.Id == templateId);
        }

        public async Task<List<CaseTemplate>> GetAllCaseTemplates()
        {
            return await context.CaseTemplates.AsQueryable().OrderByDescending(x => x.CreatedAt).ToListAsync();
        }

        public async Task<List<CaseTemplate>> GetAllTemplatesFromUser(ulong userId)
        {
            return await context.CaseTemplates.AsQueryable().Where(x => x.UserId == userId).OrderByDescending(x => x.CreatedAt).ToListAsync();
        }

        public async Task DeleteAllTemplatesForGuild(ulong guildId)
        {
            var templates = await context.CaseTemplates.AsQueryable().Where(x => x.CreatedForGuildId == guildId).ToListAsync();
            context.CaseTemplates.RemoveRange(templates);
        }

        public async Task<int> CountAllCaseTemplates()
        {
            return await context.CaseTemplates.AsQueryable().CountAsync();
        }

        // ==================================================================================
        //
        // Motd
        //
        // ==================================================================================

        public async Task<GuildMotd> GetMotdForGuild(ulong guildId)
        {
            return await context.GuildMotds.AsQueryable().Where(x => x.GuildId == guildId).FirstOrDefaultAsync();
        }
        public void SaveMotd(GuildMotd motd)
        {
            context.GuildMotds.Update(motd);
        }
        public async Task DeleteMotdForGuild(ulong guildId)
        {
            var motd = await context.GuildMotds.AsQueryable().Where(x => x.GuildId == guildId).ToListAsync();
            context.GuildMotds.RemoveRange(motd);
        }

        // ==================================================================================
        //
        // Tokens
        //
        // ==================================================================================

        public async Task SaveToken(APIToken token)
        {
            await context.APITokens.AddAsync(token);
        }

        public void DeleteToken(APIToken token)
        {
            context.APITokens.Remove(token);
        }

        public async Task<int> CountAllAPITokens()
        {
            return await context.APITokens.AsQueryable().CountAsync();
        }

        public async Task<List<APIToken>> GetAllAPIToken()
        {
            return await context.APITokens.AsQueryable().ToListAsync();
        }

        public async Task<APIToken> GetAPIToken()
        {
            return await context.APITokens.AsQueryable().FirstOrDefaultAsync();
        }

        public async Task<APIToken> GetAPIToken(int id)
        {
            return await context.APITokens.AsQueryable().Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        // ==================================================================================
        //
        // UserInvites
        //
        // ==================================================================================

        public async Task<List<UserInvite>> GetInvitedUsersByUser(ulong userId)
        {
            return await context.UserInvites.AsQueryable().Where(x => x.InviteIssuerId == userId).ToListAsync();
        }

        public async Task<List<UserInvite>> GetInvitedUsersByUserAndGuild(ulong userId, ulong guildId)
        {
            return await context.UserInvites.AsQueryable().Where(x => x.InviteIssuerId == userId && x.GuildId == guildId).ToListAsync();
        }

        public async Task<List<UserInvite>> GetUsedInvitesByUser(ulong userId)
        {
            return await context.UserInvites.AsQueryable().Where(x => x.JoinedUserId == userId).ToListAsync();
        }

        public async Task<List<UserInvite>> GetUsedInvitesByUserAndGuild(ulong userId, ulong guildId)
        {
            return await context.UserInvites.AsQueryable().Where(x => x.JoinedUserId == userId && x.GuildId == guildId).ToListAsync();
        }

        public async Task<int> CountTrackedInvites()
        {
            return await context.UserInvites.AsQueryable().CountAsync();
        }

        public async Task<int> CountTrackedInvitesForGuild(ulong guildId)
        {
            return await context.UserInvites.AsQueryable().Where(x => x.GuildId == guildId).CountAsync();
        }

        public async Task DeleteInviteHistoryByGuild(ulong guildId)
        {
            var userinvites = await context.UserInvites.AsQueryable().Where(x => x.GuildId == guildId).ToListAsync();
            context.UserInvites.RemoveRange(userinvites);
        }

        public async Task<List<UserInvite>> GetInvitesByCode(string code)
        {
            return await context.UserInvites.AsQueryable().Where(x => x.UsedInvite == code).ToListAsync();
        }

        public async Task SaveInvite(UserInvite userInvite)
        {
            await context.UserInvites.AddAsync(userInvite);
        }

        // ==================================================================================
        //
        // UserMapping
        //
        // ==================================================================================

        public async Task<List<UserMapping>> SelectLatestUserMappings(DateTime timeLimit, int limit)
        {
            return await context.UserMappings.AsQueryable().Where(x => x.CreatedAt > timeLimit).OrderByDescending(x => x.CreatedAt).Take(limit).ToListAsync();
        }
        public async Task<UserMapping> GetUserMappingById(int id)
        {
            return await context.UserMappings.AsQueryable().Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<List<UserMapping>> GetUserMappingsByUserId(ulong userId)
        {
            return await context.UserMappings.AsQueryable().Where(x => x.UserA == userId || x.UserB == userId).OrderByDescending(x => x.CreatedAt).ToListAsync();
        }

        public async Task<List<UserMapping>> GetUserMappingsByUserIdAndGuildId(ulong userId, ulong guildId)
        {
            return await context.UserMappings.AsQueryable().Where(x => (x.UserA == userId || x.UserB == userId) && x.GuildId == guildId).OrderByDescending(x => x.CreatedAt).ToListAsync();
        }

        public async Task<UserMapping> GetUserMappingByUserIdsAndGuildId(ulong userAId, ulong userBId, ulong guildId)
        {
            return await context.UserMappings.AsQueryable().Where(x => (x.UserA == userAId || x.UserB == userAId) && (x.UserA == userBId || x.UserB == userBId) && x.GuildId == guildId).FirstOrDefaultAsync();
        }

        public async Task<List<UserMapping>> GetUserMappingsByGuildId(ulong guildId)
        {
            return await context.UserMappings.AsQueryable().Where(x => x.GuildId == guildId).OrderByDescending(x => x.CreatedAt).ToListAsync();
        }

        public async Task<int> CountUserMappings()
        {
            return await context.UserMappings.AsQueryable().CountAsync();
        }
        public async Task<int> CountUserMappingsForGuild(ulong guildId)
        {
            return await context.UserMappings.AsQueryable().Where(x => x.GuildId == guildId).CountAsync();
        }

        public void DeleteUserMapping(UserMapping userMapping)
        {
            context.UserMappings.Remove(userMapping);
        }

        public void SaveUserMapping(UserMapping userMapping)
        {
            context.UserMappings.Update(userMapping);
        }

        public async Task DeleteUserMappingByGuild(ulong guildId)
        {
            var userMappings = await context.UserMappings.AsQueryable().Where(x => x.GuildId == guildId).ToListAsync();
            context.UserMappings.RemoveRange(userMappings);
        }

        // ==================================================================================
        //
        // UserNotes
        //
        // ==================================================================================

        public async Task<List<UserNote>> SelectLatestUserNotes(DateTime timeLimit, int limit)
        {
            return await context.UserNotes.AsQueryable().Where(x => x.UpdatedAt > timeLimit).OrderByDescending(x => x.UpdatedAt).Take(limit).ToListAsync();
        }

        public async Task<UserNote> GetUserNoteById(int id)
        {
            return await context.UserNotes.AsQueryable().Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<List<UserNote>> GetUserNotesByUserId(ulong userId)
        {
            return await context.UserNotes.AsQueryable().Where(x => x.UserId == userId).OrderByDescending(x => x.UpdatedAt).ToListAsync();
        }

        public async Task<List<UserNote>> GetUserNotesByGuildId(ulong guildId)
        {
            return await context.UserNotes.AsQueryable().Where(x => x.GuildId == guildId).OrderByDescending(x => x.UpdatedAt).ToListAsync();
        }

        public async Task<UserNote> GetUserNoteByUserIdAndGuildId(ulong userId, ulong guildId)
        {
            return await context.UserNotes.AsQueryable().Where(x => x.UserId == userId && x.GuildId == guildId).FirstOrDefaultAsync();
        }

        public async Task<int> CountUserNotes()
        {
            return await context.UserNotes.AsQueryable().CountAsync();
        }

        public async Task<int> CountUserNotesForGuild(ulong guildId)
        {
            return await context.UserNotes.AsQueryable().Where(x => x.GuildId == guildId).CountAsync();
        }

        public void DeleteUserNote(UserNote userNote)
        {
            context.UserNotes.Remove(userNote);
        }

        public void SaveUserNote(UserNote userNote)
        {
            context.UserNotes.Update(userNote);
        }

        public async Task DeleteUserNoteByGuild(ulong guildId)
        {
            var userNotes = await context.UserNotes.AsQueryable().Where(x => x.GuildId == guildId).ToListAsync();
            context.UserNotes.RemoveRange(userNotes);
        }

        // ==================================================================================
        //
        // GuildLevelAuditLogConfig
        //
        // ==================================================================================

        public async Task<List<GuildLevelAuditLogConfig>> SelectAllAuditLogConfigsForGuild(ulong guildId)
        {
            return await context.GuildLevelAuditLogConfigs.AsQueryable().Where(x => x.GuildId == guildId).ToListAsync();
        }
        public async Task<GuildLevelAuditLogConfig> SelectAuditLogConfigForGuildAndType(ulong guildId, GuildAuditLogEvent type)
        {
            return await context.GuildLevelAuditLogConfigs.AsQueryable().FirstOrDefaultAsync(x => x.GuildId == guildId && x.GuildAuditLogEvent == type);
        }
        public void PutAuditLogConfig(GuildLevelAuditLogConfig auditLogConfig)
        {
            context.GuildLevelAuditLogConfigs.Update(auditLogConfig);
        }
        public void DeleteSpecificAuditLogConfig(GuildLevelAuditLogConfig auditLogConfig)
        {
            context.GuildLevelAuditLogConfigs.Remove(auditLogConfig);
        }
        public async Task DeleteAllAuditLogConfigsForGuild(ulong guildId)
        {
            var events = await context.GuildLevelAuditLogConfigs.AsQueryable().Where(x => x.GuildId == guildId).ToListAsync();
            context.GuildLevelAuditLogConfigs.RemoveRange(events);
        }
    }
}
