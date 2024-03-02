using Lab1.Data;
using Lab1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Lab1.Pages;

public class TurnoverSheetModel : PageModel
{
  DataContextDapper _dapper;
  public List<ChargesList> sortedChargesList;
  public List<PaymentsList> sortedPaymentsList;
  public List<SaldoList> sortedSaldoList;
  public TurnoverSheetModel(IConfiguration config)
  {
    _dapper = new DataContextDapper(config);
  }

  public void OnGet()
  {
    string sql = @"select *
      from public.saldo";
    var saldoList = _dapper.LoadData<SaldoList>(sql);
    sortedSaldoList = (from s in saldoList
                       orderby s.Apartment
                       select s).ToList();

    sql = @"select 
    charges.apartment,
    charges.january,
    charges.february,
    charges.march,
    charges.april,
    charges.may,
    charges.june,
    charges.july,
    charges.august,
    charges.september,
    charges.october,
    charges.november,
    charges.december
      from public.charges JOIN public.saldo ON public.charges.Apartment=public.saldo.Apartment";
    var chargesList = _dapper.LoadData<ChargesList>(sql);
    sortedChargesList = (from c in chargesList
                         orderby c.Apartment
                         select c).ToList();

    sql = @"select 
    payments.apartment,
    payments.january,
    payments.february,
    payments.march,
    payments.april,
    payments.may,
    payments.june,
    payments.july,
    payments.august,
    payments.september,
    payments.october,
    payments.november,
    payments.december
      from public.payments JOIN public.saldo ON public.payments.Apartment=public.saldo.Apartment;";
    var paymentsList = _dapper.LoadData<PaymentsList>(sql);

    sortedPaymentsList = (from p in paymentsList
                          orderby p.Apartment
                          select p).ToList();
  }
}
