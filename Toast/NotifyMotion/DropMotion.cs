using System.Drawing;
using System.Threading.Tasks;

namespace Toast
{
    class DropMotion : NotifyMotionBase
    {
        public override ToastType TostType => ToastType.Drop;

        public DropMotion(NotifyContent content, Size area) : base(content, area) { }

        protected override async Task ShowMotion()
        {
            Content.Location = new Point(0, motionArea.Height);
            Content.Visible = true;

            while (Content.Location.Y > 0)
            {
                await Task.Delay(MotionDelay);
                if (Content.Location.Y - MotionValue < 0)
                {
                    Content.Location = new Point(Content.Location.X, 0);
                }
                else
                {
                    Content.Location = new Point(Content.Location.X, Content.Location.Y - MotionValue);
                }
            }
        }

        protected override async Task CloseMotion()
        {
            while (Content.Location.Y < motionArea.Height)
            {
                await Task.Delay(MotionDelay);
                Content.Location = new Point(Content.Location.X, Content.Location.Y + MotionValue);
            }

            Content.Visible = false;
        }
    }
}
