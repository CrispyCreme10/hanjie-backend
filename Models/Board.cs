namespace Hanjie.Models;

public class Board
{
    public string Id { get; set; } = "";
    public List<Cell> Cells { get; set; } = new List<Cell>();
}

public class PostgresBoard
{
    public string BoardId { get; set; } = "";
    public int[,]? Cells { get; set; }
}