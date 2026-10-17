/*
MSSV: 202418832
Họ tên: Bùi Ngọc Trâm Anh
*/

using System;
using Labw06.Enums;

namespace Labw06.Models
{
    public abstract class Device
    {
        public string DeviceId { get; init; }
        public string DeviceName { get; set; }
        public int YearOfUse { get; set; }
        public decimal PurchasePrice { get; set; }
        public DeviceStatus Status { get; set; }

        protected Device(string deviceId, string deviceName, int yearOfUse, decimal purchasePrice, DeviceStatus status = DeviceStatus.Active)
        {
            if (string.IsNullOrWhiteSpace(deviceId))
                throw new ArgumentException("Mã thiết bị không được để trống.", nameof(deviceId));

            if (purchasePrice <= 0)
                throw new ArgumentOutOfRangeException(nameof(purchasePrice), "Giá mua phải lớn hơn 0.");

            int currentYear = DateTime.Now.Year;
            if (yearOfUse > currentYear)
                throw new ArgumentOutOfRangeException(nameof(yearOfUse), "Năm đưa vào sử dụng không được lớn hơn năm hiện tại.");

            DeviceId = deviceId;
            DeviceName = deviceName;
            YearOfUse = yearOfUse;
            PurchasePrice = purchasePrice;
            Status = status;
        }

        //Tính số năm đã sử dụng
        public int GetYearsInUse() => DateTime.Now.Year - YearOfUse;

        //Chi phí bảo trì
        public abstract decimal CalculateAnnualMaintenanceCost();

        //Ghi đè
        public override string ToString()
        {
            return $"[{DeviceId}] {DeviceName} | Năm SD: {YearOfUse} | Giá: {PurchasePrice:N0} VNĐ | Trạng thái: {Status} | Phí BT dự kiến: {CalculateAnnualMaintenanceCost():N0} VNĐ";
        }
    }
}
