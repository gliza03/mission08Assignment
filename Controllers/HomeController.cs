using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using mission8Assignment.Models;
using Microsoft.EntityFrameworkCore;


namespace mission8Assignment.Controllers
{
    public class HomeController : Controller
    {
        private TaskContext _context;

        public HomeController(TaskContext temp)
        {
            _context = temp;
        }

        public IActionResult Index()
        {
            return View();
        }

        // Form to submit tasks
        [HttpGet]
        public IActionResult Task()
        {
            ViewBag.Categories = _context.Categories.ToList();
            return View(new mission8Assignment.Models.Task());
        }
        [HttpPost]
        public IActionResult Task(mission8Assignment.Models.Task task)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = _context.Categories.ToList();
                return View(task);
            }

            if (task.TaskId == 0)  // New Task (Insert)
            {
                _context.Tasks.Add(task);
            }
            else  // Existing Task (Update)
            {
                var existingTask = _context.Tasks.Find(task.TaskId);
                if (existingTask != null)
                {
                    _context.Entry(existingTask).CurrentValues.SetValues(task);
                }
            }

            _context.SaveChanges();
            return RedirectToAction("Quadrants");
        }

        // Show task list

        // Edit
        [HttpGet]
        public IActionResult EditTask(int id)
        {
            var editedTask = _context.Tasks.Single(x => x.TaskId == id);
            ViewBag.Categories = _context.Categories.ToList();
            return View("Task", editedTask);
        }
        [HttpPost]
        public IActionResult EditTask(mission8Assignment.Models.Task editedTask)
        {
            _context.Update(editedTask);
            _context.SaveChanges();

            return RedirectToAction("Quadrants");
        }

        // Delete
        [HttpGet]
        // GET: Home/Delete/5
        public IActionResult Delete(int id)
        {
            var task = _context.Tasks.FirstOrDefault(t => t.TaskId == id);
            if (task == null)
            {
                return NotFound();
            }

            return View(task); // Pass the task to the view
        }

// POST: Home/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var task = _context.Tasks.FirstOrDefault(t => t.TaskId == id);
            if (task != null)
            {
                _context.Tasks.Remove(task);
                _context.SaveChanges();
            }

            return RedirectToAction("Quadrants"); // Redirect to the Quadrants page after deletion
        }

        // Quadrants view
        public IActionResult Quadrants()
        {
            // Include the Category table to load Category data along with the tasks
            var tasks = _context.Tasks
                .Include(t => t.Category) // This line adds the join to Category table
                .Where(t => !t.Completed)  // Only get tasks that are not completed
                .ToList();

            var viewModel = new QuadrantsViewModel
            {
                QuadrantI = tasks.Where(t => t.Quadrant == "Important / Urgent").ToList(),
                QuadrantII = tasks.Where(t => t.Quadrant == "Important / Not Urgent").ToList(),
                QuadrantIII = tasks.Where(t => t.Quadrant == "Not Important / Urgent").ToList(),
                QuadrantIV = tasks.Where(t => t.Quadrant == "Not Important / Not Urgent").ToList(),
            };

            return View(viewModel);
        }


        // Mark task as completed
        [HttpPost]
        public IActionResult MarkComplete(int id)
        {
            var task = _context.Tasks.FirstOrDefault(t => t.TaskId == id);
            if (task != null)
            {
                task.Completed = true;
                _context.SaveChanges();
            }
            return RedirectToAction("Quadrants");
        }




    }
}
