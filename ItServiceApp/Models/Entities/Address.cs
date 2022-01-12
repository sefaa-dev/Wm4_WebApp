﻿using ItServiceApp.Models.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ItServiceApp.Models.Entities
{
    public class Address : BaseEntity
    {
        public string Line { get; set; }
        public string PostCode { get; set; }
        public AddressTypes AddressType { get; set; }
        public int CityId { get; set; }
        public int StateId { get; set; }


        [StringLength(128)]
        public string UserId { get; set; }

        [ForeignKey(nameof(CityId))]
        public virtual City City { get; set; }

        [ForeignKey(nameof(StateId))]
        public virtual State State { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual ApplicationUser User { get; set; }
    }

    public enum AddressTypes
    {
        Fatura,
        Teslimat
    }
}
