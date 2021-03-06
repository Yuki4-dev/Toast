using System;

namespace Toast
{
    public interface INotifyMotion
    {
        ToastType TostType { get; }
        NotifyContent Content { get; }
        void Hide();
        void Show();
        event EventHandler DoneCloseMotion;
    }

    public enum ToastType
    {
        Slide, Drop, Transparent, None
    }
}
