using System;
using System.Threading.Tasks;

namespace ConsoleApp1 {
    class Program {

        static async Task Main() {

            var deadlineIter = DateTime.Now;
            var step = TimeSpan.FromSeconds(5);

            for (var i = 0; i < 5; i++) {
                deadlineIter += step;
                await Scheduler.AddEvent(new TicketDeadlineEvent(deadlineIter, $"Test{i}"));
            }

            await Task.Delay(2500);
            await Scheduler.AddEvent(new TicketDeadlineEvent(DateTime.Now, "Test2"));

            await Task.Delay(10000);
            await Scheduler.AddEvent(new TicketDeadlineEvent(DateTime.Now.AddSeconds(20), "Test4"));

            Console.ReadLine();
        }
    }
}
