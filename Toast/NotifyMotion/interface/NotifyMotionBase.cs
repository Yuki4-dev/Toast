using System;
using System.Drawing;
using System.Threading.Tasks;

namespace Toast
{
    public abstract class NotifyMotionBase : INotifyMotion
    {
        private readonly object lockObj = new object();
        private bool runClose = false;
        private bool runShow = false;

        protected readonly Size motionArea;

        public int MotionValue { get; set; } = 50;
        public int MotionDelay { get; set; } = 20;
        public abstract ToastType TostType { get; }
        public NotifyContent Content { get; }

        public event EventHandler DoneCloseMotion;

        public NotifyMotionBase(NotifyContent content, Size area)
        {
            Content = content;
            motionArea = area;

            Content.RequestClose += (_, __) => this.Hide();
        }

        public async void Hide()
        {
            lock (lockObj)
            {
                if(runClose)
                {
                    return;
                }
                runClose = true;
            }

            await CloseMotion();

            OnDoneCloseMotion();
        }

        public async void Show()
        {
            lock (lockObj)
            {
                if (runShow)
                {
                    return;
                }
                runShow = true;
            }

            await ShowMotion();
        }

        protected void OnDoneCloseMotion()
        {
            DoneCloseMotion?.Invoke(this, EventArgs.Empty);
        }

        protected abstract Task CloseMotion();
        protected abstract Task ShowMotion();
    }

    public class DefaultMotion : NotifyMotionBase
    {
        public override ToastType TostType => ToastType.None;

        public DefaultMotion(NotifyContent content, Size area) : base(content, area) { }

        protected override Task CloseMotion()
        {
            Content.Visible = false;
            return Task.Delay(1);
        }

        protected override Task ShowMotion()
        {
            Content.Visible = true;
            return Task.Delay(1);
        }
    }

}
