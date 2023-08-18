using Akka.Actor;
namespace ChatApplication;
public class GroupActor:UntypedActor
{
    public static Props Props(string groupName, string groupId) =>
        Akka.Actor.Props.Create(()=>new GroupActor(groupName, groupId));
    private string GroupName { get; set; }
    private string GroupId { get; set; }
    private List<IActorRef> _users = new();
    private Dictionary<string, string?> GroupChat { get; set; } = new();

    private GroupActor(string groupName, string groupId)
    {
        GroupName = groupName ?? throw new ArgumentNullException(nameof(groupName));
        GroupId = groupId;
    }
    protected override void OnReceive(object message)
    {
        switch (message)
        {
            case Messages.CreateGroup msg:
                if (msg.CreatorRef != null) _users.Add(msg.CreatorRef);
                GroupName = msg.GroupName;
                GroupId = msg.GroupId;
                break;

            case Messages.JoinGroup msg:
                if (msg.GroupMember != null) _users.Add(msg.GroupMember);
                Console.WriteLine($"Total Members in {GroupName} are now {_users.Count}");
                break;

            case Messages.SendMessageToGroup msg:
                if (msg.GroupMember != null && _users.Contains(msg.GroupMember))
                {
                    if (msg.UserName != null) GroupChat.Add(msg.UserName, msg.MessageBody);
                }
                else
                {
                    Console.WriteLine("You are not in the Group!!!");
                }
                break;

            case Messages.ShowGroupChat msg:
                if (msg.GroupMember != null && _users.Contains(msg.GroupMember))
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