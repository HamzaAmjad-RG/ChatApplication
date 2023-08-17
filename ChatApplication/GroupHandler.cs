using Akka.Actor;
namespace ChatApplication;
public class GroupHandler: UntypedActor
{
    public static Props Props() => Akka.Actor.Props.Create(() => new GroupHandler());
    private Dictionary<string, IActorRef> GroupsToActor = new();
    private IActorRef Group { get; set; }
    protected override void OnReceive(object message)
    {
        switch (message)
        {
            case Messages.CreateGroup msg:
                Group = Context.ActorOf(GroupActor.Props(msg.GroupName,msg.GroupId));
                GroupsToActor.Add(msg.GroupId,Group);
                Group.Tell(msg);
                Console.WriteLine("Group Created!!");
                break;
            case Messages.JoinGroup msg:
                if (GroupsToActor.TryGetValue(msg.GroupId, out var groupRef))
                {
                    groupRef.Tell(msg);
                }
                break;
            case Messages.SendMessageToGroup messageToGroup:
                if (GroupsToActor.TryGetValue(messageToGroup.GroupId, out var groupRef1))
                {
                    groupRef1.Tell(messageToGroup);
                }
                break;
            case Messages.ShowGroupChat showGroupChat:
                if (GroupsToActor.TryGetValue(showGroupChat.GroupId, out var groupRef2))
                {
                    groupRef2.Tell(showGroupChat);
                }
                break;
        }
    }
}