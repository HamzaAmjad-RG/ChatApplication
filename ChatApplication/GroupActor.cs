using Akka.Actor;
namespace ChatApplication;

public class GroupActor:UntypedActor
{
    public static Props Props(string groupName, string groupId) =>
        Akka.Actor.Props.Create(()=>new GroupActor(groupName, groupId));
    private string GroupName { get; set; }
    private string GroupId { get; set; }
    private List<IActorRef> Users;


    public GroupActor(string groupName, string groupId)
    {
        GroupName = groupName;
        GroupId = groupId;
    }

    protected override void PreStart()
    {
        Users = new List<IActorRef>();
    }

    protected override void OnReceive(object message)
    {
        switch (message)
        {
            case Messages.CreateGroup msg:
                Users.Add(msg.CreatorRef);
                GroupName = msg.GroupName;
                GroupId = msg.GroupId;
                break;
        }
    }
}