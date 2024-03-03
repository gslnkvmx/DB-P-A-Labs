using System.Globalization;
using Lab1.Data;
using Lab1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Lab1.Pages.Saldo;

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
    SaldoList saldoToAdd = new SaldoList();
    saldoToAdd.Apartment = Convert.ToInt32(Request.Form["Apartment"]);
    saldoToAdd.InSaldo = Convert.ToDecimal(Request.Form["InSaldo"]);
    saldoToAdd.OutSaldo = 0;

    string sql = @"INSERT INTO public.saldo
      SELECT 
      public.charges.Apartment,
      '" + saldoToAdd.InSaldo.ToString("0.00", CultureInfo.InvariantCulture) +
      "', '" + saldoToAdd.OutSaldo.ToString("0.00", CultureInfo.InvariantCulture) +
      "', public.charges.January - public.payments.January AS January, " +
      "public.charges.February - public.payments.February AS February, " +
      "public.charges.March - public.payments.March AS March, " +
      "public.charges.April - public.payments.April AS April, " +
      "public.charges.May - public.payments.May AS May, " +
      "public.charges.June - public.payments.June AS June, " +
      "public.charges.July - public.payments.July AS July, " +
      "public.charges.August - public.payments.August AS August, " +
      "public.charges.September - public.payments.September AS September, " +
     "public.charges.October - public.payments.October AS October, " +
      "public.charges.November - public.payments.November AS November, " +
      "public.charges.December - public.payments.December AS December " +
      "FROM public.charges JOIN public.payments ON public.charges.Apartment=public.payments.Apartment WHERE charges.apartment = " + saldoToAdd.Apartment + ";";

    //System.Console.WriteLine(sql);

    try
    {
      if (_dapper.ExecuteSqlWithRows(sql) == 0)
      {
        ErrorMessage = "Недостаточно данных о данной кваритре!\nСначала внесите данные о платежах и начислениях.";
        return;
      };
    }
    catch (Exception ex)
    {
      ErrorMessage = ex.Message;
      return;
    }

    string sqlUpdate = @"UPDATE public.saldo SET january = insaldo + january WHERE apartment= " + saldoToAdd.Apartment + ";" +
      "UPDATE public.saldo SET February = january + February WHERE apartment= " + saldoToAdd.Apartment + ";" +
      "UPDATE public.saldo SET March = February + March WHERE apartment= " + saldoToAdd.Apartment + ";" +
      "UPDATE public.saldo SET April = March + April WHERE apartment= " + saldoToAdd.Apartment + ";" +
      "UPDATE public.saldo SET May = April + May WHERE apartment= " + saldoToAdd.Apartment + ";" +
      "UPDATE public.saldo SET June = May + June WHERE apartment= " + saldoToAdd.Apartment + ";" +
      "UPDATE public.saldo SET July = June + July WHERE apartment= " + saldoToAdd.Apartment + ";" +
      "UPDATE public.saldo SET August = July + August WHERE apartment= " + saldoToAdd.Apartment + ";" +
      "UPDATE public.saldo SET September = August + September WHERE apartment= " + saldoToAdd.Apartment + ";" +
      "UPDATE public.saldo SET October = September + October WHERE apartment= " + saldoToAdd.Apartment + ";" +
      "UPDATE public.saldo SET November = October + November WHERE apartment= " + saldoToAdd.Apartment + ";" +
      "UPDATE public.saldo SET December = November + December WHERE apartment= " + saldoToAdd.Apartment + ";" +
      "UPDATE public.saldo SET outsaldo = December WHERE apartment= " + saldoToAdd.Apartment + ";";

    //System.Console.WriteLine(sqlUpdate);
    try
    {
      _dapper.ExecuteSql(sqlUpdate);
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