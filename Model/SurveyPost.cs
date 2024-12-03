namespace Forms.Model
{
    public class SurveyPost
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public List<QuestionPost> Questions { get; set; }
    }
}
