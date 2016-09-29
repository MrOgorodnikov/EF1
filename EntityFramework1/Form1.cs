using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EntityFramework1
{
    public partial class Form1 : Form
    {
        SoccerContext db;
        public Form1()
        {
            InitializeComponent();

            db = new SoccerContext();
            db.Players.Load();

            dataGridView1.DataSource = db.Players.Local.ToBindingList();
        }

        private void Add_Click(object sender, EventArgs e)
        {
            Player player = new Player();
            AddPlayer plForm = new AddPlayer();

            db.Players.Add(CreatePlayer(player, plForm));
            db.SaveChanges();
        }

        private void Change_Click(object sender, EventArgs e)
        {
            if (dataGridView1.RowCount < 0)
                return;
            //var index = dataGridView1.SelectedRows[0].Index;
            int playerId = 0;
            bool getId = Int32.TryParse(dataGridView1[0, dataGridView1.SelectedRows[0].Index].Value.ToString(), out playerId);
            if (!getId)
                return;

            var player = db.Players.Find(playerId);

            AddPlayer changeForm = new AddPlayer();
            changeForm.NameTextBox.Text = player.Name;
            changeForm.AgeNumericUpDown.Value = player.Age;
            changeForm.PositionComboBox.SelectedItem = player.Position;
            CreatePlayer(player, changeForm);

            db.SaveChanges();

            dataGridView1.Refresh();
        }

        private Player CreatePlayer(Player player, AddPlayer form)
        {
            DialogResult result = form.ShowDialog(this);

            if (result == DialogResult.Cancel)
                return null;

            player.Name = form.NameTextBox.Text;
            player.Age = (int)form.AgeNumericUpDown.Value;
            player.Position = form.PositionComboBox.SelectedItem.ToString();

            return player;
        }

        private void Delete_Click(object sender, EventArgs e)
        {
            db.Players.Remove(db.Players.Find(int.Parse(dataGridView1[0, dataGridView1.SelectedRows[0].Index].Value.ToString())));
        }
    }
}
