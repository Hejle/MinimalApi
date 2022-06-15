using MinimalApi.Common.Models;
using MinimalApi.Database.Context;

namespace MinimalApi.Database.Services;

public interface IJokersDataAccess
{
    Joker? GetJokerByDate(DateOnly dateOnly);

    void CreateJoker(Joker joker);
}

public class JokersDataAccess : IJokersDataAccess
{
    private readonly JokerContext _context;

    public JokersDataAccess(JokerContext jokerContext)
    {
        _context = jokerContext;
    }

    public void CreateJoker(Joker joker)
    {
        _context.Add(joker);
        _context.SaveChanges();
    }

    public Joker? GetJokerByDate(DateOnly dateOnly)
    {
        var joker = _context.Jokers.FirstOrDefault(joke => joke.JokeDay == dateOnly);
        return joker;
    }
}