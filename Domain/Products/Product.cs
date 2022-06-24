using Flunt.Validations;

namespace IwantApp.Domain.Products;

public class Product : Entity
{
    public string Name { get; private set; }
    public Guid CategoryId { get; private set; }
    public Category Category { get; private set; }
    public string Description { get; private set; }
    public bool HasStock { get; private set; }
    public bool Active { get; private set; } = true;
    public decimal Price { get; private set; } 

    private Product() { }

    public Product(string name, Category category, string description, bool hasStock, decimal price, string createdBy)
    {
        Name = name;
        Category = category;
        Description = description;
        HasStock = hasStock;
        Price = price;

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
            .IsNotNull(Category,nameof(Category), "Category not found")
            .IsNotNullOrEmpty(Description, nameof(Description))
            .IsGreaterOrEqualsThan(Description, 3, nameof(Description))
            .IsGreaterOrEqualsThan(Price, 1, nameof(Price)) 
            .IsNotNullOrEmpty(CreatedBy, nameof(CreatedBy))
            .IsNotNullOrEmpty(EditedBy, nameof(EditedBy));

        AddNotifications(contract);
    }
}
