namespace NewBridge.Zoro.Library
{
    public class Rootobject
    {
        public int count { get; set; }
        public int start { get; set; }
        public int total { get; set; }
        public Subject[] subjects { get; set; }
        public string title { get; set; }
    }

    public class Subject
    {
        public Rating rating { get; set; }
        public string[] genres { get; set; }
        public string title { get; set; }
        public Cast[] casts { get; set; }
        public int collect_count { get; set; }
        public string original_title { get; set; }
        public string subtype { get; set; }
        public Director[] directors { get; set; }
        public string year { get; set; }
        public Images images { get; set; }
        public string alt { get; set; }
        public string id { get; set; }
    }

    public class Rating
    {
        public int max { get; set; }
        public float average { get; set; }
        public string stars { get; set; }
        public int min { get; set; }
    }

    public class Images
    {
        public string small { get; set; }
        public string large { get; set; }
        public string medium { get; set; }
    }

    public class Cast
    {
        public string alt { get; set; }
        public Avatars avatars { get; set; }
        public string name { get; set; }
        public string id { get; set; }
    }

    public class Avatars
    {
        public string small { get; set; }
        public string large { get; set; }
        public string medium { get; set; }
    }

    public class Director
    {
        public string alt { get; set; }
        public Avatars avatars { get; set; }
        public string name { get; set; }
        public string id { get; set; }
    }
}