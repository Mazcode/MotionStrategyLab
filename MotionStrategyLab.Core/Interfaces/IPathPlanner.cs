using MotionStrategyLab.Core.Models;

namespace MotionStrategyLab.Core.Interfaces
{
    /// <summary>
    /// 路径规划接口
    /// 根据起终点和参数算出整条轨迹的点；只算点、不碰硬件、不await，可以单测。
    /// </summary>
    public interface IPathPlanner
    {
        /// <summary>规划器名称，用于注册表查找和 UI 下拉框显示。</summary>
        string Name { get; }

        /// <summary>
        /// 算出从 start 到 end 这条轨迹依次要经过的点。
        /// 只负责算点：不控制硬件、不等待到位、不处理取消，这些由 Controller 负责。
        /// </summary>
        /// <param name="start">起始坐标</param>
        /// <param name="end">终点坐标</param>
        /// <param name="options">运动轨迹参数对象</param>
        /// <returns>
        /// 按运动顺序排列的点。调用方遍历到一个，才算出一个，
        /// 不会预先把整条轨迹全算完——所以按停止后，剩下的点根本不会被计算。
        /// </returns>

        IEnumerable<Point3D> Plan(Point3D start,Point3D end,PathOptions options);
    }
}
