using Akka.Actor;
namespace ChatApplication;


public class UserHandler:UntypedActor
{
    public static Props Props() => Akka.Actor.Props.Create(() => new UserHandler());

    private Dictionary<string, IActorRef> usersToActor = new();
    private IActorRef user { get; set; }
    protected override void OnReceive(object message)
    {
        switch (message)
        {
            case Messages.CreateUser msg:
                user = Context.ActorOf(UserActor.Props(msg.UserName));
                usersToActor.Add(msg.UserName,user);
                break;
            case Messages.ShowUser:
                //Just checking forward
                user.Forward(message);
                break;
            case Messages.ChangeUser msg:
                if (usersToActor.TryGetValue(msg.UserName, out var actorRef))
                {
                    user = actorRef;
                }
                break;
            case Messages.SendMessageToUser msg:
                if (usersToActor.TryGetValue(msg.UserName, out var receiver))
                {
                    //Why I used tell here not forward? // Not required to know original sender
                    user.Tell(new Messages.SendMessage(msg.MessageBody,receiver));
                }
                break;
            case Messages.ShowMessages:
                user.Tell(message);
                break;
        }
    }
}