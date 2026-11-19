using MotionStrategyLab.Core.Interfaces;
using MotionStrategyLab.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MotionStrategyLab.Drivers.Proxies
{
    /// <summary>
    /// 模拟硬件代理：不接真实控制卡，只把下发的点回调给订阅者用于界面显示和逻辑验证。
    /// </summary>
    public class SimulatedMotionProxy : IMotionProxy
    {
        public string HardwareName => "模拟硬件";

        /// <summary>
        /// 每收到一个下发点时触发，供 UI 绘制轨迹。
        /// </summary>
        public event Action<Point3D>? OnPointMoved;

        public void MoveLine(double x, double y, double z, double speed)
        { 
            // 触发事件，把点传给订阅者
            OnPointMoved?.Invoke(new Point3D(x, y, z));
        }

        public void WaitMoveDone(CancellationToken ct)
        {
            throw new NotImplementedException();
        }
    }
}
