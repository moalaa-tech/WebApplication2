using System.ComponentModel.DataAnnotations;

namespace CRM.WebApp.DTOs.CustomerService.KBArticle
{
    public class KBArticleCommentCreateDto
    {
        [Required(ErrorMessage = "Commenter ID is required")]
        public int CommenterId { get; set; }

        [Required(ErrorMessage = "Comment text is required")]
        [StringLength(2000, ErrorMessage = "Comment cannot exceed 2000 characters")]
        public string CommentText { get; set; }

        public bool IsHelpful { get; set; }
    }
}
