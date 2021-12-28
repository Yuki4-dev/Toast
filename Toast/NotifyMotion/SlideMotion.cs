using System.Drawing;
using System.Threading.Tasks;

namespace Toast
{
    public class SlideMotion : NotifyMotionBase
    {
        public SlideMotion(INotifyContent content) : base(content)
        {
            MotionDelay = 10;
        }

        protected override async Task ShowMotion()
        {
            notifyContent.Location = new Point(MotionArea.Width, 0);
            notifyContent.IsVisible = true;

            while (notifyContent.Location.X > 0)
            {
                await Task.Delay(MotionDelay);
                if (notifyContent.Location.X - MotionValue < 0)
                {
                    notifyContent.Location = new Point(0, notifyContent.Location.Y);
                }
                else
                {
                    notifyContent.Location = new Point(notifyContent.Location.X - MotionValue, notifyContent.Location.Y);
                }

            }
        }

        protected override async Task CloseMotion()
        {
            while (notifyContent.Location.X < MotionArea.Width)
            {
                await Task.Delay(MotionDelay);
                notifyContent.Location = new Point(notifyContent.Location.X + MotionValue, notifyContent.Location.Y);
            }

            notifyContent.IsVisible = false;
        }
    }
}
