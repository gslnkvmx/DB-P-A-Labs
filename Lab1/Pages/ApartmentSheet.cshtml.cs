using Lab1.Data;
using Lab1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Lab1.Pages;

public class ApartmentSheetModel : PageModel
{
  DataContextDapper _dapper;
  public ChargesList chargesList = new ChargesList();
  public PaymentsList paymentsList = new PaymentsList();

  public int sumCharges;
  public int sumPayments;

  public int dolg;
  string _errorMessage = "";

  public string ErrorMessage { get => _errorMessage; set => _errorMessage = value; }
  public ApartmentSheetModel(IConfiguration config)
  {
    _dapper = new DataContextDapper(config);
  }

  public void OnGet() { }

  public void OnPost(int? Apartment)
  {
    System.Console.WriteLine(Apartment);
    string sql = @"select * from public.charges WHERE apartment = " + Apartment + ";";

    if(_dapper.LoadDataSingle<ChargesList>(sql) == null){
       ErrorMessage = "Недостаточно данных по квартире с данным номером!";
       return;}

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
      

    sql = @"select * from public.payments WHERE apartment = " + Apartment + ";";

      System.Console.WriteLine(_dapper.ExecuteSqlWithRows(sql));
      System.Console.WriteLine(sql);

    paymentsList = _dapper.LoadDataSingle<PaymentsList>(sql);

    if(_dapper.LoadDataSingle<ChargesList>(sql) == null){
       ErrorMessage = "Недостаточно данных по квартире с данным номером!";
       return;}

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
      from public.payments WHERE apartment = '" + Apartment + "';";

    sumPayments = _dapper.LoadDataSingle<int>(sql);

    dolg = sumCharges - sumPayments;
}
}
