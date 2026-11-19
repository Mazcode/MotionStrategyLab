using MotionStrategyLab.Core.Interfaces;
using MotionStrategyLab.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MotionStrategyLab.Core.Strategies
{
    /// <summary>
    /// 职责：根据起终点通过三角函数算出圆弧轨迹上依次经过的点。
    /// 契约：只算点，不碰硬件、不等待、不取消；Z 锁定在 end.Z。
    /// 理由：纯函数，能脱离设备单测。
    /// </summary>
    public class ArcPlanner : IPathPlanner
    {
        public string Name => "圆弧";

        public IEnumerable<Point3D> Plan(Point3D start, Point3D end, PathOptions options)
        {
            if (start == end) yield break;
            //使用勾股定理算出两点之间的距离再除以2就是半径；
            var radius = Math.Sqrt(Math.Pow(end.X - start.X, 2) + Math.Pow(end.Y - start.Y, 2)) / 2;

            var centerX = (start.X + end.X) / 2;
            var centerY = (start.Y + end.Y) / 2;

            int steps = 100;

            double startAngle = Math.Atan2(start.Y - centerY, start.X - centerX);
            for (int i = 0; i <= steps; i++)
            {
                var angle = Math.PI * i / steps;
                double curX = centerX + radius * Math.Cos(startAngle + angle);
                double curY = centerY + radius * Math.Sin(startAngle + angle);

                yield return new Point3D(curX, curY, end.Z);
            }
        }
    }
}
