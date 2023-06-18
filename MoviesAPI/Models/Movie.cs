using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoviesAPI.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageAdress { get; set; }
        public string Description { get; set; }
        public int Duration { get; set; }
        public enum ClassificationType
        {
            Seven, Thirteen, Sixteen, Eighteen
        }
        public ClassificationType Classification { get; set; }
        public DateTime InitialDate { get; set; }

        [ForeignKey("CategoryId")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
