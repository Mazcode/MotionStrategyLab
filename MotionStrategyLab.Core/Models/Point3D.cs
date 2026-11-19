using System;
using System.Collections.Generic;
using System.Text;

namespace MotionStrategyLab.Core.Models
{
    /// <summary>
    /// 三维坐标结构体
    /// </summary>
    /// <remarks>
    /// 选用 record struct 而非 record class：坐标会被极高频地创建和传递，
    /// 值类型在栈上分配，没有 GC 压力。属性只有 getter，创建后不可篡改。
    /// record struct 从 C# 10 起支持；向下兼容的问题以后再考虑。
    /// </remarks>
    public record struct Point3D
    {
        public double X { get; }
        public double Y { get; }
        public double Z { get; }

        public Point3D(double x,double y,double z)
        {
            X = x;
            Y = y; 
            Z = z; 
        }
    }
}
