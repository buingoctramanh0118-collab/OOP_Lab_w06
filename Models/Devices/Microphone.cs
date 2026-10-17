
/*
MSSV: 202418832
Họ tên: Bùi Ngọc Trâm Anh
*/

namespace Labw06.Models.Devices;

public class Microphone : Device
{
    public Microphone(string id, string name, int year, decimal price, int maxDays = 1) 
        : base(id, name, year, price, maxDays) { }

    public override decimal CalculateAnnualMaintenanceCost() => PurchasePrice * 0.15m;
}
