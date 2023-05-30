using MathLibrary.DataTypes;

namespace NDimentionalSearch.Utils;

public static class MathHelper
{
    public static Matrix MultiplyVectorByTransposeVector(Vector vector, Vector transposeVector)
    {
        if (vector.Size != transposeVector.Size)
        {
            throw new ArgumentException("Vectors have different sizes!");
        }

        Matrix output = new Matrix(new double[vector.Size, vector.Size]);
        
        for (int i = 0; i < vector.Size; i++)
        {
            for (int j = 0; j < vector.Size; j++)
            {
                output[i, j] = vector[i] * transposeVector[j];
            }
        }

        return output;
    } 
    public static double MultiplyTransposeVectorByVector(Vector transposeVector, Vector vector)
    {
        if (vector.Size != transposeVector.Size)
        {
            throw new ArgumentException("Vectors have different sizes!");
        }

        return vector.Select((value, index) => value * transposeVector[index]).Sum();;
    }
}