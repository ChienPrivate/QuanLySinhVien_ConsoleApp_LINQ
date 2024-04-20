using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Assignment
{
    internal partial class ManageStudent
    {
        // khởi tạo và gán giá trị cho biến
        ConsoleKeyInfo keyInfo = new ConsoleKeyInfo();
        private const string ConnectionString = @"Data Source=CHIENNNPS27765;Initial Catalog=Asm_C#2;Integrated Security=True";
        private const string filepath = @"D:\NET102_C#2_K3\excercise\PS27765_NguyenNgocChien_ASM2\Assignment\Assignment\ASM_C#2.txt";

        // phương thức thêm sinh viên vào danh sách
        public void AddStudentIntoList()
        {
            using (var db = new SQLServiceDataContext(ConnectionString))
            {
                var stl = new List<Student>();
                do
                {
                    var sti = new Student();
                    Console.Write("nhập tên sinh viên: ");
                    sti.Name = CheckNameInput();
                    Console.Write("nhập điểm sinh viên: ");
                    sti.Mark = CheckMarkInput();
                    Console.Write("nhập email sinh viên: ");
                    sti.Email = CheckMailInput();
                    Console.Write("nhập lớp của sinh viên: ");
                    sti.IdClass = CheckIdClassInput();
                    stl.Add(sti);
                    Console.WriteLine("nhấn Enter để tiếp tục, nhấp Esc để dừng");
                    keyInfo = Console.ReadKey(true);
                    Console.Clear();
                } while (keyInfo.Key != ConsoleKey.Escape);
                db.Students.InsertAllOnSubmit(stl);
                db.SubmitChanges();
            }
        }

        // phương thức xét học lực
        public string Classification(double mark)
        {
            if( mark >= 9)
            {
                return "Xuất sắc";
            }
            else if( mark >= 7.5)
            {
                return "Giỏi";
            }
            else if( mark >= 6.5)
            {
                return "Khá";
            }
            else if( mark >= 5)
            {
                return "Trung bình";
            }
            else if( mark < 5)
            {
                return "Yếu";
            }
            else
            {
                return "Kém";
            }
        }

        // phương thức thức hiển thị sinh viên (có tham số truyền vào)
        public void DisplayStudent(dynamic stl)
        {
            using (var db = new SQLServiceDataContext(ConnectionString))
            {
                Console.WriteLine($"{"Stid",-10} | {"Name",-20} | {"Mark",-10} | {"Email",-25} | {"IdClass",-10} | {"Classcification",-10}");
                foreach (var st in stl)
                {
                    Console.WriteLine($"{st.StId,-10} | {st.Name,-20} | {st.Mark,-10} | {st.Email,-25} | {st.IdClass,-10} | {Classification(st.Mark),-10}");
                }
            }
            Console.ReadKey();
        }

        // phương thức hiển thị sinh viên
        public void DisplayStudentList()
        {
            using (var db = new SQLServiceDataContext(ConnectionString))
            {
                var stl = db.Students.Select(st => st);
                if (stl.Any())
                {
                    DisplayStudent(stl);
                }
                else
                {
                    Console.WriteLine("Danh sách học sinh hiện đang trống");
                }
            }
        }

        // phương thức tìm kiếm sinh viên theo khoảng điểm được nhập từ bàn phím
        public void SearchWithRangeOfPoint()
        {
            using (var db = new SQLServiceDataContext(ConnectionString))
            {
                Console.Write("nhập điểm bắt đầu trong khoảng điểm: ");
                double stp = CheckMarkInput();
                Console.Write("nhập điểm kết thúc trong khoảng điểm: ");
                double ep = CheckMarkInput();
                var search = db.Students.Where(s => s.Mark >= stp && s.Mark <= ep);
                DisplayStudent(search);
            }
        }

        // phương thức tìm kiếm sinh viên theo id và cập nhật thông tin sinh viên
        public void SearchWithStId()
        {
            using (var db = new SQLServiceDataContext(ConnectionString))
            {
                Console.Write("nhập Id sinh viên cần tìm: ");
                int id = CheckIdStudentInput();
                var stl = db.Students.FirstOrDefault(s => s.StId == id);
                Console.Write("cập nhật tên mới: ");
                stl.Name = CheckNameInput();
                Console.Write("Cập nhật điểm mới: ");
                stl.Mark = CheckMarkInput();
                Console.Write("cập nhật email mới: ");
                stl.Email = CheckMailInput();
                Console.Write("cập nhập lớp mới: ");
                stl.IdClass = CheckIdClassInput();
                db.SubmitChanges();
                var updatedst = new List<Student> {stl};
                Console.WriteLine("\ncập nhật thành công");
                DisplayStudent(updatedst);
            }
        }

        // phương thức xuất danh sách sinh viên theo điểm giảm dần
        public void DisplayStudentMarkDesc()
        {
            using (var db = new SQLServiceDataContext(ConnectionString))
            {
                var stl = db.Students.Select(s => s).OrderByDescending(s => s.Mark);
                DisplayStudent(stl);
            }
        }

        // phương thức xuất danh sách top 5 sinh viên cao điểm nhất
        public void DisplayTop5Mark()
        {
            using (var db = new SQLServiceDataContext(ConnectionString))
            {
                var stl = (from st in db.Students
                          orderby st.Mark descending
                          select st).Take(5);
                DisplayStudent(stl);
            }
        }

        // phương thức tính điểm trung bình theo từng lớp và ghi vào file ASM_C#2.txt
        public void AverageMarkEachClass()
        {
            using (var db = new SQLServiceDataContext(ConnectionString))
            {
                var stl = from st in db.Students
                          join cl in db.Classes on st.IdClass equals cl.IdClass
                          group st by new { st.IdClass, cl.NameClass } into gr
                          select new { gr.Key.IdClass, gr.Key.NameClass, AvgMark = gr.Average(s => s.Mark) };

                FileInfo file = new FileInfo(filepath);
                using (StreamWriter sw = file.CreateText())
                {
                    sw.WriteLine($"{"mã lớp",-10} | {"Tên lớp",-10} | {"Điểm trung bình",-15}");
                    foreach (var item in stl)
                    {
                        sw.WriteLine($"{item.IdClass,-10} | {item.NameClass,-10} | {item.AvgMark}");
                    }
                }
                Console.Clear();
                Console.WriteLine("--------Xuất file thành công--------");
                Console.WriteLine("tên file: " + file.Name);
                Console.WriteLine("ngày tạo: " + file.CreationTime);
                Console.WriteLine("lần truy cập gần đây: " + file.LastAccessTime);
                using (StreamReader sr = new StreamReader(filepath))
                {
                    Console.WriteLine(sr.ReadToEnd());  
                }
            }
        }

        // phương thức khởi tạo và khởi động Thread có tên DTB dùng để tính trung bình theo từng lớp
        public void ThreadAvgMarkEachClass()
        {
            Thread DTB = new Thread(AverageMarkEachClass);
            DTB.Name = "DTB";
            DTB.Start();
        }
    }
}
