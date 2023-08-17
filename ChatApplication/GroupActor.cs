using Akka.Actor;
namespace ChatApplication;
public class GroupActor:UntypedActor
{
    public static Props Props(string groupName, string groupId) =>
        Akka.Actor.Props.Create(()=>new GroupActor(groupName, groupId));
    private string GroupName { get; set; }
    private string GroupId { get; set; }
    private List<IActorRef> Users;
    public Dictionary<string, string> GroupChat { get; set; } = new();
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
            case Messages.JoinGroup msg:
                Users.Add(msg.GroupMember);
                Console.WriteLine($"Total Members in {GroupName} are now {Users.Count}");
                break;
            case Messages.SendMessageToGroup msg:
                if (Users.Contains(msg.GroupMember))
                {
                    GroupChat.Add(msg.UserName,msg.MessageBody);
                }
                else
                {
                    Console.WriteLine("You are not in the Group!!!");
                }
                break;
            case Messages.ShowGroupChat msg:
                if (Users.Contains(msg.GroupMember))
                {
                   msg.DisplayGroupChat(chat:GroupChat);
                }
                else
                {
                    Console.WriteLine("You are not in the Group!!!");
                }
                break;
        }
    }
}