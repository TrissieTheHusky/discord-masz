using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using masz.Models;

namespace masz.Dtos.ModCase
{
    public class ModCaseForPutDto
    {
        [Required(ErrorMessage = "Title field is required")]
        [MaxLength(100)]
        public string Title { get; set; }
        [Required(ErrorMessage = "Description field is required")]
        public string Description { get; set; }
        [Required(ErrorMessage = "UserId field is required")]
        public ulong UserId { get; set; }
        [DataType(DataType.Date)]
        public DateTime? OccuredAt { get; set; }
        public string[] Labels { get; set; } = new string[0];
        public string Others { get; set; }
        [Required(ErrorMessage = "PunishmentType field is required")]
        [EnumDataType(typeof(PunishmentType))]
        public PunishmentType PunishmentType { get; set; }
        public DateTime? PunishedUntil { get; set; }
        public bool PunishmentActive { get; set; } = false;
    }
}