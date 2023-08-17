using Akka.Actor;
namespace ChatApplication;
public class UserActor:UntypedActor
{
    public static Props Props(string userName) => Akka.Actor.Props.Create(() => new UserActor(userName));
    private string UserName { get; set; }
    private Dictionary<string, string> MessagesCollection = new();
    public UserActor(string userName)
    {
        UserName = userName;
    }
    protected override void OnReceive(object message)
    {
        switch (message)
        {
            case Messages.ShowUser user:
            {
                user.DisplayUser(UserName);
                break;
            }
            case Messages.SendMessage sendMessage:
            {
                sendMessage.ReceiverRef.Tell(new Messages.ReceiveMessage(sendMessage.MessageBody,UserName));
                break;
            }
            case Messages.ReceiveMessage receiveMessage:
            {
                MessagesCollection.Add(receiveMessage.SenderUser,receiveMessage.MessageBody);
                break;
            }
            case Messages.ShowMessages messages:
            {
                messages.Display(MessagesCollection);
                break;
            }
            case Messages.CreateGroup createGroup:
                createGroup.GroupHandler.Tell(new Messages.CreateGroup(createGroup.GroupId,createGroup.GroupName,Self));
                break;
            case Messages.JoinGroup joinGroup:
                joinGroup.GroupHandler.Tell(new Messages.JoinGroup(groupId:joinGroup.GroupId,groupMember:Self));
                break;
            case Messages.SendMessageToGroup messageToGroup:
                messageToGroup.GroupHandler.Tell(new Messages.SendMessageToGroup(groupId:messageToGroup.GroupId,userName:UserName,messageBody:messageToGroup.MessageBody,groupMember:Self));
                break;
            case Messages.ShowGroupChat showGroupChat:
                showGroupChat.GroupHandler.Tell(new Messages.ShowGroupChat(groupId:showGroupChat.GroupId,groupMember:Self));
                break;
        }
    }
}