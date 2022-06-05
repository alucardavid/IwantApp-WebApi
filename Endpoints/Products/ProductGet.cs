using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IwantApp.Endpoints.Products;

public class ProductGet
{
    public static string Template => "/products/{id:guid}";
    public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
    public static Delegate Handle => Action;

    [Authorize(Policy = "EmployeePolicy")]
    public static async Task<IResult> Action([FromRoute] Guid id, ApplicationDbContext context)
    {
        var product = context.Products.Include(p => p.Category).Where(p => p.Id == id)
            .Select(p => new ProductResponse(p.Name, p.Category.Name, p.Description, p.HasStock, p.Active));
        
        return Results.Ok(product);
    }
}

