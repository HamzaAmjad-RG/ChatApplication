using Akka.Actor;
namespace ChatApplication;

public class UserActor:UntypedActor
{
    public static Props Props(string userName) => Akka.Actor.Props.Create(() => new UserActor(userName));
    protected string UserName { get; private set; }

    private Dictionary<string, string> MessagesCollection = new();
    public UserActor(string userName)
    {
        UserName = userName;
    }
    protected override void OnReceive(object message)
    {
        if(message is Messages.ShowUser)
        {
            var msg = message as Messages.ShowUser;
            msg.DisplayUser(UserName);
        }
        else if(message is Messages.SendMessage)
        {
            var msg = message as Messages.SendMessage;
            msg.ReceiverRef.Tell(new Messages.ReceiveMessage(msg.MessageBody,UserName));
        }
        else if (message is Messages.ReceiveMessage)
        {
            var msg = message as Messages.ReceiveMessage;
            MessagesCollection.Add(msg.SenderUser,msg.MessageBody);
        }
        else  if(message is Messages.ShowMessages)
        {
            var msg = message as Messages.ShowMessages;
            msg.Display(MessagesCollection);
        }
    }
}