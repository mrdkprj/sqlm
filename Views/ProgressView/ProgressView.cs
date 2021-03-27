using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MasudaManager.Views
{
    public partial class ProgressView : Form
    {
        public event EventHandler CancelButtonClicked;

        Stopwatch _stopWatch = new Stopwatch();
        string _elapsedFormat = "hh\\:mm\\:ss\\.fff";

        public ProgressView()
        {
            InitializeComponent();

            this.progressBar.Style = ProgressBarStyle.Marquee;
            this.btCancel.Click += OnCancelButtonClicked;
            this.timer.Tick += timer_Tick;
        }

        public bool CloseOnCancel { get; set; }

        public string Caption
        {
            get { return this.Text; }
            set { this.Text = value; }
        }

        public string StatusLabelText
        {
            get { return this.statusLabel.Text; }
            set { this.statusLabel.Text = value; }
        }

        public void ShowView(IWin32Window owner)
        {
            this.StartPosition = FormStartPosition.CenterScreen;
            this.timer.Start();
            _stopWatch.Reset();
            _stopWatch.Start();

            this.Show(owner);
        }

        public void CloseView()
        {
            this.Close();
        }

        void timer_Tick(object sender, EventArgs e)
        {
            this.TimeLabel.Text = _stopWatch.Elapsed.ToString(_elapsedFormat);
        }

        void OnCancelButtonClicked(object sender, EventArgs e)
        {
            if (this.CancelButtonClicked != null)
                this.CancelButtonClicked(sender, e);

            if (this.CloseOnCancel)
                CloseView();
        }
    }
}
