using Hello.NET.Models;
using Hello.NET.SignalR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hello.NET.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase {

        private readonly IConfiguration _configuration;
        private IHubContext<RezervacijeHub, IHubClient> _hubContext;

        public MessageController(IConfiguration configuration, IHubContext<RezervacijeHub, IHubClient> hubContext) {
            _configuration = configuration;
            _hubContext = hubContext;
        }

        [HttpGet]
        [Route("get")]
        public void get() {
            Message message = new Message();
            message.Poruka = "Imate novu rezervaciju";
            try {
                _hubContext.Clients.All.BroadcastMessage(message);
            } catch (Exception e) {
                throw e;
            }
        }

        [HttpPost]
        [Route("post")]
        public void post() {
            Message message = new Message();
            message.Poruka = "Nova poruka";
            try {
                _hubContext.Clients.All.BroadcastMessage(message);
            } catch (Exception e) {
                throw e;
            }
        }

    }
}
