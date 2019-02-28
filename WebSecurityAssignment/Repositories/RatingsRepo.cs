using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebSecurityAssignment.Data;

namespace WebSecurityAssignment.Repositories
{
    public class RatingsRepo
    {
        ApplicationDbContext _context;

        public RatingsRepo(ApplicationDbContext context)
        {
            this._context = context;
        }

        public List<Ratings> GetAllRatings()
        {
            var ratings = _context.Ratings;
            List<Ratings> ratingList = new List<Ratings>();

            foreach (var item in ratings)
            {
                ratingList.Add(new Ratings() {
                    employeeID = item.employeeID,
                    jobID = item.jobID,
                    score = item.score,
                    review = item.review});
            }
            return ratingList;
        }

        public Ratings GetRating(string employeeID, int jobID)
        {
            var rating = _context.Ratings.Where(r => r.employeeID == employeeID && r.jobID == jobID).FirstOrDefault();
            if (rating != null)
            {
                return new Ratings() {
                    employeeID = rating.employeeID,
                    jobID = rating.jobID,
                    score = rating.score,
                    review = rating.review
                                       };
            }
            return null;
        }

        public bool RemoveRating(string employeeID, int jobID)
        {
            var rating = _context.Ratings.Where(r => r.employeeID == employeeID && r.jobID == jobID).FirstOrDefault();

            _context.Ratings.Remove(rating);
            _context.SaveChanges();
            return true;
        }

        public bool UpdateRating(int jobID, string employeeID, string review, float score)
        {
            var rating = _context.Ratings.Where(r => r.employeeID == employeeID && r.jobID == jobID).FirstOrDefault();
            // Remember you can't update the primary key without 
            // causing trouble.  Just update the review and score
            // for now.
            rating.score = score;
            rating.review = review;

            _context.SaveChanges();
            return true;
        }

        public bool CreateRating(int jobID, string employeeID, string review, float score)
        {
            var rating = GetRating(employeeID, jobID);
            if (rating != null)
            {
                return false;
            }
            _context.Ratings.Add(new Ratings
            {
                jobID = jobID,
                employeeID = employeeID,
                review = review,
                score = score
            });
            _context.SaveChanges();
            return true;
        }
    }
}
