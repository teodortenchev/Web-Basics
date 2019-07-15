using SULS.Data;
using SULS.Models;
using System.Linq;

namespace SULS.Services
{
    public class ProblemsService : IProblemsService
    {
        private readonly SULSContext db;
        public ProblemsService(SULSContext db)
        {
            this.db = db;
        }

        public int GetSubmissionsCount(string problemId)
        {
            int count = 0;//this.db.Submissions.Where(x => x.ProblemId == problemId).Count();

            return count;
        }

        public IQueryable<Problem> GetProblems()
        {
            var problems = this.db.Problems.AsQueryable();

            return problems;
        }

        public void CreateProblem(string name, int points)
        {
            var problem = new Problem
            {
                Name = name,
                Points = points
            };

            this.db.Problems.Add(problem);
            this.db.SaveChanges();
        }
    }
}
