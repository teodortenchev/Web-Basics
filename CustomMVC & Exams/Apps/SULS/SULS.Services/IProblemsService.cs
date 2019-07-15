using SULS.Models;
using System.Linq;

namespace SULS.Services
{
    public interface IProblemsService
    {
        IQueryable<Problem> GetProblems();
        /// <summary>
        /// Returns the count of submissions associated with the problem id
        /// </summary>
        /// <returns></returns>
        int GetSubmissionsCount(string problemId);

        void CreateProblem(string name, int points);
    }
}
