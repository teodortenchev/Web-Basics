using SULS.Models;
using System.Collections.Generic;
using System.Linq;

namespace SULS.Services
{
    public interface ISubmissionsService
    {
        IEnumerable<Submission> GetAllSubmissions();
        IQueryable<Submission> GetSubmissionsById(string problemId);

        void CreateSubmission(string code, string problemId, string userId);

        void DeleteSubmission(string id);
    }
}
