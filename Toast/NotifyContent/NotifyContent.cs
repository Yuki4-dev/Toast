using System;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Toast
{
    public class NotifyContent : Control
    {
        protected readonly Button CancelButton;

        public bool AutoColor { get; set; } = true;
        public bool HasCancelButton
        {
            get => CancelButton.Visible;
            set => CancelButton.Visible = value;
        }

        public event EventHandler RequestClose;
        public event EventHandler ClickContent;

        public NotifyContent()
        {
            CancelButton = new Button()
            {
                Text = "x",
                FlatStyle = FlatStyle.Flat,
                Size = new Size(20, 30),
                TextAlign = ContentAlignment.TopCenter,
                Font = new Font("Yu Gothic UI", 11, FontStyle.Bold),
                Anchor = AnchorStyles.Top | AnchorStyles.Right
            };
            CancelButton.FlatAppearance.BorderSize = 0;

            this.Controls.Add(CancelButton);

            SetEvent();
        }

        private void SetEvent()
        {
            CancelButton.Click += (_, __) =>
            {
                ContentCancelInvoke();
                OnRequestClose();
            };

            this.BackColorChanged += (_, __) =>
            {
                if (AutoColor)
                {
                    ChengeContentColor(this.BackColor);
                }
            };

            this.SizeChanged += (_, __) => CancelButton.Location = new Point(this.Width - (CancelButton.Width + 5), 0);
        }

        protected void ChengeContentColor(Color baseColor)
        {
            foreach (var c in this.Controls.OfType<Control>())
            {
                c.BackColor = baseColor;
                c.ForeColor = GetForeColor(baseColor);
            }
        }

        protected Color GetForeColor(Color color)
        {
            var r = color.R > 128 ? 0 : 255;
            var g = color.G > 128 ? 0 : 255;
            var b = color.B > 128 ? 0 : 255;

            return Color.FromArgb(r, g, b);
        }

        protected async Task ClickMotion(bool reqestClose = true)
        {
            this.Location = new Point(this.Location.X + 10, this.Location.Y + 10);

            await Task.Delay(100);

            if (reqestClose)
            {
                OnRequestClose();
            }
        }

        protected virtual void ContentCancelInvoke() { }

        protected void OnRequestClose()
        {
            RequestClose?.Invoke(this, EventArgs.Empty);
        }

        protected void OnClickContent()
        {
            ClickContent?.Invoke(this, EventArgs.Empty);
        }
    }
}
