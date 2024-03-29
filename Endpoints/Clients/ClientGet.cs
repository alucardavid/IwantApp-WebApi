﻿using IwantApp.Domain.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace IwantApp.Endpoints.Clients;

public class ClientGet
{
    public static string Template => "/client";
    public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
    public static Delegate Handle => Action;

    [AllowAnonymous]
    public static async Task<IResult> Action(HttpContext http)
    {
        var user = http.User;
        var result = new
        {
            Id = user.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value,
            Name = user.Claims.First(c => c.Type == "Name").Value,
            CPF = user.Claims.First(c => c.Type == "Cpf").Value
        };

        return Results.Ok(result);

    }
}
