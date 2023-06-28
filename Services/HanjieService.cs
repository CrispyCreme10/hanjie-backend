using AutoMapper;
using Hanjie.Models;
using Hanjie.Repositories;

namespace Hanjie.Services;

public interface IHanjieService
{
    Task<Board> GetBoard(string id);
    Task<bool> TrySaveBoard(Board board);
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

    public async Task<Board> GetBoard(string id)
    {
        return new Board
        {
            Id = id,
            Cells = (await _hanjieRepository.GetBoardCells(id)).ToList()
        };
    }

    public async Task<bool> BoardExists(string id)
    {
        return await _hanjieRepository.BoardExists(id);
    }

    public async Task<bool> TrySaveBoard(Board board)
    {
        if (await BoardExists(board.Id)) return false;

        foreach(Cell cell in board.Cells)
        {
            if (string.IsNullOrEmpty(cell.BoardId)) cell.BoardId = board.Id;
            await _hanjieRepository.CreateCell(cell);
        }

        return true;
    }
}