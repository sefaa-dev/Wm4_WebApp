﻿using AutoMapper;
using ItServiceApp.Data;
using ItServiceApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ItServiceApp.Components
{
    public class PricingTableViewComponent : ViewComponent
    {
        private readonly MyContext _dbContext;
        private readonly Mapper _mapper;

        public PricingTableViewComponent(MyContext dbContext, Mapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public IViewComponentResult Invoke()
        {
           
            var data = _dbContext.SubscriptionTypes
                .ToList()
                .Select(x => _mapper.Map<SubscriptionTypeViewModel>(x))
                .OrderBy(x => x.Price)
                .ToList();

            return View(data);
        }
    }
}
