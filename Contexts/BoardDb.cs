using Microsoft.EntityFrameworkCore;

class BoardDb : DbContext
{
    public BoardDb(DbContextOptions<BoardDb> opts) : base(opts) { }

    public DbSet<Board> Boards => Set<Board>();
}