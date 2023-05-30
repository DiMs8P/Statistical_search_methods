using MathLibrary.DataTypes;
using Statistical_search_methods.Core.Methods;
using Statistical_search_methods.DataTypes;

namespace Statistical_search_methods.Settings;

public class Config
{
    private static double[] C = new double[] { 10, 9, 7, 9, 3, 10 };
    private static double[] a = new double[] { -5, 8, -8, 0, -10, 5};
    private static double[] b = new double[] { -1, -9, -4, 3, 1, 0};
    public static int[] M = new int[] { 100,250,400,500,600,750, 1000};
    public static int FuncCount = 0;

    public static Func<Point, double> Func = point =>
    {
        FuncCount++;
        double sum = 0;
        for (int i = 0; i < C.Length; i++)
        {
            sum += C[i] / (1 + Double.Pow((point[0] - a[i]), 2) + Double.Pow((point[1] - b[i]), 2));
        }
        return -sum;
    };
    
    
    public static int Seed = 600;
    public static Eps Eps = new Eps(new double[] { 1, 1 });
    public static Area Area = new Area( new Interval[]{new (-10, 10), new (-10, 10)});
    
    public static SearchMethodParams searchMethodParams = new SearchMethodParams(
        Config.Func,
        Config.Eps,
        Config.Area);
}