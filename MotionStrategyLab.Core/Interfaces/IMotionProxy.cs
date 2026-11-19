using System;
using System.Collections.Generic;
using System.Text;

namespace MotionStrategyLab.Core.Interfaces
{
    /// <summary>
    /// 硬件代理接口（隔离层）。
    /// 上层业务只认这个接口，不管底层是运动卡实际品牌、型号，统统适配这个接口。
    /// </summary>
    public interface IMotionProxy
    {
        /// <summary>
        /// 当前硬件名称，用于日志和UI显示
        /// </summary>
        string HardwareName { get; }

        /// <summary>
        /// 移动指令
        /// </summary>
        /// <param name="x">目标 X 坐标</param>
        /// <param name="y">目标 Y 坐标</param>
        /// <param name="z">目标 Z 坐标</param>
        /// <param name="speed">速度</param>
        void MoveLine(double x, double y,double z,double speed);

        /// <summary>
        /// 等待硬件运动到位
        /// </summary>
        /// <param name="ct">取消Token令牌</param>
        void WaitMoveDone(CancellationToken ct);
    }
}
