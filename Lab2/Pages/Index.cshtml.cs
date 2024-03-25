using Lab2.Data;
using Lab2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Lab2.Pages;

public class IndexModel : PageModel
{
    public IEnumerable<Book> Books = [];
    private DataContextDapper _dapper;

    public IndexModel(IConfiguration config)
    {
        _dapper = new DataContextDapper(config);
    }


    public void OnGet()
    {
        string sql = "SELECT * FROM labs.books;";
        Books = _dapper.LoadData<Book>(sql);
    }

    public void OnPostAuthor()
    {
        string sql = "SELECT * FROM books WHERE author REGEXP '" + Request.Form["author"] + "';";
        Books = _dapper.LoadData<Book>(sql);
    }

    public void OnPostName()
    {
        string sql = "SELECT * FROM books WHERE name REGEXP '" + Request.Form["bookName"] + "';";
        Books = _dapper.LoadData<Book>(sql);
    }

    public void OnPostPublisher()
    {
        string sql = "SELECT * FROM books WHERE publisher REGEXP '" + Request.Form["publisher"] + "';";
        Books = _dapper.LoadData<Book>(sql);
    }

    public void OnPostYear()
    {
        string sql = "SELECT * FROM books WHERE year REGEXP '" + Request.Form["year"].ToString() + "';";
        Books = _dapper.LoadData<Book>(sql);
    }

    public void OnPostAnnotation()
    {
        string sql = "SELECT * FROM books WHERE MATCH(annotation) AGAINST('" + Request.Form["annotation"] + "' IN NATURAL LANGUAGE MODE);";
        Books = _dapper.LoadData<Book>(sql);
    }
}
