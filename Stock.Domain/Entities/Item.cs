namespace Stock.Domain.Entities;
public class Item
{
    public Item()
    {
        StoreItems = new HashSet<StoreItem>();
    }
    public Guid Id { get; set; }
    public string Name { get; set; }
    public virtual ICollection<StoreItem> StoreItems { get; set; }
}
