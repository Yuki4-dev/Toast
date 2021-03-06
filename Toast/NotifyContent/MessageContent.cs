using System.Drawing;
using System.Windows.Forms;

namespace Toast
{
    public class MessageContent : NotifyContent
    {
        private readonly Label title;
        private readonly Label content;

        public string Title
        {
            get => title.Text;
            set => title.Text = value;
        }

        public string Content
        {
            get => content.Text;
            set => content.Text = value;
        }

        public MessageContent()
        {
            title = new Label()
            {
                Dock = DockStyle.Top,
                Font = new Font("Yu Gothic UI", 24, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter
            };

            content = new Label()
            {
                Dock = DockStyle.Bottom,
                Font = new Font("Yu Gothic UI", 11, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter
            };

            this.Controls.Add(title);
            this.Controls.Add(content);

            title.Click += (_, __) => Content_Click();
            content.Click += (_, __) => Content_Click();
            this.SizeChanged += (_, __) => Control_SizeChenged();
        }

        private void Control_SizeChenged()
        {
            title.Height = this.Height / 5;
            content.Height = this.Height - title.Height;
        }

        private void Content_Click()
        {
            ClickMotion().ContinueWith((_) => OnClickContent());
        }
    }
}
