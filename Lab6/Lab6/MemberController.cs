using GlobalCommunity.Models;
using Microsoft.AspNetCore.Mvc;

public class MemberController : Controller
{
    private readonly ApplicationDbContext _context;

    public MemberController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var members = await _context.Members.Include(m => m.LocalCommunity).ToListAsync();
        return View(members);
    }

    public async Task<IActionResult> Details(int id)
    {
        var member = await _context.Members
            .Include(m => m.LocalCommunity)
            .FirstOrDefaultAsync(m => m.Id == id);

        if (member == null) return NotFound();

        return View(member);
    }
}