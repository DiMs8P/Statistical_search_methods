using MathLibrary.DataTypes;
using MathLibrary.DataTypes.Internal;
using Statistical_search_methods.DataTypes;

namespace Statistical_search_methods.Core.Methods;

public readonly record struct SearchMethodParams(Func<Point, double> Func, Eps Eps, Area Area);
