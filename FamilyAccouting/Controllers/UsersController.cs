using FamilyAccounting.Data.Context;
using FamilyAccounting.Data.Entities;
using FamilyAccounting.Data.Helpers;
using FamilyAccouting.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FamilyAccouting.Controllers;

public class UsersController : Controller
{
    private readonly AppDbContext _context;
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;

    public UsersController(SignInManager<User> signInManager, UserManager<User> userManager, AppDbContext context)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> SignUp()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> SignUp(SignUpUserDto signUpUser)
    {

        if (!ModelState.IsValid)
            return View(signUpUser);

        var user = new User
        {    
            FirstName = signUpUser.FirstName,
            LastName = signUpUser.LastName,
            UserName = signUpUser.Username
        };
        user.PhotoUrl = await FileHelper.SaveUserFile(signUpUser.Photo);

        var result = await _userManager.CreateAsync(user, signUpUser.Password);

        if (!result.Succeeded)
        {
            ModelState.AddModelError("Username", result.Errors.First().Description);
            return View();
        }
        
        await _signInManager.SignInAsync(user, isPersistent: true);

        return RedirectToAction("Profile");
    }

    public async Task<IActionResult> SignIn()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> SignIn([FromForm]SignInUserDto signInUser)
    {
        if (!ModelState.IsValid) return View(signInUser);

        var result = await _signInManager.PasswordSignInAsync(signInUser.Username, signInUser.Password, true, false);
        if (!result.Succeeded)
        {
            ModelState.AddModelError("Username", "Username or password is incorrect");
            return View(signInUser);
        }

        return RedirectToAction("Profile");
    }




    [Authorize]
    public async Task<IActionResult> Profile()
    {
        var user = await _userManager.GetUserAsync(User);
        Guid userId = user.Id;

        var oldUser = _context.Users
            .Include(u => u.Incomes)
            .Include(v => v.Expenses)
            .FirstOrDefault(u => u.Id == userId);

        return View(oldUser);
    }
    [Authorize]
    public async Task<IActionResult> LogOut()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction(nameof(SignIn));
    }

    public IActionResult Index()
    {
        return View();
    }
    [Authorize]
    public async Task<IActionResult> AddIncome()
    {
        return View();
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> AddIncome(IncomeDto addIncome)
    {
        var user = _userManager.GetUserAsync(User).Result;

        var income = new Income()
        {
            Id = Guid.NewGuid(),
            CreatedAt = addIncome.CreatedAt,
            AmountIncome = addIncome.AmountIncome,
            Description = addIncome.Description,
            IncomeType = (EIncomeType)Enum.Parse(typeof(EIncomeType), Request.Form["Incometype"]),
            UserId = user.Id,
            
        };

        if (user.Incomes == null)
        {
            user.Incomes = new List<Income>() { income };
        }
        else
        {
            user.Incomes.Add(income);
        }

        _context.Incomes.Add(income);


        _context.SaveChanges();

        return RedirectToAction("profile");
    }

    [Authorize]
    public async Task<IActionResult> AddExpense()
    {
        return View();
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> AddExpense(ExpenseDto addExpense)
    {
        var user = _userManager.GetUserAsync(User).Result;

        var expense = new Expense()
        {
            Id = Guid.NewGuid(),
            CreatedAt = addExpense.CreatedAt,
            ExpenseAmount = addExpense.AmountExpense,
            Description = addExpense.Description,
            ExpenseType = (EExpensesType)Enum.Parse(typeof(EExpensesType), Request.Form["ExpenseType"]),
            UserId = user.Id,

        };

        if (user.Expenses == null)
        {
            user.Expenses = new List<Expense>() { expense };
        }
        else
        {
            user.Expenses.Add(expense);
        }

        _context.Expenses.Add(expense);


        _context.SaveChanges();

        return RedirectToAction("profile");
    }


}
