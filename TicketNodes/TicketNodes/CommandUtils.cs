using System;
using System.Collections.Generic;
using System.Linq;

namespace TicketNodes
{
    public class CommandUtils
    {
        private Ticket Root = new Ticket("Root");

        public bool ExecuteCommand(string cmd)
        {
            var split = cmd.Split(' ');
            var args = split.Skip(Math.Max(0, 1));
            return ExecuteCommand(split.FirstOrDefault() ?? throw new InvalidOperationException(), args.ToList());
        }

        private bool ExecuteCommand(string cmd, IList<string> args)
        {
            switch (cmd.ToLower())
            {
                case "print":
                    return ExecutePrintTree(Root, "", true);
                case "create-root-ticket":
                case "create":
                    return ExecuteCreateRootTicket(args);
                case "add-successor":
                case "as":
                    return ExecuteAddSuccessor(args);
                case "add-child":
                case "ac":
                    return ExecuteAddChild(args);
                default:
                    return ExecutePrintHelp();
            }
        }

        private bool ExecutePrintHelp()
        {
            Console.WriteLine("Help: ");
            Console.WriteLine("\tprint ticket hierarchy: /print");
            Console.WriteLine("\tcreate root ticket: /create <name>");
            Console.WriteLine(
                "\tadd successor: /[add-successor, as] <Id des Nachfolgers> <Id des Vorgängers>");
            Console.WriteLine("\tadd child ticket: /[add-child, ac] <name> <Id des obertickets>");
            return true;
        }

        private bool ExecutePrintTree(Ticket ticket, string indent, bool last)
        {
            Console.Write(indent);
            if (last)
            {
                Console.Write("\\-");
                indent += "  ";
            }
            else
            {
                Console.Write("|-");
                indent += "| ";
            }

            Console.WriteLine(ticket);

            for (int i = 0; i < ticket.Children.Count; i++)
                ExecutePrintTree(ticket.Children[i], indent, i == ticket.Children.Count - 1);
            return true;
        }

        private bool ExecuteCreateRootTicket(IList<string> args)
        {
            if (args.Count < 1) return false;
            Root.AddChild(args[0]);

            return true;
        }

        private bool ExecuteAddSuccessor(IList<string> args)
        {
            try
            {
                int successorId = Int32.Parse(args[0]);
                int predecessorId = Int32.Parse(args[1]);
                var successor = FindTicket(successorId) ?? throw new Exception("error");
                var predecessor = FindTicket(predecessorId) ?? throw new Exception("error");
                return successor.AddPredecessor(predecessor);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                return false;
            }
        }

        private bool ExecuteAddChild(IList<string> args)
        {
            try
            {
                string nameOfChild = args[0];
                int parentTicketId = Int32.Parse(args[1]);
                var parent = FindTicket(parentTicketId) ?? throw new Exception("error");
                parent.AddChild(nameOfChild);

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                return false;
            }
        }

        private Ticket? FindTicket(int id)
        {
            return Root.FindChild(id);
        }
    }
}