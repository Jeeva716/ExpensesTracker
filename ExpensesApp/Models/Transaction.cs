using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;



namespace ExpensesApp.Models
{
    public class Transaction
    {
        [Key]
        public int TeansactionId { get; set; }

        [Range(1,int.MaxValue,ErrorMessage ="Please  select a category.")]
        public int CategoryId { get; set; }
        public Category? Category { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Amount should be greater than 0.")]
        public int Amount { get; set; }
        [Column(TypeName = "nvarchar(75)")]
        public string? Note { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;

        [NotMapped]
        public string? CategoryTitle
        {

            get
            {
                return Category == null ? "" : Category.Title;
            }

        }
    }
}
