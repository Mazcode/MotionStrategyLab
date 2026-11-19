using MotionStrategyLab.Core.Models;

namespace MotionStrategyLab.Core.Interfaces
{
    /// <summary>
    /// 控制卡接口抽象，屏蔽各品牌 SDK 差异，让上层不依赖具体控制卡。
    /// 继承IDisposable 考虑将来会用于释放资源句柄，例如SDK的句柄。
    /// </summary>
    public interface IMotionCard:IDisposable
    {
        /// <summary>
        /// 控制卡名称
        /// </summary>
        string Name {  get; }

        /// <summary>
        /// 下发一次运动指令：把轴从当前位置移动到 target。
        /// </summary>
        /// <param name="target">目标位置的绝对坐标（世界坐标系中的固定点，而非相对当前位置的增量）</param>
        /// <param name="speed">运动速度</param>
        /// <param name="ct">取消令牌，用于中止当前运动</param>
        /// <remarks>
        /// 这是所有品牌控制卡都必须提供的最小能力，上层 Controller 只认这个方法，不认具体 SDK。
        /// 什么时候用：Controller 遍历规划器给出的点序列时，每个点调用一次。
        /// 完成：Task 在硬件真正到位时才算完成，不是"指令发出去"就算完成。
        ///             所以实现方必须等硬件到位信号，不能用固定延时代替。
        /// 取消：ct 被取消时中断本次运动，让 Task 以 OperationCanceledException 结束。
        /// 为什么这样设计：把"到位等待"折叠进 Task 的完成语义，调用方只需 await，
        ///             杜绝上一个点没到位就发下一个点导致的轨迹错位；
        ///             Task + 值类型参数跨进程友好，将来把卡放进 x86 辅助进程时接口不用改。
        /// </remarks>
        Task MoveToAsync(Point3D target, double speed, CancellationToken ct);

        /// <summary>
        /// 立即急停：断开电机使能，让硬件马上停下，不依赖软件循环检查取消标记。
        /// 这是安全命令——即使调用方的取消令牌已经取消，也必须执行，
        /// 所以签名里故意不带 CancellationToken。
        /// </summary>
        Task EStopAsync();
    }
}
