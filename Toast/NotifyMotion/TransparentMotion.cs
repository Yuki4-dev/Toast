using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Toast
{
    public class TransparentMotion : NotifyMotionBase
    {
        public override ToastType TostType => ToastType.Transparent;

        public TransparentMotion(NotifyContent content, Size area) : base(content, area) { }

        private Form SerchForm(Control ctrl)
        {
            if (ctrl.Parent != null)
            {
                return ctrl.Parent is Form form ? form : SerchForm(ctrl.Parent);
            }
            return null;
        }

        protected override async Task ShowMotion()
        {
            var parentForm = SerchForm(Content);
            Content.Location = new Point(0, 0);
            Content.Visible = true;

            if (parentForm != null)
            {
                parentForm.Opacity = 0;

                while (parentForm.Opacity < 1.0)
                {
                    await Task.Delay(MotionDelay);
                    parentForm.Opacity += 0.1;
                }
            }
        }

        protected override async Task CloseMotion()
        {
            var parentForm = SerchForm(Content);
            if (parentForm != null)
            {
                while (parentForm.Opacity > 0.0)
                {
                    await Task.Delay(MotionDelay);
                    parentForm.Opacity -= 0.1;
                }
            }
            else
            {
                Content.Visible = false;
            }
        }
    }
}
