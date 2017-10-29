using CNTK.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CNTK.Demo
{
    public partial class CoordinatesDemo : Form
    {
        public CoordinatesDemo()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            var ls = new float[] { 0.62924f, 0.27205f, 0.18896f, 0.15387f, 0.13300f, 0.11825f, 0.12472f, 0.10494f,
                                   0.10230f, 0.09195f, 0.08799f, 0.08918f, 0.08870f, 0.07815f, 0.07645f, 0.0722f,
                                   0.07872f, 0.07150f, 0.06673f, 0.06978f, 0.06506f, 0.06094f, 0.05842f, 0.05477f,
                                   0.05496f, 0.05020f, 0.05247f, 0.05426f, 0.05103f, 0.05112f, 0.05074f, 0.05016f,
                                   0.04741f, 0.04846f, 0.04658f, 0.04856f, 0.05050f, 0.04832f, 0.04482f, 0.04547f
                                };

            var data = new DataSet();
            data.Pen = Pens.Red;

            for (var i = 0; i < ls.Length; i++)
            {
                data.Add(new PointF(i + 1, ls[i]));
            }

            coordinates1.DataSets.Add(data);
        }
    }
}
