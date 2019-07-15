using SULS.Models;
using System.Collections.Generic;

namespace SULS.App.ViewModels.Problems
{
    public class ProblemListViewModel
    {
        public IEnumerable<ProblemViewModel> Problems { get; set; }
    }
}
