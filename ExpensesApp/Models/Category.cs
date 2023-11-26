using Microsoft.CodeAnalysis.Scripting;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExpensesApp.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }

        [Column(TypeName ="nvarchar(50)")]
        [Required(ErrorMessage="Title is required.")]
        public String Title { get; set; }
      
    }
}
