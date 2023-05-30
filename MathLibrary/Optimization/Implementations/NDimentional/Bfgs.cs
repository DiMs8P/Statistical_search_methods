using MathLibrary;
using MathLibrary.DataTypes;
using MathLibrary.DataTypes.Templates;
using MathLibrary.Optimization;
using NDimentionalSearch.Utils;

namespace NDimentionalSearch.Methods;

public class Bfgs
{
    private readonly double _derrAccuracy = 1e-4;
    private readonly Func<Point, double> _func;
    public Bfgs(Func<Point, double> func)
    {
        _func = func;
    }

    public Point Search(Point startPoint, double accuracy)
    {
        Vector gradient = Derivative.Gradient(_func, startPoint, _derrAccuracy); // Must

        double gradientLenght = Derivative.Divergence(_func, startPoint, _derrAccuracy); // Must

        if (gradientLenght <= accuracy)
        {
            return startPoint;
        }

        IdentityMatrix identityMatrix = new IdentityMatrix(startPoint.Size); // Must
        Matrix hessian = new Matrix(identityMatrix); // Must
        ExtremumHelper helper = new ExtremumHelper();

        Vector direction = (-hessian * gradient); // Must
        
        Point minimumPoint = helper.FindMinimumPoint(_func, startPoint, direction, accuracy); // Must

        Vector step = minimumPoint - startPoint; // Must
        Vector gradientDiff = Derivative.Gradient(_func, minimumPoint, _derrAccuracy) - gradient; // Must

        hessian = NextHessian(identityMatrix, 1 / MathHelper.MultiplyTransposeVectorByVector(gradientDiff, step), step, gradientDiff, hessian);
        Point prevPoint = new Point();
        
        int iterationNum = 1;
        while (gradientLenght > accuracy)
        {
            gradient = Derivative.Gradient(_func, minimumPoint, _derrAccuracy);
            
            if (Derivative.Divergence(_func, minimumPoint, _derrAccuracy) <= accuracy)
            {
                return minimumPoint;
            }
            
            direction = (-hessian * gradient);
            
            prevPoint = minimumPoint;
            minimumPoint = helper.FindMinimumPoint(_func, minimumPoint, direction, accuracy);

            step = minimumPoint - prevPoint;
            
            gradientDiff = Derivative.Gradient(_func, minimumPoint, _derrAccuracy) - gradient;
            hessian = NextHessian(identityMatrix, 1 / MathHelper.MultiplyTransposeVectorByVector(gradientDiff, step), step, gradientDiff, hessian);
            iterationNum++;
        }

        return minimumPoint;
    }

    private Matrix NextHessian(Matrix identityMatrix, double stepLenght, Vector step, Vector gradientDiff,
        Matrix prevHessian)
    {
        Matrix leftValue =
            (identityMatrix - stepLenght * MathHelper.MultiplyVectorByTransposeVector(step, gradientDiff));
        
        Matrix rightValue =
            (identityMatrix - stepLenght * MathHelper.MultiplyVectorByTransposeVector(gradientDiff, step));

        Matrix result =(leftValue * prevHessian) * rightValue;
        
        return result + stepLenght * MathHelper.MultiplyVectorByTransposeVector(step, step);
    }

}