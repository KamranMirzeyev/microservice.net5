﻿using System.ComponentModel.DataAnnotations;

namespace ASP.NET_Web.Models.Orders
{
    public class CheckoutInfoInput
    {
        [Display(Name = "il")]
        public string Province { get; set; }

        [Display(Name = "İlçe")]
        public string District { get; set; }

        [Display(Name = "Cadde")]
        public string Street { get; set; }

        [Display(Name = "Posta Kodu")]
        public string ZipCode { get; set; }

        [Display(Name = "adres")]
        public string Line { get; set; }

        [Display(Name = "Kart isim soy isim")]
        public string CardName { get; set; }

        [Display(Name = "Kart numarası")]
        public string CardNumber { get; set; }

        [Display(Name = "son kullanma tarih(ay/yıl)")]
        public string Expiration { get; set; }

        [Display(Name = "CVV/CVC2 numarası")]
        public string CVV { get; set; }
    }
}
