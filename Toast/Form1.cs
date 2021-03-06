using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Toast
{
    public partial class Form1 : Form
    {
        public decimal Content_H { get; set; } = 180;
        public decimal Content_W { get; set; } = 380;
        public decimal Area_H { get; set; } = 200;
        public decimal Area_W { get; set; } = 400;

        private int current = 0;

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

        public Form1()
        {
            InitializeComponent();

            comboBox1.DataSource = Enum.GetValues(typeof(ToastType)).Cast<ToastType>().Select(x => Tuple.Create(x.ToString(), x)).ToList();

            button1.Click += Button_Click;

            numericUpDown1.DataBindings.Add(new Binding(nameof(numericUpDown1.Value), this, nameof(Area_W)));
            numericUpDown2.DataBindings.Add(new Binding(nameof(numericUpDown2.Value), this, nameof(Area_H)));
            numericUpDown3.DataBindings.Add(new Binding(nameof(numericUpDown3.Value), this, nameof(Content_W)));
            numericUpDown4.DataBindings.Add(new Binding(nameof(numericUpDown4.Value), this, nameof(Content_H)));
        }

        private void Button_Click(object sender, EventArgs e)
        {
            current = ++current % colors.Length;

            var toastForm = new ToastForm()
            {
                Size = new Size((int)Area_W, (int)Area_H)
            };
            toastForm.SetPosition(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.WorkingArea.Height);

            var content = new MessageContent()
            {
                BackColor = colors[current],
                Size = new Size((int)Content_W, (int)Content_H),
                Content = DateTime.Now.ToString(),
                Title = this.GetType().ToString()
            };
            content.ClickContent += (s, e) => MessageBox.Show($"Click : {((Control)s).BackColor}");

            toastForm.Show(GetMotion(content, toastForm.Size, (ToastType)comboBox1.SelectedValue), 5000);
        }

        private INotifyMotion GetMotion(NotifyContent content, Size areaSize, ToastType type)
        {
            return type switch
            {
                ToastType.Slide => new SlideMotion(content, areaSize),
                ToastType.Drop => new DropMotion(content, areaSize),
                ToastType.Transparent => new TransparentMotion(content, areaSize),
                ToastType.None => new DefaultMotion(content, areaSize),
                _ => throw new NotImplementedException()
            };
        }
    }
}
