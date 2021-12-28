using System;
using System.Drawing;

namespace Toast
{

    public interface INotifyMotion
    {
        public int MotionValue { get; set; }

        public int MotionDelay { get; set; }

        public Size MotionArea { get; set; }

        void Hide();

        void Show();

        event EventHandler DoneHideMotion;

        event EventHandler DoneShowMotion;
    }
}
