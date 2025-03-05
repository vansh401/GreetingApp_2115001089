using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelLayer.Model;
using NLog;
using RepositoryLayer.Interface;

namespace RepositoryLayer.Services
{
    public class GreetingRL:IGreetingRL
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();


        public string Greeting(UserModel userModel)
        {
            string greetingMessage = string.Empty;

            if (!string.IsNullOrEmpty(userModel.firstname) && !string.IsNullOrEmpty(userModel.lastname))
            {
                greetingMessage = $"Hello {userModel.firstname} {userModel.lastname}";
            }
            else if (!string.IsNullOrEmpty(userModel.firstname))
            {
                greetingMessage = $"Hello {userModel.firstname}";
            }
            else if (!string.IsNullOrEmpty(userModel.lastname))
            {
                greetingMessage = $"Hello {userModel.lastname}";
            }
            else
            {
                greetingMessage = "Hello World";
            }

            _logger.Info($"Generated Greeting: {greetingMessage}");
            return greetingMessage;
        }
    }
}
