using MinimalApi.Common.Models;
using MinimalApi.Database.Services;

namespace MinimalApi.Logic.Services;

public interface IJokeService
{
    Joker? GetTodaysJoker();

    Joker? GetJokerByDate(DateOnly dateOnly);

    Joker CreateJoker(JokerSignup jokerSignup);

}

internal class JokeService : IJokeService
{
    private readonly Random _random;
    private readonly IJokersDataAccess _jokersDataAccess;

    public JokeService(IJokersDataAccess jokersDataAccess)
    {
        _random = new Random();
        _jokersDataAccess = jokersDataAccess;
    }

    public Joker CreateJoker(JokerSignup jokerSignup)
    {
        if (jokerSignup?.Participants == null || jokerSignup?.Participants.Count == 0)
        {
            throw new MinimalApiException("There must be participants to choose a new joker.");
        }
        int index = _random.Next(jokerSignup!.Participants.Count);
        var nextJoker = jokerSignup.Participants[index];
        DateOnly jokeDay = GetDateForNextJoke(jokerSignup);

        var joker = new Joker
        {
            CreatedDate = DateOnly.FromDateTime(DateTime.Now),
            JokeDay = jokeDay,
            JokerName = nextJoker
        };
        _jokersDataAccess.CreateJoker(joker);
        return joker;
    }

    public Joker? GetJokerByDate(DateOnly dateOnly)
    {
        return _jokersDataAccess.GetJokerByDate(dateOnly);
    }

    public Joker? GetTodaysJoker()
    {
        var dateOnly = DateOnly.FromDateTime(DateTime.Now);
        return _jokersDataAccess.GetJokerByDate(dateOnly);
    }

    private static DateOnly GetDateForNextJoke(JokerSignup jokerSignup)
    {
        DateOnly jokeDay;
        
        if (jokerSignup.JokeDay.HasValue)
        {
            jokeDay = jokerSignup.JokeDay.Value;
        }
        else
        {
            var now = DateTime.Now;
            jokeDay = DateOnly.FromDateTime(DateTime.Today).AddDays(1);
            if (now.DayOfWeek == DayOfWeek.Friday)
            {
                jokeDay.AddDays(2);
            }
        }

        return jokeDay;
    }
}
