using Akka.Actor;
using NUnit.Framework.Constraints;

namespace ChatApplication;

public class GroupHandler: UntypedActor
{
    public static Props Props() => Akka.Actor.Props.Create(() => new GroupHandler());
    private Dictionary<string, IActorRef> GroupsToActor = new();
    private string GroupId { get; set; }
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
        }
    }
}