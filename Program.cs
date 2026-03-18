using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CategoryManagementSystem
{
    internal class Program
    {
        static void Main(string[] args)
        {
            SqlConnection connection = new SqlConnection("Data Source=localhost\\evaleyn5;initial catalog=SqlProject;integrated security=true");

            while (true)
            {
                Console.Clear();
                Console.WriteLine("****Restoran Menü Sipariş İşlem Paneli****");
                Console.WriteLine();

                Console.WriteLine("Yapmak İstediğiniz İşlemi Seçiniz: ");
                Console.WriteLine("1- Ürün Ekle");
                Console.WriteLine("2- Ürün Listele");
                Console.WriteLine("3- Ürün Sil");
                Console.WriteLine("4- Ürün Güncelle");
                Console.WriteLine("0- Çıkış Yap");
                Console.Write("Seçiminiz: ");
                
                int choice = int.Parse(Console.ReadLine());

                if (choice == 0) 
                {
                    Console.WriteLine("Programdan çıkılıyor...");
                    break; 
                }

                #region Kategori Ekleme İşlemi

                if (choice == 1)
                {
                    Console.Write("Ürün Adı: ");
                    string name = Console.ReadLine();

                    Console.Write("Ürün Fiyatı: ");
                    decimal price = decimal.Parse(Console.ReadLine());

                    Console.WriteLine("Kategori Seç: ");
                    Console.WriteLine("1- Başlangıç");
                    Console.WriteLine("2-Ana Yemek");
                    Console.WriteLine("3-Tatlı");
                    int categoryId = int.Parse(Console.ReadLine());

                    connection.Open();

                    SqlCommand command = new SqlCommand("insert into TbsProduct (ProductName, ProductPrice, CategoryId) values (@pname, @pprice, @pcategoryid)", connection);
                    command.Parameters.AddWithValue("@pname", name);
                    command.Parameters.AddWithValue("@pprice", price);
                    command.Parameters.AddWithValue("@pcategoryid", categoryId);

                    command.ExecuteNonQuery();
                    connection.Close();

                    Console.WriteLine("Ürün kategoriyle birlikte eklendi!");
                }

                #endregion


                #region Kategori Listeleme İşlemi

                if (choice == 2)
                {
                    connection.Open();
                    string query = "Select * From TbsProduct";
                    SqlCommand command = new SqlCommand(query, connection);

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    connection.Close();

                    Console.WriteLine("******Ürün Listesi******");

                    foreach (DataRow row in dataTable.Rows)
                    {
                        Console.WriteLine($"{row["ProductId"],-5} | {row["ProductName"],-30} | {row["ProductPrice"]} | {row["CategoryId"]}");
                    }
                    Console.WriteLine("********************************");
                }

                #endregion


                #region Kategori Silme İşlemi

                if (choice == 3)
                {
                    connection.Open();
                    Console.Write("Silmek istediğiniz ürünün ID'sini giriniz: ");
                    int id = int.Parse(Console.ReadLine());

                    string query = "Delete From TbsProduct Where ProductId = @pid";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@pid", id);

                    command.ExecuteNonQuery();
                    connection.Close();

                    Console.WriteLine("Ürün başarıyla silindi!");
                }

                #endregion


                #region Kategori Güncelleme İşlemi

                if (choice == 4)
                {
                    connection.Open();
                    Console.Write("Güncellemek istediğiniz ürünün ID numarasını giriniz: ");
                    int id = int.Parse(Console.ReadLine());

                    Console.Write("Yeni Ürün Adı: ");
                    string name = Console.ReadLine();

                    Console.Write("Yeni Ürün Fiyatı: ");
                    decimal price = decimal.Parse(Console.ReadLine());

                    Console.WriteLine("Yeni Kategori ID: ");
                    int categoryId = int.Parse(Console.ReadLine());

                    string query = "Update TbsProduct Set ProductName = @pname, ProductPrice = @pprice, CategoryId = @pcategoryid Where ProductId = @pid";
                    SqlCommand command = new SqlCommand(query, connection);

                    command.Parameters.AddWithValue("@pname", name);
                    command.Parameters.AddWithValue("@pprice", price);
                    command.Parameters.AddWithValue("@pcategoryid", categoryId);
                    command.Parameters.AddWithValue("@pid", id);

                    command.ExecuteNonQuery();
                    connection.Close();

                    Console.WriteLine("Ürün başarıyla güncellendi!");
                }

                #endregion

                Console.ReadKey();
            }
        }
    }
}
