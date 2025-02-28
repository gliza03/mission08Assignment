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
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine(error.ErrorMessage);
                }
            }
            if (ModelState.IsValid)
            {
                _context.Tasks.Add(task);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Categories = _context.Categories.ToList();
            return View(task);
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

            return RedirectToAction("Task");
        }

        // Delete
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var deletedTask = _context.Tasks.Single(x => x.TaskId == id);
            return View(deletedTask);
        }

        [HttpPost]
        public IActionResult Delete(mission8Assignment.Models.Task deletedTask)
        {
            _context.Remove(deletedTask);
            _context.SaveChanges();

            return RedirectToAction("TaskList");
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
