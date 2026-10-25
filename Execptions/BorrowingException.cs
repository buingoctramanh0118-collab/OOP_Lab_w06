
/*
MSSV: 202418832
Họ tên: Bùi Ngọc Trâm Anh
*/

using System;
using Labw06.Enums;

namespace Labw06.Exceptions
{
    public class BorrowingException : Exception
    {
        public BorrowingException(string message) : base(message) { }
        public BorrowingException(string message, Exception innerException) : base(message, innerException) { }
    }

    public class DeviceUnavailableException : BorrowingException
    {
        public string DeviceId { get; }
        public DeviceStatus CurrentStatus { get; }
        public DeviceUnavailableException(string deviceId, DeviceStatus currentStatus) 
            : base($"Thiết bị {deviceId} không ở trạng thái Available. Trạng thái hiện tại: {currentStatus}.")
        {
            DeviceId = deviceId;
            CurrentStatus = currentStatus;
        }
    }

    public class BorrowLimitExceededException : BorrowingException
    {
        public string BorrowerId { get; }
        public int MaximumDevices { get; }
        public BorrowLimitExceededException(string borrowerId, int maximumDevices) 
            : base($"Người mượn {borrowerId} đã vượt quá giới hạn mượn ({maximumDevices} thiết bị).")
        {
            BorrowerId = borrowerId;
            MaximumDevices = maximumDevices;
        }
    }

    public class InvalidBorrowPeriodException : BorrowingException
    {
        public InvalidBorrowPeriodException(string message, string deviceId, int requestedDays, int maxDays) 
            : base($"{message} (Mã TB: {deviceId}, Yêu cầu: {requestedDays} ngày, Tối đa: {maxDays} ngày).") { }
    }

    public class DuplicateDeviceException : BorrowingException
    {
        public DuplicateDeviceException(string message) : base(message) { }
    }

    public class DeviceNotInSlipException : BorrowingException
    {
        public DeviceNotInSlipException(string message) : base(message) { }
    }

    public class DeviceAlreadyReturnedException : BorrowingException
    {
        public DeviceAlreadyReturnedException(string message) : base(message) { }
    }
}
