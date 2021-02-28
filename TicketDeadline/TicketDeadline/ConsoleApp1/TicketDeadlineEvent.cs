using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConsoleApp1 {
    public sealed class TicketDeadlineEvent : IEvent {
        private static readonly object _runningDeadlinesLock = new object();
        private static readonly Dictionary<string, EventCanceller> _runningDeadlines = new Dictionary<string, EventCanceller>();

        public string TicketId { get; }
        public DateTime Time { get; }

        public TicketDeadlineEvent(DateTime deadline, string ticketId) {
            Time = deadline;
            TicketId = ticketId;
        }

        public async Task OnStarted(EventExecutor executor) {
            EventCanceller? oldCanceller;

            lock (_runningDeadlinesLock) {
                _runningDeadlines.TryGetValue(TicketId, out oldCanceller);

                // Get a canceller from the EventExecutor and update the index
                _runningDeadlines[TicketId] = executor.GetEventCanceller();
            }

            // old canceller cannot be awaited in the lock
            if (oldCanceller != null) {
                await oldCanceller.Cancel();
            }

            Console.WriteLine($"Set       Deadline of Ticket {TicketId} to  {Time}");
        }

        public Task OnTimeReached() {
            // This deadline is not running anymore, clean up from dictionary
            lock (_runningDeadlinesLock) {
                _runningDeadlines.Remove(TicketId);
            }

            // TODO
            // Look for Ticket in DB:
            // Is the deadline still the same?

            // If yes, stop the ticket, generate messages

            Console.WriteLine($"Reached   Deadline of Ticket {TicketId}     {Time}");
            return Task.CompletedTask;
        }

        public Task OnCancelled() {
            Console.WriteLine($"Cancelled Deadline of Ticket {TicketId} was {Time}");
            return Task.CompletedTask;
        }
    }
}
