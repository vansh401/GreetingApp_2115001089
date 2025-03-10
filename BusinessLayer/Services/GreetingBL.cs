﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Interface;
using ModelLayer.Model;
using RepositoryLayer.Interface;
using RepositoryLayer.Services;

namespace BusinessLayer.Services
{
    public class GreetingBL: IGreetingBL
    {

        private readonly IGreetingRL _greetingRL;
        public GreetingBL(IGreetingRL greetingRL)
        {
            _greetingRL = greetingRL;
        }
        
        public string GetGreet()
        {
            return "Hello! World";
        }
        public string greeting(UserModel userModel)
        {
            return _greetingRL.Greeting(userModel);
        }
        public bool GreetMessage(GreetingModel greetModel)
        {
            return _greetingRL.GreetMessage(greetModel);
        }

        public GreetingModel GetGreetingById(int id)
        {
            return _greetingRL.GetGreetingById(id);
        }
    }
}
