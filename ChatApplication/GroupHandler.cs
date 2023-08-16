using Akka.Actor;

namespace ChatApplication;

public class GroupHandler: UntypedActor
{
    public static Props Props() => Akka.Actor.Props.Create(() => new GroupHandler());
    private Dictionary<string, IActorRef> GroupsToActor = new();
    private string GroupId { get; set; }

    protected override void OnReceive(object message)
    {
        throw new NotImplementedException();
    }
}