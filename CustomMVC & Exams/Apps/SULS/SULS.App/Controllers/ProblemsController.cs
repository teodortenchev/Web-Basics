using SIS.MvcFramework;
using SIS.MvcFramework.Attributes;
using SIS.MvcFramework.Attributes.Security;
using SIS.MvcFramework.Result;
using SULS.App.ViewModels.Problems;
using SULS.App.ViewModels.Submissions;
using SULS.Services;
using System.Linq;

namespace SULS.App.Controllers
{
    public class ProblemsController : Controller
    {
        private readonly IProblemsService problemsService;
        private readonly ISubmissionsService submissionsService;

        public ProblemsController(IProblemsService problemsService, ISubmissionsService submissionsService)
        {
            this.problemsService = problemsService;
            this.submissionsService = submissionsService;
        }

        [Authorize]
        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        [Authorize]
        public IActionResult Create(ProblemInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.Redirect("/Problems/Create");
            }

            this.problemsService.CreateProblem(input.Name, input.Points);

            return this.Redirect("/");
        }

        [Authorize]
        public IActionResult Details(string id)
        {
            var submissions = submissionsService.GetSubmissionsById(id)
                .Select(x => new SubmissionViewModel
                {
                    Id = x.Id,
                    AchievedResult = x.AchievedResult,
                    MaxPoints = x.Problem.Points,
                    CreatedOn = x.CreatedOn.ToString("dd/MM/yyyy"),                    
                    Username = x.User.Username

                }).ToList();

            string problemName = problemsService.GetProblems().Where(x => x.Id == id).Select(x => x.Name).FirstOrDefault();

            if (problemName == null)
            {
                return this.Redirect("/");
            }


            return this.View(new SubmissionListViewModel { ProblemName = problemName, Submissions = submissions });
        }
    }
}
