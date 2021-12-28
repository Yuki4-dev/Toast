using System;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Toast
{
    public class NotifyContent : Control, INotifyContent
    {
        protected readonly Button CloseButton;

        public bool AutoColor { get; set; } = true;

        public bool HasCloseButton
        {
            get => CloseButton.Visible;
            set => CloseButton.Visible = value;
        }

        public bool IsVisible
        {
            get => Visible;
            set => Visible = value;
        }

        private double dummyOpacity;
        public double Opacity
        {
            get
            {
                var f = FindForm();
                return f?.Opacity ?? dummyOpacity;
            }
            set
            {
                var f = FindForm();
                if (f != null)
                {
                    f.Opacity = value;
                }
                else
                {
                    dummyOpacity = value;
                }
            }
        }

        public event EventHandler RequestClose;

        public event EventHandler ClickContent;

        public event EventHandler ClickClose;

        public NotifyContent()
        {
            CloseButton = new Button()
            {
                Text = "x",
                FlatStyle = FlatStyle.Flat,
                Size = new Size(20, 30),
                TextAlign = ContentAlignment.TopCenter,
                Font = new Font("Yu Gothic UI", 11, FontStyle.Bold),
                Anchor = AnchorStyles.Top | AnchorStyles.Right
            };
            CloseButton.FlatAppearance.BorderSize = 0;
            Controls.Add(CloseButton);

            CloseButton.Click += (_, __) =>
            {
                ClickClose?.Invoke(this, EventArgs.Empty);
                OnRequestClose();
            };

            BackColorChanged += (_, __) =>
            {
                if (AutoColor)
                {
                    foreach (var c in Controls.OfType<Control>())
                    {
                        c.BackColor = BackColor;
                        c.ForeColor = GetForeColor(BackColor);
                    }
                }
            };

            SizeChanged += (_, __) =>
            {
                CloseButton.Location = new Point(Width - (CloseButton.Width + 5), 0);
            };
        }

        private Color GetForeColor(Color color)
        {
            var r = color.R > 128 ? 0 : 255;
            var g = color.G > 128 ? 0 : 255;
            var b = color.B > 128 ? 0 : 255;

            return Color.FromArgb(r, g, b);
        }

        protected async Task ClickMotion(bool reqestClose = true)
        {
            Location = new Point(Location.X + 10, Location.Y + 10);

            await Task.Delay(100);

            if (reqestClose)
            {
                OnRequestClose();
            }
        }

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
