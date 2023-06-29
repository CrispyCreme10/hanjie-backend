using Hanjie.Models;
using Hanjie.Operations;
using Hanjie.Services;
using Microsoft.AspNetCore.Mvc;

namespace Hanjie.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BoardController : ControllerBase
{
    private readonly IHanjieService _hanjieService;

    public BoardController(IHanjieService hanjieService)
    {
        _hanjieService = hanjieService;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PostgresBoard>> GetById(string boardId)
    {
        return await _hanjieService.GetBoard(boardId) is PostgresBoard board
            ? Ok(board)
            : NotFound();
    }

    [HttpGet]
    [Route("CheckValue")]
    public async Task<bool> CheckValue(string boardId, int row, int col, int val)
    {
        return await _hanjieService.CheckValue(boardId, row, col, val);
    }

    [HttpPost]
    public async Task<ActionResult<PostgresBoard>> Create(BoardCreationOptions opts)
    {
        // TODO
        PostgresBoard newBoard = await _hanjieService.CreateBoard(opts);
        return CreatedAtAction(nameof(GetById), newBoard);
    }
}