namespace Hanjie.Models;

public class Cell
{
    public string BoardId { get; set; } = "";
    public int Row { get; set; } = -1;
    public int Col { get; set; } = -1;
    public int Value { get; set; } = -10;
}