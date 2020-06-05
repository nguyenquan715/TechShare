using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using TechShare.Entity;
using TechShare.Models;

namespace TechShare.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private static ConnectionFactory factory = new ConnectionFactory() { HostName = "localhost" };
        public EmployeeController(UserManager<AppUser> userManager)
        {           
            _userManager = userManager;
        }

        /*Nhận message từ member*/
        [Route("ReceiveMess")]
        [HttpGet]
        public ActionResult ReceiveMessFromMember()
        {
            MessageModel message=null;
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    Console.WriteLine("Channel ready!");
                    channel.QueueDeclare(
                        queue: "memtoemp_queue",
                        durable: true,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null
                    );
                    channel.BasicQos(0, 1, false);
                    Console.WriteLine("Waiting for messages");
                    var consumer = new EventingBasicConsumer(channel);
                    consumer.Received += (model, ea) => {
                        var body = ea.Body;
                        var mess = Encoding.UTF8.GetString(body.ToArray());
                        message = JsonConvert.DeserializeObject<MessageModel>(mess);                        
                        Console.WriteLine("Received");
                        channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
                    };
                    channel.BasicConsume(queue: "memtoemp_queue", autoAck: false, consumer: consumer);                    
                }
            }            
            return Ok(message);                    
        }
    }
}