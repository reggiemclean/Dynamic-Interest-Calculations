using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace A2Q2
{
    class Program
    {
        static void Main(string[] args)
        {
            int numberOfYears; 
            double temp=0;
            double[] storeRate;
            double[,] interestArray, inputArray;
            int[] splitArray;
            double interestRate;
            Stack<int> stratStack = new Stack<int>();

            numberOfYears = 5;
            storeRate = new double[numberOfYears];
            inputArray= new double[numberOfYears,numberOfYears];

            interestArray = new double[numberOfYears, numberOfYears];

            splitArray = new int[numberOfYears];

            Console.WriteLine("Enter the interest rates for the next " + numberOfYears+ " years");
            for (int i = 0; i < numberOfYears - 1; i++)
            {
                for (int j = i + 1; j < numberOfYears; j++)
                {
                    Console.WriteLine("Interest rate for year " + i + " to year " + j + ":");
                    interestRate = Convert.ToDouble(Console.ReadLine());
                    inputArray[i, j] = interestRate;
                }
            }

            for (int a = 0; a < numberOfYears; a++) //determines if the interestneeds to be compounded
            {
                for (int b = 0; b < numberOfYears; b++)
                {
                    if (b - a > 1)
                    {
                        interestArray[a, b] = (double)Math.Pow((1+inputArray[a, b]), (b - a))-1;// +(double)(inputArray[a, b] * (b - a));
                    }
                    else if (b > a)
                    {
                        interestArray[a, b] = inputArray[a, b];
                    }
                    else
                    {
                        interestArray[a, b] = 0;
                    }
                }
            }
            for (int j = 1; j < numberOfYears; j++)
            {
                for (int i = 1; i < j; i++)
                {
                    temp = ((1+interestArray[i, j]) * (1+interestArray[i - 1, i]))-1;// +interestArray[i, j] + interestArray[i - 1, i];
                    interestArray[i, j] = temp;
                    Console.WriteLine("i: " + i + " j: " + j + " interest: " + temp);
                    if (temp > interestArray[0, j])
                    {
                        interestArray[0, j] = temp;
                        splitArray[j] = i;
                    }
                }
            }

            int index = numberOfYears - 1;
            stratStack.Push(index);
            while (index != 0)
            {
                stratStack.Push(splitArray[index]);
                index = splitArray[index];
            }

            temp = stratStack.Pop();
            double temp2;
            while (stratStack.Count != 0)
            {
                temp2 = temp;
                Console.Write("Invest in year: " + temp + " and ");
                temp = stratStack.Pop();
                Console.Write("Sell in year: " + temp + " at interest rate: " + interestArray[(int)temp2, (int)temp]);
                Console.WriteLine();
            }

            Console.WriteLine("By following this strategy you will make: " + interestArray[0, numberOfYears - 1] * 100 + "% profit");
            Console.ReadKey();
        }
    }
}
