using FluentValidation;
using MinimalApi.Common.Models;
using MinimalApi.Logic.Services;

namespace MinimalApi.Endpoints;

public static class JokesEndpoint
{
    private const string DefaultPath = "jokester";
    public static void UseJokeEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapGet(DefaultPath, GetTodaysJoke)
            .Produces<Joker>(200).Produces(404)
            .WithName("GetJokester")
            .AllowAnonymous();

        app.MapGet($"{DefaultPath}/{{date}}", GetJokerForDay)
            .Produces<Joker>(200).Produces(404)
            .WithName("GetJokesterForDay")
            .AllowAnonymous();

        app.MapPost(DefaultPath, CreateNextJoker)
            .Accepts<JokerSignup>("application/json")
            .Produces<string>(201)
            .WithName("GetNextJokester")
            .AllowAnonymous();
    }


    private static IResult GetTodaysJoke(IJokeService jokeService)
    {
        var joker = jokeService.GetTodaysJoker();
        if (joker == null)
        {
            return Results.NotFound();
        }
        return Results.Ok(joker.JokerName);
    }

    private static IResult GetJokerForDay(string date, IJokeService jokeService)
    {
        var joker = jokeService.GetJokerByDate(DateOnly.Parse(date));
        if (joker == null)
        {
            return Results.NotFound();
        }
        return Results.Ok(joker.JokerName);
    }

    private static IResult CreateNextJoker(JokerSignup jokerSignup, IJokeService jokeService)
    {
        try
        {
            var joker = jokeService.CreateJoker(jokerSignup);
            return Results.Ok(joker.JokerName);
        }
        catch (Exception e)
        {
            return Results.BadRequest(e);
        }
    }
}