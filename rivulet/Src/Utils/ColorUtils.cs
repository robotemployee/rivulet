namespace rivulet.Utils;

public static class ColorUtils {
    
    public static Color Interpolate(Color start, Color end, float amount) {
        amount = Math.Clamp(amount, 0, 1);

        float r = start.Red + (end.Red - start.Red) * amount;
        float g = start.Green + (end.Green - start.Green) * amount;
        float b = start.Blue + (end.Blue - start.Blue) * amount;
        float a = start.Alpha + (end.Alpha - start.Alpha) * amount;

        return new Color(r, g, b, a);
    }
}