using Flunt.Validations;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace IwantApp.Domain.Products;

public class Category : Entity
{
    public string Name { get; private set; }
    public bool Active { get; private set; }

    public Category(string name, string createdBy, string editedBy)
    {
        Name = name;
        Active = true;
        CreatedBy = createdBy;
        EditedBy = editedBy;
        CreatedOn = DateTime.Now;
        EditedOn = DateTime.Now;

        Validate();
    }

    private void Validate()
    {
        var contract = new Contract<Category>()
                    .IsNotNullOrEmpty(Name, "Name")
                    .IsGreaterOrEqualsThan(Name, 3, "Name")
                    .IsNotNull(Active, "Active")
                    .IsNotNullOrEmpty(CreatedBy, "createdBy")
                    .IsNotNullOrEmpty(EditedBy, "editedBy");
        AddNotifications(contract);
    }

    public void EditInfo(string name, bool active, string editedBy) {
        Active = active;
        Name = name;
        EditedBy = editedBy;
        EditedOn = DateTime.Now;

        Validate();
    }

}
