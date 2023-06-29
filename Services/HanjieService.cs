using AutoMapper;
using Hanjie.Models;
using Hanjie.Operations;
using Hanjie.Repositories;

namespace Hanjie.Services;

public interface IHanjieService
{
    Task<PostgresBoard> GetBoard(string id);
    Task<PostgresBoard> CreateBoard(BoardCreationOptions opts);
    Task<bool> CheckValue(string boardId, int row, int col, int val);
    Task<bool> TrySaveBoard(PostgresBoard board);
}

public class HanjieService : IHanjieService
{
    private IHanjieRepository _hanjieRepository;
    private readonly IMapper _mapper;

    public HanjieService(IHanjieRepository hanjieRepository, IMapper mapper)
    {
        _hanjieRepository = hanjieRepository;
        _mapper = mapper;
    }

    public async Task<PostgresBoard> GetBoard(string id)
    {
        return await _hanjieRepository.GetBoard(id);
    }

    public async Task<bool> CheckValue(string boardId, int row, int col, int val)
    {
        return await _hanjieRepository.CheckValue(boardId, row, col, val);
    }

    public async Task<PostgresBoard> CreateBoard(BoardCreationOptions opts)
    {
        PostgresBoard board = BoardFactory.CreateRandomPostgresBoard(opts);
        await _hanjieRepository.CreateBoard(board);
        return board;
    }

    public async Task<bool> BoardExists(string id)
    {
        return await _hanjieRepository.BoardExists(id);
    }

    public async Task<bool> TrySaveBoard(PostgresBoard board)
    {
        if (await BoardExists(board.BoardId)) return false;
        await _hanjieRepository.CreateBoard(board);
        return true;
    }
}