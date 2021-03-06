using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Toast
{
    public partial class ToastForm : Form
    {
        protected override bool ShowWithoutActivation => true;
        private INotifyMotion notifyMotion;
        private bool isCloseRequest = false;

        public ToastForm()
        {
            InitializeComponent();
            this.FormClosing += ToastForm_Closing;
        }

        private void ToastForm_Closing(object sender, FormClosingEventArgs e)
        {
            if (!isCloseRequest && notifyMotion != null)
            {
                notifyMotion.Hide();
                e.Cancel = true;
            }
        }

        public void SetPosition(int x, int y)
        {
            this.Location = new Point(x - this.Width, y - this.Height);
        }

        public async void ShowAsync(INotifyMotion motion, int delay = 0)
        {
            notifyMotion = motion;
            this.Controls.Add(motion.Content);

            notifyMotion.DoneCloseMotion += (_, __) =>
            {
                isCloseRequest = true;
                this.Close();
            };

            this.Show();
            notifyMotion.Show();

            if (delay > 0)
            {
                await Task.Delay(delay);
                this.Close();
            }
        }
    }
}
