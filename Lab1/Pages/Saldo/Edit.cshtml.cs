using System.Globalization;
using Lab1.Data;
using Lab1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Lab1.Pages.Saldo;

public class EditModel : PageModel
{
  DataContextDapper _dapper;
  public SaldoList saldoList = new SaldoList();
  public EditModel(IConfiguration config)
  {
    _dapper = new DataContextDapper(config);
  }
  public void OnGet()
  {
    string? apartment = Request.Query["apartment"];

    string sql = @"SELECT 
      public.charges.Apartment, public.saldo.insaldo, public.saldo.outsaldo, 
      public.charges.January - public.payments.January AS January, " +
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
      "FROM public.charges JOIN public.payments ON public.charges.Apartment=public.payments.Apartment JOIN public.saldo ON public.payments.Apartment=public.saldo.Apartment WHERE charges.apartment = " + apartment + ";";

    if(_dapper.ExecuteSqlWithRows(sql) > 0) {
            saldoList = _dapper.LoadDataSingle<SaldoList>(sql);
        }

    string sqlUpdate = @"UPDATE public.saldo SET january = insaldo + " + saldoList.January.ToString("0.00", CultureInfo.InvariantCulture) + " WHERE apartment= " + apartment + ";" +
      "UPDATE public.saldo SET February = january + " + saldoList.February.ToString("0.00", CultureInfo.InvariantCulture) + " WHERE apartment= " + apartment + ";" +
      "UPDATE public.saldo SET March = February + " + saldoList.March.ToString("0.00", CultureInfo.InvariantCulture) + " WHERE apartment= " + apartment + ";" +
      "UPDATE public.saldo SET April = March + " + saldoList.April.ToString("0.00", CultureInfo.InvariantCulture) + " WHERE apartment= " + apartment + ";" +
      "UPDATE public.saldo SET May = April + " + saldoList.May.ToString("0.00", CultureInfo.InvariantCulture) + " WHERE apartment= " + apartment + ";" +
      "UPDATE public.saldo SET June = May + " + saldoList.June.ToString("0.00", CultureInfo.InvariantCulture) + " WHERE apartment= " + apartment + ";" +
      "UPDATE public.saldo SET July = June + " + saldoList.July.ToString("0.00", CultureInfo.InvariantCulture) + " WHERE apartment= " + apartment + ";" +
      "UPDATE public.saldo SET August = July + " + saldoList.August.ToString("0.00", CultureInfo.InvariantCulture) + " WHERE apartment= " + apartment + ";" +
      "UPDATE public.saldo SET September = August + " + saldoList.September.ToString("0.00", CultureInfo.InvariantCulture) + " WHERE apartment= " + apartment + ";" +
      "UPDATE public.saldo SET October = September + " + saldoList.October.ToString("0.00", CultureInfo.InvariantCulture) + " WHERE apartment= " + apartment + ";" +
      "UPDATE public.saldo SET November = October + " + saldoList.November.ToString("0.00", CultureInfo.InvariantCulture) + " WHERE apartment= " + apartment + ";" +
      "UPDATE public.saldo SET December = November + " + saldoList.December.ToString("0.00", CultureInfo.InvariantCulture) + " WHERE apartment= " + apartment + ";" +
      "UPDATE public.saldo SET outsaldo = December WHERE apartment= " + apartment + ";";

    _dapper.ExecuteSql(sqlUpdate);

    Response.Redirect("/MainTables");
  }
}