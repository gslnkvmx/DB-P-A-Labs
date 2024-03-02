using System.Globalization;
using Lab1.Data;
using Lab1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Lab1.Pages.Payments;

public class EditModel : PageModel
{
  DataContextDapper _dapper;
  string _errorMessage = "";
  string _successMessage = "";
  public PaymentsList paymentsList = new PaymentsList();
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
      from public.payments WHERE apartment = " + apartment + ";";
    //System.Console.WriteLine(sql);
    try
    {
      paymentsList = _dapper.LoadDataSingle<PaymentsList>(sql);
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
    paymentsList.Apartment = Convert.ToInt32(apartment);
    paymentsList.January = Convert.ToDecimal(Request.Form["January"]);
    paymentsList.February = Convert.ToDecimal(Request.Form["February"]);
    paymentsList.March = Convert.ToDecimal(Request.Form["March"]);
    paymentsList.April = Convert.ToDecimal(Request.Form["April"]);
    paymentsList.May = Convert.ToDecimal(Request.Form["May"]);
    paymentsList.June = Convert.ToDecimal(Request.Form["June"]);
    paymentsList.July = Convert.ToDecimal(Request.Form["July"]);
    paymentsList.August = Convert.ToDecimal(Request.Form["August"]);
    paymentsList.September = Convert.ToDecimal(Request.Form["September"]);
    paymentsList.October = Convert.ToDecimal(Request.Form["October"]);
    paymentsList.November = Convert.ToDecimal(Request.Form["November"]);
    paymentsList.December = Convert.ToDecimal(Request.Form["December"]);

    string sql = @"UPDATE public.payments SET 
      January = " + paymentsList.January.ToString("0.00", CultureInfo.InvariantCulture) +
      ", February = " + paymentsList.February.ToString("0.00", CultureInfo.InvariantCulture) +
      ", March = " + paymentsList.March.ToString("0.00", CultureInfo.InvariantCulture) +
      ", April = " + paymentsList.April.ToString("0.00", CultureInfo.InvariantCulture) +
      ", May = " + paymentsList.May.ToString("0.00", CultureInfo.InvariantCulture) +
      ", June = " + paymentsList.June.ToString("0.00", CultureInfo.InvariantCulture) +
      ", July = " + paymentsList.July.ToString("0.00", CultureInfo.InvariantCulture) +
      ", August = " + paymentsList.August.ToString("0.00", CultureInfo.InvariantCulture) +
      ", September = " + paymentsList.September.ToString("0.00", CultureInfo.InvariantCulture) +
      ", October = " + paymentsList.October.ToString("0.00", CultureInfo.InvariantCulture) +
      ", November = " + paymentsList.November.ToString("0.00", CultureInfo.InvariantCulture) +
      ", December = " + paymentsList.December.ToString("0.00", CultureInfo.InvariantCulture)
      + " WHERE apartment = " + paymentsList.Apartment + ";";

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
  }
}