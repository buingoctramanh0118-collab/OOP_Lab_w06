using System;
using System.Collections.Generic;
using System.Linq;
using Labw06.Exceptions;
using Labw06.Enums;      
using Labw06.Models.Borrowers;
using Labw06.Models.Devices;

namespace Labw06.Models
{
    public class BorrowingSlip
    {
        public string SlipId { get; }
        public IBorrower Borrower { get; }
        public string TeachingRoom { get; }
        public DateTime BorrowedDate { get; }
        
        private readonly List<BorrowItem> _items;
        public IReadOnlyList<BorrowItem> Items => _items.AsReadOnly();

        public BorrowingSlip(string slipId, IBorrower borrower, string teachingRoom, DateTime borrowedDate)
        {
            if (string.IsNullOrWhiteSpace(slipId) || borrower == null || string.IsNullOrWhiteSpace(teachingRoom))
                throw new ArgumentException("SlipId, Borrower và TeachingRoom phải hợp lệ."); 

            SlipId = slipId;
            Borrower = borrower;
            TeachingRoom = teachingRoom;
            BorrowedDate = borrowedDate;
            _items = new List<BorrowItem>();
        }

        public void AddDevice(Device device, int borrowDays)
        {
            if (device == null) throw new ArgumentNullException(nameof(device));
            
            if (borrowDays <= 0) 
                throw new InvalidBorrowPeriodException("Số ngày mượn không dương.", device.DeviceId, borrowDays, device.MaxBorrowDays); 
                
            if (borrowDays > device.MaxBorrowDays)
                throw new InvalidBorrowPeriodException("Thời hạn mượn lớn hơn giới hạn của thiết bị.", device.DeviceId, borrowDays, device.MaxBorrowDays); 

            if (_items.Any(i => i.Device.DeviceId == device.DeviceId))
                throw new DuplicateDeviceException($"Thiết bị {device.DeviceId} đã tồn tại trong phiếu."); 

            if (_items.Count >= Borrower.GetMaximumDevices())
                throw new BorrowLimitExceededException(Borrower.BorrowerId, Borrower.GetMaximumDevices()); 

            if (device.Status != DeviceStatus.Available)
                throw new DeviceUnavailableException(device.DeviceId, device.Status); 

            var item = new BorrowItem(device, BorrowedDate, borrowDays); 
            device.Status = DeviceStatus.Borrowed;
            _items.Add(item);
        }

        public decimal ReturnDevice(string deviceId, DateTime returnedDate)
        {
            if (string.IsNullOrWhiteSpace(deviceId)) throw new ArgumentException("Mã thiết bị không hợp lệ.");

            var item = _items.FirstOrDefault(i => i.Device.DeviceId == deviceId);
            if (item == null) throw new DeviceNotInSlipException($"Thiết bị {deviceId} không thuộc phiếu."); 

            item.Return(returnedDate); 
            
            item.Device.Status = DeviceStatus.Available; 
            
            return CalculateTotalLateFee();
        }

        public decimal CalculateTotalLateFee()
        {
            return 0m;
        }
    }
}
