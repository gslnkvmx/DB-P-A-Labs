using System.Globalization;
using Lab1.Data;
using Lab1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Lab1.Pages.Payments;

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
    PaymentsList paymentsListToAdd = new PaymentsList();
    foreach (var item in Request.Form)
    {
      if (item.Value == "")
      {
        ErrorMessage = "Необходимо заполнить все поля!";
        return;
      }
    }
    paymentsListToAdd.Apartment = Convert.ToInt32(Request.Form["Apartment"]);
    paymentsListToAdd.January = Convert.ToDecimal(Request.Form["January"]);
    paymentsListToAdd.February = Convert.ToDecimal(Request.Form["February"]);
    paymentsListToAdd.March = Convert.ToDecimal(Request.Form["March"]);
    paymentsListToAdd.April = Convert.ToDecimal(Request.Form["April"]);
    paymentsListToAdd.May = Convert.ToDecimal(Request.Form["May"]);
    paymentsListToAdd.June = Convert.ToDecimal(Request.Form["June"]);
    paymentsListToAdd.July = Convert.ToDecimal(Request.Form["July"]);
    paymentsListToAdd.August = Convert.ToDecimal(Request.Form["August"]);
    paymentsListToAdd.September = Convert.ToDecimal(Request.Form["September"]);
    paymentsListToAdd.October = Convert.ToDecimal(Request.Form["October"]);
    paymentsListToAdd.November = Convert.ToDecimal(Request.Form["November"]);
    paymentsListToAdd.December = Convert.ToDecimal(Request.Form["December"]);

    string sql = @"INSERT INTO public.payments (
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
        paymentsListToAdd.Apartment.ToString("0.00", CultureInfo.InvariantCulture) + "," +
        paymentsListToAdd.January.ToString("0.00", CultureInfo.InvariantCulture) + "," +
        paymentsListToAdd.February.ToString("0.00", CultureInfo.InvariantCulture) + "," +
        paymentsListToAdd.March.ToString("0.00", CultureInfo.InvariantCulture) + "," +
        paymentsListToAdd.April.ToString("0.00", CultureInfo.InvariantCulture) + "," +
        paymentsListToAdd.May.ToString("0.00", CultureInfo.InvariantCulture) + "," +
        paymentsListToAdd.June.ToString("0.00", CultureInfo.InvariantCulture) + "," +
        paymentsListToAdd.July.ToString("0.00", CultureInfo.InvariantCulture) + "," +
        paymentsListToAdd.August.ToString("0.00", CultureInfo.InvariantCulture) + "," +
        paymentsListToAdd.September.ToString("0.00", CultureInfo.InvariantCulture) + "," +
        paymentsListToAdd.October.ToString("0.00", CultureInfo.InvariantCulture) + "," +
        paymentsListToAdd.November.ToString("0.00", CultureInfo.InvariantCulture) + "," +
        paymentsListToAdd.December.ToString("0.00", CultureInfo.InvariantCulture)
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