using MotionStrategyLab.Core.Models;
using MotionStrategyLab.Core.Services;
using MotionStrategyLab.Drivers.Proxies;
namespace MotionStrategyLab.UI
{
    public partial class Form1 : Form
    {
        // 记录模拟硬件生成的轨迹点，用于绘图
        private List<Point3D> _drawPoints = new List<Point3D>();

        private readonly DispenseContext _context;
        private readonly SimulatedMotionProxy _motionProxy;

        public Form1()
        {
            InitializeComponent();

            //1. 实例化代理
            _motionProxy = new SimulatedMotionProxy();
            //2. 订阅事件
            _motionProxy.OnPointMoved += (point) =>
            {
                this.BeginInvoke(new Action(() =>
                {

                    _drawPoints.Add(point);
                    PB_Canvas.Invalidate();
                }));
            };

            //3. 将硬件注入上下文
            _context = new DispenseContext(_motionProxy);
            InitialSetting();
           
        } 

        private void InitialSetting()
        {
            this.Text = "Motion Strategy Lab - V1.0";
            this.DoubleBuffered = true;
            this.BackColor = Color.WhiteSmoke;
            // 绑定画布的 Paint 事件
            PB_Canvas.Paint += Canvas_Print;
            BindingHandler();
        }

        /// <summary>
        /// 按钮事件绑定处理方法
        /// </summary>
        private void BindingHandler()
        {
            btnLine.Click += async (s, e) => await RunTest("Linear", new Point3D(50, 50, 0), new Point3D(500, 350, 0));
            btnArc.Click += async (s, e) => await RunTest("Arc", new Point3D(50, 200, 0), new Point3D(500, 200, 0));
            btnAbort.Click += (s, e) => _context.AbortCurrent();
        }

        private async Task RunTest(string key, Point3D start, Point3D end)
        {
            _drawPoints.Clear();
            PB_Canvas.Invalidate();

            var options = new PathOptions(100, 5);

            _context.SetStrategy(key, options);
            await _context.RunAsync(start, end);
        }

        private void Canvas_Print(object? sender, PaintEventArgs e)
        {
            using (var pen = new Pen(Color.Blue, 2))
            {
                for (int i = 0; i < _drawPoints.Count-1; i++)
                {
                    var p1 = new Point((int)_drawPoints[i].X, (int)_drawPoints[i].Y);
                    var p2 = new Point((int)_drawPoints[i + 1].X, (int)_drawPoints[i + 1].Y);
                    e.Graphics.DrawLine(pen, p1, p2);
                }
            }
        }

    }
}
