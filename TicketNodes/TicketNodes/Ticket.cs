using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace TicketNodes
{
    public class Ticket
    {
        private static int ID = 0;

        public int Id = ID++;

        //  public TicketHierarchy Hierarchy { get; private set; }
        public Ticket? Parent { get; private set; }

        private Ticket? _predecessor = null;

        public Ticket? Predecessor
        {
            get
            {
                var cur = this;
                while (cur.Parent != null)
                {
                    if (cur._predecessor != null) return cur._predecessor;
                    cur = cur.Parent;
                }

                return null;
            }
            private set => _predecessor = value;
        }

        public IList<Ticket> Children = new List<Ticket>();

        public IList<Ticket> Successors = new List<Ticket>();

        public string Name;

        public Ticket(string name)
        {
            //      this.Hierarchy = hierarchy;
            this.Name = name;
        }

        public Ticket AddChild(string name)
        {
            var child = new Ticket(name);
            Children.Add(child);
            child.Parent = this;
            return child;
        }

        public bool AddPredecessor(Ticket predecessor)
        {
            if (CanAddPredecessor(predecessor))
            {
                Predecessor = predecessor;
                predecessor.Successors.Add(this);
                return true;
            }

            return false;
        }

        public Ticket GetRoot()
        {
            var ticket = this;
            while (ticket.Parent != null)
                ticket = ticket.Parent;
            return ticket;
        }

        public IList<Ticket> GetSuccessorsAdvanced()
        {
            List<Ticket> result = new List<Ticket>(Successors);
            result.AddRange(Successors.SelectMany(child => child.GetChildrenRecursive()));
            return result;
        }

        public IList<Ticket> GetChildrenRecursive()
        {
            List<Ticket> result = new List<Ticket>(Children);
            result.AddRange(Children.SelectMany(child => child.GetChildrenRecursive()));
            return result;
        }

        public Ticket? FindChild(int id)
        {
            return GetChildrenRecursive().FirstOrDefault(ticket => ticket.Id == id);
        }


        private bool PredecessorIsInSameHierarchy(Ticket other)
        {
            return this.GetRoot().Equals(other.GetRoot());
        }

        private bool CanAddPredecessor(Ticket other)
        {
            Queue<Ticket> tickets = new Queue<Ticket>();
            tickets.Enqueue(this);
            while (tickets.Count > 0)
            {
                var element = tickets.Dequeue();
                if (element == null) continue;
                if (element.Equals(other)) return false;
                if (element.Parent != null) tickets.Enqueue(element.Parent);
                foreach (var ticket in element.Successors)
                    tickets.Enqueue(ticket);
            }

            return true;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Ticket)) return false;
            return this.Id.Equals((obj as Ticket).Id);
        }

        public override string ToString()
        {
            return $"{Name}({Id})";
        }
    }
}