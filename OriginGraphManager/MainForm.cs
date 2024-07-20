using System.Windows.Forms;

namespace OriginGraphManager
{
    public partial class MainForm : Form
    {
        private WorkFlowForm workFlowForm;

        public MainForm()
        {
            InitializeComponent();
            OriginController.Initialize();
            this.IsMdiContainer = true;
            workFlowForm = new WorkFlowForm(this);
            workFlowForm.MdiParent = this;
            workFlowForm.Show();
        }

        public void PutInFront()
        {
            this.Activate();
        }
    }
}
