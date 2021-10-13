using Hello.NET.Domain.Models;
using Hello.NET.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hello.NET.SignalR {
    public interface IHubClient {
        Task BroadcastMessage(Message message);

        
    }
}
