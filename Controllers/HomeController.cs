using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Mission11.Models;
using Mission11.Models.ViewModels;

namespace Mission11.Controllers;

public class HomeController : Controller
{
    private readonly IBookRepository _repo;

    public HomeController(IBookRepository context)
    {
        _repo = context;
    }

    public IActionResult Index(int pageNumber)
    {
        int pageSize = 10;

        var data = new BooksListViewModel()
        {
            Books = _repo.Books
                .OrderBy(b => b.Title)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList(),

            PaginationInfo = new PaginationInfo()
            {
                CurrentPage = pageNumber,
                ItemsPerPage = pageSize,
                TotalItems = _repo.Books.Count()
            }
        };

        return View(data);
    }


}