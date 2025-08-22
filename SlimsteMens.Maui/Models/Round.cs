namespace SlimsteMens.Maui.Models;

public class Round
{
    public string Title { get; set; } = string.Empty;
    public List<Question> Questions { get; set; } = new();
}
