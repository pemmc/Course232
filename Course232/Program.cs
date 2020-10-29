
using System;
using System.IO;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
using Course232.Entities;

namespace Course232
{
    class Program
    {
        static void Main(string[] args)
        { 
        //windows C:\Users\<YourUserName>
        //string sourcePath = "c:\\Users\\Administrator\\Projects\\Exercicio\\Files\\csv\\Estoque.csv";
        //ou simplesmente usar o @ na frente antes das pastas... daí nao precisa duas barras \\
        //string sourcePath = @"c:\Users\Administrator\Projects\Exercicio\Files\csv\Estoque.csv";

        //mac
        //diretorio padrao '/Users/nxgames/Exercicio/Files/bin/Debug/netcoreapp3.1
        //Console.WriteLine(Directory.GetCurrentDirectory());

        //https://www.udemy.com/course/programacao-orientada-a-objetos-csharp/learn/lecture/11443358#overview
        Console.WriteLine("EXERCÍCIO DE FIXAÇÃ0");
        Console.WriteLine("====================");
        Console.WriteLine();

        //Diretorio que coloquei o arquivo
        string pathSeparator = Path.DirectorySeparatorChar.ToString();

        string path = "/Users/nxgames/Projects/Course232/Course232";
        string sourceFile = "csv" + pathSeparator + "Estoque.csv";

        string sourceFilePath = path + pathSeparator + sourceFile;

        Console.Write("Confirme o path: " + sourceFilePath);
            Console.WriteLine();

            Console.ReadLine();

            List<Product> list = new List<Product>();

            using (StreamReader sr = File.OpenText(sourceFilePath))
            {
                while (!sr.EndOfStream)
                {
                    string[] fields = sr.ReadLine().Split(',');
                    string name = fields[0];
                    double price = double.Parse(fields[1], CultureInfo.InvariantCulture);

                    list.Add(new Product(name, price));
                }
            }

            //double avg = list.Select(p => p.Price).DefaultIfEmpty(0.0).Average();


            double avg = (from p in list
                       select p.Price)
                       .DefaultIfEmpty(0.0)
                       .Average();

            Console.WriteLine("Average price = " + avg.ToString("C", new CultureInfo("pt-BR")));

            /* padrão LINQ
            var names = list
                            .Where(
                                    p => p.Price < avg
                                   )
                            .OrderByDescending(
                                p => p.Name
                                )
                            .Select(
                                p => p.Name
                                );
            */

            //USANDO O LINQ COM NOTAÇÃO SQL - colocando em ordem alfabética
            IEnumerable<Product> names =
                    from p in list
                    where p.Price < avg
                    orderby p.Name descending
                    select p;

            foreach (Product produc in names)
            {
                Console.WriteLine(produc.Name + " " + produc.Price.ToString("C", new CultureInfo("pt-BR")));

            }
        }
    }
}
