using System;

namespace TicketNodes
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            /*
             * Ticket a = new Ticket("A");
            var b = a.AddChild("b");
            var c = b.AddChild("c");
            var d = c.AddChild("d");
            var e = d.AddChild("e");
            var f = e.AddChild("f");
            var g = f.AddChild("g");
            f.AddChild("i");
            var h = g.AddChild("h");
            var children = a.GetChildrenRecursive();
            PrintPretty(a, "", true);
             */
            
            var commandUtils = new CommandUtils();
            commandUtils.ExecuteCommand("create 2");
            commandUtils.ExecuteCommand("ac 7 2");
            commandUtils.ExecuteCommand("ac 4 2");
            
            commandUtils.ExecuteCommand("create 3");
            commandUtils.ExecuteCommand("create 5");
            commandUtils.ExecuteCommand("ac 6 5");

            commandUtils.ExecuteCommand("help");
            while (true)
            {
                Console.WriteLine("Command: ");
                var command = Console.ReadLine();
                if (command == "exit") break;
                if(command != null) Console.WriteLine(commandUtils.ExecuteCommand(command));
            }
        }
        
    }
}