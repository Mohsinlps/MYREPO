namespace BeajLearner.WebApi.Helpers
{
    public class PaginationParameters : QueryStringParameters
    {
        public string Keyword { get; set; }
        public string OrderBy { get; set; }
        public bool Ascending { get; set; }
    }
}
