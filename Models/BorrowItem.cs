/*
MSSV: 202418832
Họ tên: Bùi Ngọc Trâm Anh
*/

using System;
using Labw06.Exceptions; 
using Labw06.Enums;      

namespace Labw06.Models
{
    // 6. Lớp BorrowItem[cite: 14]
    public class BorrowItem
    {
        public Device Device { get; }
        public DateTime BorrowedDate { get; }
        
        public DateTime DueDate { get; } 
        
        public int fromPeriod { get; set; }
        public int toPeriod { get; set; }
        public DateTime? ReturnedDate { get; private set; }
        public bool IsReturned => ReturnedDate.HasValue;

        public BorrowItem(Device device, DateTime borrowedDate, int borrowDays)
        {
            Device = device ?? throw new ArgumentNullException(nameof(device)); 
            BorrowedDate = borrowedDate;
            
            DueDate = borrowedDate.AddDays(borrowDays); 
            
            if (DueDate < BorrowedDate) 
                throw new ArgumentException("DueDate phải sau hoặc bằng BorrowedDate."); 

            ReturnedDate = null; 
        }

        public void Return(DateTime returnedDate)
        {
            if (IsReturned) 
                throw new DeviceAlreadyReturnedException($"Thiết bị {Device.DeviceId} đã được trả."); 
                
            if (returnedDate < BorrowedDate) 
                throw new ArgumentException("Ngày trả không được trước ngày mượn."); 
                
            ReturnedDate = returnedDate;
        }
    }
}
