using SIS.MvcFramework;
using SIS.MvcFramework.Attributes;
using SIS.MvcFramework.Result;
using SULS.App.ViewModels.Problems;
using SULS.App.ViewModels.Submissions;
using SULS.Services;
using System.Linq;

namespace SULS.App.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProblemsService problemsService;
        private readonly ISubmissionsService submissionsService;

        public HomeController(IProblemsService problemsService, ISubmissionsService submissionsService)
        {
            this.problemsService = problemsService;
            this.submissionsService = submissionsService;
        }

        [HttpGet(Url = "/")]
        public IActionResult IndexSlash()
        {
            return this.Index();
        }

        // /Home/Index
        public IActionResult Index()
        {
            if (this.IsLoggedIn())
            {
                var submissions = submissionsService.GetAllSubmissions();

                var problems = problemsService.GetProblems()
                    .Select(x => new ProblemViewModel
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Count = submissions.Where(s => s.ProblemId == x.Id).Count()
                    }).ToList();

                return this.View(new ProblemListViewModel { Problems = problems }, "IndexLoggedIn");
            }
            else
            {
                return this.View();
            }
        }
    }
}