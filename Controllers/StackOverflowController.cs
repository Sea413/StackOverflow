using System.Threading.Tasks;
using System.Security.Claims;
using System.Linq;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Identity;
using StackOverflow.Models;

namespace BasicAuthentication.Controllers
{
    [Authorize]
    public class StackOverflowController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;

        public StackOverflowController(
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

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Question question)
        {
            var currentUser = await _userManager.FindByIdAsync(User.GetUserId());
            question.User = currentUser;
            _db.Questions.Add(question);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}