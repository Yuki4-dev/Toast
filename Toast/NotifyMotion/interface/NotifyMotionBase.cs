using System;
using System.Drawing;
using System.Threading.Tasks;

namespace Toast
{
    public abstract class NotifyMotionBase : INotifyMotion
    {
        private bool runHide = false;

        private bool runShow = false;

        protected readonly INotifyContent notifyContent;

        public int MotionValue { get; set; } = 50;

        public int MotionDelay { get; set; } = 20;

        public Size MotionArea { get; set; }

        public event EventHandler DoneHideMotion;

        public event EventHandler DoneShowMotion;

        public NotifyMotionBase(INotifyContent content)
        {
            notifyContent = content;
            notifyContent.RequestClose += (_, __) => Hide();
        }

        public void Hide()
        {
            if (runHide)
            {
                return;
            }
            runHide = true;

            CloseMotion().ContinueWith((_) => OnDoneHideMotion());
        }

        public void Show()
        {
            if (runShow)
            {
                return;
            }
            runShow = true;

            ShowMotion().ContinueWith((_) => OnDoneShowMotion());
        }

        protected void OnDoneHideMotion()
        {
            DoneHideMotion?.Invoke(this, EventArgs.Empty);
        }

        protected void OnDoneShowMotion()
        {
            DoneShowMotion?.Invoke(this, EventArgs.Empty);
        }

        protected abstract Task CloseMotion();

        protected abstract Task ShowMotion();
    }

    public class DefaultMotion : NotifyMotionBase
    {
        public DefaultMotion(NotifyContent content) : base(content) { }

        protected override Task CloseMotion()
        {
            notifyContent.IsVisible = false;
            return Task.CompletedTask;
        }

        protected override Task ShowMotion()
        {
            notifyContent.IsVisible = true;
            return Task.CompletedTask;
        }
    }

}
