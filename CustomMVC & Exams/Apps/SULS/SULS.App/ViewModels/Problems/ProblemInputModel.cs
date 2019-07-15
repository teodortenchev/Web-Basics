using SIS.MvcFramework.Attributes.Validation;

namespace SULS.App.ViewModels.Problems
{
    public class ProblemInputModel
    {
        [RequiredSis]
        [StringLengthSis(5, 20, "Username should be between 5 and 20 characters")]
        public string Name { get; set; }

        [RequiredSis]
        [RangeSis(50, 300, "Points are required. Can be between 50 and 300")]
        public int Points { get; set; }
    }
}
