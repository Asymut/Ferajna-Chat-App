using MySql.Data.MySqlClient;
using System.Data;
using System.Windows.Forms;

namespace Ferajna
{


    public partial class Logowanie : Form
    {

        string connectionString = "server=localhost;port=3306;database=ferajna;uid=root;password=;";

        public Logowanie()
        {
            InitializeComponent();

        }

        Color new_color = Color.FromArgb(252, 48, 99);
        Color basic_color = Color.FromArgb(27, 31, 48);

        private void btnLog_Click(object sender, EventArgs e)
        {
            panelLog.BringToFront();
            panel2.BackColor = basic_color;
            panel1.BackColor = new_color;
        }

        private void bntRej_Click(object sender, EventArgs e)
        {
            panelReg.BringToFront();
            panel1.BackColor = basic_color;
            panel2.BackColor = new_color;
        }

        private void btnZajerestruj_Click(object sender, EventArgs e)
        {
            MySqlConnection connection = new MySqlConnection(connectionString);

          #region walidacja_rejestracji

            ErrorProvider errorProvider1 = new ErrorProvider();

            if (string.IsNullOrEmpty(imieTextBox.Text.Trim()))
            {
                errorProvider1.SetError(imieTextBox, "Pole imiê jest wymagane!");
                return;
            }
            else
                errorProvider1.SetError(imieTextBox, string.Empty);

            if (imieTextBox.Text.Trim().Length < 3)
            {
                errorProvider1.SetError(imieTextBox, "Pole imie musi mieæ minimum 3 znaki!");
                return;
            }
            else
                errorProvider1.SetError(imieTextBox, string.Empty);


            if (string.IsNullOrEmpty(nazwiskoTextBox.Text.Trim()))
            {
                errorProvider1.SetError(nazwiskoTextBox, "Pole nazwisko jest wymagane!");
                return;
            }
            else
                errorProvider1.SetError(nazwiskoTextBox, string.Empty);

            if (nazwiskoTextBox.Text.Trim().Length < 3)
            {
                errorProvider1.SetError(nazwiskoTextBox, "Pole nazwisko musi mieæ minimum 3 znaki!");
                return;
            }
            else
                errorProvider1.SetError(nazwiskoTextBox, string.Empty);


            if (string.IsNullOrEmpty(loginTextBox.Text.Trim()))
            {
                errorProvider1.SetError(loginTextBox, "Pole login jest wymagane!");
                return;
            }
            else
                errorProvider1.SetError(loginTextBox, string.Empty);

            if (loginTextBox.Text.Trim().Length < 3)
            {
                errorProvider1.SetError(loginTextBox, "Pole login musi mieæ minimum 3 znaki!");
                return;
            }
            else
                errorProvider1.SetError(loginTextBox, string.Empty);

            try
            {
                string q2 = "SELECT COUNT(*) FROM uzytkownicy WHERE login = @login";
                MySqlCommand command2 = new MySqlCommand(q2, connection);
                command2.Parameters.AddWithValue("@login", loginTextBox.Text);

                connection.Open();
                long lWystapien = (long)command2.ExecuteScalar();
                connection.Close();

                if (lWystapien > 0)
                {
                    errorProvider1.SetError(loginTextBox, "Ten login (nickname) ju¿ istnieje - Wybierz inny.");
                    return;
                }
                else
                {
                    errorProvider1.SetError(loginTextBox, string.Empty);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Wyst¹pi³ b³¹d podczas sprawdzania loginu w bazie danych: " + ex.Message, "B³¹d", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }



            if (string.IsNullOrEmpty(emailTextBox.Text.Trim()))
            {
                errorProvider1.SetError(emailTextBox, "Pole email jest wymagane!");
                return;
            }  
            else if (emailTextBox.Text.Trim().Length < 3)
            {
                errorProvider1.SetError(emailTextBox, "Pole email musi mieæ minimum 3 znaki!");
                return;
            }
            else if (!emailTextBox.Text.Trim().Contains("@"))
            {
                errorProvider1.SetError(emailTextBox, "Pole email musi zawieraæ znak @.");
                return;
            }
            else
                errorProvider1.SetError(emailTextBox, string.Empty);



            if (string.IsNullOrEmpty(hasloTextBox.Text.Trim()))
            {
                errorProvider1.SetError(hasloTextBox, "Pole haslo jest wymagane!");
                return;
            }
            else if (hasloTextBox.Text.Trim().Length < 6 || !hasloTextBox.Text.Any(char.IsUpper) || !hasloTextBox.Text.Any(char.IsDigit))
            {
                errorProvider1.SetError(hasloTextBox, "Has³o musi zawieraæ co najmniej 6 znaków, przynajmniej jedn¹ wielk¹ literê i przynajmniej jedn¹ cyfrê.");
                return;
            }
            else
                errorProvider1.SetError(hasloTextBox, string.Empty);


            if (string.IsNullOrEmpty(nrtelTextBox.Text.Trim()))
            {
                errorProvider1.SetError(nrtelTextBox, "Pole numeru telefonu jest wymagane!");
                return;
            }
            else if (nrtelTextBox.Text.Trim().Length != 9 || !int.TryParse(nrtelTextBox.Text.Trim(), out _))
            {
                errorProvider1.SetError(nrtelTextBox, "Numer telefonu musi sk³adaæ siê z 9 cyfr!");
                return;
            }
            else
                errorProvider1.SetError(nrtelTextBox, string.Empty);


          
            #endregion

            string q1 = "INSERT INTO uzytkownicy (imie, nazwisko, login, email, haslo, nr_tel, typ_konta, ban)VALUES (@imie, @nazwisko, @login, @email, @haslo, @nr_tel, 1, 0)";

            MySqlCommand command1 = new MySqlCommand(q1, connection);
                command1.Parameters.AddWithValue("@imie", imieTextBox.Text);
                command1.Parameters.AddWithValue("@nazwisko", nazwiskoTextBox.Text);
                command1.Parameters.AddWithValue("@login", loginTextBox.Text );
                command1.Parameters.AddWithValue("@email", emailTextBox.Text );
                command1.Parameters.AddWithValue("@haslo", hasloTextBox.Text );
                command1.Parameters.AddWithValue("@nr_tel", nrtelTextBox.Text );

            try
            {
                connection.Open();
                command1.ExecuteNonQuery();
                MessageBox.Show("Uda³o siê zarejestrowaæ konto!", "Hurra!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                imieTextBox.Clear();
                nazwiskoTextBox.Clear();
                loginTextBox.Clear();
                emailTextBox.Clear();
                hasloTextBox.Clear();
                nrtelTextBox.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Wyst¹pi³ b³¹d: " + ex.Message, "B³¹d", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                connection.Close();
            }
        }

        private void btnLoguj_Click(object sender, EventArgs e)
        {

            #region walidacja_logowania

            ErrorProvider errorProvider2 = new ErrorProvider();

            if (string.IsNullOrEmpty(loginTextLOG.Text.Trim()))
            {
                errorProvider2.SetError(loginTextLOG, "Pole login jest wymagane!");
                return;
            }
            else
                errorProvider2.SetError(loginTextLOG, string.Empty);

            if (loginTextLOG.Text.Trim().Length < 3)
            {
                errorProvider2.SetError(loginTextLOG, "Pole login musi mieæ minimum 3 znaki!");
                return;
            }
            else
                errorProvider2.SetError(loginTextLOG, string.Empty);


            if (string.IsNullOrEmpty(hasloTextLog.Text.Trim()))
            {
                errorProvider2.SetError(hasloTextLog, "Pole haslo jest wymagane!");
                return;
            }
            else
                errorProvider2.SetError(hasloTextLog, string.Empty);



            #endregion

            MySqlConnection connection = new MySqlConnection(connectionString);

            string q1 = "SELECT * FROM uzytkownicy WHERE login = @login AND haslo = @haslo";

            MySqlCommand command1 = new MySqlCommand(q1, connection);
            command1.Parameters.AddWithValue("@login", loginTextLOG.Text);
            command1.Parameters.AddWithValue("@haslo", hasloTextLog.Text);

            try
            {
                connection.Open();
                MySqlDataReader reader = command1.ExecuteReader();

                if (reader.Read()) 
                {
                    Form2 form2 = new Form2();
                    form2.loginName = loginTextLOG.Text;
                    form2.Show();
                    this.Hide(); //ukrywa ten formularz!
                }
                else
                {
                    MessageBox.Show("Podane dane s¹ nieprawid³owe.", "SprawdŸ dane!", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Wyst¹pi³ b³¹d podczas logowania: " + ex.Message, "B³¹d", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                connection.Close();
            }


        }
    }
}