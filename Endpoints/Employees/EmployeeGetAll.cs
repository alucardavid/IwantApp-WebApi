using IwantApp.Infra.Data;
using Microsoft.AspNetCore.Authorization;

namespace IwantApp.Endpoints.Employees;

public class EmployeeGetAll
{
    public static string Template => "/employees";
    public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
    public static Delegate Handle => Action;

    [Authorize(Policy = "Employee005Policy")]
    public static IResult Action(int? page, int? rows, QueryAllUsersWithClaimName query)
    {
        if (page == null)
            return Results.BadRequest("O numero da pagina nao foi inclusa na request.");

        if(rows == null)
            return Results.BadRequest("O numero de linhas nao foi incluso na request.");

        if (rows > 10)
            return Results.BadRequest("Permitido somente 10 linhas para exibicao");

        return Results.Ok(query.Execute(page.Value, rows.Value));
    }
}
