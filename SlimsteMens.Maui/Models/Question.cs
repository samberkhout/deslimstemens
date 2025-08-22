namespace SlimsteMens.Maui.Models;

public class Question
{
    public string Text { get; set; } = string.Empty;
    public List<AnswerOption> Options { get; set; } = new();
    public int CorrectIndex { get; set; }
}
