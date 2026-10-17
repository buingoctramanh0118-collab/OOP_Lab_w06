/*
MSSV: 202418832
Họ tên: Bùi Ngọc Trâm Anh
*/

using System;
using System.Collections.Generic;
using System.Linq;
using Labw06.Enums;

namespace Labw06.Models
{
    public class ClassRoom
    {
        public string RoomId { get; set; }
        public string RoomName { get; set; }
        public int Capacity { get; set; }
        
        private readonly List<Device> _devices = new();
        public IReadOnlyList<Device> Devices => _devices.AsReadOnly();

        public ClassRoom(string roomId, string roomName, int capacity)
        {
            RoomId = roomId;
            RoomName = roomName;
            Capacity = capacity;
            _devices = new List<Device>();
        }

        public void AddDevice(Device device)
        {
            if (device == null)
                throw new ArgumentNullException(nameof(device), "Không thể thêm thiết bị null vào phòng.");

            if (_devices.Any(d => d.DeviceId.Equals(device.DeviceId, StringComparison.OrdinalIgnoreCase)))
                throw new InvalidOperationException($"Lỗi: Thiết bị có mã '{device.DeviceId}' đã tồn tại trong phòng {RoomName}.");

            _devices.Add(device);
        }

        public bool RemoveDevice(string deviceId)
        {
            var device = FindDevice(deviceId);
            if (device != null)
                return _devices.Remove(device);
            return false;
        }

        public Device? FindDevice(string deviceId)
        {
            return _devices.FirstOrDefault(d => d.DeviceId.Equals(deviceId, StringComparison.OrdinalIgnoreCase));
        }

        public decimal CalculateAnnualMaintenanceCost()
        {
            return _devices.Sum(d => d.CalculateAnnualMaintenanceCost());
        }

        public List<Device> GetDevicesRequiringMaintenance()
        {
            return _devices.Where(d => d.Status == DeviceStatus.UnderMaintenance || d.GetYearsInUse() > 5).ToList();
        }
    }
}
