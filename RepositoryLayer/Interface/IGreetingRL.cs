﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelLayer.Model;

namespace RepositoryLayer.Interface
{
    public interface IGreetingRL
    {
        public string Greeting(UserModel userModel);
        public bool GreetMessage(GreetingModel greetingModel);

        public GreetingModel GetGreetingById(int id);
    }
}
