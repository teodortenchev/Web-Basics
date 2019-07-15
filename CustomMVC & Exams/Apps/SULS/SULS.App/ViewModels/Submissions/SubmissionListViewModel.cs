using System.Collections.Generic;

namespace SULS.App.ViewModels.Submissions
{
    public class SubmissionListViewModel
    {
        public string ProblemName { get; set; }
        public IEnumerable<SubmissionViewModel> Submissions { get; set; }
    }
}
