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

    public string FormatCells()
    {
        string output = "{";
        for (int x = 0; x < Cells?.GetLength(0); x++) {
            output += "{";
            for (int y = 0; y < Cells.GetLength(1); y++) {
                output += Cells[x, y];
                if (y < Cells.GetLength(1) - 1) {
                    output += ",";
                }
            }
            output += "}";
        }
        return output + "}";
    }
}