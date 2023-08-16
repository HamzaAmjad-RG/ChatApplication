using Akka.Actor;

namespace ChatApplication;

class ChatApp
{
    public static ActorSystem ChatActorSystem;
    public static void Main(string[] args)
    {
        //Initialize Actor System
        ChatActorSystem = ActorSystem.Create("ChatActorSystem");
        IActorRef userHandler=ChatActorSystem.ActorOf(UserHandler.Props());
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

        ChatActorSystem.WhenTerminated.Wait();

    }

}