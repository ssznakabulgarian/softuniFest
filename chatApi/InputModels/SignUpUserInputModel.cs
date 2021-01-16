﻿using System.ComponentModel.DataAnnotations;

namespace WebMonitoringApi.InputModels
{
    public class SignUpUserInputModel
    {
        [MaxLength(100)]
        public string UserName { get; set; }

        public string Password { get; set; }

        [EmailAddress]
        public string Email { get; set; }
    }
}