namespace IwantApp.Endpoints.Clients;

public record OrderRequest(List<Guid> ProductsId, string DeliveryAddress);