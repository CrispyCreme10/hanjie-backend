using System.ComponentModel.DataAnnotations;

namespace Hanjie.Operations;
public class BoardCreationOptions
{
    const int MIN_ROW_COL = 3;
    const int MAX_ROW_COL = 50;
    const int MAX_HITS = MAX_ROW_COL * MAX_ROW_COL;

    [Required]
    [Range(MIN_ROW_COL, MAX_ROW_COL)]
    public int Rows { get; set; }

    [Required]
    [Range(MIN_ROW_COL, MAX_ROW_COL)]
    public int Cols { get; set; }

    [Range(0, MAX_HITS)]
    public int Hits { get; set; }

    [Range(0f, 1f)]
    public float HitsPercentage { get; set; }

}