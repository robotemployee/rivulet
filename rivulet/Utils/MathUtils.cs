using System.Numerics;

namespace rivulet.Utils;

public class MathUtils {

    public static T ToRadians<T>(T degrees) where T : IFloatingPoint<T> {
        return degrees * (T.Pi / T.CreateChecked<int>(180));
    }
    
    public static T ToDegrees<T>(T radians) where T : IFloatingPoint<T> {
        return radians * (T.CreateChecked<int>(180) / T.Pi);
    }
}