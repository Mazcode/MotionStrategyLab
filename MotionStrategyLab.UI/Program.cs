namespace MotionStrategyLab.UI
{
    internal static class Program
    {
        /// <summary>
        /// 应用入口。目前只启动窗体；V1 起将在此装配依赖项（即"组合根"——全系统唯一 new 具体实现的地方）。
        /// </summary>
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            Application.Run(new Form1());
        }
    }
}