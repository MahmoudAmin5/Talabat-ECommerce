﻿using System.ComponentModel.DataAnnotations;
using Talabat.Core.Entities;

namespace Talabat.APIs.DTO
{
    public class CustomerBasketDto
    {
        [Required]
        public  string Id { get; set; }
        public List<BasketItemDto> Items { get; set; }

    }
}
