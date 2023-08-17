﻿using Akka.Actor;

namespace ChatApplication;

class ChatApp
{
    public static ActorSystem ChatActorSystem;
    public static void Main(string[] args)
    {
        //Initialize Actor System
        ChatActorSystem = ActorSystem.Create("ChatActorSystem");
        IActorRef userHandler=ChatActorSystem.ActorOf(UserHandler.Props());
        IActorRef groupHandler = ChatActorSystem.ActorOf(GroupHandler.Props(),"GroupHandler");
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
        userHandler.Tell(new Messages.CreateGroup("Group-01", "TrainingGroup"));
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

        ChatActorSystem.WhenTerminated.Wait();

    }

}