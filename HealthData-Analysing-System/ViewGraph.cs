using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZedGraph;

namespace HealthData_Analysing_System
{
    public partial class ViewGraph : Form
    {
        public static Dictionary<string, List<string>> _hrData;

        // public GraphPane GraphPane { get; private set; }

        public ViewGraph()
        {
            InitializeComponent();
        }

        private void ViewGraph_Load(object sender, EventArgs e)
        {
            plotGraph();
            SetSize();


        }

        private void plotGraph()
        {
            
            GraphPane panel1 = zedGraphControl1.GraphPane;

            panel1.Title.Text = "Overview";
            panel1.XAxis.Title.Text = "Time in second";
            panel1.YAxis.Title.Text = "Data";

            /* myPane.XAxis.Scale.MajorStep = 50;
             myPane.YAxis.Scale.Mag = 0;
             myPane.XAxis.Scale.Max = 1000;*/

            PointPairList cadencePairList = new PointPairList();
            PointPairList altitudePairList = new PointPairList();
            PointPairList heartPairList = new PointPairList();
            PointPairList powerPairList = new PointPairList();

            for (int i = 0; i < _hrData["cadence"].Count; i++)
            {
                cadencePairList.Add(i, Convert.ToInt16(_hrData["cadence"][i]));
            }

            for (int i = 0; i < _hrData["altitude"].Count; i++)
            {
                altitudePairList.Add(i, Convert.ToInt16(_hrData["altitude"][i]));
            }

            for (int i = 0; i < _hrData["heartRate"].Count; i++)
            {
                heartPairList.Add(i, Convert.ToInt16(_hrData["heartRate"][i]));
            }

            for (int i = 0; i < _hrData["watt"].Count; i++)
            {
                powerPairList.Add(i, Convert.ToInt16(_hrData["watt"][i]));
            }

            LineItem cadence = panel1.AddCurve("Cadence",
                   cadencePairList, Color.Red, SymbolType.None);

            LineItem altitude = panel1.AddCurve("Altitude",
                  altitudePairList, Color.Blue, SymbolType.None);

            LineItem heart = panel1.AddCurve("Heart",
                   heartPairList, Color.Black, SymbolType.None);

            LineItem power = panel1.AddCurve("Power",
                  powerPairList, Color.Orange, SymbolType.None);

            zedGraphControl1.AxisChange();
        }

        private void SetSize()
        {
            zedGraphControl1.Location = new Point(0, 0);
            zedGraphControl1.IsShowPointValues = true;
            zedGraphControl1.Size = new Size(this.ClientRectangle.Width - 20, this.ClientRectangle.Height - 50);

        }

        private void zedGraphControl1_Load(object sender, EventArgs e)
        {

        }
    }
}
