using Akka.Actor;
namespace ChatApplication;
class ChatApp
{
    private static readonly ActorSystem? ChatActorSystem= ActorSystem.Create("ChatActorSystem");
    public static void Main(string[] args)
    {
        IActorRef? userHandler=ChatActorSystem?.ActorOf(UserHandler.Props());
        ChatActorSystem?.ActorOf(GroupHandler.Props(),"GroupHandler");

        userHandler.Tell(new Messages.CreateUser("HamzaAliAmjad"));
        userHandler.Tell(new Messages.ShowUser());

        userHandler.Tell(new Messages.CreateUser("OsamaRana"));
        userHandler.Tell(new Messages.ShowUser());

        userHandler.Tell(new Messages.ChangeUser("HamzaAliAmjad"));
        userHandler.Tell(new Messages.ShowUser());
        userHandler.Tell(new Messages.SendMessageToUser("OsamaRana","Hello buddy!!"));

        userHandler.Tell(new Messages.ChangeUser("OsamaRana"));
        Console.ReadLine();
        userHandler.Tell(new Messages.ShowMessages());
        userHandler.Tell(new Messages.SendMessageToUser("HamzaAliAmjad","HEYYYYY"));

        userHandler.Tell(new Messages.ChangeUser("HamzaAliAmjad"));
        Console.ReadLine();
        userHandler.Tell(new Messages.ShowMessages());
        Console.ReadLine();
        userHandler.Tell(new Messages.CreateGroup(groupId:"Group-01", groupName:"TrainingGroup",null,null));
        Console.ReadLine();

        userHandler.Tell(new Messages.ChangeUser("OsamaRana"));
        userHandler.Tell(new Messages.JoinGroup("Group-01"));
        Console.ReadLine();
        userHandler.Tell(new Messages.SendMessageToGroup("Group-01",messageBody:"Hello all members!!"));
        Console.ReadLine();

        userHandler.Tell(new Messages.ChangeUser("HamzaAliAmjad"));
        Console.ReadLine();
        userHandler.Tell(new Messages.SendMessageToGroup("Group-01",messageBody:"HOLAAAA!!"));
        Console.ReadLine();
        userHandler.Tell(new Messages.ShowGroupChat("Group-01"));
        
        ChatActorSystem?.WhenTerminated.Wait();
    }
}