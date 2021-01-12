namespace TicketNodes
{
    public class TicketHierarchy
    {
        private static int ID = 0;

        public int Id = ID++;
        public string Name { get; private set; }

        public TicketHierarchy(string name)
        {
            this.Name = name;
        }
    }
}