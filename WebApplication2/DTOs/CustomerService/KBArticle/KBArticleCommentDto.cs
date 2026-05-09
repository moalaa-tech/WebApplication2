namespace CRM.WebApp.DTOs.CustomerService.KBArticle
{
    public class KBArticleCommentDto
    {
        public int Id { get; set; }
        public int CommenterId { get; set; }
        public string CommenterName { get; set; }
        public string CommenterAvatarUrl { get; set; }
        public string CommentText { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsHelpful { get; set; }
        public int HelpfulVotes { get; set; }
        public int UnhelpfulVotes { get; set; }
        public bool IsAuthorResponse { get; set; }
    }
}
