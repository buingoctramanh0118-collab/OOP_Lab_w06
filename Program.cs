/*
MSSV: 202418832
Họ tên: Bùi Ngọc Trâm Anh
*/

using System;
using System.Linq;
using Labw06.Models;
using Labw06.Models.Borrowers;
using Labw06.Models.Devices;
using Labw06.Exceptions;
using Labw06.Enums;

namespace Labw06
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            DateTime today = DateTime.Now.Date;

            Console.WriteLine(" 11.1. LUỒNG THÀNH CÔNG ");
            
            IBorrower lecturer = new Lecturer("GV01", "Nguyễn Văn A");
            Device projector = new ProjectorRemote("PRJ01", "Điều khiển máy chiếu 1", 2023, 500m, 5);
            Device airCon = new AirConditionerRemote("AC01", "Điều khiển điều hòa 1", 2023, 200m, 5);
            Device micro = new Microphone("MIC01", "Micro không dây 1", 2023, 1000m, 1);
            BorrowingSlip slip = new BorrowingSlip("S01", lecturer, "Phòng D9-101", today);

            try
            {
                slip.AddDevice(projector, 2);
                slip.AddDevice(micro, 1);
                Console.WriteLine("Đã thêm PRJ01 và MIC01 vào phiếu mượn.");

                slip.ReturnDevice("PRJ01", today.AddDays(1));
                Console.WriteLine("Đã trả PRJ01 đúng hạn.");

                slip.ReturnDevice("MIC01", today.AddDays(3));
                Console.WriteLine("Đã trả MIC01 muộn 2 ngày.");
                
                Device extraDevice = new ProjectorRemote("PRJ-EXTRA", "Máy chiếu phụ", 2023, 500m, 5);
                slip.AddDevice(extraDevice, 2);
                
                Console.WriteLine("\n-> DANH SÁCH THIẾT BỊ TRẢ MUỘN HOẶC CHƯA TRẢ:");
                var lateOrUnreturned = slip.Items.Where(i => 
                    !i.IsReturned || (i.IsReturned && i.ReturnedDate > i.DueDate)).ToList();

                foreach (var item in lateOrUnreturned)
                {
                    string status = !item.IsReturned ? "Chưa trả" : $"Trả muộn (Hạn: {item.DueDate:dd/MM} - Trả: {item.ReturnedDate?.ToString("dd/MM")})";
                    Console.WriteLine($"- Thiết bị {item.Device.DeviceId}: {status}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi luồng thành công: {ex.Message}");
            }


            Console.WriteLine(" 11.2. LUỒNG PHÁT SINH NGOẠI LỆ ");
            BorrowingSlip errorSlip = new BorrowingSlip("S02", lecturer, "Phòng D9-102", today);

            // 1
            Console.WriteLine("\n[Tình huống 1]:");
            airCon.Status = DeviceStatus.UnderMaintenance;
            try { errorSlip.AddDevice(airCon, 2); }
            catch (BorrowingException ex) { Console.WriteLine($"=> {ex.GetType().Name}: {ex.Message}"); }

            // 2
            Console.WriteLine("\n[Tình huống 2]:");
            IBorrower ta = new TeachingAssistant("TA01", "Trần Thị B");
            BorrowingSlip taSlip = new BorrowingSlip("S03", ta, "Phòng D9-103", today);
            try 
            {
                taSlip.AddDevice(new ProjectorRemote("PRJ02", "Máy 2", 2023, 100m, 5), 2);
                taSlip.AddDevice(new ProjectorRemote("PRJ03", "Máy 3", 2023, 100m, 5), 2);
                taSlip.AddDevice(new ProjectorRemote("PRJ04", "Máy 4", 2023, 100m, 5), 2); 
            }
            catch (BorrowingException ex) { Console.WriteLine($"=> {ex.GetType().Name}: {ex.Message}"); }

            // 3
            Console.WriteLine("\n[Tình huống 3]:");
            Device testMicro = new Microphone("MIC02", "Micro Test", 2023, 1000m, 5);
            try 
            {
                errorSlip.AddDevice(testMicro, 1);
                errorSlip.AddDevice(testMicro, 1); 
            }
            catch (BorrowingException ex) { Console.WriteLine($"=> {ex.GetType().Name}: {ex.Message}"); }

            // 4
            Console.WriteLine("\n[Tình huống 4]:");
            Device shortMicro = new Microphone("MIC03", "Micro ngắn hạn", 2023, 1000m, 1);
            try { errorSlip.AddDevice(shortMicro, 3); }
            catch (BorrowingException ex) { Console.WriteLine($"=> {ex.GetType().Name}: {ex.Message}"); }

            // 5
            Console.WriteLine("\n[Tình huống 5]:");
            try { slip.ReturnDevice("PRJ-GIA-MAO", today); }
            catch (BorrowingException ex) { Console.WriteLine($"=> {ex.GetType().Name}: {ex.Message}"); }

            // 6
            Console.WriteLine("\n[Tình huống 6]:");
            try 
            {
                slip.ReturnDevice("PRJ01", today.AddDays(2)); 
            }
            catch (BorrowingException ex) { Console.WriteLine($"=> {ex.GetType().Name}: {ex.Message}"); }

            // 7
            Console.WriteLine("\n[Tình huống 7]:");
            Device timePrj = new ProjectorRemote("PRJ05", "Máy 5", 2023, 100m, 5);
            BorrowingSlip timeSlip = new BorrowingSlip("S04", lecturer, "Phòng D9-104", today);
            try 
            {
                timeSlip.AddDevice(timePrj, 2);
                timeSlip.ReturnDevice("PRJ05", today.AddDays(-1)); 
            }
            catch (Exception ex) { Console.WriteLine($"=> {ex.GetType().Name}: {ex.Message}"); }

            Console.ReadLine();
        }
    }
}
