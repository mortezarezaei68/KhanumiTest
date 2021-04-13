using Framework.Commands;

namespace TicketManagement.Commands
{
    public class TicketSmsEvent:Event
    {
    
        
        public string TrackingCode { get; private set; }

        public TicketSmsEvent(string exchangeName, string trackingCode) : base(exchangeName)
        {
            TrackingCode = trackingCode;
        }
    }
}