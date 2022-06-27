using IwantApp.Domain.Orders;
using IwantApp.Domain.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace IwantApp.Endpoints.Clients;

public class OrderPost
{
    public static string Template => "/order";
    public static string[] Methods => new string[] { HttpMethod.Post.ToString() };
    public static Delegate Handle => Action;

    [Authorize(policy: "CpfPolicy")]
    public static async Task<IResult> Action(OrderRequest orderRequest, HttpContext httpContext, ApplicationDbContext context)
    {
        var clientId = httpContext.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
        var clientName = httpContext.User.Claims.First(c => c.Type == "Name").Value;

        if (orderRequest.ProductsId == null || !orderRequest.ProductsId.Any())
            return Results.BadRequest("Products is necessary for the order");

        var products = context.Products.Where(p => orderRequest.ProductsId.Contains(p.Id)).ToList();
        var order = new Order(clientId, clientName, products, orderRequest.DeliveryAddress);

        await context.Orders.AddAsync(order);
        await context.SaveChangesAsync();

        return Results.Created($"/order/{order.Id}", order.Id);
    }
}
