using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace FootballTradingCards
{
    public partial class Form1 : Form
    {
        private BindingList<Player> players;

        public Form1()
        {
            InitializeComponent();
            InitializePlayers();
        }

        public void BindPlayer(Player player)
        {
            lblName.DataBindings.Add("Text", player, "Name");
            lblTeam.DataBindings.Add("Text", player, "Team");
            lblPosition.DataBindings.Add("Text", player, "Position");
            lblMatchesPlayed.DataBindings.Add("Text", player, "MatchesPlayed");
            lblGoals.DataBindings.Add("Text", player, "Goals");
            lblAssists.DataBindings.Add("Text", player, "Assists");

            // Dynamic visual updates
            lblGoals.ForeColor = player.Goals > 50 ? Color.Green : Color.Red;
            lblAssists.ForeColor = player.Assists > 30 ? Color.Green : Color.Red;

            // Update the picture dynamically
            if (!string.IsNullOrEmpty(player.PhotoPath) && File.Exists(player.PhotoPath))
            {
                picPlayerPhoto.Image = Image.FromFile(player.PhotoPath);
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (players != null && players.Count > 0)
            {
                lstPlayers.DataSource = players.OrderBy(p => p.Name).ToList(); // Sorting players by name
                lstPlayers.DisplayMember = "Name";
            }
            else
            {
                MessageBox.Show("No players loaded!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            // Ensure visibility settings
            lstPlayers.Visible = true;
            btnAddPlayer.Visible = true;
            pnlPlayerDetails.Visible = false;
            btnBack.Visible = false;
            btnRemovePlayer.Visible = false;
        }

        private void InitializePlayers()
        {
            players = new BindingList<Player>
            {new Player { Name = "Robert Lewandowski", Team = "Inter Miami", PhotoPath = @"messi.jpg", Position = "Forward", MatchesPlayed = 105, Goals = 85, Assists = 45, Nationality = "Polish", Age = 35 },
                new() { Name = "Zlatan Ibrahimović", Team = "Al-Nassr", PhotoPath = @"ronaldo.jpg", Position = "Forward", MatchesPlayed = 160, Goals = 130, Assists = 35, Nationality = "Swedish", Age = 42 },
                new() { Name = "Vinícius Júnior", Team = "Manchester City", PhotoPath = @"haaland.jpg", Position = "Forward", MatchesPlayed = 95, Goals = 70, Assists = 40, Nationality = "Brazilian", Age = 23 },
                new() { Name = "Luka Modrić", Team = "Inter Miami", PhotoPath = @"busquets.jpg", Position = "Midfielder", MatchesPlayed = 90, Goals = 8, Assists = 20, Nationality = "Croatian", Age = 38 },
                new() { Name = "Andrew Robertson", Team = "Inter Miami", PhotoPath = @"alba.jpg", Position = "Defender", MatchesPlayed = 125, Goals = 15, Assists = 50, Nationality = "Scottish", Age = 30 },
                new() { Name = "Antoine Griezmann", Team = "Al-Nassr", PhotoPath = @"mbappe.jpg", Position = "Forward", MatchesPlayed = 140, Goals = 95, Assists = 65, Nationality = "French", Age = 32 },
                new() { Name = "Paulo Dybala", Team = "Al-Nassr", PhotoPath = @"neymar.jpg", Position = "Forward", MatchesPlayed = 115, Goals = 75, Assists = 50, Nationality = "Argentinian", Age = 30 },
                new() { Name = "Toni Kroos", Team = "Manchester City", PhotoPath = @"debruyne.jpg", Position = "Midfielder", MatchesPlayed = 140, Goals = 45, Assists = 100, Nationality = "German", Age = 34 },
                new() { Name = "Jude Bellingham", Team = "Manchester City", PhotoPath = @"foden.jpg", Position = "Midfielder", MatchesPlayed = 85, Goals = 30, Assists = 35, Nationality = "English", Age = 21 },
                new() { Name = "Bukayo Saka", Team = "Manchester City", PhotoPath = @"grealish.jpg", Position = "Winger", MatchesPlayed = 100, Goals = 20, Assists = 45, Nationality = "English", Age = 22 },
                new() { Name = "João Félix", Team = "Manchester City", PhotoPath = @"bernardo.jpg", Position = "Midfielder", MatchesPlayed = 115, Goals = 35, Assists = 60, Nationality = "Portuguese", Age = 25 }
};
        }

        private void lstPlayers_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstPlayers.SelectedItem is Player selectedPlayer)
            {
                DisplayPlayerDetails(selectedPlayer);
            }
        }

        private void DisplayPlayerDetails(Player selectedPlayer)
        {
            // Update labels with player details
            lblName.Text = $"Name: {selectedPlayer.Name}";
            lblTeam.Text = $"Team: {selectedPlayer.Team}";
            lblPosition.Text = $"Position: {selectedPlayer.Position}";
            lblMatchesPlayed.Text = $"Matches Played: {selectedPlayer.MatchesPlayed}";
            lblGoals.Text = $"Goals: {selectedPlayer.Goals}";
            lblAssists.Text = $"Assists: {selectedPlayer.Assists}";
            lblNationality.Text = $"Nationality: {selectedPlayer.Nationality}";
            lblAge.Text = $"Age: {selectedPlayer.Age}";

            // Dynamic color for Goals and Assists based on thresholds
            lblGoals.ForeColor = selectedPlayer.Goals > 50 ? Color.Green : Color.Red;
            lblAssists.ForeColor = selectedPlayer.Assists > 30 ? Color.Green : Color.Red;

            // Check if the photo path is valid
            if (!string.IsNullOrEmpty(selectedPlayer.PhotoPath) && File.Exists(selectedPlayer.PhotoPath))
            {
                picPlayerPhoto.Image = Image.FromFile(selectedPlayer.PhotoPath);
            }
            else
            {
                picPlayerPhoto.Image = Image.FromFile("D:\\BOW VALLEY COLLEGE 3\\SODV 2101 Rapid Application Development\\assignments\\assignment3_tradingCards\\FootballTradingCards\\images/defaultImage.jpg"); // Set to default image if photo is not found
            }

            Color borderColor;
            Color backgroundColor; // Variable for background color

            switch (selectedPlayer.Team)
            {
                case "Inter Miami":
                    backgroundColor = Color.Blue;  
                    borderColor = Color.LightCyan;  
                    break;
                case "Al-Nassr":
                    backgroundColor = Color.Orange;  
                    borderColor = Color.LightYellow;  
                    break;
                case "Manchester City":
                    backgroundColor = Color.Gray;  
                    borderColor = Color.LightGray;   
                    break;
                default:
                    backgroundColor = Color.White; // Default background color
                    borderColor = Color.White; // Default border color
                    break;
            }

            // Apply background and border colors
            this.BackColor = backgroundColor;
            pnlPlayerDetails.BorderStyle = BorderStyle.FixedSingle;
            pnlPlayerDetails.BackColor = borderColor;

            pnlPlayerDetails.Visible = true;
            btnBack.Visible = true;
            lstPlayers.Visible = false;
            btnAddPlayer.Visible = false;
            btnRemovePlayer.Visible = true;
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            pnlPlayerDetails.Visible = false;
            btnBack.Visible = false;
            lstPlayers.Visible = true;
            btnAddPlayer.Visible = true;
            btnRemovePlayer.Visible = false;
        }

        private void btnRemovePlayer_Click(object sender, EventArgs e)
        {
            if (lstPlayers.SelectedItem is Player selectedPlayer)
            {
                var result = MessageBox.Show($"Are you sure you want to remove {selectedPlayer.Name}?", "Confirm Remove", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    // Remove player from the list
                    players.Remove(selectedPlayer);

                    ClearPlayerDetails();

                    // Rebind the ListBox to show the updated list of players
                    lstPlayers.DataSource = null;  
                    lstPlayers.DataSource = players.OrderBy(p => p.Name).ToList();  
                    lstPlayers.DisplayMember = "Name";  

                    pnlPlayerDetails.Visible = false;
                    btnBack.Visible = false;
                    lstPlayers.Visible = true;
                    btnAddPlayer.Visible = true;
                    btnRemovePlayer.Visible = false;

                    if (players.Count == 0)
                    {
                        MessageBox.Show("No players left to display.", "List Empty", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show($"{selectedPlayer.Name} has been removed.", "Player Removed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }
        private void ClearPlayerDetails()
        {
            lblName.Text = "Name";
            lblTeam.Text = "Team";
            lblPosition.Text = "Position";
            lblMatchesPlayed.Text = "Matches Played";
            lblGoals.Text = "Goals";
            lblAssists.Text = "Assists";
            lblNationality.Text = "Nationality";
            lblAge.Text = "Age";
            picPlayerPhoto.Image = null;  // Clear the image
        }


        private void btnBack_Click_1(object sender, EventArgs e)
        {
            pnlPlayerDetails.Visible = false;
            btnBack.Visible = false;

            lstPlayers.Visible = true;
            btnAddPlayer.Visible = true;

            btnRemovePlayer.Visible = false;

            Color BorderColor = SystemColors.Control; // Reset 

            btnBack.BackColor = SystemColors.Control;
            btnAddPlayer.BackColor = SystemColors.Control;
            btnRemovePlayer.BackColor = SystemColors.Control;
        }
        private void btnAddPlayer_Click(object sender, EventArgs e)
        {
            using (AddPlayerForm addPlayerForm = new AddPlayerForm())
            {
                if (addPlayerForm.ShowDialog() == DialogResult.OK)
                {
                    Player newPlayer = addPlayerForm.NewPlayer;
                    players.Add(newPlayer); // Add new player to the list

                    lstPlayers.DataSource = null;
                    lstPlayers.DataSource = players.OrderBy(p => p.Name).ToList(); 
                    lstPlayers.DisplayMember = "Name"; 

                    MessageBox.Show($"{newPlayer.Name} has been added.", "Player Added", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void picPlayerPhoto_Click(object sender, EventArgs e)
        {

        }
    }
    public class AddPlayerForm : Form
    {
        public Player NewPlayer { get; private set; }

        private TextBox txtName, txtTeam, txtPosition, txtGoals, txtAssists, txtNationality, txtAge, txtPhotoPath;
        private Button btnSave, btnCancel;

        public AddPlayerForm()
        {
            InitializeComponents();
        }

        private void InitializeComponents()
        {
            // Form Properties
            this.Text = "Add New Player";
            this.Size = new Size(300, 400);
            this.StartPosition = FormStartPosition.CenterParent;

            // Name Label and TextBox
            var lblName = new Label { Text = "Name", Location = new Point(10, 10), Width = 80 };
            txtName = new TextBox { Location = new Point(100, 10), Width = 150 };

            // Team Label and TextBox
            var lblTeam = new Label { Text = "Team", Location = new Point(10, 40), Width = 80 };
            txtTeam = new TextBox { Location = new Point(100, 40), Width = 150 };

            // Position Label and TextBox
            var lblPosition = new Label { Text = "Position", Location = new Point(10, 70), Width = 80 };
            txtPosition = new TextBox { Location = new Point(100, 70), Width = 150 };

            // Goals Label and TextBox
            var lblGoals = new Label { Text = "Goals", Location = new Point(10, 100), Width = 80 };
            txtGoals = new TextBox { Location = new Point(100, 100), Width = 150 };

            // Assists Label and TextBox
            var lblAssists = new Label { Text = "Assists", Location = new Point(10, 130), Width = 80 };
            txtAssists = new TextBox { Location = new Point(100, 130), Width = 150 };

            // Nationality Label and TextBox
            var lblNationality = new Label { Text = "Nationality", Location = new Point(10, 160), Width = 80 };
            txtNationality = new TextBox { Location = new Point(100, 160), Width = 150 };

            // Age Label and TextBox
            var lblAge = new Label { Text = "Age", Location = new Point(10, 190), Width = 80 };
            txtAge = new TextBox { Location = new Point(100, 190), Width = 150 };

            // Photo Path Label
            var lblPhotoPath = new Label { Text = "Photo Path", Location = new Point(10, 220), Width = 80 };
            txtPhotoPath = new TextBox { Location = new Point(100, 220), Width = 150 };

            // Save 
            btnSave = new Button { Text = "Save", Location = new Point(40, 260), Width = 80 };
            btnSave.Click += BtnSave_Click;

            
            btnCancel = new Button { Text = "Cancel", Location = new Point(140, 260), Width = 80 };
            btnCancel.Click += BtnCancel_Click;

            // Add controls to the form
            this.Controls.Add(lblName);
            this.Controls.Add(txtName);
            this.Controls.Add(lblTeam);
            this.Controls.Add(txtTeam);
            this.Controls.Add(lblPosition);
            this.Controls.Add(txtPosition);
            this.Controls.Add(lblGoals);
            this.Controls.Add(txtGoals);
            this.Controls.Add(lblAssists);
            this.Controls.Add(txtAssists);
            this.Controls.Add(lblNationality);
            this.Controls.Add(txtNationality);
            this.Controls.Add(lblAge);
            this.Controls.Add(txtAge);
            this.Controls.Add(lblPhotoPath);
            this.Controls.Add(txtPhotoPath);
            this.Controls.Add(btnSave);
            this.Controls.Add(btnCancel);
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            // Validate inputs
            if (string.IsNullOrWhiteSpace(txtName.Text) ||
                string.IsNullOrWhiteSpace(txtTeam.Text) ||
                string.IsNullOrWhiteSpace(txtPosition.Text) ||
                !int.TryParse(txtGoals.Text, out int goals) ||
                !int.TryParse(txtAssists.Text, out int assists) ||
                string.IsNullOrWhiteSpace(txtNationality.Text) ||
                !int.TryParse(txtAge.Text, out int age))
            {
                MessageBox.Show("Please fill in all fields correctly.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Create the new player
            NewPlayer = new Player
            {
                Name = txtName.Text,
                Team = txtTeam.Text,
                Position = txtPosition.Text,
                Goals = goals,
                Assists = assists,
                Nationality = txtNationality.Text,
                Age = age,
                PhotoPath = txtPhotoPath.Text
            };

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
