﻿using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chat.PresentationLayer.Models
{
   public class UserSignUpModel:UserSignInModel
    {
        public string ConfirmPassword { get; set; }
    }
}
