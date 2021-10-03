using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace moovpad
{
    class GMath : GObject
    {
        #region LINEAR ALGEBRA DECLARATIONS
        public double[] v; //This is a vector
        public double[][] M; //This is a matrix. The first [] declares the number of rows, and the second holds the number of columns
        public double[][] MM; //This is to hold the result of two matrices (multiply, add)
        public double[] Mv; //This is to hold the product of a matrix and vector
        public double val = 0; //This is the product/result of operations
        public double[] row;
        public double[] col;
        public double[] row1;
        public double[] col1;
        public double[] row2;
        public double[] col2;
        public double[] stdvec;
        public double[] meanvec;
        public int rows;
        public int cols;
        #endregion

        #region BASIC VARIABLES
        public double sum = 0;
        public double mean = 0;
        #endregion

        #region LINEAR ALGEBRA FUNCTIONS
        //This functionmultiplies two matrices together, which is a very common requirement in applications involving machine learning
        public double[][] MMmultiply(double[][] matrix1, double[][] matrix2)
        {
            int i = 0;//only initialised here because it is used to index the check for the number of cols
            int j;
            int r1 = matrix1.Length;
            int c1 = matrix1[i].Length;
            int r2 = matrix2.Length;
            int c2 = matrix2[i].Length;
            MM = new double[r1][];
            v = new double[c1];

            if (r1 == c2) //dimension checking, recalling the in linear algebra, unless the two matrices are both square matrices of the same dimensions, then matrix multiplication is non-commutative (i.e. A*B != B*A)
            {
                col = new double[c2];
                row1 = new double[c1];
                row2 = new double[c2];
                col2 = new double[r2];
                for (i = 0; i < r1; i++)
                {
                    val = 0;
                    row1 = matrix1[i];

                    for (j = 0; j < r2; j++)
                    {
                        row2 = matrix2[j];
                        col[i] += row1[j] * row2[j];
                    }
                    MM[i] = col;
                }
                return MTranspose(MM);
            }
            else
            {
                return null;
            }
        }

        //This function transposes an input matrix, essentially taking the elements of each row and converting them into elements of columns, then returning the resultant transpose matrix.
        public double[][] MTranspose(double[][] M)
        {
            int i = 0;
            int j;
            cols = M[i].Length;
            rows = M.Length; ;
            MM = new double[cols][];
            row1 = new double[cols];
            for (i = 0; i < cols; i++)
            {
                row = new double[rows];
                for (j = 0; j < cols; j++)
                {
                    row1 = M[j];
                    row[j] = row1[0];//sets the next element in row to the first element of row1 (which is actually the columns from M)
                }
                MM[i] = row;
            }
            return MM;
        }
        
        //This function multiplies a matrix and a vector, both inputs.
        //The function checks the dimensions of both inputs, and either performs the multiplication and returns a vector, or returns an empty result
        public double[] MVmultiply(double[][] matrix, double[] vector)
        {
            int i = 0;//only initialised here because it is used to index the check for the number of cols
            int j;
            int rows = matrix.Length;
            int cols = matrix[i].Length;
            int dim = vector.Length;
            Mv = new double[rows];
            val = 0;
            if (cols == dim)
            {
                for (i = 0; i < rows; i++)
                {
                    for (j = 0; j < cols; j++)
                    {
                        val += matrix[i][j]*vector[j];
                    }
                    Mv[i] = val;
                }
                return Mv;
            }
            else
            {
                Mv = new double[0];
                return Mv;
            }
        }

        //This simple function squares all elements of an input vector.
        //In linear algebra, this is the equivalent of multiplying the transpose of a vector by the original vector, as seen in regularised logistic regression
        public double[] VSquared(double[] vec)
        {
            v = new double[vec.Length];
            for (i = 0; i < v.Length; i++)
            {
                v[i] = vec[i] * vec[i];
            }
            return v;
        }

        //This function multiplies a matrix by a scalar, both received as inputs.
        public double[][] MSmultiply(double[][] matrix, double scalar)
        {
            int i;
            int j;
            int rows = matrix.Length;
            M = new double[rows][];
            for (i = 0; i < rows; i++)
            {
                row = matrix[i];
                v = new double[row.Length];
                for (j = 0; j < row.Length; j++)
                {
                    v[j] = row[j] * scalar;
                }
                M[i] = v;
            }
            return M;
        }

        //This function divides a matrix by a scalar, both received as inputs.
        public double[][] MSdivide(double[][] matrix, double scalar)
        {
            int i;
            int j;
            int rows = matrix.Length;
            M = new double[rows][];
            for (i = 0; i < rows; i++)
            {
                row = matrix[i];
                v = new double[row.Length];
                for (j = 0; j < row.Length; j++)
                {
                    v[j] = row[j] / scalar;
                }
                M[i] = v;
            }
            return M;
        }

        //This function adds two matrices, both received as inputs.
        public double[][] MMadd(double[][] matrix1, double[][] matrix2)
        {
            int i = 0;
            int j;
            int r1 = matrix1.Length;
            int c1 = matrix1[i].Length;
            int r2 = matrix2.Length;
            int c2 = matrix2[i].Length;
            row1 = new double[c1];
            row2 = new double[c2];
            if (r1 == r2 && c1 == c2)
            {
                MM = new double[r1][];
                for (i = 0; i < r1; i++)
                {
                    row1 = matrix1[i];
                    row2 = matrix2[i];
                    v = new double[c1];
                    for (j = 0; j < c1; j++)
                    {
                        v[j] = row1[j] + row2[j];
                    }
                    MM[i] = v;
                }
                return MM;
            }
            else
            {
                MM = new double[0][];
                return MM;
            }
        }

        //This function subtracts one matrix from another, both received as inputs.
        public double[][] MMsubtract(double[][] matrix1, double[][] matrix2)
        {
            int i = 0;
            int j;
            int r1 = matrix1.Length;
            int c1 = matrix1[i].Length;
            int r2 = matrix2.Length;
            int c2 = matrix2[i].Length;
            row1 = new double[c1];
            row2 = new double[c2];
            if (r1 == r2 && c1 == c2)
            {
                MM = new double[r1][];
                for (i = 0; i < r1; i++)
                {
                    row1 = matrix1[i];
                    row2 = matrix2[i];
                    v = new double[c1];
                    for (j = 0; j < c1; j++)
                    {
                        v[j] = row1[j] - row2[j];
                    }
                    MM[i] = v;
                }
                return MM;
            }
            else
            {
                MM = new double[0][];
                return MM;
            }
        }

        //This function subtracts one vector from another, both received as inputs.
        public double[] VVsubtract(double[] vec1, double[] vec2)
        {
            int i = 0;
            int r1 = vec1.Length;
            int r2 = vec2.Length;
            if (r1 == r2)
            {
                v = new double[r1];
                for (i = 0; i < r1; i++)
                {
                    v[i] = vec1[i] - vec2[i];
                }
                return v;
            }
            else
            {
                v = new double[0];
                return v;
            }
        }
        #endregion

        
    }

}
