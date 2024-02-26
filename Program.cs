using System;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

class OptimizedPrimeFinder
{
    static void Main(string[] args)
    {
        Console.WriteLine("Enter the maximum number (x) to find primes up to: ");
        int x = Convert.ToInt32(Console.ReadLine());
        int iteration = 1;
        Stopwatch stopwatch = new Stopwatch(); // Start timing
        List<string> primeList = new List<string>();
        while (true)
        {
            Console.Clear();
            Console.WriteLine($"Currently calculating for: {iteration * x}, iteration {iteration}");
            //int x = Convert.ToInt32(Console.ReadLine());
            stopwatch.Start();

            int numToCalTo = x * iteration;
            bool[] isPrime = Enumerable.Repeat(true, numToCalTo + 1).ToArray();
            isPrime[0] = isPrime[1] = false; // 0 and 1 are not prime numbers

            for (int i = 2; i * i <= numToCalTo; i++)
            {
                if (isPrime[i])
                {
                    for (int j = i * i; j <= numToCalTo; j += i)
                        isPrime[j] = false;
                }
            }

            int primeCount = 0;
            for (int i = 2; i <= numToCalTo; i++)
            {
                if (isPrime[i])
                {
                    //Console.WriteLine(i);
                    //primeList.Add(i.ToString());
                    primeCount++;
                }
            }

            stopwatch.Stop(); // Stop timing    
            Console.WriteLine($"Total primes found: {primeCount}");
            Console.WriteLine($"Elapsed time: {stopwatch.ElapsedMilliseconds} ms");
            string str = $"{iteration * x} - {stopwatch.ElapsedMilliseconds}ms - {primeCount} primes";
            primeList.Add(str);
            WriteListOfStringsToFile(primeList, Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "primelist.txt"));
            //Console.WriteLine($"Saved to file!");

            //Console.WriteLine("Press any key to go to next number.");
            //Console.ReadKey();
            stopwatch.Reset();
            iteration++;
        }
    }

    public static void WriteListOfStringsToFile(List<string> massiveList, string filePath)
    {
        // Adjust the bufferSize as needed for optimal performance depending on the system and file sizes
        int bufferSize = 65536; // 64 KB

        // Using 'using' statement for automatic resource management
        using (StreamWriter writer = new StreamWriter(filePath, false, System.Text.Encoding.UTF8, bufferSize))
        {
            foreach (string line in massiveList)
            {
                writer.WriteLine(line);
            }
        }
    }
}