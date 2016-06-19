﻿using System;

public class Server
{
    const int PORT = 5287;
    const string HELP_MESSAGE = @"h/help - help
q - quit (shutdown server)
live - how many users are online right now
";

    static int usersOnline = 0;

    static void Main(string[] args)
    {
        SocketHandler.Server servSocket = new SocketHandler.Server(5287);
        //servSocket.onNewConnection += StartNewSession;

        bool running = true;
        servSocket.onCloseConnection += (e) =>
        {
            if (e != null)
            {
                Console.WriteLine("Server crashed:");
                Console.WriteLine(e);
                running = false;
            }
        };

        Console.WriteLine(HELP_MESSAGE);

        string message;
        while (running)
        {
            message = Console.ReadLine();
            if (message == "q")
            {
                running = false;
                servSocket.Stop();
            }
            else if (message == "h" || message == "help")
            {
                Console.Write(HELP_MESSAGE);
            }
            else if (message == "live")
            {
                Console.WriteLine(usersOnline);
            }
        }
    }
}
