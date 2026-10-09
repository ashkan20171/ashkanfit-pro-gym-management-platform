using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace AshkanFitPro
{
    public partial class UsersForm : Form
    {
        private readonly AccountService accounts = new AccountService();
        private ComboBox trainerSelector;
        private Button assignTrainerButton;
        public UsersForm()
        {
            InitializeComponent();
            addButton.Click += AddUser;
            removeButton.Click += RemoveUser;
            BuildTrainerAssignment();
            RefreshGrid();
        }
        private void BuildTrainerAssignment()
        {
            // SQL-backed account linking; controls are intentionally placed in the existing Designer action area.
            trainerSelector = new ComboBox { Name="trainerSelector", Width=175, Height=35,
                DropDownStyle=ComboBoxStyle.DropDownList, DisplayMember="Name", Margin=new Padding(8,3,5,0) };
            assignTrainerButton = new Button { Name="assignTrainerButton", Text="Link trainer / اتصال مربی",
                Width=180, Height=37, BackColor=Color.FromArgb(34,160,155), ForeColor=Color.White,
                FlatStyle=FlatStyle.Flat };
            actionsPanel.Controls.Add(trainerSelector);
            actionsPanel.Controls.Add(assignTrainerButton);
            assignTrainerButton.Click += AssignTrainer;
            usersGrid.SelectionChanged += (sender,args) => UpdateTrainerSelection();
            bool sql = SqlStore.Enabled;
            trainerSelector.Enabled = sql;
            assignTrainerButton.Enabled = sql;
            if(sql)
            {
                try {
                    var coaches = new Repository().Data.Trainers.OrderBy(t=>t.Name).ToList();
                    trainerSelector.Items.Add(new Trainer { Id=Guid.Empty, Name="— Unassign / بدون مربی —" });
                    foreach(var coach in coaches)trainerSelector.Items.Add(coach);
                    trainerSelector.SelectedIndex=0;
                }
                catch(Exception ex) { assignTrainerButton.Enabled=false; MessageBox.Show(this,ex.Message,"SQL / Trainers"); }
            }
        }
        private void UpdateTrainerSelection()
        {
            if(trainerSelector==null || !SqlStore.Enabled || usersGrid.CurrentRow==null)return;
            var name=Convert.ToString(usersGrid.CurrentRow.Cells["Username"].Value);
            var account=accounts.ListUsers().FirstOrDefault(x=>x.UserName==name);
            assignTrainerButton.Enabled=account!=null && account.Role=="Trainer";
            if(account==null)return;
            foreach(Trainer item in trainerSelector.Items)
                if(item.Id==(account.TrainerId??Guid.Empty)){trainerSelector.SelectedItem=item;break;}
        }
        private void AssignTrainer(object sender, EventArgs e)
        {
            if(usersGrid.CurrentRow==null || !(trainerSelector.SelectedItem is Trainer))return;
            var name=Convert.ToString(usersGrid.CurrentRow.Cells["Username"].Value);
            var trainer=(Trainer)trainerSelector.SelectedItem;
            try { accounts.AssignTrainer(name,trainer.Id==Guid.Empty?(Guid?)null:trainer.Id);RefreshGrid(); }
            catch(Exception ex) { MessageBox.Show(this,ex.Message,"AshkanFit Pro",MessageBoxButtons.OK,MessageBoxIcon.Warning); }
        }
        private void AddUser(object sender, EventArgs e)
        {
            try
            {
                accounts.AddUser(usernameInput.Text.Trim(), passwordInput.Text, roleInput.Text);
                usernameInput.Clear(); passwordInput.Clear();
                RefreshGrid();
            }
            catch (Exception ex) { MessageBox.Show(this, ex.Message, "AshkanFit Pro", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
        }
        private void RemoveUser(object sender, EventArgs e)
        {
            if (usersGrid.CurrentRow == null) return;
            var username = Convert.ToString(usersGrid.CurrentRow.Cells[0].Value);
            if (MessageBox.Show(this, "Remove " + username + "?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;
            try { accounts.DeleteUser(username); RefreshGrid(); }
            catch (Exception ex) { MessageBox.Show(this, ex.Message, "AshkanFit Pro", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
        }
        private void RefreshGrid()
        {
            usersGrid.DataSource = accounts.ListUsers().Select(x => new { Username = x.UserName, Role = x.Role, TrainerId = x.TrainerId }).ToList();
        }
    }
}
