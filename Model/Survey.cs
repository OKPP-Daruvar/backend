namespace Forms.Model
{
    public class Survey
    {
        public string Id { get; set; } 
        public string UserId { get; set; } 
        public string Title { get; set; } 
        public string Description { get; set; } 
        public List<Question> Questions { get; set; }
        public DateTime CreatedAt { get; set; }
    }

}
