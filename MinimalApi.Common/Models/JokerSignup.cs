namespace MinimalApi.Common.Models;
public class JokerSignup
{
    public DateOnly? JokeDay { get; set; }
    public List<string> Participants { get; set; } = new List<string>();
}
