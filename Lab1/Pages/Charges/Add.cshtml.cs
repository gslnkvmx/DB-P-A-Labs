using System.Globalization;
using Lab1.Data;
using Lab1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Lab1.Pages.Charges;

public class AddModel : PageModel
{
  DataContextDapper _dapper;
  string _errorMessage = "";
  string _successMessage = "";
  public string ErrorMessage { get => _errorMessage; set => _errorMessage = value; }
  public string SuccessMessage { get => _successMessage; set => _successMessage = value; }
  public AddModel(IConfiguration config)
  {
    _dapper = new DataContextDapper(config);
  }
  public void OnGet()
  {

  }

  public void OnPost()
  {
    ChargesList chargesListToAdd = new ChargesList();
    foreach (var item in Request.Form)
    {
      if (item.Value == "")
      {
        ErrorMessage = "Необходимо заполнить все поля!";
        return;
      }
    }
    chargesListToAdd.Apartment = Convert.ToInt32(Request.Form["Apartment"]);
    chargesListToAdd.January = Convert.ToDecimal(Request.Form["January"]);
    chargesListToAdd.February = Convert.ToDecimal(Request.Form["February"]);
    chargesListToAdd.March = Convert.ToDecimal(Request.Form["March"]);
    chargesListToAdd.April = Convert.ToDecimal(Request.Form["April"]);
    chargesListToAdd.May = Convert.ToDecimal(Request.Form["May"]);
    chargesListToAdd.June = Convert.ToDecimal(Request.Form["June"]);
    chargesListToAdd.July = Convert.ToDecimal(Request.Form["July"]);
    chargesListToAdd.August = Convert.ToDecimal(Request.Form["August"]);
    chargesListToAdd.September = Convert.ToDecimal(Request.Form["September"]);
    chargesListToAdd.October = Convert.ToDecimal(Request.Form["October"]);
    chargesListToAdd.November = Convert.ToDecimal(Request.Form["November"]);
    chargesListToAdd.December = Convert.ToDecimal(Request.Form["December"]);

    string sql = @"INSERT INTO public.charges (
      Apartment,
      January,
      February,
      March,
      April,
      May,
      June,
      July,
      August,
      September,
      October,
      November,
      December
      ) VALUES (" +
        chargesListToAdd.Apartment.ToString("0.00", CultureInfo.InvariantCulture) + "," +
        chargesListToAdd.January.ToString("0.00", CultureInfo.InvariantCulture) + "," +
        chargesListToAdd.February.ToString("0.00", CultureInfo.InvariantCulture) + "," +
        chargesListToAdd.March.ToString("0.00", CultureInfo.InvariantCulture) + "," +
        chargesListToAdd.April.ToString("0.00", CultureInfo.InvariantCulture) + "," +
        chargesListToAdd.May.ToString("0.00", CultureInfo.InvariantCulture) + "," +
        chargesListToAdd.June.ToString("0.00", CultureInfo.InvariantCulture) + "," +
        chargesListToAdd.July.ToString("0.00", CultureInfo.InvariantCulture) + "," +
        chargesListToAdd.August.ToString("0.00", CultureInfo.InvariantCulture) + "," +
        chargesListToAdd.September.ToString("0.00", CultureInfo.InvariantCulture) + "," +
        chargesListToAdd.October.ToString("0.00", CultureInfo.InvariantCulture) + "," +
        chargesListToAdd.November.ToString("0.00", CultureInfo.InvariantCulture) + "," +
        chargesListToAdd.December.ToString("0.00", CultureInfo.InvariantCulture)
      + ");";

    System.Console.WriteLine(sql);

    try
    {
      _dapper.ExecuteSql(sql);
    }
    catch (Exception ex)
    {
      ErrorMessage = ex.Message;
      return;
    }
    SuccessMessage = "Успешно добавлено!";

    Response.Redirect("/MainTables");
  }
}