using AutoMapper;
using ItServiceApp.Models.Entities;
using ItServiceApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ItServiceApp.MapperProfiles
{
    public class SubscriptionProfile: Profile
    {
        public SubscriptionProfile()
        {
            CreateMap<SubscriptionType, SubscriptionTypeViewModel>().ReverseMap();
        }
    }
}
