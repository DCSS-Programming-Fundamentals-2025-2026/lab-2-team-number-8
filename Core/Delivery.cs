namespace DeliveryRoutePlanner.Core
{
    public class Delivery : TaskBase, IRoutable
    {
        public string Address { get; set; }
        public PriorityLevel Priority { get; set; }
        public Status Status { get; set; }

        public Delivery(int id, string title, string address, PriorityLevel priority) : base(id, title)
        {
            Address = address;
            Priority = priority;
            Status = Status.Pending;
        }

        public int GetPriorityKey()
        {
            return (int)Priority;
        }

        public void Update(string title, string address, PriorityLevel p)
        {
            Title = title;
            Address = address;
            Priority = p;
        }

        public void UpdateStatus(Status s)
        {
            Status = s;
        }

        public override string ToString()
        {
            return $"[{Id}] {Title} ({Address}) - {Status} [{Priority}]";
        }
    }
}