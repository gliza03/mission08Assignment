namespace mission8Assignment.Models
{
    public class EfTaskRepository : ITaskRepository
    {
        private TaskContext _context;
        public EfTaskRepository(TaskContext temp)
        {
            _context = temp;
        }

        public List<Task> Tasks => _context.Tasks.ToList();
    }
}
