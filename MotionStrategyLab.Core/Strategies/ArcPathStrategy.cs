using MotionStrategyLab.Core.Interfaces;
using MotionStrategyLab.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MotionStrategyLab.Core.Strategies
{
    /// <summary>
    /// 圆弧轨迹策略：以起终点连线为直径，按半圆插补逐点下发。
    /// </summary>
    public class ArcPathStrategy : IPathStrategy
    {
        public string Name => "圆弧";

        private PathOptions _options;
        private IMotionProxy _motion;
        private bool _isAborted;

        public void Abort()
        {
           _isAborted = true; 
        }

        public async Task ExecuteAsync(Point3D start, Point3D end, CancellationToken ct)
        {
            double radius = Math.Sqrt(Math.Pow(end.X - start.X, 2) + Math.Pow(end.Y - start.Y, 2)) / 2;
            double centerX = (start.X + end.X) / 2;
            double centerY = (start.Y + end.Y) / 2;
            int steps = 40;
            for (int i = 0; i <= steps; i++)
            {
                if (ct.IsCancellationRequested || _isAborted)
                    throw new OperationCanceledException();

                // 利用三角函数算圆弧上的点
                double angle = Math.PI * i / steps;
                double curX = centerX - radius * Math.Cos(angle);
                double curY = centerY + radius * Math.Sin(angle);

                _motion.MoveLine(curX, curY, end.Z, _options.Speed);
                try
                {
                    // 尝试等待 20 毫秒
                    await Task.Delay(20, ct);
                }
                catch (TaskCanceledException)
                {
                    // 等待期间被取消时 Task.Delay 会抛异常，安静退出循环
                    return;
                }
            }
        }

        public void Initialize(PathOptions options, IMotionProxy motion)
        {
            _options = options;
            _motion = motion;
            _isAborted = false;
        }
    }
}
