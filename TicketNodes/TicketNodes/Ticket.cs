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
                do
                {
                    if (cur._predecessor != null) return cur._predecessor;
                    cur = cur.Parent;
                } while (cur != null);

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

        public bool IsChildOf(Ticket other)
        {
            return other.GetChildrenRecursive().FirstOrDefault(child => child.Equals(this)) != null;
        }

        //TODO CanAddPredecessor sollte eine Begründung (in form einer exception) zurückgeben, warum es fehlgeschlagen ist
        private bool CanAddPredecessor(Ticket other)
        {
            //Falls A Ein Ober-/Unter-ticket von B ist 
            if (IsChildOf(other) || other.IsChildOf(this)) return false;
            
            //Falls A schon einen Vorgänger hat
            if (Predecessor != null) return false;

            //Gibt false zurück falls ein Unterticket schon einen Vorgänger hat. Alle Untertickets müssen nämlich den gleichen Vorgänger wie das Oberticket haben
            if (GetChildrenRecursive().FirstOrDefault(child => child.Predecessor != null) != null)
                return false;
            
            Queue<Ticket> tickets = new Queue<Ticket>();
            //fügt sich selber und alle Kinder hinzu. Die Kinder werden hinzugefügt, da der Vorgänger eines Obertickets der Vorgänger aller 
            //Untertickets ist
            tickets.Enqueue(this);
            foreach (var ticket in GetChildrenRecursive())
                tickets.Enqueue(ticket);
            
            /*
             * Iteriert durch jedes Ticket bis die Queue leer ist oder element.Equals(other) true ist. Bei jeder Iteration werden Eltern
             * und alle Nachfolger (auch indirekte) der Queue hinzugefügt
             */
            while (tickets.Count > 0)
            {
                var element = tickets.Dequeue();
                if (element == null) continue;
                if (element.Equals(other)) return false;
                if (element.Parent != null) tickets.Enqueue(element.Parent);
                foreach (var ticket in element.GetSuccessorsAdvanced())
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