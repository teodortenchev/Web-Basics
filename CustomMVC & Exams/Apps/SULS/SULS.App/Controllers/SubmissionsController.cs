using SIS.MvcFramework;
using SIS.MvcFramework.Attributes;
using SIS.MvcFramework.Attributes.Security;
using SIS.MvcFramework.Result;
using SULS.App.ViewModels.Submissions;
using SULS.Services;
using System.Linq;

namespace SULS.App.Controllers
{
    public class SubmissionsController : Controller
    {
        private readonly ISubmissionsService submissionsService;
        private readonly IProblemsService problemsService;


        public SubmissionsController(ISubmissionsService submissionsService, IProblemsService problemsService)
        {
            this.submissionsService = submissionsService;
            this.problemsService = problemsService;
        }

        [Authorize]
        public IActionResult Create(string id)
        {
            string problemName = problemsService.GetProblems().Where(x => x.Id == id).Select(x => x.Name).FirstOrDefault();
            
            if (problemName == null)
            {
                return this.Redirect("/");
            }

            return this.View(new SubmissionCreationModel { ProblemId = id, ProblemName = problemName });
        }

        [HttpPost]
        [Authorize]
        public IActionResult Create(SubmissionInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.Redirect($"/Submissions/Create?id={input.ProblemId}&userId={input.UserId}");
            }

            submissionsService.CreateSubmission(input.Code, input.ProblemId, input.UserId);

            return this.Redirect("/");
        }

        [Authorize]
        public IActionResult Delete(string id)
        {
            submissionsService.DeleteSubmission(id);

            return this.Redirect("/");
        }
    }
}
