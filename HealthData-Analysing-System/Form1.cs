﻿using System;
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

namespace HealthData_Analysing_System
{
    public partial class Form1 : Form
    {
        private int count = 0;
        private Dictionary<string, List<string>> _hrData = new Dictionary<string, List<string>>();
        private Dictionary<string, string> _param = new Dictionary<string, string>();
        private string endTime;
        private List<int> smode = new List<int>();

        public Form1()
        {
            InitializeComponent();
            InitGrid();
            this.CenterToScreen();
           // this.radioButton1.Checked = true;
        }

        private string[] SplitString(string text)
        {
            var splitString = new string[] { "[Params]", "[Note]", "[IntTimes]", "[IntNotes]",
                "[ExtraData]", "[LapNames]", "[Summary-123]",
                "[Summary-TH]", "[HRZones]", "[SwapTimes]", "[Trip]", "[HRData]"};

            var splittedText = text.Split(splitString, StringSplitOptions.RemoveEmptyEntries);

            return splittedText;
        }

        private string[] SplitStringByEnter(string text)
        {
            return text.Split(new[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
        }

        private string[] SplitStringBySpace(string text)
        {
            var formattedText = string.Join(" ", text.Split().Where(x => x != ""));
            return formattedText.Split(' ');
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
                _param = new Dictionary<string, string>();
                _hrData = new Dictionary<string, List<string>>();
                string text = File.ReadAllText(openFileDialog2.FileName);
                var splittedString = SplitString(text);

                var splittedParamsData = SplitStringByEnter(splittedString[0]);

                foreach (var data in splittedParamsData)
                {
                    if (data != "\r")
                    {
                        string[] parts = data.Split(new[] { "=" }, StringSplitOptions.RemoveEmptyEntries);
                        _param.Add(parts[0], parts[1]);
                    }
                }
                

                lblStartTime.Text = "Start Time" + "= " + _param["StartTime"];
                lblInterval.Text = "Interval" + "= " + Regex.Replace(_param["Interval"], @"\t|\n|\r", "") + " sec";
                lblMonitor.Text = "Monitor" + "= " + _param["Monitor"];
                lblSMode.Text = "SMode" + "= " + _param["SMode"];
                lblDate.Text = "Date" + "= " + ConvertToDate(_param["Date"]);
                lblLength.Text = "Length" + "= " + _param["Length"];
                lblWeight.Text = "Weight" + "= " + Regex.Replace(_param["Weight"], @"\t|\n|\r", "") + " kg";

                var sMode = _param["SMode"];
                for (int i = 0; i < sMode.Length; i++)
                {
                    smode.Add((int)Char.GetNumericValue(_param["SMode"][i]));
                }

                List<string> cadence = new List<string>();
                List<string> altitude = new List<string>();
                List<string> heartRate = new List<string>();
                List<string> watt = new List<string>();
                List<string> speed = new List<string>();
                List<string> time = new List<string>();

                //adding data for datagrid
                var splittedHrData = SplitStringByEnter(splittedString[11]);
                DateTime dateTime = DateTime.Parse(_param["StartTime"]);
                int temp = 0;
                foreach (var data in splittedHrData)
                {
                    temp++;
                    var value = SplitStringBySpace(data);

                    if (value.Length >= 5)
                    {
                        cadence.Add(value[0]);
                        altitude.Add(value[1]);
                        heartRate.Add(value[2]);
                        watt.Add(value[3]);
                        speed.Add(value[4]);

                        if (temp > 2) dateTime = dateTime.AddSeconds(Convert.ToInt32(_param["Interval"]));
                        endTime = dateTime.TimeOfDay.ToString();

                        List<string> hrData = new List<string>();
                        hrData.Add(value[0]);
                        hrData.Add(value[1]);
                        hrData.Add(value[2]);
                        hrData.Add(value[3]);
                        hrData.Add(value[4]);
                        hrData.Add(dateTime.TimeOfDay.ToString());
                        dataGridView2.Rows.Add(hrData.ToArray());
                    }
                }

                _hrData.Add("cadence", cadence);
                _hrData.Add("altitude", altitude);
                _hrData.Add("heartRate", heartRate);
                _hrData.Add("watt", watt);
                _hrData.Add("speed", speed);

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

                //Total Distance Covered = Average Speed * Total Time;
                double startDate = TimeSpan.Parse(_param["StartTime"]).TotalSeconds;
                double endDate = TimeSpan.Parse(endTime).TotalSeconds;
                double totalTime = endDate - startDate;

                string averageSpeed = Summary.FindAverage(_hrData["speed"]).ToString();
                string totalDistanceCovered = (Convert.ToDouble(averageSpeed) * totalTime).ToString();
                string maxSpeed = Summary.FindMax(_hrData["speed"]).ToString();

                string averageHeartRate = Summary.FindAverage(_hrData["heartRate"]).ToString();
                string maximumHeartRate = Summary.FindMax(_hrData["heartRate"]).ToString();
                string minHeartRate = Summary.FindMin(_hrData["heartRate"]).ToString();

                string averagePower = Summary.FindAverage(_hrData["watt"]).ToString();
                string maxPower = Summary.FindMax(_hrData["watt"]).ToString();

                string averageAltitude = Summary.FindAverage(_hrData["altitude"]).ToString();
                string maximumAltitude = Summary.FindAverage(_hrData["altitude"]).ToString();

                string[] summarydata = new string[] { totalDistanceCovered, averageSpeed, maxSpeed, averageHeartRate, maximumHeartRate, minHeartRate, averagePower, maxPower, averageAltitude, maximumAltitude };
                dataGridView3.Rows.Clear();
                dataGridView3.Rows.Add(summarydata);
            }
        }

        private string ConvertToDate(string date)
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
                        string[] hrData = new string[] { _hrData["cadence"][i], _hrData["altitude"][i], _hrData["heartRate"][i], _hrData["watt"][i], _hrData["speed"][i] };
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
                        // string[] hrData = new string[] { _hrData["cadence"][i], _hrData["altitude"][i], _hrData["heartRate"][i], _hrData["watt"][i], _hrData["speed"][i] };
                        if (i > 0) dateTime = dateTime.AddSeconds(Convert.ToInt32(_param["Interval"]));
                        string[] hrData = new string[] { _hrData["cadence"][i], _hrData["altitude"][i], _hrData["heartRate"][i], _hrData["watt"][i], _hrData["speed"][i], dateTime.TimeOfDay.ToString() };

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

        
    } }