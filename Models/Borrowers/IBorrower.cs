/*
MSSV: 202418832
Họ tên: Bùi Ngọc Trâm Anh
*/

namespace Labw06.Models.Borrowers;

public interface IBorrower
{
    string BorrowerId { get; }
    string FullName { get; }
    int GetMaximumDevices();
}
