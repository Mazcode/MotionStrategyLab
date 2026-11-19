namespace MotionStrategyLab.Core.Interfaces
{
    /// <summary>
    /// 把"软件有没有授权"这件事从具体实现（加密狗 / 授权文件 / 网络许可）中隔离出来。
    /// 上层只问 IsLicensed，不关心授权来自哪里。
    /// 授权失效时上层必须立即停机——宁可误停，不可失控。
    /// </summary>
    /// <remarks>
    /// 提前预留的接口
    /// 关于许可的提供，如果没有授权，或者授权变更，
    /// 上层调用可以根据返回的结果，或者事件处理的结果来进行判定
    /// </remarks>
    public interface ILicenseProvider:IDisposable
    {
        /// <summary>
        /// 当前是否有授权。
        /// </summary>
        /// <remarks>
        /// 读的是实现方维护的本地缓存，不发起跨进程或网络调用，
        /// 因此可以安全地在循环里高频读取。
        /// </remarks>
        bool IsLicensed { get; }

        /// <summary>
        /// 授权变更事件
        /// </summary>
        /// <remarks>
        /// 授权状态变化时触发（如加密狗被拔、许可证到期）。
        /// 事件本身不携带新状态，收到通知后要重新读取 IsLicensed。
        /// </remarks>
        event EventHandler? LicenseChanged;
    }
}
