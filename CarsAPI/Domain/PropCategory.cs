namespace Domain;

public class PropCategory: BaseEntity
{
    public string Name { get; set; }

    public int Priority { get; set; }

    public virtual List<Property>? Properties { get; set; }
}
