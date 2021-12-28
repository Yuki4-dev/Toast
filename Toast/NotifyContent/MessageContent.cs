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
            Controls.Add(title);
            Controls.Add(content);

            title.Click += (_, __) => Content_Click();
            content.Click += (_, __) => Content_Click();
            SizeChanged += (_, __) =>
            {
                title.Height = Height / 5;
                content.Height = Height - title.Height;
            };
        }

        private void Content_Click()
        {
            ClickMotion().ContinueWith((_) => OnClickContent());
        }
    }
}
