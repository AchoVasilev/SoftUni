﻿using System.ComponentModel.DataAnnotations;
using VaporStore.Data.Models.Enums;

namespace VaporStore.DataProcessor.Dto.Import
{
    public class CardJsonImportModel
    {
        [Required]
        [RegularExpression(@"[0-9]{4}\s[0-9]{4}\s[0-9]{4}\s[0-9]{4}")]
        public string Number { get; set; }

        [Required]
        [RegularExpression("[0-9]{3}")]
        public string Cvc { get; set; }

        [Required]
        public CardType? Type { get; set; }
    }
}