using Flunt.Validations;

namespace IwantApp.Domain.Products;

public class Product : Entity
{
    public string Name { get; set; }
    public Guid CategoryId { get; set; }
    public Category Category { get; set; }
    public string Description { get; set; }
    public bool HasStock { get; set; }
    public bool Active { get; set; } = true;

    private Product() { }

    public Product(string name, Category category, string description, bool hasStock, string createdBy)
    {
        Name = name;
        Category = category;
        Description = description;
        HasStock = hasStock;

        CreatedBy = createdBy;
        EditedBy = createdBy;
        CreatedOn = DateTime.Now;
        EditedOn = DateTime.Now;

        Validate();

    }

    private void Validate()
    {
        var contract = new Contract<Product>()
            .IsNotNullOrEmpty(Name, nameof(Name))
            .IsGreaterOrEqualsThan(Name, 3, nameof(Name))
            .IsNotNull(Category, nameof(Category))
            .IsNotNullOrEmpty(Description, nameof(Description))
            .IsGreaterOrEqualsThan(Description, 3, nameof(Description))
            .IsNotNullOrEmpty(CreatedBy, nameof(CreatedBy))
            .IsNotNullOrEmpty(EditedBy, nameof(EditedBy));

        AddNotifications(contract);
    }
}
