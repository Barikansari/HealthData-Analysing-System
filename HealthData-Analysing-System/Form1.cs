using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using HealthData_Analysing_System.Action;

namespace HealthData_Analysing_System
{
    public partial class Form1 : Form
    {
        private int count = 0;
        private Dictionary<string, List<string>> _hrData = new Dictionary<string, List<string>>();
        private Dictionary<string, string> _param = new Dictionary<string, string>();
        private string endTime;
        private List<int> smode = new List<int>();
        private FileConvertor c = new FileConvertor();
        
        public Form1()
        {
            InitializeComponent();
            InitGrid();
            this.CenterToScreen();
            // this.radioButton1.Checked = true;
            dataGridView2.MultiSelect = true;
        }

       

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            DialogResult result = openFileDialog2.ShowDialog();
            if (result == DialogResult.OK)
            {
                Cursor.Current = Cursors.WaitCursor;
                string text = File.ReadAllText(openFileDialog2.FileName);
                Dictionary<string, object> hrData = new TableFiller().FillTable(text, dataGridView2);
                _hrData = hrData.ToDictionary(k => k.Key, k => k.Value as List<string>);

                var metricsCalculation = new AdvanceMetricsCalculation();

                // calculation for advance mettrics
                double np = metricsCalculation.CalculateNormalizedPower(hrData);
                label3.Text = "Normalized power = " + Summary.RoundUp(np, 2);

                double ftp = metricsCalculation.CalculateFunctionalThresholdPower(hrData);
                label4.Text = "Training Stress Score = " + Summary.RoundUp(ftp, 2);

                double ifa = metricsCalculation.CalculateIntensityFactor(hrData);
                label5.Text = "Intensity Factor = " + Summary.RoundUp(ifa, 2);

                double pb = metricsCalculation.CalculatePowerBalance(hrData);
                label2.Text = "Power balance = " + Summary.RoundUp(pb, 2);

                // calculation for smode
                var param = hrData["params"] as Dictionary<string, string>;
                var sMode = param["SMode"];
                for (int i = 0; i < sMode.Length; i++)
                {
                    smode.Add((int)Char.GetNumericValue(param["SMode"][i]));
                }

                if (smode[0] == 0)
                {
                    dataGridView2.Columns[0].Visible = false;
                }
                else if (smode[1] == 0)
                {
                    dataGridView2.Columns[1].Visible = false;
                }
                else if (smode[2] == 0)
                {
                    dataGridView2.Columns[2].Visible = false;
                }
                else if (smode[3] == 0)
                {
                    dataGridView2.Columns[3].Visible = false;
                }
                else if (smode[4] == 0)
                {
                    dataGridView2.Columns[4].Visible = false;
                }

                dataGridView3.Rows.Clear();
                dataGridView3.Rows.Add(new TableFiller().FillDataInSumaryTable(hrData, hrData["endTime"] as string, hrData["params"] as Dictionary<string, string>));

            }
        }

      /*  private string ConvertToDate(string date)
        {
            string year = "";
            string month = "";
            string day = "";
            for (int i = 0; i < 4; i++)
            {
                year = year + date[i];
            };
            for (int i = 4; i < 6; i++)
            {
                month = month + date[i];
            };
            for (int i = 6; i < 8; i++)
            {
                day = day + date[i];
            };
            string convertedDate = year + "-" + month + "-" + day;
            return convertedDate;
        }
        */
        private void InitGrid()
        {
            dataGridView2.ColumnCount = 6;
            dataGridView2.Columns[0].Name = "Cadence(RPM)";
            dataGridView2.Columns[1].Name = "Altitude(m/ft)";
            dataGridView2.Columns[2].Name = "Heart rate(bpm)";
            dataGridView2.Columns[3].Name = "Power(watt)";
            dataGridView2.Columns[4].Name = "Speed(Mile/hr)";
            dataGridView2.Columns[5].Name = "Time";

            dataGridView3.ColumnCount = 10;
            dataGridView3.Columns[0].Name = "Total distance covered";
            dataGridView3.Columns[1].Name = "Average speed(km/hr)";
            dataGridView3.Columns[2].Name = "Maximum speed(km/hr)";
            dataGridView3.Columns[3].Name = "Average heart rate(bpm)";
            dataGridView3.Columns[4].Name = "Maximum heart rate(bpm)";
            dataGridView3.Columns[5].Name = "Minimum heart rate(bpm)";
            dataGridView3.Columns[6].Name = "Average power(watt)";
            dataGridView3.Columns[7].Name = "Maximum power(watt)";
            dataGridView3.Columns[8].Name = "Average altitude(RPM)";
            dataGridView3.Columns[9].Name = "Maximum altitude(RPM)";
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void viewGraphToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_hrData.Count < 1)
            {
                MessageBox.Show("Must select a file first");
            }
            else
            {
                ViewGraph._hrData = _hrData;
                new ViewGraph().Show();
            }
        }

        private void showGraphToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_hrData.Count < 1)
            {
                MessageBox.Show("Must select a file first");
            }
            else
            {
                IndividualGraph._hrData = _hrData;
                new IndividualGraph().Show();
            }
        }

        private void CalculateSpeed(string type)
        {
            if (_hrData.Count > 0)
            {
                List<string> data = new List<string>();
                if (type == "mile")
                {
                    dataGridView2.Columns[4].Name = "Speed(Mile/hr)";
                    data.Clear();
                    for (int i = 0; i < _hrData["cadence"].Count; i++)
                    {
                        string temp = (Convert.ToDouble(_hrData["speed"][i]) / 1.60934).ToString();
                        data.Add(temp);
                    }

                    _hrData["speed"] = data;
                    dataGridView2.Rows.Clear();
                    for (int i = 0; i < _hrData["cadence"].Count; i++)
                    {
                        string[] hrData = new string[] { _hrData["cadence"][i], _hrData["altitude"][i], _hrData["heartRate"][i],
                            _hrData["watt"][i], _hrData["speed"][i] };
                        dataGridView2.Rows.Add(hrData);
                    }
                }
                else
                {
                    dataGridView2.Columns[4].Name = "Speed(km/hr)";
                    data.Clear();
                    for (int i = 0; i < _hrData["cadence"].Count; i++)
                    {
                        string temp = (Convert.ToDouble(_hrData["speed"][i]) / 1.60934).ToString();
                        data.Add(temp);
                    }
                        _hrData["speed"] = data;
                        dataGridView2.Rows.Clear();
                    DateTime dateTime = DateTime.Parse(_param["StartTime"]);
                    for (int i = 0; i < _hrData["cadence"].Count; i++)
                        {
                       if (i > 0) dateTime = dateTime.AddSeconds(Convert.ToInt32(_param["Interval"]));
                        string[] hrData = new string[] { _hrData["cadence"][i], _hrData["altitude"][i], _hrData["heartRate"][i],
                            _hrData["watt"][i], _hrData["speed"][i], dateTime.TimeOfDay.ToString() };
                        dataGridView2.Rows.Add(hrData);
                        }
                    }
                }
            }

  

        private void perMileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            count++;
            if (count > 1) CalculateSpeed("mile");
        }

        private void perKmToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CalculateSpeed("km");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            new FileCompare().Show();
        }

        private void graphToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        private Dictionary<string, object> data = new Dictionary<string, object>();
        List<string> listCadence = new List<string>();
        List<string> listAltitude = new List<string>();
        List<string> listHeartRate = new List<string>();
        List<string> listPower = new List<string>();
        List<string> listSpeed = new List<string>();
        List<string> listTime = new List<string>();
        private void dataGridView2_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            try
            {
                if (e.StateChanged != DataGridViewElementStates.Selected) return;
                int index = Convert.ToInt32(e.Row.Index.ToString());

                //_hrData.Add("cadence", new List());
                string cadence = dataGridView2.Rows[index].Cells[0].Value.ToString();
                listCadence.Add(cadence);

                string altitude = dataGridView2.Rows[index].Cells[1].Value.ToString();
                listAltitude.Add(altitude);

                string heartRate = dataGridView2.Rows[index].Cells[2].Value.ToString();
                listHeartRate.Add(heartRate);

                string power = dataGridView2.Rows[index].Cells[3].Value.ToString();
                listPower.Add(power);

                string speed = dataGridView2.Rows[index].Cells[4].Value.ToString();
                listSpeed.Add(speed);

                string time = dataGridView2.Rows[index].Cells[5].Value.ToString();
                listTime.Add(time);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            data.Clear();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                data.Add("cadence", listCadence);
                data.Add("altitude", listAltitude);
                data.Add("heartRate", listHeartRate);
                data.Add("watt", listPower);
                data.Add("speed", listSpeed);
                data.Add("time", listTime);

                var endTime = data["time"] as List<string>;
                int count = endTime.Count();
                Dictionary<string, string> _param = new Dictionary<string, string>();
                _param.Add("StartTime", endTime[0]);

                dataGridView3.Rows.Clear();
                dataGridView3.Rows.Add(new TableFiller().FillDataInSumaryTable(data, endTime[count - 1], _param));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Please click on reset button first");
            }
        }

        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var data = _hrData.ToDictionary(k => k.Key, k => k.Value as object);
            new IntervalDetectionForm(data).Show();

        }
    } }