namespace BeajLearner.WebApp.Models.Users
{
    public class User
    {
        public bool succeeded { get; set; }
        public object message { get; set; }
        public object errors { get; set; }
        public Datum[] data { get; set; }
    }
}
