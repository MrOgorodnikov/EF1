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
            DialogResult result = plForm.ShowDialog(this);

            if (result == DialogResult.Cancel)
                return;

            player.Name = plForm.NameTextBox.Text;
            player.Age = (int)plForm.AgeNumericUpDown.Value;
            player.Position = plForm.PositionComboBox.SelectedItem.ToString();

            db.Players.Add(player);
            db.SaveChanges();
        }
    }
}
