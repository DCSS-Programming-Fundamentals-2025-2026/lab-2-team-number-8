namespace DeliveryRoutePlanner
{
    public abstract class TaskBase
    {
        public int Id { get; set; }
        public string Title { get; set; }

        protected TaskBase(int id, string title)
        {
            Id = id;
            Title = title;
        }
    }
}