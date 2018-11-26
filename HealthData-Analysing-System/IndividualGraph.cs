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
    public partial class IndividualGraph : Form
    {
        public static Dictionary<string, List<string>> _hrData;
        public IndividualGraph()
        {
            InitializeComponent();
            this.CenterToScreen();
            plotGraph();
        }

        private void plotGraph()
        {
            GraphPane speedpanel1 = zedGraphControl1.GraphPane;
            GraphPane heartRatepanel = zedGraphControl2.GraphPane;
            GraphPane cadencepanel = zedGraphControl3.GraphPane;
            GraphPane powerpanel = zedGraphControl4.GraphPane;


            // Seting the Titles for graph
            speedpanel1.Title.Text = "Overview";
            speedpanel1.XAxis.Title.Text = "Time in second";
            speedpanel1.YAxis.Title.Text = "Data";

            heartRatepanel.Title.Text = "Overview";
            heartRatepanel.XAxis.Title.Text = "Time in second";
            heartRatepanel.YAxis.Title.Text = "Data";

            cadencepanel.Title.Text = "Overview";
            cadencepanel.XAxis.Title.Text = "Time in second";
            cadencepanel.YAxis.Title.Text = "Data";

            powerpanel.Title.Text = "Overview";
            powerpanel.XAxis.Title.Text = "Time in second";
            powerpanel.YAxis.Title.Text = "Data";

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

            LineItem cadence = cadencepanel.AddCurve("Cadence",
                   cadencePairList, Color.Red, SymbolType.None);
            //cadence.Symbol.Fill = new Fill(new Color[] { Color.Blue, Color.Green, Color.Red });

            LineItem altitude = speedpanel1.AddCurve("Altitude",
                  altitudePairList, Color.Blue, SymbolType.None);

            LineItem heart = heartRatepanel.AddCurve("Heart",
                   heartPairList, Color.Black, SymbolType.None);

            LineItem power = powerpanel.AddCurve("Power",
                  powerPairList, Color.Orange, SymbolType.None);

            zedGraphControl1.AxisChange();
            zedGraphControl2.AxisChange();
            zedGraphControl3.AxisChange();
            zedGraphControl4.AxisChange();
        }

        private void SetSize()
        {
            zedGraphControl1.Location = new Point(0, 0);
            zedGraphControl1.IsShowPointValues = true;
            zedGraphControl1.Size = new Size(this.ClientRectangle.Width - 20, this.ClientRectangle.Height - 50);

            zedGraphControl2.Location = new Point(0, 0);
            zedGraphControl2.IsShowPointValues = true;
            zedGraphControl2.Size = new Size(this.ClientRectangle.Width - 20, this.ClientRectangle.Height - 50);

            zedGraphControl3.Location = new Point(0, 0);
            zedGraphControl3.IsShowPointValues = true;
            zedGraphControl3.Size = new Size(this.ClientRectangle.Width - 20, this.ClientRectangle.Height - 50);

            zedGraphControl4.Location = new Point(0, 0);
            zedGraphControl4.IsShowPointValues = true;
            zedGraphControl4.Size = new Size(this.ClientRectangle.Width - 20, this.ClientRectangle.Height - 50);
        }

    }
}
