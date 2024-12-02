namespace Forms.Model
{
    public class Answer
    {
        public Demographics Respondent { get; set; }
        public Dictionary<string, object> Answers { get; set; } // (Key = QuestionId, Value = (string/list<T>))
        public DateTime SubmittedAt { get; set; } 
    }

}
