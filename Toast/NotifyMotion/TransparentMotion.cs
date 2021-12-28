using System.Drawing;
using System.Threading.Tasks;

namespace Toast
{
    public class TransparentMotion : NotifyMotionBase
    {
        public TransparentMotion(INotifyContent content) : base(content) { }

        protected override async Task ShowMotion()
        {
            notifyContent.Location = new Point(0, 0);
            notifyContent.Opacity = 0;
            notifyContent.IsVisible = true;

            while (notifyContent.Opacity < 1.0)
            {
                await Task.Delay(MotionDelay);
                notifyContent.Opacity += 0.1;
            }
        }

        protected override async Task CloseMotion()
        {
            while (notifyContent.Opacity > 0.0)
            {
                await Task.Delay(MotionDelay);
                notifyContent.Opacity -= 0.1;
            }
        }
    }
}
