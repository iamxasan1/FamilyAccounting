using FamilyAccounting.Data.Context;
using FamilyAccounting.Data.Entities;
using FamilyAccouting.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using Microsoft.Diagnostics.NETCore.Client;


namespace FamilyAccouting.Controllers;

public class FamilyController : Controller
{
    private readonly AppDbContext _context;
    private readonly UserManager<User> _userManager;
    public FamilyController(AppDbContext context, UserManager<User> userManager) 
    {
        _context = context;
        _userManager = userManager;
    }
    [Authorize]
    public IActionResult CreateFamily()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> CreateFamily([FromForm]CreateFamilyDto newFamily)
    {
        if (!ModelState.IsValid) return View(newFamily);
        var user = await _userManager.GetUserAsync(User);
        user.UserType = newFamily.CreatorType;
        var family = new Family()
        {
            Id = new Guid(),
            FamilyName = newFamily.FamilyName,
            Password = newFamily.Password,
            Users = new List<User>() { user }
            
        };

         _context.Families.Add(family);
        await _context.SaveChangesAsync();
        return RedirectToAction("Profile");
    }


    [Authorize]
    public async Task<IActionResult> Profile([FromForm] DateDto dto)
    {
        var user = await _userManager.GetUserAsync(User);

        ViewBag.userid = user.Id;
        var family = new Family();

        family = await _context.Families
                      .Include(f => f.Users)
                 .Include(f => f.Users)
                         .ThenInclude(u => u.Incomes.Where(i => i.CreatedAt >= dto.From && i.CreatedAt <= dto.To))
                 .Include(f => f.Users)
                         .ThenInclude(u => u.Expenses.Where(e => e.CreatedAt >= dto.From && e.CreatedAt <= dto.To))
                     .FirstOrDefaultAsync(u => u.Id == user.FamilyId);

        return View(family);
    }

    [HttpGet]
    public IActionResult Profile1()
    {
        return View();
    }


    [Authorize]
    public IActionResult Index()
    {
        var families = _context.Families
            .Include(oila => oila.Users)
            .ThenInclude(familyUser => familyUser.Incomes)
           .Include(familyUser => familyUser.Users)
           .ThenInclude(f => f.Expenses).ToList();
        ViewBag.familyId = _userManager.GetUserAsync(User).Result.FamilyId;
        return View(families);
    }

    [Authorize]
    public async Task<IActionResult> Joinfamily(Guid familyid)
    {
        var family = _context.Families.FirstOrDefault(u=>u.Id == familyid);
        var user = await _userManager.GetUserAsync(User);
        user.FamilyId = familyid;
        user.Family = family;
        user.UserType = EUserType.Son;
        await _context.SaveChangesAsync();

        return RedirectToAction("Profile");
    }

    /* [HttpPost]
     public async Task<IActionResult> JoinFamily(JoinFamilyDto joinFamily)
     {
         var user = await _userManager.GetUserAsync(User);
         user.UserType = joinFamily.UserType;
         user.FamilyId = joinFamily.FamilyId;
         await _context.SaveChangesAsync();
         return RedirectToAction("Profile");
     }*/
    [Authorize]
    public IActionResult UpdateUserFamilyRole(Guid familyId, Guid userId, EUserType role)
    {
        var user = _context.Users.FirstOrDefault(u => u.Id == userId);
        user.UserType = role;
        _context.SaveChanges();
        return RedirectToAction("Profile", new { familyid = familyId });
    }


    public IActionResult DeleteFamily(Guid familyId)
    {

        //var processId = System.Diagnostics.Process.GetCurrentProcess().Id;

        //var diagnosticsClient = new DiagnosticsClient(processId);
        //var dumpFileName = "mydumpfile.dmp";
        //var dumpOptions = new DumpOptions { CompressDump = true };

        //diagnosticsClient.WriteDump(DumpType.Full, dumpFileName, dumpOptions);

     
        var delFamily = _context.Families.First(f => f.Id == familyId);
        _context.Families.Remove(delFamily);
        _context.SaveChanges();

        return RedirectToAction("Profile", "users");
    }
}
