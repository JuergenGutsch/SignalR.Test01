using System;
using System.Collections.Generic;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace SignalR.Test01.Hubs
{
    public class Chatter
    {
        public IDictionary<string, string> Users { get; set; }

        private IHubConnectionContext<dynamic> Clients { get; set; }

        private Chatter(IHubConnectionContext<dynamic> clients)
        {
            Clients = clients;
            Users = new Dictionary<string, string>();
        }

        // Singleton instance
        private readonly static Lazy<Chatter> _instance = new Lazy<Chatter>(
            () => new Chatter(GlobalHost.ConnectionManager.GetHubContext<ChatHub>().Clients));
        public static Chatter Instance
        {
            get { return _instance.Value; }
        }

        public void SignIn(string connectionId, string name)
        {
            Users.Add(connectionId, name);
            Clients.Client(connectionId).userAdded();
            Clients.Client(connectionId).postMessage("> ## Hello " + name + " ##");
            Clients.AllExcept(connectionId).postMessage("> ## new user '" + name + "' added ##");
        }

        public void Post(string connectionId, string message)
        {
            string user;
            if (Users.TryGetValue(connectionId, out user))
            {
                Clients.All.postMessage(user + "> " + message);
            }
        }

        public void SignOut(string connectionId, string name)
        {
            Users.Remove(connectionId);
            Clients.Client(connectionId).userRemoved();
            Clients.Client(connectionId).postMessage("> ## Goodby " + name + " ##");
            Clients.AllExcept(connectionId).postMessage("> ## user '" + name + "' removed ##");
        }
    }
}