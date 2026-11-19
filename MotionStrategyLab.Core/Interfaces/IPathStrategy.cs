using MotionStrategyLab.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MotionStrategyLab.Core.Interfaces
{
    /// <summary>
    /// 轨迹算法策略接口，定义了算法的生命周期
    /// </summary>
    public interface IPathStrategy
    {
        /// <summary>
        /// 策略名称
        /// </summary>
        string Name {  get; }

        /// <summary>
        /// 策略初始化：注入参数和硬件代理
        /// </summary>
        /// <param name="options">运动轨迹参数</param>
        /// <param name="motion">硬件代理</param>
        void Initialize(PathOptions options, IMotionProxy motion);

        /// <summary>
        /// 执行阶段：可以随时取消
        /// </summary>
        /// <param name="start">起点坐标</param>
        /// <param name="end">终点坐标</param>
        /// <param name="ct">取消令牌</param>
        /// <returns></returns>
        Task ExecuteAsync(Point3D start, Point3D end, CancellationToken ct);

        /// <summary>
        /// 中止：切换策略的时候必须先中止然后再切换。
        /// </summary>
        void Abort();
    }
}
