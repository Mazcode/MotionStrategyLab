using MotionStrategyLab.Core.Interfaces;
using MotionStrategyLab.Core.Models;

namespace MotionStrategyLab.Core.Strategies
{
    /// <summary>
    /// 职责：根据起终点算出直线轨迹上依次经过的点。
    /// 契约：只算点，不碰硬件、不等待、不取消；Z 锁定在 end.Z。
    /// 理由：纯函数，能脱离设备单测。
    /// </summary>
    public class LinearPlanner: IPathPlanner
    {
        public string Name => "直线";

        public IEnumerable<Point3D> Plan(Point3D start, Point3D end, PathOptions options)
        {
            // 算步长，for 循环 yield return
            int steps = 100;
            //各个坐标轴要走的步幅
            double stepX = (end.X - start.X) / steps;
            double stepY = (end.Y - start.Y) / steps;

            for (int i = 0; i <= steps; i++)
            {
                double currentX = start.X + stepX * i;
                double currentY = start.Y + stepY * i;
                yield return new Point3D(currentX, currentY, end.Z);
            }
        }
    }
}
