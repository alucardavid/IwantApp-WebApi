using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Dynamic.Core;

namespace IwantApp.Endpoints.Products;

public class ProductGetShowcase
{
    public static string Template => "/products/showcase";
    public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
    public static Delegate Handle => Action;

    [AllowAnonymous]
    public static async Task<IResult> Action(ApplicationDbContext context, int page = 1, int row = 10, string orderBy = "name")
    {
        string[] paramsOrderBy = new string[] { "name", "price", };

        if (row > 10)
            return Results.Problem(title: "Row with max 10", statusCode: 400);

        if (!paramsOrderBy.Contains(orderBy))
            return Results.Problem(title: "This field to order is not exist!", statusCode: 400);

        var products = context.Products
            .AsNoTracking()
            .Include(p => p.Category)
            .Where(p => p.HasStock && p.Category.Active)
            .OrderBy(orderBy)
            .Skip((page - 1) * row)
            .Take(row);

        var results = products.Select(p => new ProductResponse(p.Id, p.Name, p.Category.Name, p.Description, p.HasStock, p.Price, p.Active));

        return Results.Ok(results);

    }
}

