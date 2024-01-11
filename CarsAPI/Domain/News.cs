namespace Domain;

public class News: BaseEntity
{
    public string Title { get; set; }

    public string ShortDescription { get; set; }

    public DateTime CreatedAt { get; set; }

    public string ContentHtml { get; set; }
}
