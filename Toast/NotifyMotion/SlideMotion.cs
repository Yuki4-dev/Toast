using System.Drawing;
using System.Threading.Tasks;

namespace Toast
{
    public class SlideMotion : NotifyMotionBase
    {
        public override ToastType TostType => ToastType.Slide;

        public SlideMotion(NotifyContent content, Size area) : base(content, area)
        {
            MotionDelay = 10;
        }

        protected override async Task ShowMotion()
        {
            Content.Location = new Point(motionArea.Width, 0);
            Content.Visible = true;

            while (Content.Location.X > 0)
            {
                await Task.Delay(MotionDelay);
                if (Content.Location.X - MotionValue < 0)
                {
                    Content.Location = new Point(0, Content.Location.Y);
                }
                else
                {
                    Content.Location = new Point(Content.Location.X - MotionValue, Content.Location.Y);
                }

            }
        }

        protected override async Task CloseMotion()
        {
            while (Content.Location.X < motionArea.Width)
            {
                await Task.Delay(MotionDelay);
                Content.Location = new Point(Content.Location.X + MotionValue, Content.Location.Y);
            }

            Content.Visible = false;
        }
    }
}
