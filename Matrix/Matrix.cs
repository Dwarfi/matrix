using System;
using System.Runtime.Serialization;

namespace MatrixLibrary
{
    ///Create custom exception "MatrixException"
    [Serializable]
    public class MatrixException : Exception
    {
        public MatrixException()
        {
        }
        public MatrixException(string message)
    : base(message)
        {
        }

        public MatrixException(string message, Exception inner)
            : base(message, inner)
        {
        }

        protected MatrixException(SerializationInfo info, StreamingContext context)
           : base(info, context)
        {
        }
    }

    public class Matrix : ICloneable
    {
        private double[,] mx;
        /// <summary>
        /// Number of rows.
        /// </summary>
        public int Rows
        {
            get => mx.GetLength(0);
        }

        /// <summary>
        /// Number of columns.
        /// </summary>
        public int Columns
        {
            get => mx.GetLength(1);
        }
        
        /// <summary>
        /// Gets an array of floating-point values that represents the elements of this Matrix.
        /// </summary>
        public double[,] Array
        {
            get => mx;
        }
        
        /// <summary>
        /// Initializes a new instance of the <see cref="Matrix"/> class.
        /// </summary>
        /// <param name="rows"></param>
        /// <param name="columns"></param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public Matrix(int rows, int columns)
        {
            if (rows < 0 || columns < 0)
                throw new ArgumentOutOfRangeException("zadaleko") ;
            mx = new double[rows, columns];
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Matrix"/> class with the specified elements.
        /// </summary>
        /// <param name="array">An array of floating-point values that represents the elements of this Matrix.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public Matrix(double[,] array)
        {
            if (array == null)
                throw new ArgumentNullException("bublik");
            else
            {
                mx = array;
            }
        }

        /// <summary>
        /// Allows instances of a Matrix to be indexed just like arrays.
        /// </summary>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <exception cref="ArgumentException"></exception>
        public double this[int row, int column]
        {

            get
            {
                try
                {
                    return mx[row, column];
                }
                catch (Exception) { throw new ArgumentException("bublik"); }
            }
            set
            {
                try
                {
                    mx[row, column] = value;
                }
                catch (Exception) { throw new ArgumentException("bublik"); }
            }
        }

        /// <summary>
        /// Creates a deep copy of this Matrix.
        /// </summary>
        /// <returns>A deep copy of the current object.</returns>
        public object Clone()
        {
            Matrix matrixClone = (Matrix)this.MemberwiseClone();
            matrixClone.mx = (double[,])mx.Clone();
            return matrixClone;
        }

        /// <summary>
        /// Adds two matrices.
        /// </summary>
        /// <param name="matrix1"></param>
        /// <param name="matrix2"></param>
        /// <returns>New <see cref="Matrix"/> object which is sum of two matrices.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="MatrixException"></exception>
        public static Matrix operator +(Matrix matrix1, Matrix matrix2)
        {
            if (matrix1 == null || matrix2 == null)
                throw new ArgumentNullException("bublik");
            try
            {
                Matrix result= new Matrix(matrix1.mx.GetLength(0),matrix1.mx.GetLength(1));
                for (int i = 0; i < matrix1.mx.GetLength(0); i++)
                {
                    for (int j = 0; j < matrix1.mx.GetLength(1); j++)
                    {
                        result[i, j] = matrix1.mx[i, j] + matrix2.mx[i, j];
                    }
                }
                return result;
            }
            catch (Exception exception) { throw new MatrixException("+ error",exception.InnerException); }
        }

        /// <summary>
        /// Subtracts two matrices.
        /// </summary>
        /// <param name="matrix1"></param>
        /// <param name="matrix2"></param>
        /// <returns>New <see cref="Matrix"/> object which is subtraction of two matrices</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="MatrixException"></exception>
        public static Matrix operator -(Matrix matrix1, Matrix matrix2)
        {
            if (matrix1 == null || matrix2 == null)
                throw new ArgumentNullException("bublik");
                try
                {
                    Matrix result = new Matrix(matrix1.mx.GetLength(0), matrix1.mx.GetLength(1));
                    for (int i = 0; i < matrix1.mx.GetLength(0); i++)
                    {
                        for (int j = 0; j < matrix1.mx.GetLength(1); j++)
                        {
                            result[i, j] = matrix1.mx[i, j] - matrix2.mx[i, j];
                        }
                    }
                    return result;
                }
                catch (Exception exception) { throw new MatrixException("- error",exception.InnerException); }
            }
        

        /// <summary>
        /// Multiplies two matrices.
        /// </summary>
        /// <param name="matrix1"></param>
        /// <param name="matrix2"></param>
        /// <returns>New <see cref="Matrix"/> object which is multiplication of two matrices.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="MatrixException"></exception>
        public static Matrix operator *(Matrix matrix1, Matrix matrix2)
        {
            if (matrix1 == null || matrix2 == null)
                throw new ArgumentNullException("bublik");
            try
            {
                int m1R = matrix1.mx.GetLength(0);
                int m1C = matrix1.mx.GetLength(1);
                int m2C = matrix2.mx.GetLength(1);
                int columns = m1C > m2C ? m1C : m2C;
                Matrix result = new Matrix(m1R, columns);
                for (int i = 0; i < m1R; i++)
                {
                    for (int j = 0; j < m2C; j++)
                    {
                        for (int k = 0; k < m1C; k++)
                        {
                            result[i, j] += matrix1.mx[i, k] * matrix2.mx[k, j];
                        }
                    }
                }
                if (result[0, 0] == 0)
                {
                    Exception exception = new Exception("* error");
                    throw exception;
                }
                return result;
            }
            catch (Exception exception)
            {
                throw new MatrixException("was ist das?!", exception.InnerException);
            }
        }
        /// <summary>
        /// Adds <see cref="Matrix"/> to the current matrix.
        /// </summary>
        /// <param name="matrix"><see cref="Matrix"/> for adding.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="MatrixException"></exception>
        public Matrix Add(Matrix matrix)
        {
            if (matrix == null)
                throw new ArgumentNullException("bublik");

            try
            {
                Matrix result = new Matrix(matrix.mx.GetLength(0), matrix.mx.GetLength(1));
                for (int i = 0; i < mx.GetLength(0); i++)
                {
                    for (int j = 0; j < mx.GetLength(1); j++)
                    {
                        result[i, j] = matrix.mx[i, j] + mx[i,j];
                    }
                }
                return result;
            }
            catch (Exception exception)
            {
                throw new MatrixException("was ist das?", exception.InnerException);
            }
        }

        /// <summary>
        /// Subtracts <see cref="Matrix"/> from the current matrix.
        /// </summary>
        /// <param name="matrix"><see cref="Matrix"/> for subtracting.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="MatrixException"></exception>
        public Matrix Subtract(Matrix matrix)
        {
            if (matrix == null)
                throw new ArgumentNullException("bublic");

            try
            {
                Matrix result = new Matrix(matrix.mx.GetLength(0), matrix.mx.GetLength(1));
                for (int i = 0; i < mx.GetLength(0); i++)
                {
                    for (int j = 0; j < mx.GetLength(1); j++)
                    {
                        result[i, j] =mx[i, j] - matrix.mx[i, j];
                    }
                }
                return result;
            }
            catch (Exception exception)
            {
                throw new MatrixException("was ist das?", exception.InnerException);
            }
        }

        /// <summary>
        /// Multiplies <see cref="Matrix"/> on the current matrix.
        /// </summary>
        /// <param name="matrix"><see cref="Matrix"/> for multiplying.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="MatrixException"></exception>
        public Matrix Multiply(Matrix matrix)
        {
            if (matrix == null)
                throw new ArgumentNullException("matrix");
            try
            {
                int m1R = mx.GetLength(0);
                int m1C = mx.GetLength(1);
                int m2C = matrix.mx.GetLength(1);
                int columns = m1C > m2C ? m1C : m2C;
                Matrix result = new Matrix(m1R, columns);
                for (int i = 0; i < m1R; i++)
                {
                    for (int j = 0; j < m2C; j++)
                    {
                        for (int k = 0; k < m1C; k++)
                        {
                            result[i, j] += mx[i, k] * matrix.mx[k, j];
                        }
                    }
                }
                if (result[0, 0] == 0)
                {
                    Exception exception = new Exception("matrix");
                    throw exception;
                }
                return result;
            }
            catch(Exception exception)
            {
                throw new MatrixException("MatrixLibrary", exception.InnerException);
            }
        }

        /// <summary>
        /// Tests if <see cref="Matrix"/> is identical to this Matrix.
        /// </summary>
        /// <param name="obj">Object to compare with. (Can be null)</param>
        /// <returns>True if matrices are equal, false if are not equal.</returns>
        public override bool Equals(object obj)
        {
            bool IsEqual = false;
            try
            {
                Matrix matrix = (Matrix)obj;
                double[,] temp = matrix.mx;
                for (int i = 0; i < temp.GetLength(0); i++)
                {
                    for (int j = 0; j < temp.GetLength(1); j++)
                    {
                        if (mx[i, j] == temp[i, j])
                        {
                            IsEqual = true;
                            break;
                        }
                    }
                }
                
            }
            catch(Exception)
            {
                return false;
            }
            return IsEqual;
        }

        public override int GetHashCode() => base.GetHashCode();
    }
}
