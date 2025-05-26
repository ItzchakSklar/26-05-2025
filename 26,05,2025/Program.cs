using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using System.Net.Http;
using System.Runtime.InteropServices;
//using System.Text.Json;
namespace _26_05_2025
{
    internal class Program
    {
        static async Task Main(string[] args)
        {

            //RunFileCreater();
            Post[] posts = await GetFromApi();
            Print5FromPost(posts);
            FindIdPostAndPrint(posts);

        }
        private static void RunFileCreater()
        {
            Console.WriteLine("welckom to the file writer");
            Console.WriteLine();
            bool Run = true;
            while (Run) 
            {
                Menu();
                int Choice = CoiceLoop();
                switch (Choice) {
                    case 1:
                        {
                            string[] file = InputFileRequirements();
                            file[0] = ValidatPath(file[0]);
                            FileCrweater(file);
                            break;
                        }
                    case 2:
                        {
                            Run = false;
                            break;
                        }
                }
            }
        }
        private static void Menu()
        {
            Console.WriteLine("menu: "); 
            Console.WriteLine("enter 1 to write a text to file");
            Console.WriteLine("enter 2 to exit");
        }
        private static int CoiceLoop()
        {
            bool GoodInput;
            string Choice;
            do
            {
                Choice = Console.ReadLine();
                GoodInput = ValidaitChoice(Choice);

            } while (!GoodInput);
            Console.Clear();
            return Convert.ToInt32(Choice);
        }
        private static bool ValidaitChoice(string Choice)
        {
            bool IsDigit = int.TryParse(Choice, out int result);
            if (IsDigit)
            {
                int ChoiceInt = Convert.ToInt32(Choice);
                if (ChoiceInt >= 1 && ChoiceInt <= 2)
                    return true;
                else
                {
                    Console.WriteLine("not in range choice");
                    return false;
                }
            }
            else
            {
                Console.WriteLine("not a number");
                    return false;
            }
        }
        private static string[] InputFileRequirements() 
        {
            Console.WriteLine("enter the name of file you womt to creat");
            string Titl = Console.ReadLine();
            Console.WriteLine("enter the txt you wont to write into the file");
            string txt = Console.ReadLine();
            Console.Clear();
            return new string[]{ Titl, txt };    

        }
        private static string ValidatPath(string path)
        {
            do
            {
                if (path == "")
                    return "Newfile.txt";
                if (path[path.Length - 1].Equals(' '))
                    path = path.Substring(0, path.Length - 1);
            } while (path[path.Length - 1].Equals(' '));
            
            return path+".txt";
        }
        private static void FileCrweater(string[] file)
        {
            string path = file[0];
            string content = file[1];

            File.WriteAllText(path, content);
        }
        private static async Task<Post[]> GetFromApi()
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage respone = await client.GetAsync("https://jsonplaceholder.typicode.com/posts");
            respone.EnsureSuccessStatusCode();
            string json = await respone.Content.ReadAsStringAsync();
            Post[] posts = JsonConvert.DeserializeObject<Post[]>(json);
            return posts;
        }
        private static void Print5FromPost(Post[] posts)
        {
            foreach (Post d in posts.Take(5))
                Console.WriteLine(d);
        }
        private static void FindIdPostAndPrint(Post[] post)
        {
            bool GoodInput = false;
            string ChoisId;
            int CoiceIdInt;
            do
            {
                Console.WriteLine("enter the id you wont to see: ");
                ChoisId = Console.ReadLine();
                GoodInput = ValidateChoiseId(ChoisId,post.Length);
                CoiceIdInt = Convert.ToInt32(ChoisId);
            } while (!GoodInput);
            foreach (Post p in post)
            {
                if (p.Id == CoiceIdInt)
                {
                    Console.WriteLine(p);
                }
            }
        }
        private static bool ValidateChoiseId(string ChoisId,int len)
        {
            bool IsDigit = int.TryParse(ChoisId, out int result);
            if (IsDigit)
            {
                int ChoiceInt = Convert.ToInt32(ChoisId);
                if (ChoiceInt >= 1 && ChoiceInt < len)
                    return true;
                else
                {
                    Console.WriteLine("not in range choice");
                    return false;
                }
            }
            else
            {
                Console.WriteLine("not a number");
                return false;
            }
        }
       // עשיתי כמה שיכולתי השלמתי ממש בשעות המאוחרות של הלילה (השעה 11:05 בלילה) מקווה שאחזור לחומר באופן פרטי כדי להשלים את כל הדרישות
        
    }
    public class Post
    {
        public int UserId { get; set; }
        public int Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public override string ToString()
        {
            return $"Id: {Id}\nUserId: {UserId}\nTitle: {Title}\nBody: {Body}\n\n";
        }
    }
}
