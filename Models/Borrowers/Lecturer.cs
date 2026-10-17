/*
MSSV: 202418832
Họ tên: Bùi Ngọc Trâm Anh
*/ 

namespace Labw06.Models.Borrowers;

public class Lecturer : IBorrower
{
    public string BorrowerId { get; }
    public string FullName { get; }

    public Lecturer(string id, string name)
    {
        BorrowerId = id;
        FullName = name;
    }

    public int GetMaximumDevices() => 3;
}
