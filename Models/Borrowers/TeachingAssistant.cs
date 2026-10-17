/*
MSSV: 202418832
Họ tên: Bùi Ngọc Trâm Anh
*/

namespace Labw06.Models.Borrowers;

public class TeachingAssistant : IBorrower
{
    public string BorrowerId { get; }
    public string FullName { get; }

    public TeachingAssistant(string id, string name)
    {
        BorrowerId = id;
        FullName = name;
    }

    public int GetMaximumDevices() => 2;
}
