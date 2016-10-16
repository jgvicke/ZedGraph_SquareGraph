using System;
using System.Drawing;
using System.Windows.Forms;
using ZedGraph;

namespace SquareGraph
{
    public partial class MainForm : Form
    {
        private readonly GraphPane _myPane;

        public MainForm()
        {
            InitializeComponent();

            _myPane = zedGraphControl1.GraphPane;

            _myPane.YAxis.Scale.Min = -1.2;
            _myPane.YAxis.Scale.Max = 1.2;
            _myPane.YAxis.Scale.MajorStep = 0.2;
            _myPane.YAxis.Scale.MinorStep = 0.2;
            _myPane.YAxis.MajorGrid.IsVisible = true;
            _myPane.YAxis.MajorGrid.Color = Color.Gray;
            _myPane.YAxis.MajorGrid.IsZeroLine = false;
            _myPane.YAxis.MajorGrid.DashOff = 0;

            _myPane.XAxis.Scale.Min = -1.2;
            _myPane.XAxis.Scale.Max = 1.2;
            _myPane.XAxis.Scale.MajorStep = 0.2;
            _myPane.XAxis.Scale.MinorStep = 0.2;
            _myPane.XAxis.MajorGrid.IsVisible = true;
            _myPane.XAxis.MajorGrid.Color = Color.Gray;
            _myPane.XAxis.MajorGrid.IsZeroLine = false;
            _myPane.XAxis.MajorGrid.DashOff = 0;

            _myPane.Legend.IsVisible = false;
            
            var points = new PointPairList();
            _myPane.AddCurve("Test Curve1", points, Color.Blue, SymbolType.None);

            for ( var i = 0 ; i < 5000 ; i++ )
            {
                points.Add(Math.Sin(2 * Math.PI * i * 0.0002), Math.Cos(2 * Math.PI * i * 0.0002));
            }

            Shown += MainForm_Shown;
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            ScaleGraph(2.4);
            ScaleGraph(2.4);
        }
        
        private FormWindowState _lastWindowState;

        protected override void OnSizeChanged(EventArgs e)
        {
            if (WindowState != _lastWindowState)
            {
                BeginInvoke(new MethodInvoker(delegate { ScaleGraph(2.4); }));
                _lastWindowState = WindowState;
            }

            base.OnSizeChanged(e);
        }

        private void ScaleGraph(double minSize)
        {
            double ratio;
            var xAxisCenter = (_myPane.XAxis.Scale.Max + _myPane.XAxis.Scale.Min) * 0.5;
            var yAxisCenter = (_myPane.YAxis.Scale.Max + _myPane.YAxis.Scale.Min) * 0.5;

            zedGraphControl1.Refresh();

            if ( _myPane.Chart.Rect.Height > _myPane.Chart.Rect.Width )
            {
                ratio = _myPane.Chart.Rect.Height / _myPane.Chart.Rect.Width;

                _myPane.XAxis.Scale.Min = xAxisCenter - minSize * 0.5;
                _myPane.XAxis.Scale.Max = xAxisCenter + minSize * 0.5;

                _myPane.YAxis.Scale.Min = yAxisCenter - minSize * 0.5 * ratio;
                _myPane.YAxis.Scale.Max = yAxisCenter + minSize * 0.5 * ratio;
            }
            else
            {
                ratio = _myPane.Chart.Rect.Width / _myPane.Chart.Rect.Height;

                _myPane.YAxis.Scale.Min = yAxisCenter - minSize * 0.5;
                _myPane.YAxis.Scale.Max = yAxisCenter + minSize * 0.5;

                _myPane.XAxis.Scale.Min = xAxisCenter - minSize * 0.5 * ratio;
                _myPane.XAxis.Scale.Max = xAxisCenter + minSize * 0.5 * ratio;
            }
        }

        private void zedGraphControl1_Resize(object sender, EventArgs e)
        {
            ScaleGraph(2.4);
        }
    }
}
