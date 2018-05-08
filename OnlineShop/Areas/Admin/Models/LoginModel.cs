﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Dynamic;
using System.Linq;
using System.Web;
namespace OnlineShop.Areas.Admin.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Mời nhập user name")]
        public string UserName { set; get; }
        [Required(ErrorMessage = "Mời nhập password")]
        public string PassWord { set; get; }
        public bool RememberMe { set; get; }

    }
}