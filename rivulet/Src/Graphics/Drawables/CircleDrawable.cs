using rivulet.Utils;

namespace rivulet.Graphics.Drawables;

public class CircleDrawable : IDrawable {
    
    public void Draw(ICanvas canvas, RectF dirtyRect) {
        float centerX = dirtyRect.Center.X;
        float centerY = dirtyRect.Center.Y;
        float radius = Math.Min(dirtyRect.Width, dirtyRect.Height) / 2;

        Color colorA = Colors.White;
        Color colorB = Colors.Black;

        // draw arbitrary segments of a circle
        int segmentCount = 5;
        Console.WriteLine("beginning");
        float sliceAngleDeg = 360f / segmentCount;
        float sliceAngleRad = MathUtils.ToRadians(sliceAngleDeg);
        for (int i = 0; i < segmentCount; i++) {
            // currently just alternating color, for demonstration
            canvas.FillColor = ColorUtils.Interpolate(colorA, colorB, i / (segmentCount - 1f));
            //if (i != 0) continue; // todo remove

            float startAngleDeg = sliceAngleDeg * i;
            float startAngleRad = sliceAngleRad * i;
            float endAngleDeg = startAngleDeg + sliceAngleDeg;
            float endAngleRad = startAngleRad + sliceAngleRad;
            
            canvas.FillArc(
                centerX - radius, 
                centerY - radius, 
                
                radius * 2, 
                radius * 2, 
                
                sliceAngleDeg * i, 
                sliceAngleDeg * (i + 1), 
                
                false
            );

            float cosA = (float)Math.Cos(startAngleRad);
            float sinA = (float)Math.Sin(startAngleRad);
            float cosB = (float)Math.Cos(endAngleRad);
            float sinB = (float)Math.Sin(endAngleRad);

            PathF path = new PathF();
            path.MoveTo(centerX, centerY);
            const int addedToRadius = 1;
            path.LineTo(new PointF(centerX + (radius + addedToRadius) * cosA, centerY - (radius + addedToRadius) * sinA));
            path.LineTo(new PointF(centerX + (radius + addedToRadius) * cosB, centerY - (radius + addedToRadius) * sinB));
            path.Close();
            
            canvas.FillPath(path);
            
            
            
            Console.WriteLine("miao from {0} to {1}", i * sliceAngleDeg, (i + 1) * sliceAngleDeg);
        }
        
        /*
        canvas.FillArc(
            centerX - radius, 
            centerY - radius, 
                
            radius * 2, 
            radius * 2, 
                
            100, 
            280, 
                
            true
        );
        */
    }
}