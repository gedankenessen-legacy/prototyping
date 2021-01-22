using System;
using NUnit.Framework;

namespace TicketNodes
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void Case01()
        {
            var ticket1 = new Ticket("1");
            var ticket2 = new Ticket("2");
            Assert.IsTrue(ticket1.AddPredecessor(ticket2), "1 nachfolger von 2");
            Assert.IsFalse(ticket2.AddPredecessor(ticket1), "2 nachfolger von 1");
        }

        [Test]
        public void Case02()
        {
            var ticket1 = new Ticket("1");
            var ticket2 = ticket1.AddChild("2");
            Assert.IsFalse(ticket2.AddPredecessor(ticket1), "2 nachfolger von 1");
        }

        [Test]
        public void Case03()
        {
            var ticket1 = new Ticket("1");
            var ticket2 = ticket1.AddChild("2");
            Assert.IsFalse(ticket1.AddPredecessor(ticket2), "1 nachfolger von 2");
        }

        [Test]
        public void Case04()
        {
            var ticket1 = new Ticket("1");
            var ticket2 = ticket1.AddChild("2");
            var ticket3 = ticket1.AddChild("3");
            var ticket4 = ticket3.AddChild("4");
            Assert.IsTrue(ticket4.AddPredecessor(ticket2), "4 nachfolger von 2");
        }

        [Test]
        public void Case05()
        {
            var ticket1 = new Ticket("1");
            var ticket2 = ticket1.AddChild("2");
            var ticket3 = ticket1.AddChild("3");
            var ticket4 = ticket3.AddChild("4");
            Assert.IsTrue(ticket4.AddPredecessor(ticket2), "4 nachfolger von 2");
            Assert.IsFalse(ticket2.AddPredecessor(ticket3), "2 nachfolger von 3");
        }

        [Test]
        public void Case06()
        {
            var ticket1 = new Ticket("1");
            var ticket2 = new Ticket("2");
            var ticket3 = new Ticket("3");
            Assert.IsTrue(ticket3.AddPredecessor(ticket2), "3 nachfolger von 2");
            Assert.IsTrue(ticket2.AddPredecessor(ticket1), "2 nachfolger von 1");
            Assert.IsFalse(ticket1.AddPredecessor(ticket3), "1 nachfolger von 3");
        }

        [Test]
        public void Case07()
        {
            var ticket1 = new Ticket("1");
            var ticket2 = ticket1.AddChild("2");
            var ticket3 = new Ticket("3");
            Assert.IsTrue(ticket1.AddPredecessor(ticket3), "1 nachfolger von 3");
            Assert.IsFalse(ticket3.AddPredecessor(ticket2), "3 nachfolger von 2");
        }

        [Test]
        public void Case08()
        {
            var ticket1 = new Ticket("1");
            var ticket2 = ticket1.AddChild("2");
            var ticket3 = new Ticket("3");
            Assert.IsTrue(ticket3.AddPredecessor(ticket1), "3 nachfolger von 1");
            Assert.IsFalse(ticket2.AddPredecessor(ticket3), "2 nachfolger von 3");
        }

        [Test]
        public void Case09()
        {
            var ticket1 = new Ticket("1");
            var ticket2 = ticket1.AddChild("2");
            var ticket3 = ticket2.AddChild("3");
            var ticket4 = ticket1.AddChild("1");

            var ticket5 = new Ticket("5");
            var ticket6 = ticket5.AddChild("6");
            var ticket7 = new Ticket("7");
            var ticket8 = ticket7.AddChild("8");
            Assert.IsTrue(ticket1.AddPredecessor(ticket6), "1 nachfolger von 6");
            Assert.IsTrue(ticket5.AddPredecessor(ticket8), "5 nachfolger von 8");
            Assert.IsFalse(ticket7.AddPredecessor(ticket1), "7 nachfolger von 1");
        }

        [Test]
        public void Case10()
        {
            var ticket1 = new Ticket("1");
            var ticket2 = ticket1.AddChild("2");
            var ticket3 = ticket2.AddChild("3");
            var ticket4 = ticket1.AddChild("1");

            var ticket5 = new Ticket("5");
            var ticket6 = ticket5.AddChild("6");
            var ticket7 = new Ticket("7");
            var ticket8 = ticket7.AddChild("8");
            Assert.IsTrue(ticket1.AddPredecessor(ticket6), "1 nachfolger von 6");
            Assert.IsTrue(ticket5.AddPredecessor(ticket8), "5 nachfolger von 8");
            Assert.IsFalse(ticket1.AddPredecessor(ticket7), "1 nachfolger von 7");
        }

        [Test]
        public void Case11()
        {
            var ticket1 = new Ticket("1");
            var ticket2 = ticket1.AddChild("2");
            var ticket3 = ticket2.AddChild("3");
            var ticket4 = ticket1.AddChild("1");

            var ticket5 = new Ticket("5");
            var ticket6 = ticket5.AddChild("6");
            var ticket7 = new Ticket("7");
            var ticket8 = ticket7.AddChild("8");
            Assert.IsTrue(ticket1.AddPredecessor(ticket6), "1 nachfolger von 6");
            Assert.IsTrue(ticket5.AddPredecessor(ticket8), "5 nachfolger von 8");
            Assert.IsFalse(ticket3.AddPredecessor(ticket7), "3 nachfolger von 7");
        }

        [Test]
        public void Case12()
        {
            var ticket1 = new Ticket("1");
            var ticket2 = ticket1.AddChild("2");
            var ticket3 = ticket2.AddChild("3");
            var ticket4 = ticket1.AddChild("4");

            var ticket5 = new Ticket("5");
            var ticket6 = ticket5.AddChild("6");
            var ticket7 = new Ticket("7");
            var ticket8 = ticket7.AddChild("8");
            
            Assert.IsTrue(ticket1.AddPredecessor(ticket6), "1 nachfolger von 6");
            Assert.IsTrue(ticket5.AddPredecessor(ticket8), "5 nachfolger von 8");
            Assert.IsTrue(ticket1.AddPredecessor(ticket7), "1 nachfolger von 7");
            Assert.IsFalse(ticket8.AddPredecessor(ticket2), "8 nachfolger von 2");
        }
        [Test]
        public void Case13()
        {
            var ticket1 = new Ticket("1");
            var ticket2 = ticket1.AddChild("2");
            var ticket3 = new Ticket("3");
            var ticket4 = new Ticket("4");
            Assert.IsTrue(ticket2.AddPredecessor(ticket3), "2 nachfolger von 3");
            Assert.IsTrue(ticket1.AddPredecessor(ticket4), "1 ist nachfolger von 4");
        }

        [Test]
        public void Case14()
        {
            var ticket1 = new Ticket("1");
            var ticket2 = ticket1.AddChild("2");
            var ticket3 = new Ticket("3");
            var ticket4 = ticket3.AddChild("4");
            
            Assert.IsTrue(ticket2.AddPredecessor(ticket4), "2 nachfolger von 4");
            Assert.IsTrue(ticket1.AddPredecessor(ticket3), "1 nachfolger von 3");
        }
        [Test]
        public void Case15()
        {
            var ticket1 = new Ticket("1");
            var ticket2 = ticket1.AddChild("2");
            var ticket3 = new Ticket("3");
            var ticket4 = new Ticket("4");
            
            Assert.IsTrue(ticket1.AddPredecessor(ticket3), "1 nachfolger von 3");
            Assert.IsTrue(ticket1.AddPredecessor(ticket4), "1 nachfolger von 4");
        }
        [Test]
        public void Case16()
        {
            var ticket1 = new Ticket("1");
            var ticket2 = ticket1.AddChild("2");
            var ticket3 = ticket2.AddChild("3");
            var ticket4 = new Ticket("4");
            var ticket5 = ticket4.AddChild("5");
            var ticket6 = ticket4.AddChild("6");
            var ticket7 = new Ticket("7");
            
            Assert.IsTrue(ticket1.AddPredecessor(ticket4), "1 nachfolger von 4");
            Assert.IsTrue(ticket2.AddPredecessor(ticket5), "2 nachfolger von 5");
            Assert.IsTrue(ticket2.AddPredecessor(ticket6), "2 nachfolger von 6");
            Assert.IsTrue(ticket1.AddPredecessor(ticket7), "1 nachfolger von 7");
        }
    }
}