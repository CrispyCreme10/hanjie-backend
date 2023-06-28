using System.Security.Cryptography;
using System.Text;
using Hanjie.Contexts;
using Hanjie.Models;

namespace Hanjie.Operations;

public static class BoardFactory
{
    public static Board CreateRandomBoard(BoardCreationOptions opts)
    {
        Board board = new Board();
        Random rnd = new Random();

        float hitPercent = opts.HitsPercentage;
        if (opts.Hits > 0) hitPercent = (float)opts.Hits / (opts.Rows * opts.Cols);
        if (hitPercent == 0) return board;

        for (int rowNo = 0; rowNo < opts.Rows; rowNo++)
        {
            for (int colNo = 0; colNo < opts.Cols; colNo++)
            {
                board.Cells.Add(new Cell
                {
                    Row = rowNo,
                    Col = colNo,
                    Value = rnd.NextDouble() < hitPercent ? 1 : -1
                });
            }
        }

        board.Id = CreateBoardHash(board);
        return board;
    }

    public static string CreateBoardHash(Board board)
    {
        string flatBoard = string.Join("", board.Cells.OrderBy(c => c.Row).ThenBy(c => c.Col).Select(c => c.Value));
        return GetHashString(flatBoard);
    }

    private static byte[] GetHash(string input)
    {
        using (HashAlgorithm algorithm = SHA256.Create())
            return algorithm.ComputeHash(Encoding.UTF8.GetBytes(input));
    }

    private static string GetHashString(string input)
    {
        StringBuilder sb = new StringBuilder();
        foreach (byte b in GetHash(input))
            sb.Append(b.ToString("X2"));

        return sb.ToString();
    }
}