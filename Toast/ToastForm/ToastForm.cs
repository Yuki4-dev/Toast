using System.Drawing;
using System.Windows.Forms;

namespace Toast
{
    public partial class ToastForm : Form
    {
        protected override bool ShowWithoutActivation => true;

        private bool isMotionDone = false;

        private ToastForm()
        {
            InitializeComponent();
        }

        public static void Show(Control content, INotifyMotion motion, Size toastAreaSize, Point startLocation)
        {
            var toast = new ToastForm()
            {
                StartPosition = FormStartPosition.Manual,
                Size = toastAreaSize,
                Location = startLocation
            };
            
            toast.FormClosing += (_, e) =>
            {
                if (!toast.isMotionDone)
                {
                    motion.Hide();
                    e.Cancel = true;
                }
            };

            toast.Controls.Add(content);

            motion.MotionArea = toastAreaSize;
            motion.DoneHideMotion += (_, __) =>
            {
                toast.isMotionDone = true;
                toast.Close();
            };

            toast.Show();
            motion.Show();
        }
    }
}
