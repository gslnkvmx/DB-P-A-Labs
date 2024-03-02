using System.Globalization;
using Lab1.Data;
using Lab1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Lab1.Pages.Charges;

public class EditModel : PageModel
{
  DataContextDapper _dapper;
  string _errorMessage = "";
  string _successMessage = "";
  public ChargesList chargesList = new ChargesList();
  public string ErrorMessage { get => _errorMessage; set => _errorMessage = value; }
  public string SuccessMessage { get => _successMessage; set => _successMessage = value; }
  public EditModel(IConfiguration config)
  {
    _dapper = new DataContextDapper(config);
  }
  public void OnGet()
  {
    string? apartment = Request.Query["apartment"];

    string sql = @"select *
      from public.charges WHERE apartment = " + apartment + ";";
    //System.Console.WriteLine(sql);
    try
    {
      chargesList = _dapper.LoadDataSingle<ChargesList>(sql);
    }
    catch (Exception ex)
    {
      ErrorMessage = ex.Message;
      return;
    }
  }

  public void OnPost()
  {
    // foreach (var item in Request.Form)
    // {
    //   if (item.Value == "")
    //   {
    //     ErrorMessage = "Необходимо заполнить все поля!";
    //     return;
    //   }
    // }
    string? apartment = Request.Query["apartment"];
    chargesList.Apartment = Convert.ToInt32(apartment);
    chargesList.January = Convert.ToDecimal(Request.Form["January"]);
    chargesList.February = Convert.ToDecimal(Request.Form["February"]);
    chargesList.March = Convert.ToDecimal(Request.Form["March"]);
    chargesList.April = Convert.ToDecimal(Request.Form["April"]);
    chargesList.May = Convert.ToDecimal(Request.Form["May"]);
    chargesList.June = Convert.ToDecimal(Request.Form["June"]);
    chargesList.July = Convert.ToDecimal(Request.Form["July"]);
    chargesList.August = Convert.ToDecimal(Request.Form["August"]);
    chargesList.September = Convert.ToDecimal(Request.Form["September"]);
    chargesList.October = Convert.ToDecimal(Request.Form["October"]);
    chargesList.November = Convert.ToDecimal(Request.Form["November"]);
    chargesList.December = Convert.ToDecimal(Request.Form["December"]);

    string sql = @"UPDATE public.charges SET 
      January = " + chargesList.January.ToString("0.00", CultureInfo.InvariantCulture) +
      ", February = " + chargesList.February.ToString("0.00", CultureInfo.InvariantCulture) +
      ", March = " + chargesList.March.ToString("0.00", CultureInfo.InvariantCulture) +
      ", April = " + chargesList.April.ToString("0.00", CultureInfo.InvariantCulture) +
      ", May = " + chargesList.May.ToString("0.00", CultureInfo.InvariantCulture) +
      ", June = " + chargesList.June.ToString("0.00", CultureInfo.InvariantCulture) +
      ", July = " + chargesList.July.ToString("0.00", CultureInfo.InvariantCulture) +
      ", August = " + chargesList.August.ToString("0.00", CultureInfo.InvariantCulture) +
      ", September = " + chargesList.September.ToString("0.00", CultureInfo.InvariantCulture) +
      ", October = " + chargesList.October.ToString("0.00", CultureInfo.InvariantCulture) +
      ", November = " + chargesList.November.ToString("0.00", CultureInfo.InvariantCulture) +
      ", December = " + chargesList.December.ToString("0.00", CultureInfo.InvariantCulture)
      + " WHERE apartment = " + chargesList.Apartment + ";";

    //System.Console.WriteLine(sql);

    try
    {
      _dapper.ExecuteSql(sql);
    }
    catch (Exception ex)
    {
      ErrorMessage = ex.Message;
      return;
    }
    SuccessMessage = "Успешно изменено!";

    Response.Redirect("/Saldo/Edit?apartment=" + Request.Query["apartment"]);
  }
}