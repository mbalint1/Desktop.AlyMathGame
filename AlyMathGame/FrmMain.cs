using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AlyMathGame
{
    /*
     * 
     * Program Name:           Aly's Super Duper Math Game
     * Author:                 Michael Balint
     * Initial Implementation: 11/22/2014
     * 
     */

    public partial class frmMain : Form
    {
        Random random = new Random();
        int num1;
        int num2;

        public frmMain()
        {
            InitializeComponent();
            lblProblem.Font = new Font("serif", 12, FontStyle.Bold);
            lblProblem.Hide();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {

            txtAnswer.Focus();
            if (rdoAdd.Checked)
            {
                CreateAdditionProblem();
            }
            else
            {
                CreateSubractionProblem();
            }
        }

        private void btnEnter_Click(object sender, EventArgs e)
        {
            try
            {
                if (IsValidInput())
                {

                    if (CheckAnswer())
                    {
                        Form Correct = new FrmCorrect();
                        Correct.ShowDialog();
                    }
                    else
                    {
                        Form Incorrect = new FrmIncorrect();
                        Incorrect.ShowDialog();
                    }

                    txtAnswer.Clear();
                    btnStart.PerformClick();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n\n" +
                    ex.GetType().ToString() + "\n" +
                    ex.StackTrace, "Exception");
            }
        }


        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        public bool CheckAnswer()
        {
            int answer = Convert.ToInt32(txtAnswer.Text);
            if (rdoAdd.Checked && answer == num1 + num2)
            {
                return true;
            }

            if (rdoMultiplication.Checked && answer == num1 * num2)
            {
                return true;
            }

            else
            {
                return false;
            }
        }

        public void CreateAdditionProblem()
        {
            num1 = random.Next(10);
            num2 = random.Next(10);
            lblProblem.Text = num1 + " + " + num2.ToString();
            lblProblem.Show();
        }

        public void CreateSubractionProblem()
        {
            // To eliminate negative number answers
            do
            {
            num1 = random.Next(10);
            num2 = random.Next(10);
            }
            while (num1 <= num2);
            lblProblem.Text = num1 + " X " + num2;      
            lblProblem.Show();
        }

        public bool IsPresent(TextBox textbox)
        {
            if (textbox.Text == "")
            {
                MessageBox.Show("You did not provide an answer", "Entry Error");
                textbox.Focus();
                return false;
            }
            return true;
        }

        public bool IsInt(TextBox textbox)
        {
            int number = 0;
            if (Int32.TryParse(textbox.Text, out number))
            {
                return true;
            }
            else
            {
                MessageBox.Show("You must enter an integer!", "Entry Error");
                textbox.Focus();
                return false;
            }
        }

        public bool IsValidInput()
        {
            return
                IsPresent(txtAnswer) &&
                IsInt(txtAnswer);
        }
    }
}
