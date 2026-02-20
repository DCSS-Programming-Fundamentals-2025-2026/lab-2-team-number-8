namespace lab_2_team_number_8
{
    public class Delivery : TaskBase, IRoutable, IComparable
    {
        public string Address { get; set; }
        public PriorityLevel Priority { get; set; }
        public Status Status { get; set; }

        public Delivery(int id, string title, string address, PriorityLevel priority) 
            : base(id, title)
        {
            Address = address;
            Priority = priority;
            Status = Status.Pending;
        }

        public int GetPriorityKey() => (int)Priority;
        
        public void Update(string title, string address, PriorityLevel p)
        {
            Title = title;
            Address = address;
            Priority = p;
        }

        public void UpdateStatus(Status s) => Status = s;

        public override string ToString() => $"[{Id}] {Title} ({Address}) - {Status} [{Priority}]";

        public int CompareTo(object? obj)
        {
            if (obj is not Delivery other)
            {
                throw new ArgumentException("При порівнянні виникла проблема з типом даних елемента");
            }

            return Id.CompareTo(other.Id);
        }
    }
}