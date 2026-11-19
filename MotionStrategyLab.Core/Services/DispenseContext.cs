using MotionStrategyLab.Core.Interfaces;
using MotionStrategyLab.Core.Models;

namespace MotionStrategyLab.Core.Services
{
    /// <summary>
    /// 点胶执行上下文：持有当前策略与硬件代理，负责驱动一次完整轨迹的执行与中止。
    /// </summary>
    public class DispenseContext
    {
        private IPathStrategy? _strategy;
        private readonly IMotionProxy _motion;
        private CancellationTokenSource? _cts;

        /// <param name="motion">硬件代理，由上层注入，Context 不关心具体实现</param>
        public DispenseContext(IMotionProxy motion)
        {
            _motion = motion;
        }

        /// <summary>
        /// 设置（或热切换）当前轨迹策略，并注入参数与硬件代理。
        /// </summary>
        /// <param name="key">策略标识，对应工厂注册表中的键</param>
        /// <param name="options">轨迹参数</param>
        public void SetStrategy(string key, PathOptions options)
        {
            _strategy?.Abort();// 红线：热切换必须先停掉上一个
            _strategy = PathStrategyFactory.Create(key);// 从工厂拿新算法
            _strategy.Initialize(options, _motion);// 注入参数和硬件
        }

        /// <summary>
        /// 按当前策略执行从起点到终点的轨迹。
        /// </summary>
        /// <exception cref="InvalidOperationException">尚未设置策略时抛出</exception>
        public async Task RunAsync(Point3D start, Point3D end)
        {
            if (_strategy == null)
                throw new InvalidOperationException("策略必须先设置才可以运行.");

            _cts = new CancellationTokenSource();
            try
            {
                await _strategy.ExecuteAsync(start, end, _cts.Token);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
        }
        /// <summary>
        /// 紧急中止当前流程
        /// </summary>
        public void AbortCurrent()
        {
            _strategy?.Abort();
            _cts?.Cancel();
        }
    }
}
