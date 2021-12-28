using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Toast
{
    public partial class Form1 : Form
    {
        public decimal Content_H { get; set; } = 280;

        public decimal Content_W { get; set; } = 380;

        public decimal Area_H { get; set; } = 300;

        public decimal Area_W { get; set; } = 400;

        private readonly Color[] colors = new Color[]
        {
            Color.White,
            Color.Red,
            Color.DimGray,
            Color.Yellow,
            Color.Black,
            Color.Blue,
            Color.DeepPink,
        };

        private readonly Dictionary<string, Func<NotifyContent, INotifyMotion>> Motions = new Dictionary<string, Func<NotifyContent, INotifyMotion>>();

        private int current = 0;

        public Form1()
        {
            InitializeComponent();

            Motions.Add("Slide", c => new SlideMotion(c));
            Motions.Add("Drop", c => new DropMotion(c));
            Motions.Add("Transparent", c => new TransparentMotion(c));
            Motions.Add("None", c => new DefaultMotion(c));

            comboBox1.DataSource = Motions.Select(m => m.Key).ToList();

            numericUpDown1.DataBindings.Add(new Binding(nameof(numericUpDown1.Value), this, nameof(Area_W)));
            numericUpDown2.DataBindings.Add(new Binding(nameof(numericUpDown2.Value), this, nameof(Area_H)));
            numericUpDown3.DataBindings.Add(new Binding(nameof(numericUpDown3.Value), this, nameof(Content_W)));
            numericUpDown4.DataBindings.Add(new Binding(nameof(numericUpDown4.Value), this, nameof(Content_H)));
            button1.Click += Button_Click;
        }

        private async void Button_Click(object sender, EventArgs e)
        {
            current = ++current % colors.Length;

            var content = new MessageContent()
            {
                BackColor = colors[current],
                Size = new Size((int)Content_W, (int)Content_H),
                Content = DateTime.Now.ToString(),
                Title = GetType().ToString()
            };
            content.ClickContent += (s, e) => MessageBox.Show($"Click : {((Control)s).BackColor}");

            var motion = Motions[(string)comboBox1.SelectedValue].Invoke(content);
            var screen = Screen.FromHandle(Handle);
            var toastAreaSize = new Size((int)Area_W, (int)Area_H);
            var location = new Point(screen.Bounds.X + (screen.Bounds.Width - toastAreaSize.Width), screen.Bounds.Y + (screen.Bounds.Height - toastAreaSize.Height));
            ToastForm.Show(content, motion, toastAreaSize, location);

            await Task.Delay(5000);

            motion.Hide();
        }

    }
}
