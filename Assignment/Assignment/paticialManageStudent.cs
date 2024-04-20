using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Assignment
{
    internal partial class ManageStudent
    {
        // phương thức đặt bẫy lỗi kiểm tra tên được nhập từ bàn phím
        public string CheckNameInput()
        {
            string name;
            bool isDuplicate;
            bool isContainNum;
            bool isNullOrEmpty;
            using (var db = new SQLServiceDataContext(ConnectionString))
            {
                do
                {
                    name = Console.ReadLine();
                    var checkname = db.Students.Where(x => x.Name == name).FirstOrDefault();
                    isDuplicate = checkname != null;
                    isContainNum = name.Any(char.IsDigit);
                    isNullOrEmpty = String.IsNullOrEmpty(name);
                    if (isDuplicate)
                    {
                        Console.WriteLine("tên đã tồn tại trong CSDL hãy thêm hậu tố (A-Z hoặc số) cho tên và nhập lại");
                    }
                    if(isContainNum)
                    {
                        Console.WriteLine("tên không được chứa số, hãy nhập lại");
                    }
                    if (isNullOrEmpty)
                    {
                        Console.WriteLine("tên không được để trống, hãy nhập lại");
                    }
                } while (isContainNum || isNullOrEmpty || isDuplicate);
                return name;
            }
        }

        // phương thức kiểm tra định dạng của email 
        public bool IsValidEmail(string email)
        {
            try
            {
                var addr = new MailAddress(email);
                return true;
            }
            catch
            { 
                return false; 
            }

        }

        // phương thức đặt bẫy lỗi kiểm tra Email được nhập từ bàn phím
        public string CheckMailInput()
        {
            string email;
            bool isDuplicate;
            using (var db = new SQLServiceDataContext(ConnectionString))
            {
                do
                {
                    email = Console.ReadLine();
                    var isDuplicateMail = db.Students.Where(m => m.Email == email).FirstOrDefault();
                    isDuplicate = isDuplicateMail != null;
                    if (!IsValidEmail(email))
                    {
                        Console.WriteLine("mail sai định dạng, hãy nhập lại");
                    }
                    if (isDuplicate)
                    {
                        Console.WriteLine("mail không được trùng nhau, hãy nhập lại");
                    }
                } while (!IsValidEmail(email) || isDuplicate);
                return email;
            }
        }

        // phương thức đặt bẫy lỗi và kiểm tra điểm được nhập từ bàn phím
        public double CheckMarkInput()
        {
            double mark;
            bool isOutOfRange;
            bool isANumber;
            do
            {
                isANumber = double.TryParse(Console.ReadLine(), out mark);
                isOutOfRange = mark < 0 || mark > 10;
                if (isOutOfRange)
                {
                    Console.WriteLine("điểm phải trong khoảng từ 1 đến 10, hãy nhập lại");
                }
                if (!isANumber)
                {
                    Console.WriteLine("điểm nhập vào phải là số, hãy nhập lại");
                }
            } while (isOutOfRange || !isANumber);
            return mark;
        }

        // phương thức đặt bẫy lỗi và kiểm tra IdClass được nhập từ bàn phím
        public int CheckIdClassInput()
        {

            using (var db = new SQLServiceDataContext(ConnectionString))
            {
                int id;
                bool isANumber;
                bool clIdIsNull;
                do
                {
                    isANumber = int.TryParse(Console.ReadLine(), out id);
                    var existstid = db.Classes.Where(i => i.IdClass == id).FirstOrDefault();
                    clIdIsNull = existstid == null;
                    if (!isANumber)
                    {
                        Console.WriteLine("id phải là số, hãy nhập lại");
                    }
                    if (clIdIsNull)
                    {
                        Console.WriteLine("id Class này không tồn tại, hãy nhập lại");
                    }
                } while (!isANumber || clIdIsNull);
                return id;
            }
        }

        // phương thức đặt bẫy lỗi và kiểm tra IdStudent được nhập từ bàn phím
        public int CheckIdStudentInput()
        {
            int id;
            bool isNotExist;
            bool isANumber;
            using (var db = new SQLServiceDataContext(ConnectionString))
            {
                
                do
                {
                    isANumber = int.TryParse(Console.ReadLine(), out id);
                    var existId = db.Students.FirstOrDefault(i => i.StId == id);
                    isNotExist = existId == null;
                    if (!isANumber)
                    {
                        Console.WriteLine("mã học viên phải là số, hãy nhập lại");
                    }
                    if (isNotExist)
                    {
                        Console.WriteLine("mã học viên không tồn tại, hãy nhập lại");
                    }
                } while (!isANumber || isNotExist);
            }
            return id;
        }
    }
}
