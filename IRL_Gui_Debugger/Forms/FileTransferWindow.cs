namespace IRL_Gui_Debugger.Forms
{
    public partial class FileTransferWindow : Form
    {
        private int m_totalFileBytes;
        public bool CancelTransfer { get; private set; } = false;

        public FileTransferWindow(int totalFileBytes)
        {
            m_totalFileBytes = totalFileBytes;
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            UpdateProgress(m_totalFileBytes);
        }

        public void UpdateProgress(int fileBytesRemaining)
        {
            if (fileBytesRemaining < 0)
            {
                fileBytesRemaining = 0;
            }

            int progress = (int)((m_totalFileBytes - fileBytesRemaining) * 100.0f / m_totalFileBytes);

            if (progress > 100)
            {
                progress = 100;
            }

            if (progress < 0)
            {
                progress = 0;
            }

            FileTransferProgressBar.Value = progress;
            BytesWrittenLabel.Text = $"Bytes Written: {m_totalFileBytes - fileBytesRemaining} / {m_totalFileBytes}";
            FileTransferProgressBar.Refresh();
            BytesWrittenLabel.Refresh();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            CancelTransfer = true;
        }
    }
}
