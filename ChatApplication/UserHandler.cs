using Akka.Actor;
namespace ChatApplication;
public class UserHandler:UntypedActor
{
    public static Props Props() => Akka.Actor.Props.Create(() => new UserHandler());
    private readonly Dictionary<string, IActorRef> _usersToActor = new();
    private IActorRef? User { get; set; }
    private readonly ActorSelection? _groupHandlerVar = Context.ActorSelection("akka://ChatActorSystem/user/GroupHandler");
    protected override void OnReceive(object message)
    {
        switch (message)
        {
            case Messages.CreateUser msg:
                User = Context.ActorOf(UserActor.Props(msg.UserName));
                if (msg.UserName != null) _usersToActor.Add(msg.UserName, User);
                break;

            case Messages.ShowUser:
                //Just checking forward
                User.Forward(message);
                break;

            case Messages.ChangeUser msg:
                if (msg.UserName != null && _usersToActor.TryGetValue(msg.UserName, out var actorRef))
                {
                    User = actorRef;
                }
                break;

            case Messages.SendMessageToUser msg:
                if (msg.UserName != null && _usersToActor.TryGetValue(msg.UserName, out var receiver))
                {
                    //Why I used tell here not forward? // Not required to know original sender
                    User.Tell(new Messages.SendMessage(msg.MessageBody,receiver));
                }
                break;

            case Messages.ShowMessages:
                User.Tell(message);
                break;

            case Messages.CreateGroup msg:
                User.Tell(new Messages.CreateGroup(msg.GroupId,msg.GroupName,groupHandler:_groupHandlerVar,null));
                break;

            case Messages.JoinGroup msg:
                User.Tell(new Messages.JoinGroup(groupId:msg.GroupId,groupHandler:_groupHandlerVar));
                break;

            case Messages.SendMessageToGroup msg:
                User.Tell(new Messages.SendMessageToGroup(groupId:msg.GroupId,groupHandler:_groupHandlerVar,messageBody:msg.MessageBody));
                break;

            case Messages.ShowGroupChat msg:
                User.Tell(new Messages.ShowGroupChat(msg.GroupId,_groupHandlerVar));
                break;
        }
    }
}