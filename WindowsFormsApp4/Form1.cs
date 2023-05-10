using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp4
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        double P(double x)
        {
            return Math.Exp(-(x - avg) * (x - avg) / (2 * var)) / Math.Sqrt(2 * Math.PI * var);
        }

        int[] count2 = new int[5];
        int a, b, j;
        double[] count = new double[5], number, prob;
        double avg, var, eAvg = 0, eVar = 0, chisq = 0, errAvg, errVar;

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            chart1.Series[0].Points.Clear();

            Random generator = new Random();
            number = new double[(int)numericUpDown5.Value];
            avg = (double)numericUpDown1.Value;
            var = (double)numericUpDown2.Value;
            double s;

            for (int i = 0; i < (int)numericUpDown5.Value; i++)
            {
                s = 0;

                for (j = 0; j < 12; j++)
                {
                    s += generator.NextDouble();
                }

                number[i] = (double)((s - 6) * Math.Sqrt(var)) + avg;
            }

            double min = number[0], max = number[0];

            for (int i = 1; i < (int)numericUpDown5.Value; i++)
            {
                if (number[i] < min)
                {
                    min = number[i];
                }

                if (number[i] > max)
                {
                    max = number[i];
                }
            }

            a = (int)Math.Floor(min); 
            b = (int)Math.Ceiling(max);

            for (int i = 0; i < (int)numericUpDown5.Value; i++)
            {
                j = 0;

                while (a + j * ((double)(b - a)) / 5 > number[i] || a + (j + 1) * ((double)(b - a)) / 5 <= number[i])
                {
                    j++;
                }

                count2[j]++;
            }

            for (int i = 0; i < 5; i++)
            {
                count[i] = (double)count2[i] / (int)numericUpDown5.Value;
            }

            double[] counter = count;
            double c = a;
            chart1.ChartAreas[0].Axes[0].Interval = ((double)(b - a)) / 5;

            for (int i = 0; i < counter.Length; i++)
            {
                chart1.Series[0].Points.AddXY(c + ((double)(b - a)) / 10, Math.Round(counter[i], 3));
                c += ((double)(b - a)) / 5;
            }

            prob = new double[5];

            for (int i = 0; i < 5; i++)
            {
                prob[i] = ((b - a) * P((2 * a + i * ((double)(b - a)) / 5 + (i + 1) * ((double)(b - a)) / 5) / 2) / 5);
                chisq += count2[i] * count2[i] / (prob[i] * (int)numericUpDown5.Value);
            }

            chisq -= (int)numericUpDown5.Value;

            for (int i = 0; i < (int)numericUpDown5.Value; i++)
            {
                eAvg += number[i];
                eVar += number[i] * number[i];
            }

            eAvg /= (int)numericUpDown5.Value;
            eAvg = Math.Round(eAvg, 3);

            eVar = eVar / (int)numericUpDown5.Value - eAvg * eAvg;
            eVar = Math.Round(eVar, 3);

            errAvg = Math.Round(Math.Abs((eAvg - avg)) * 100 / Math.Abs(avg), 3);
            errVar = Math.Round(Math.Abs((eVar - var)) * 100 / Math.Abs(var), 3);

            label11.Text = avg.ToString() + " ( error = " + errAvg.ToString("0") + " % )";
            label12.Text = var.ToString() + " ( error = " + errVar.ToString("0") + " % )";
            label13.Text = chisq.ToString("0.00");

            if (chisq > 11.07)
            {
                label15.ForeColor = Color.IndianRed;
                label15.Text = "true";
            }
            else
            {
                label15.ForeColor = Color.Green;
                label15.Text = "false";
            }


            for (int i = 0; i < 5; i++)
            {
                count2[i] = 0;
            }

            avg = 0; var = 0; eAvg = 0; eVar = 0; errAvg = 0; errVar = 0; chisq = 0;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }
    }
}
