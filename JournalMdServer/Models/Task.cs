﻿using JournalMdServer.Interfaces.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace JournalMdServer.Models
{
    public class Task : BaseAuditEntity, IBaseModel
    {
        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        [Required]
        public bool Completed { get; set; }

        public DateTime? Due { get; set; }

        public string Labels { get; set; }
    }
}
