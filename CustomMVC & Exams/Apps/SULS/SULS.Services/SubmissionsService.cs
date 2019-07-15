using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using SULS.Data;
using SULS.Models;

namespace SULS.Services
{
    public class SubmissionsService : ISubmissionsService
    {
        private readonly SULSContext db;

        public SubmissionsService(SULSContext db)
        {
            this.db = db;
        }

        public void CreateSubmission(string code, string problemId, string userId)
        {
            int? maxPoints = this.db.Problems.Where(x => x.Id == problemId).Select(x => x.Points).FirstOrDefault();

            if(maxPoints == null)
            {
                return;
            }

            Random random = new Random();

            int submissionScore = random.Next(0, (int)maxPoints);

            var submission = new Submission
            {
                Code = code,
                AchievedResult = submissionScore,
                CreatedOn = DateTime.UtcNow,
                ProblemId = problemId,
                UserId = userId
            };

            this.db.Submissions.Add(submission);
            this.db.SaveChanges();
        }

        public void DeleteSubmission(string id)
        {
            var submission = this.db.Submissions.Find(id);

            if(submission == null)
            {
                return;
            }

            this.db.Submissions.Remove(submission);

            this.db.SaveChanges();
        }

        public IEnumerable<Submission> GetAllSubmissions()
        {
            var submissions = this.db.Submissions.ToList();
            return submissions;
        }
        public IQueryable<Submission> GetSubmissionsById(string problemId)
        {
            var submissions = this.db.Submissions.Where(x => x.ProblemId == problemId).Include(x => x.Problem).Include(x => x.User).AsQueryable();

            return submissions;
        }

        
    }
}
