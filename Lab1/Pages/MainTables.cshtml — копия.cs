using Lab1.Data;
using Lab1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Lab1.Pages;

public class MainTablesModel : PageModel
{
  DataContextDapper _dapper;
  public IEnumerable<ChargesList> chargesList;
  public IEnumerable<PaymentsList> paymentsList;
  public IEnumerable<SaldoList> saldoList;
  public MainTablesModel(IConfiguration config)
  {
    _dapper = new DataContextDapper(config);
  }

  public void OnGet()
  {
    string sql = @"select *
      from public.charges";
    chargesList = _dapper.LoadData<ChargesList>(sql);

    sql = @"select *
      from public.payments";
    paymentsList = _dapper.LoadData<PaymentsList>(sql);

    sql = @"select *
      from public.saldo";
    saldoList = _dapper.LoadData<SaldoList>(sql);
  }
}
