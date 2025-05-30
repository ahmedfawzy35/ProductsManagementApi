﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Products_Management_API.Models.DTO.Auth
{
    public class UpdateRoleDto
    {
        [Required(ErrorMessage = "Old Role is required.")]
        [DefaultValue("User")]
        public string OldRoleName { get; set; }

        [Required(ErrorMessage = "New Role is required.")]
        [DefaultValue("User")]
        public string NewRoleName { get; set; }
    }
}
