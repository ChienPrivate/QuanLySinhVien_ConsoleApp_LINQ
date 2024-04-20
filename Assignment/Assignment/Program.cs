using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Assignment
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.InputEncoding = System.Text.Encoding.UTF8;
            Menu();
        }

        // hàm Menu chứa các chức năng từ Y1 đến Y7
        static void Menu()
        {
            ConsoleKeyInfo key;
            ConsoleKeyInfo minikey;
            bool exitloop = false;
            var stl = new ManageStudent();
            var cll = new ManageClass();
            do
            {
                Console.WriteLine("----Ấn các phím[số] tương ứng để chọn chức năng----\n");
                Console.WriteLine("[1] Nhập danh sách lớp và danh sách học viên\n");
                Console.WriteLine("[2] Xuất danh sách học viên\n");
                Console.WriteLine("[3] Tìm kiếm học viên theo khoảng điểm\n");
                Console.WriteLine("[4] tìm học viên theo StId và cập nhật thông tin\n");
                Console.WriteLine("[5] Xuất học viên ra màn hình theo thứ tự từ điểm cao tới thấp\n");
                Console.WriteLine("[6] Xuất 5 học viên có điểm cao nhất\n");
                Console.WriteLine("[7] tạo thread DTB tính điểm trung bình theo từng lớp\n");
                Console.WriteLine("[Esc] thoát khỏi menu");
                key = Console.ReadKey(true);
                Console.Clear();
                switch (key.Key)
                {
                    case ConsoleKey.D1:
                        do
                        {
                            Console.WriteLine("1.Nhập danh sách học viên");
                            Console.WriteLine("2.Nhập danh sách lớp");
                            Console.Write("nhấn Esc để thoát chức năng ");
                            minikey = Console.ReadKey(true);
                            if (minikey.Key == ConsoleKey.Escape)
                            {
                                break;
                            }
                            else
                            {
                                Console.Clear();
                                switch (minikey.Key)
                                {
                                    case ConsoleKey.D1:
                                        stl.AddStudentIntoList();
                                        break;
                                    case ConsoleKey.D2:
                                        cll.AddClassIntoList();
                                        break;
                                    default:
                                        break;
                                }
                            }
                        } while (true);
                        break;
                    case ConsoleKey.D2:
                        stl.DisplayStudentList();
                        break;
                    case ConsoleKey.D3:
                        stl.SearchWithRangeOfPoint();
                        break;
                    case ConsoleKey.D4:
                        stl.SearchWithStId();
                        break;
                    case ConsoleKey.D5:
                        stl.DisplayStudentMarkDesc();
                        break;
                    case ConsoleKey.D6:
                        stl.DisplayTop5Mark();
                        break;
                    case ConsoleKey.D7:
                        stl.ThreadAvgMarkEachClass();
                        break;
                }
                Console.Clear();
            }while (key.Key != ConsoleKey.Escape);
            do
            {
                Console.WriteLine("Bạn có muốn thoát khỏi chương trình ?");
                Console.WriteLine("\n[Ấn Esc lần nữa để thoát]\t[Ấn Enter để trở lại chương trình]");
                key = Console.ReadKey(true);
                Console.Clear();
                switch (key.Key)
                {
                    case ConsoleKey.Enter:
                        Menu();
                        break;
                }
            } while (key.Key != ConsoleKey.Escape);
            Environment.Exit(0);
        }
    }
}
