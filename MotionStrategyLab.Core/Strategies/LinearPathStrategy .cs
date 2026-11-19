using MotionStrategyLab.Core.Interfaces;
using MotionStrategyLab.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MotionStrategyLab.Core.Strategies
{
    /// <summary>
    /// 直线策略
    /// </summary>
    public class LinearPathStrategy : IPathStrategy
    {
        public string Name => "直线";


        private PathOptions _options;

        private IMotionProxy _motion;

        private bool _isAborted = false;

        public void Initialize(PathOptions options, IMotionProxy motion)
        {
            _options = options;
            _motion = motion;
            _isAborted = false;//初始化的时候重置中止标志；
        }

        

        public async Task ExecuteAsync(Point3D start, Point3D end, CancellationToken ct)
        {
            if (_isAborted) return;

            //分段下发
            int steps = 100;
            double stepX = (end.X- start.X) / steps;
            double stepY = (end.Y- start.Y) / steps;

            for (int i = 0; i < steps; i++)
            {
                //每次循环前检查是否有外部取消，例如急停或者内部中止
                if (ct.IsCancellationRequested||_isAborted)
                {
                    throw new OperationCanceledException();
                }

                double curX = start.X + stepX * i;
                double curY = start.Y + stepY * i;

                // 调用硬件接口，上层不知道具体硬件是什么
                _motion.MoveLine(curX, curY, end.Z, _options.Speed);

                // 模拟硬件运动耗时，并释放 UI 线程
                try
                {
                    // 模拟硬件运动耗时（500 毫秒），同时让出线程
                    await Task.Delay(500, ct);
                }
                catch (TaskCanceledException)
                {
                    // 等待期间被取消时 Task.Delay 会抛异常，安静退出循环
                    return;
                }
            }

        }
        public void Abort()
        {
            _isAborted=true;
        }

    }
}
