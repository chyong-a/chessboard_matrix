using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ChessMatrix.CBMatrix;

namespace ChessMatrix
{
    class Menu
    {
        private List<CBMatrix> listOfMatrices = new();
        public void Run()
        {
            int option;
            do
            {
                option = DisplayMenu();
                switch (option)
                {
                    case 1:
                        AddMatrix();
                        break;
                    case 2:
                        PrintMatrix();
                        break;
                    case 3:
                        GetElement();
                        break;
                    case 4:
                        SetElement();
                        break;
                    case 5:
                        SumMatrices();
                        break;
                    case 6:
                        MultiplyMatrices();
                        break;
                    case 0:
                        Console.WriteLine("Bye-bye!");
                        break;
                    default:
                        Console.WriteLine("\nInvalid operation!");
                        break;
                }
            } while (option != 0);
        }
        private static int DisplayMenu()
        {
            int option;
            Console.WriteLine("\n********************************");
            Console.WriteLine("0. Exit");
            Console.WriteLine("1. Create a matrix");
            Console.WriteLine("2. Print a matrix");
            Console.WriteLine("3. Get an entry");
            Console.WriteLine("4. Overwrite an element");
            Console.WriteLine("5. Add two matrices");
            Console.WriteLine("6. Multiply two matrices");
            Console.WriteLine("****************************************");
            option = int.Parse(Console.ReadLine());
            return option;
        }
        private int GetIndex()
        {
            if (listOfMatrices.Count() == 0) return -1;
            int input = 0;
            bool ok;
            do
            {
                Console.Write("Give a matrix index: ");
                ok = false;
                try
                {
                    input = int.Parse(Console.ReadLine()!);
                    ok = true;
                }
                catch (System.FormatException)
                {
                    Console.WriteLine("Integer must be provided!");
                }
                if (input <= 0 || input > listOfMatrices.Count())
                {
                    ok = false;
                    Console.WriteLine("Matrix with this index does not exist!");
                }
            } while (!ok);
            return input-1;
        }
        private void AddMatrix()
        {
            bool ok = false;
            int sizeOfNewMatrix = -1;

            do
            {
                Console.Write("Size: ");
                try
                {
                    sizeOfNewMatrix = int.Parse(Console.ReadLine()!);
                    ok = sizeOfNewMatrix>0;
                }
                catch (CBMatrix.InvalidSizeException)
                {
                    Console.WriteLine("Positive integer must be provided!");
                }
            } while (!ok);

            ok = true;
            List<int> elements = new();
            int numberOfValues;
            if (sizeOfNewMatrix % 2 == 0)
            {
                numberOfValues = sizeOfNewMatrix*sizeOfNewMatrix/2;
            }
            else
            {
                numberOfValues = (sizeOfNewMatrix*sizeOfNewMatrix/2) + 1;
            }
            for (int i = 0; i < numberOfValues; i++)
            {
                Console.Write("Element: ");
                try
                {
                    int elem = int.Parse(Console.ReadLine()!);
                    elements.Add(elem);
                }
                catch (System.FormatException)
                {
                    Console.WriteLine("Integer is expected!");
                    ok = false;
                    break;
                }
            }

            if (ok)
            {
                CBMatrix newMatrix = new CBMatrix(elements);
                listOfMatrices.Add(newMatrix);
            }
        }
        private void PrintMatrix()
        {
            if (listOfMatrices.Count() == 0)
            {
                Console.WriteLine("Set a matrix first!");
            }
            else
            {
                int index = GetIndex();
                Console.WriteLine(listOfMatrices[index]);
            }
        }
        private void GetElement()
        {
            if (listOfMatrices.Count() == 0)
            {
                Console.WriteLine("Set a matrix first!");
            }
            else
            {
                int ind = GetIndex();
                do
                {
                    try
                    {
                        Console.WriteLine("Give the index of the row: ");
                        int i = int.Parse(Console.ReadLine()!);
                        Console.WriteLine("Give the index of the column: ");
                        int j = int.Parse(Console.ReadLine()!);
                        Console.WriteLine($"The element in the row {i} and in the column {j} is " + listOfMatrices[ind].GetElement(i-1, j-1) +".");
                        break;
                    }
                    catch (System.FormatException)
                    {
                        Console.WriteLine($"Indeces must be from 1 to {listOfMatrices[ind].GetSize()}!");
                    }
                    catch (CBMatrix.InvalidIndexException)
                    {
                        Console.WriteLine($"Indeces must be from 1 to {listOfMatrices[ind].GetSize()}!");
                    }
                } while (true);
            }
        }
        private void SetElement()
        {
            if (listOfMatrices.Count() == 0)
            {
                Console.WriteLine("Set a matrix first!");
            }
            else
            {
                int ind = GetIndex();
                do
                {
                    try
                    {
                        Console.WriteLine("Give the index of the row: ");
                        int i = int.Parse(Console.ReadLine()!);
                        Console.WriteLine("Give the index of the column: ");
                        int j = int.Parse(Console.ReadLine()!);
                        Console.WriteLine("Give the value: ");
                        int e = int.Parse(Console.ReadLine()!);
                        listOfMatrices[ind].SetElement(i-1, j-1, e);
                        break;
                    }
                    catch (System.FormatException)
                    {
                        Console.WriteLine($"Indeces must be from 1 to {listOfMatrices[ind].GetSize()}!");
                    }
                    catch (CBMatrix.InvalidIndexException)
                    {
                        Console.WriteLine($"Indeces must be from 1 to {listOfMatrices[ind].GetSize()}!");
                    }
                    catch (CBMatrix.ZeroCellException)
                    {
                        Console.WriteLine("You are trying to overrwrite the value of the zero cell which cannot be rewritten.");
                    }
                } while (true);
            }
        }
        private void SumMatrices()
        {
            if (listOfMatrices.Count() < 2)
            {
                Console.WriteLine("Set, at least, 2 matrices first!");
            }
            else
            {
                Console.Write("1st matrix: ");
                int ind1 = GetIndex();
                Console.Write("2nd matrix: ");
                int ind2 = GetIndex();
                try
                {
                    Console.Write((CBMatrix.Add(listOfMatrices[ind1], listOfMatrices[ind2])));
                }
                catch (CBMatrix.DimensionMismatchException)
                {
                    Console.WriteLine("The matrices must be of the same size!");
                }
            }
        }
        private void MultiplyMatrices()
        {
            if (listOfMatrices.Count() < 2)
            {
                Console.WriteLine("Set, at least, 2 matrices first!");
                return;
            }
            else
            {
                Console.Write("1st matrix: ");
                int ind1 = GetIndex();
                Console.Write("2nd matrix: ");
                int ind2 = GetIndex();
                try
                {
                    Console.Write((CBMatrix.Multiply(listOfMatrices[ind1], listOfMatrices[ind2])));
                }
                catch (CBMatrix.DimensionMismatchException)
                {
                    Console.WriteLine("The matrices must be of the same size!");
                }
            }
        }
    }
}