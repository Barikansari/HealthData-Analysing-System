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

        }

        private void zedGraphControl1_Load(object sender, EventArgs e)
        {

        }
    }
}
