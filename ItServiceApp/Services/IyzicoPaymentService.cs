﻿using AutoMapper;
using ItServiceApp.Models;
using ItServiceApp.Models.Identity;
using ItServiceApp.Models.Payment;
using Iyzipay.Model;
using Iyzipay.Request;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using MUsefullMethods;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace ItServiceApp.Services
{
    public class IyzicoPaymentService : IPaymentService
    {
        private readonly IConfiguration _configuration;
        private readonly IyzicoPaymentOptions _options;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;

        public IyzicoPaymentService(IConfiguration configuration, IMapper mapper, UserManager<ApplicationUser> userManager)
        {
            _configuration = configuration;
            _mapper = mapper;
            _userManager = userManager;
            var section = configuration.GetSection(IyzicoPaymentOptions.Key);
            _options = new IyzicoPaymentOptions()
            {
                ApiKey = section["ApiKey"],
                SecretKey = section["SecretKey"],
                BaseUrl = section["BaseUrl"],
                ThreadsCallbackUrl = section["ThreadsCallbackUrl"],
            };

        }


        private string GenerateConversationId()
        {
            return StringHelpers.GenerateUniqueCode();
        }

        private CreatePaymentRequest InitialPaymentRequest(PaymentModel model)
        {
            var paymentRequest = new CreatePaymentRequest();

            paymentRequest.Installment = model.Installment;
            paymentRequest.Locale = Locale.TR.ToString();
            paymentRequest.ConversationId = GenerateConversationId();
            paymentRequest.Price = model.Price.ToString(new CultureInfo("en-US"));
            paymentRequest.PaidPrice = model.PaidPrice.ToString(new CultureInfo("en-US"));
            paymentRequest.Currency = Currency.TRY.ToString();
            paymentRequest.BasketId = StringHelpers.GenerateUniqueCode();
            paymentRequest.PaymentChannel = PaymentChannel.WEB.ToString();
            paymentRequest.PaymentGroup = PaymentGroup.SUBSCRIPTION.ToString();

            paymentRequest.PaymentCard = _mapper.Map<PaymentCard>(model.CardModel);

            var user = _userManager.FindByIdAsync(model.UserId).Result;

            var buyer = new Buyer()
            {
                Id = user.Id,
                Name = user.Name,
                Surname = user.SurName,
                GsmNumber = user.PhoneNumber,
                Email = user.Email,
                IdentityNumber = "11111111110",
                LastLoginDate = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss}",
                RegistrationDate = $"{user.CreatedDate:yyyy-MM-dd HH:mm:ss}",
                RegistrationAddress = "Nidakule Göztepe, Merdivenköy Mah. Bora Sok. No:1",
                Ip = model.Ip,
                City = "İstanbul",
                Country = "Turkey",
                ZipCode = "34732"
            };

            paymentRequest.Buyer = buyer;

            Address billingAddress = new Address
            {
                ContactName = $"{user.Name} {user.SurName}",
                City = "Istanbul",
                Country = "Turkey",
                Description = "Nidakule Göztepe, Merdivenköy Mah. Bora Sok. No:1",
                ZipCode = "34732"
            };

            paymentRequest.BillingAddress = billingAddress;

            var basketItems = new List<BasketItem>();
            var firstBasketItem = new BasketItem
            {
                Id = "BI101",
                Name = "Binocular",
                Category1 = "Collectibles",
                Category2 = "Accessories",
                ItemType = BasketItemType.VIRTUAL.ToString(),
                Price = model.Price.ToString(new CultureInfo("en-US"))
            };

            basketItems.Add(firstBasketItem);
            paymentRequest.BasketItems = basketItems;



            return paymentRequest;
        }


        public InstallmentModel CheckInstallments(string binNumber, decimal price)
        {
            var conversationId = GenerateConversationId();
            var request = new RetrieveInstallmentInfoRequest
            {
                Locale = Locale.TR.ToString(),
                ConversationId = conversationId,
                BinNumber = binNumber.Substring(0,6),
                Price = price.ToString(new CultureInfo("en-US")),
            };
            var result = InstallmentInfo.Retrieve(request, _options);
            if (result.Status == "failure")
            {

                throw new Exception(result.ErrorMessage);
            }

            if (result.ConversationId != conversationId)
            {
                throw new Exception("Hatalı istek oluşturuldu");
            }

            var resultModel = _mapper.Map<InstallmentModel>(result.InstallmentDetails[0]);

            Console.WriteLine();
            return resultModel;
    }

    public PaymentResponseModel Pay(PaymentModel model)
    {

            var request = this.InitialPaymentRequest(model);
            var payment = Payment.Create(request, _options);

        return _mapper.Map<PaymentResponseModel>(payment);
    }
}
}
