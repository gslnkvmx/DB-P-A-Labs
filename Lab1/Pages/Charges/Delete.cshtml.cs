using Lab1.Data;
using Lab1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Lab1.Pages.Charges;

public class DeleteModel : PageModel
{
  DataContextDapper _dapper;

  public DeleteModel(IConfiguration config)
  {
    _dapper = new DataContextDapper(config);
  }
  public void OnGet()
  {
    string apartment = Request.Query["apartment"];

    string sql = @"DELETE FROM public.charges
WHERE apartment = " + apartment + ";";
    System.Console.WriteLine(sql);

    if (!_dapper.ExecuteSql(sql))
    {
      throw new Exception("Not deleted!");
    }

    Response.Redirect("/MainTables");
  }
}