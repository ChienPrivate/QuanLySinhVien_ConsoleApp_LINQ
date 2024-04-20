using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment
{
    internal class ManageClass
    {
        ConsoleKeyInfo key;
        // gán cho hằng ConnectString địa chỉ của Server,tên Database Database và chứng thực người dùng
        private const string ConnectionString = @"Data Source=CHIENNNPS27765;Initial Catalog=Asm_C#2;Integrated Security=True";

        // hàm thêm lớp vào danh sách lớp
        public void AddClassIntoList()
        {
            using (var db = new SQLServiceDataContext(ConnectionString))
            {
                var cll = new List<Class>();
                do
                {
                    var cl = new Class();
                    Console.Write("nhập tên lớp: ");
                    cl.NameClass = Console.ReadLine();
                    cll.Add(cl);
                    Console.WriteLine("Ấn Enter để tiếp tục, ấn Esc để dừng nhập và thoát");
                    key = Console.ReadKey(true);
                    Console.Clear();
                } while (key.Key != ConsoleKey.Escape);
                db.Classes.InsertAllOnSubmit(cll);
                db.SubmitChanges();
            }
        }

        // phương thức xuất danh sách lớp học
        public void DisplayClassList()
        {
            using (var db = new SQLServiceDataContext(ConnectionString))
            {
                var cll = db.Classes.Select(c => new { c.IdClass, c.NameClass });
                Console.WriteLine($"{"Mã lớp",-10} | {"Tên lớp",-20}");
                foreach(var cl in cll)
                {
                    Console.WriteLine($"{cl.IdClass,-10} | {cl.NameClass,-20}");
                }
                Console.ReadKey();
            }
        }
    }
}
