using System;
using System.Collections.Generic;

namespace SudokuSolver
{
    class Program
    {
        static void Main(string[] args)
        {
            byte[,] task={
    { 3, 0, 0, 0, 8, 0, 4, 0, 0 },
    { 0, 8, 0, 9, 0, 3, 0, 5, 0 },
    { 0, 0, 4, 1, 2, 6, 9, 0, 0 },
    { 9, 0, 5, 3, 0, 2, 6, 0, 0 },
    { 2, 4, 0, 0, 0, 5, 3, 0, 1 },
    { 7, 6, 3, 0, 1, 0, 0, 9, 0 },
    { 1, 3, 7, 2, 0, 9, 8, 6, 0 },
    { 0, 0, 0, 0, 0, 1, 0, 3, 5 },
    { 0, 5, 6, 7, 3, 0, 0, 1, 0 }
};
            PrintSudoku(task);
            
            var result = IsSolvedSudoku(task);
            if (result)
            {
                Console.WriteLine("Solved:");
                PrintSudoku(task);
            }
            else
                Console.WriteLine("Task is not solvable");
        }
        public static void PrintSudoku(byte[,] data)
        {
            for (byte i = 0; i < 9; i++)
            {
                var row = string.Empty;
                for (byte j = 0; j < 9; j++)
                {
                    row += $"{data[i,j]} ";
                }               
                    Console.WriteLine(row);               
            }
        }

		public static void GetCellWithMinimumPossibleVariants(byte[,] data, out byte row, out byte coll, out HashSet<byte> variants)
        {
            coll = 9;
            row = 9;
            variants = new HashSet<byte>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            for (byte i = 0; i < 9; i++)
            {
                for (byte j = 0; j < 9; j++)
                    if (data[i,j] == 0)
                    {
                        var newVariants = new HashSet<byte>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
                        
                        for (byte c = 0; c < 9; c++)
                        {
                            if (data[i,c]>0 && data[i,c] < 10 )
                            {
                                newVariants.Remove(data[i,c]);
                            }
                            if (data[c,j] > 0 && data[c,j] <10)
                            {
                                newVariants.Remove(data[c,j]);
                            }
                            if (data[(i / 3) * 3 + c / 3,(j / 3) * 3 + c% 3] > 0 && data[(i / 3) * 3 + c / 3,(j / 3) * 3 + c % 3] <10)
                            {
                                newVariants.Remove(data[(i / 3) * 3 + c / 3,(j / 3) * 3 + c % 3]);
                            }
                        }
                        if (newVariants.Count<variants.Count )
                        {
                            coll = j;
                            row = i;
                            variants = newVariants;
                        }
                    }               
            }
        }
        public static bool IsSolvedSudoku(byte[,] data)
        {
            bool isSolved = false;
            GetCellWithMinimumPossibleVariants(data, out byte row,out byte col, out HashSet<byte> variants);
            if(row>8||col>8)
            {
                isSolved = true;
            }
            else
            {
                foreach(var variant in variants)
                {
                   
                    data[row,col] = variant;
                    if (IsSolvedSudoku(data))
                    {
                        isSolved = true;
                        break;
                    }
                    data[row,col] = 0;
                }
            }
            return isSolved;
        }
    }
}
