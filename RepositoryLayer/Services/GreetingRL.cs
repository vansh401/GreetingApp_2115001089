using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ModelLayer.Model;
using NLog;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;

namespace RepositoryLayer.Services
{
    public class GreetingRL:IGreetingRL
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
        private readonly GreetingContext _context;

        public GreetingRL(GreetingContext context)
        {
            _context = context;
        }


        public bool GreetMessage(GreetingModel greetingModel)
        {
            if (_context.GreetMessages.Any(greet => greet.Greeting == greetingModel.GreetMessage))  
            {
                return false;
            }
            var greetingEntity = new GreetingEntity
            {
                Greeting = greetingModel.GreetMessage,
            };
            _context.GreetMessages.Add(greetingEntity);
            _context.SaveChanges();
            return true;
        }


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

        public GreetingModel GetGreetingById(int ID)
        {
            var entity = _context.GreetMessages.FirstOrDefault(g => g.id == ID);

            if (entity != null)
            {
                return new GreetingModel()
                {
                    Id = entity.id,
                    GreetMessage = entity.Greeting
                };
            }
            return null;
        }
    }
}
