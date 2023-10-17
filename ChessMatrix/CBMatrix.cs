using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessMatrix
{
    public class CBMatrix
    {
        #region Exceptions
        public class InvalidIndexException : Exception { };
        public class ZeroCellException : Exception { };
        public class DimensionMismatchException : Exception { };
        public class InvalidVectorException : Exception { };
        public class InvalidSizeException : Exception { };
        #endregion

        #region Attributes
        private List<int> values = new();
        private int sizeOfMatrix;
        #endregion

        #region Constructors
        //with the size of matrix, will initiate each value to 1 (in contrary to 0)
        public CBMatrix(int sizeOfMatrix)
        {
            if (sizeOfMatrix < 1) { throw new InvalidSizeException(); }
            else
            {
                this.sizeOfMatrix = sizeOfMatrix; this.values = new List<int>();
                int numberOfValues;
                if (IsEven(sizeOfMatrix))
                {
                    numberOfValues = sizeOfMatrix*sizeOfMatrix / 2;
                }
                else
                {
                    numberOfValues = (sizeOfMatrix*sizeOfMatrix / 2) + 1;
                }
                for (int i = 0; i < numberOfValues; i++)
                {
                    this.values.Add(1);
                }
            }
        }
        //parameterless constructor creates a matrix of 3x3 with values 1, 2, 3, 4, 5
        public CBMatrix()
        {
            this.sizeOfMatrix = 3;
            this.values = new List<int>() { 1, 2, 3, 4, 5 };
        }
        public CBMatrix(List<int> values)
        {
            this.sizeOfMatrix = CountSizeBasedOnVector(values);
            this.values = values;
        }
        public CBMatrix(CBMatrix matrix)
        {
            for (int i = 0; i < matrix.values.Count(); i++)
            {
                values.Add(matrix.values[i]);
            }
            if (IsEven(matrix.values.Count()))
            {
                this.sizeOfMatrix = matrix.values.Count()/2;
            }
            else
            {
                this.sizeOfMatrix = matrix.values.Count()/2 + 1;
            }
        }
        #endregion

        #region Getters and Setters
        public int GetSize() { return sizeOfMatrix; }
        public int GetNumberOfValues() { return values.Count(); }
        public int GetElement(int row, int column)
        {
            if (AreIndicesGood(row, column))
            {
                if (ContainsValue(row, column))
                {
                    return this.values[GetIndexInVector(row, column)];
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                throw new InvalidIndexException();
            }
        }
        public void SetElement(int row, int column, int e)
        {
            if (AreIndicesGood(row, column))
            {
                if (IsEven(row + column))
                {
                    this.values[GetIndexInVector(row, column)] = e;
                }
                else { throw new ZeroCellException(); }
            }
            else
            {
                throw new InvalidIndexException();
            }
        }
        #endregion

        #region Main Methods
        public static CBMatrix Add(CBMatrix a, CBMatrix b)
        {
            if (a.sizeOfMatrix == b.sizeOfMatrix)
            {
                CBMatrix result = new CBMatrix(a);
                for (int i = 0; i < a.values.Count; i++)
                {
                    result.values[i] += b.values[i];
                }
                return result;
            }
            else
            {
                throw new DimensionMismatchException();
            }
        }
        public static CBMatrix Multiply(CBMatrix a, CBMatrix b)
        {
            if (a.sizeOfMatrix == b.sizeOfMatrix)
            {
                int size = a.sizeOfMatrix;
                CBMatrix result = new CBMatrix(size);
                for (int i = 0; i < size; i++)
                {
                    for (int j = 0; j < size; j++)
                    {
                        if ((i+j)%2 == 0)
                        {
                            int sum = 0;
                            for (int k = 0; k < size; k++)
                            {
                                sum += a.GetElement(i, k) * b.GetElement(k, j);
                            }
                            result.SetElement(i, j, sum);
                        }
                    }
                }
                return result;
            }
            else
            {
                throw new DimensionMismatchException();
            }
        }
        public override String ToString()
        {
            String str = "The matrix of size ";
            str += sizeOfMatrix.ToString() + "x" + sizeOfMatrix.ToString()+":\n";
            for (int i = 0; i <= sizeOfMatrix-1; i++)
            {
                for (int j = 0; j <= sizeOfMatrix-1; j++)
                {
                    str += GetElement(i, j).ToString() + " ";
                }
                str += "\n";
            }
            return str;
        }
        #endregion

        #region Utilities
        private bool IsEven(int x) { return x % 2 == 0; }

        private bool IsVectorGood(List<int> values)
        {
            int numberOfValues = values.Count();
            if (IsEven(numberOfValues))
            {
                return Math.Sqrt(numberOfValues*2) == Math.Floor(Math.Sqrt(numberOfValues*2));
            }
            else
            {
                return Math.Sqrt(numberOfValues*2-1) == Math.Floor(Math.Sqrt(numberOfValues*2-1));
            }
        }
        private int CountSizeBasedOnVector(List<int> values)
        {
            int numberOfValues = values.Count();
            if (IsVectorGood(values))
            {
                if (IsEven(numberOfValues))
                {
                    return Convert.ToInt32(Math.Sqrt(numberOfValues*2));
                }
                else
                {
                    return Convert.ToInt32(Math.Sqrt(numberOfValues*2-1));
                }
            }
            else
            {
                throw new InvalidVectorException();
            }
        }
        private bool ContainsValue(int row, int column) { return IsEven(row+column); }
        private int GetIndexInVector(int row, int column)
        {
            row++;
            column++;
            return sizeOfMatrix*(row-1) - Convert.ToInt32(sizeOfMatrix*(row-1)/2) + column - Convert.ToInt32(column/2) - 1;
        }
        private bool AreIndicesGood(int row, int column)
        {
            return row+1 <= this.sizeOfMatrix && column+1 <= this.sizeOfMatrix;
        }
        #endregion
    }
}