using System;
using System.Drawing;

namespace Toast
{
    public interface INotifyContent
    {
        bool IsVisible { get; set; }

        Point Location { get; set; }

        double Opacity { get; set; }

        public event EventHandler RequestClose;

        public event EventHandler ClickContent;

        public event EventHandler ClickClose;
    }
}
