using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace SignalR.Test01.Hubs
{
[HubName("chatter")]
public class ChatHub : Hub
{
    private readonly Chatter _chatter;

    public ChatHub() :
        this(Chatter.Instance)
    {
    }

    public ChatHub(Chatter chatter)
    {
        _chatter = chatter;
    }

    public void SignIn(string connectionId, string name)
    {
        _chatter.SignIn(connectionId, name);
    }

    public void Post(string connectionId, string post)
    {
        _chatter.Post(connectionId, post);
    }

    public void SignOut(string connectionId, string name)
    {
        _chatter.SignOut(connectionId, name);
    }
}
}