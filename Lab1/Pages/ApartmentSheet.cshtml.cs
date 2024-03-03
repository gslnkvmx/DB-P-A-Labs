using Lab1.Data;
using Lab1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Lab1.Pages;

public class ApartmentSheetModel : PageModel
{
  DataContextDapper _dapper;
  public ChargesList chargesList;
  public PaymentsList paymentsList;

  public int sumCharges;
  public int sumPayments;
  string _errorMessage = "";

  public string ErrorMessage { get => _errorMessage; set => _errorMessage = value; }
  public ApartmentSheetModel(IConfiguration config)
  {
    _dapper = new DataContextDapper(config);
  }

  public void OnGet() { }

  public void OnPost(int? Apartment)
  {
    string sql = @"select 
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
      from public.charges WHERE apartment = " + Apartment + ";";
    chargesList = _dapper.LoadDataSingle<ChargesList>(sql);

    sql = @"select 
      apartment+
      january+
      february+
      march+
      april+
      may+
      june+
      july+
      august+
      september+
      october+
      november+
      december
      from public.charges WHERE apartment = " + Apartment + ";";

    sumCharges = _dapper.LoadDataSingle<int>(sql);

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
      from public.payments WHERE apartment = " + Apartment + ";";
    paymentsList = _dapper.LoadDataSingle<PaymentsList>(sql);

    sql = @"select 
      apartment+
      january+
      february+
      march+
      april+
      may+
      june+
      july+
      august+
      september+
      october+
      november+
      december
      from public.payments WHERE apartment = " + Apartment + ";";

    sumPayments = _dapper.LoadDataSingle<int>(sql);
  }
}
