﻿using System.ComponentModel.DataAnnotations;

namespace Common.Models
{
    [Serializable]
    public record Customer
    {
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }
    }
}