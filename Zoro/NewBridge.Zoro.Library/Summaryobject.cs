namespace NewBridge.Zoro.Library { 
public class Summaryobject
{
    public Rating rating { get; set; }
    public int reviews_count { get; set; }
    public int wish_count { get; set; }
    public string douban_site { get; set; }
    public string year { get; set; }
    public Images images { get; set; }
    public string alt { get; set; }
    public string id { get; set; }
    public string mobile_url { get; set; }
    public string title { get; set; }
    public object do_count { get; set; }
    public string share_url { get; set; }
    public object seasons_count { get; set; }
    public string schedule_url { get; set; }
    public object episodes_count { get; set; }
    public string[] countries { get; set; }
    public string[] genres { get; set; }
    public int collect_count { get; set; }
    public Cast[] casts { get; set; }
    public object current_season { get; set; }
    public string original_title { get; set; }
    public string summary { get; set; }
    public string subtype { get; set; }
    public Director[] directors { get; set; }
    public int comments_count { get; set; }
    public int ratings_count { get; set; }
    public object[] aka { get; set; }
}
}