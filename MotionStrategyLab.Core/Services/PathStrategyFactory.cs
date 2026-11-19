using MotionStrategyLab.Core.Interfaces;
using MotionStrategyLab.Core.Strategies;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace MotionStrategyLab.Core.Services
{
    /// <summary>
    /// 轨迹策略注册表：用 key 映射到工厂委托，每次 Create 都产出独立的新实例。
    /// </summary>
    public static class PathStrategyFactory
    {
        private static readonly ConcurrentDictionary<string, Func<IPathStrategy>> _register = new()
        {
            ["Linear"] = () => new LinearPathStrategy(),
            ["Arc"] = () => new ArcPathStrategy()
        };

        /// <summary>
        /// 按 key 新建一个策略实例。
        /// </summary>
        /// <exception cref="NotSupportedException">key 未注册时抛出</exception>
        public static IPathStrategy Create(string key)
        {
            if (_register.TryGetValue(key, out var factory))
            {
                return factory();
            }
            else
            {
                throw new NotSupportedException($"未注册名为 {key} 的策略");
            }
        }
    }
}
