using System;
using System.IO;

namespace palindrom
{
    class palindrom
    {
        public static void Main(string[] args)
        {
            Console.Clear();
            string response = null;
            while (true)
            {
                Console.Write("Do you want to choose a file [f] or type into the command line [t]?: ");
                response = Console.ReadLine();
                Console.Clear();
                if(response == "f" | response == "t")break;
            }

            string[] input = new string[1];
            if (response == "f")
            {
                string filePath = null;
                bool first = true;
                while (!File.Exists(filePath))
                {
                    if (!File.Exists(filePath) && !first)
                    {
                        Console.Write("Please input valid file: ");
                    }
                    Console.Write("Please write the file name: ");
                    filePath = Console.ReadLine();
                    Console.Clear();
                    first = false;
                }
                input = File.ReadAllLines(filePath);
            }
            else
            {
                while (true)
                {
                    Console.Write("Please insert word or sentence (mini 5 characters, max 20 characters): ");
                    input[0] = Console.ReadLine();
                    Console.Clear();
                    if(input[0].Length > 4)break;
                }
            }

            foreach (var line in input)
            {
                string inputLine = line;
                var charsToRemove = new string[] { "@", ",", ".", ";", "'", " " };
                foreach (var c in charsToRemove)
                {
                    inputLine = inputLine.Replace(c, string.Empty);
                }

                string testIfPalindrom = null;

                for (int i = inputLine.Length - 1; i >= 0; i--)
                {
                    testIfPalindrom += inputLine[i];
                }

                if(response == "f"){
                if (inputLine == testIfPalindrom)
                {
                    Console.WriteLine(inputLine);
                }}
                else{
                    Console.WriteLine("Palindrom");
                    Console.WriteLine(input[0].Length + " characters long.");
                }
                // else
                // {
                //     Console.WriteLine("Not a palindrom.");
                // }
            }
        }
    }
}