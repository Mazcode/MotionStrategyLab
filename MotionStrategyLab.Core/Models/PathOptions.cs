using System;
using System.Collections.Generic;
using System.Text;

namespace MotionStrategyLab.Core.Models
{
    /// <summary>
    /// 运动轨迹的全局参数
    /// </summary>
    /// <param name="Speed">运动速度</param>
    /// <param name="RaiseHeight">抬刀高度：轨迹起止时 Z 轴抬升的高度，用于跨过障碍物</param>
    public record PathOptions(double Speed, double RaiseHeight);
    
}
