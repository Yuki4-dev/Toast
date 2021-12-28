using System.Drawing;
using System.Threading.Tasks;

namespace Toast
{
    internal class DropMotion : NotifyMotionBase
    {
        public DropMotion(INotifyContent content) : base(content) { }

        protected override async Task ShowMotion()
        {
            notifyContent.Location = new Point(0, MotionArea.Height);
            notifyContent.IsVisible = true;

            while (notifyContent.Location.Y > 0)
            {
                await Task.Delay(MotionDelay);
                if (notifyContent.Location.Y - MotionValue < 0)
                {
                    notifyContent.Location = new Point(notifyContent.Location.X, 0);
                }
                else
                {
                    notifyContent.Location = new Point(notifyContent.Location.X, notifyContent.Location.Y - MotionValue);
                }
            }
        }

        protected override async Task CloseMotion()
        {
            while (notifyContent.Location.Y < MotionArea.Height)
            {
                await Task.Delay(MotionDelay);
                notifyContent.Location = new Point(notifyContent.Location.X, notifyContent.Location.Y + MotionValue);
            }

            notifyContent.IsVisible = false;
        }
    }
}
