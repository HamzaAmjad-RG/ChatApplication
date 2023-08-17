using Akka.Actor;

namespace ChatApplication;

public class Messages
{
   public class CreateUser
   {
      public string UserName { get; private set; }

      public CreateUser(string userName)
      {
         UserName = userName;
      }
   }

   public class ShowUser
   {
      public  void DisplayUser(string userName)
      {
         Console.WriteLine($"User {userName}");
      }
   }

   public class SendMessage
   {
      public string MessageBody { get; private set; }
      public IActorRef ReceiverRef { get; private set; }

      public SendMessage(string messageBody, IActorRef receiverRef)
      {
         MessageBody = messageBody;
         ReceiverRef = receiverRef;
      }
   }

   public class SendMessageToUser
   {
      public string MessageBody { get; private set; }
      public string UserName { get; set; }

      public SendMessageToUser(string userName, string messageBody)
      {
         UserName = userName;
         MessageBody = messageBody;
      }
   }
   public class ReceiveMessage
   {
      public string MessageBody { get; private set; }
      public string SenderUser { get; set; }       //Sender could be internal function
      public ReceiveMessage(string messageBody,string senderUser)
      {
         MessageBody = messageBody;
         SenderUser = senderUser;
      }
   }

   public class ChangeUser
   {
      public string UserName { get;private set; }

      public ChangeUser(string userName)
      {
         UserName = userName;
      }
   }

   public class ShowMessages
   {
      public void Display(Dictionary<string,string> msgs)
      {
         Console.WriteLine("Welcome to your Inbox!!!");
         Console.WriteLine($"You have {msgs.Count} Messages to read.");
         foreach (var msg in msgs)
         {
            Console.WriteLine($"Message from: {msg.Key}");
            Console.WriteLine(msg.Value);
         }
      }
   }

   public class CreateGroup
   {
      public string GroupId { get; private set; }
      public string GroupName { get; private set; }

      public IActorRef CreatorRef { get; private set; }

      public ActorSelection GroupHandler { get; private set;  }
      public CreateGroup(string groupId, string groupName,IActorRef creatorRef)
      {
         GroupName = groupName;
         GroupId = groupId;
         CreatorRef = creatorRef;
      }

      public CreateGroup(string groupId, string groupName)
      {
         GroupId = groupId;
         GroupName = groupName;
      }

      public CreateGroup(string groupId, string groupName, ActorSelection groupHandler)
      {
         GroupName = groupName;
         GroupId = groupId;
         GroupHandler = groupHandler;
      }
   }
   public class JoinGroup
   {
      public string GroupId { get; private set; }
      public ActorSelection? GroupHandler { get; private set; }

      public IActorRef? GroupMember { get; private set; }
      public JoinGroup(string groupId, ActorSelection groupHandler=null,IActorRef groupMember=null)
      {
         GroupId = groupId;
         GroupHandler = groupHandler;
         GroupMember = groupMember;
      }

   }
   public class SendMessageToGroup
   {
      public string GroupId { get; private set; }
      public ActorSelection? GroupHandler { get; private set; }
      public IActorRef? GroupMember { get; private set; }
      public string UserName { get; private set; }
      public string MessageBody { get; private set; }
      public SendMessageToGroup(string groupId, ActorSelection groupHandler=null,IActorRef groupMember=null,string userName=null,string messageBody=null)
      {
         GroupId = groupId;
         GroupHandler = groupHandler;
         GroupMember = groupMember;
         UserName = userName;
         MessageBody = messageBody;
      }
   }
   public class ShowGroupChat
   {
      public string GroupId { get; private set; }
      public ActorSelection? GroupHandler { get; private set; }
      public IActorRef? GroupMember { get; private set; }

      public ShowGroupChat(string groupId,ActorSelection groupHandler=null,IActorRef groupMember=null)
      {
         GroupId = groupId;
         GroupHandler = groupHandler;
         GroupMember = groupMember;
      }
      public void DisplayGroupChat(Dictionary<string, string> chat)
      {
         Console.WriteLine("*********Group Chat************");
         foreach (var msg in chat)
         {
            Console.WriteLine($"{msg.Key}: {msg.Value}");
         }
      }
   }
}