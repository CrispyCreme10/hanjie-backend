using System.Data;
using Dapper;
using Hanjie.Contexts;
using Hanjie.Models;

namespace Hanjie.Repositories;

public interface IHanjieRepository
{
    Task<IEnumerable<Cell>> GetBoardCells(string boardId);
    Task CreateCell(Cell cell);
    Task<bool> BoardExists(string boardId);
    Task<PostgresBoard> GetBoard(string boardId);
}

public class HanjieRepository : IHanjieRepository
{
    private MySqlDataContext _context;

    public HanjieRepository(MySqlDataContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Cell>> GetBoardCells(string boardId)
    {
        using var conn = _context.CreateConnection();
        var query = """
            SELECT *
            FROM board_cell
            WHERE board_id = @boardId
        """;
        return await conn.QueryAsync<Cell>(query, new { boardId });
    }

    public async Task CreateCell(Cell cell)
    {
        using var conn = _context.CreateConnection();
        var query = """
            INSERT INTO board_cell (board_id, row_no, col_no, value)
            VALUES (@BoardId, @Row, @Col, @Value)
        """;
        await conn.ExecuteAsync(query, cell);
    }

    public async Task<bool> BoardExists(string boardId)
    {
        using var conn = _context.CreateConnection();
        var query = """
            SELECT 1
            FROM board_cell
            WHERE board_id = @boardId
            LIMIT 1
        """;
        var test = await conn.QuerySingleOrDefaultAsync<bool?>(query, new { boardId });
        return test.HasValue && test.Value;
    }

    public Task<PostgresBoard> GetBoard(string boardId)
    {
        throw new NotImplementedException();
    }
}

public class HanjiePostgresRepository : IHanjieRepository
{
    private IDataContext _context;

    public HanjiePostgresRepository(IDataContext context)
    {
        _context = context;
    }

    public Task<bool> BoardExists(string boardId)
    {
        throw new NotImplementedException();
    }

    public Task CreateCell(Cell cell)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Cell>> GetBoardCells(string boardId)
    {
        throw new NotImplementedException();
    }

    public async Task<PostgresBoard> GetBoard(string boardId)
    {
        using var conn = _context.CreateConnection();
        var query = """
            SELECT *
            FROM board
            WHERE board_id = @boardId
        """;
        var test = await conn.QuerySingleOrDefaultAsync<PostgresBoard>(query, new { boardId });
        return test;
    }
}

public class GenericMultiArrayHandler<T> : SqlMapper.TypeHandler<T[,]>
{
    public override T[,] Parse(object value) => (T[,]) value;

    public override void SetValue(IDbDataParameter parameter, T[,] value) => parameter.Value = value;
}