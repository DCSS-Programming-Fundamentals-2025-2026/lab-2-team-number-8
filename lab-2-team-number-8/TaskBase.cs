namespace lab_2_team_number_8
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