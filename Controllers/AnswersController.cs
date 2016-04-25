using System.Threading.Tasks;
using System.Security.Claims;
using System.Linq;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Identity;
using StackOverflow.Models;
using Microsoft.Data.Entity;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace StackOverflow.Controllers
{
    [Authorize]
    public class AnswersController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;

        public AnswersController(
            UserManager<ApplicationUser> userManager,
            ApplicationDbContext db
        )
        {
            _userManager = userManager;
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Create(int id)
        {
            ViewData["questionId"] = id;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Answer answer)
        {
            var currentUser = await _userManager.FindByIdAsync(User.GetUserId());
            answer.User = currentUser;
            _db.Answers.Add(answer);
            _db.SaveChanges();
            return RedirectToAction("Details", "Questions");
        }
    }
}
