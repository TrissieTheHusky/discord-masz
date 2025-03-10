using MASZ.Dtos.UserNote;
using MASZ.Enums;
using MASZ.Models;
using MASZ.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace MASZ.Controllers
{
    [ApiController]
    [Route("api/v1/guilds/{guildId}/usernote")]
    [Authorize]
    public class UserNoteController : SimpleController
    {

        public UserNoteController(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        [HttpGet]
        public async Task<IActionResult> GetUserNote([FromRoute] ulong guildId)
        {
            await RequirePermission(guildId, DiscordPermission.Moderator);

            List<UserNote> userNotes = await UserNoteRepository.CreateDefault(_serviceProvider, await GetIdentity()).GetUserNotesByGuild(guildId);
            return Ok(userNotes.Select(x => new UserNoteView(x)));
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUserNote([FromRoute] ulong guildId, [FromRoute] ulong userId)
        {
            await RequirePermission(guildId, DiscordPermission.Moderator);

            return Ok(new UserNoteView(await UserNoteRepository.CreateDefault(_serviceProvider, await GetIdentity()).GetUserNote(guildId, userId)));
        }

        [HttpPut]
        public async Task<IActionResult> CreateUserNote([FromRoute] ulong guildId, [FromBody] UserNoteForUpdateDto userNote)
        {
            await RequirePermission(guildId, DiscordPermission.Moderator);

            UserNote createdUserNote = await UserNoteRepository.CreateDefault(_serviceProvider, await GetIdentity()).CreateOrUpdateUserNote(guildId, userNote.UserId, userNote.Description);

            return StatusCode(201, new UserNoteView(createdUserNote));
        }

        [HttpDelete("{userId}")]
        public async Task<IActionResult> DeleteUserNote([FromRoute] ulong guildId, [FromRoute] ulong userId)
        {
            await RequirePermission(guildId, DiscordPermission.Moderator);

            await UserNoteRepository.CreateDefault(_serviceProvider, await GetIdentity()).DeleteUserNote(guildId, userId);

            return Ok();
        }
    }
}