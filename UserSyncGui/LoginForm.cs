using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UserSyncGui
{
    public partial class LoginForm : Form
    {
        public string loginName;
        public string password;

        public LoginForm()
        {
            InitializeComponent();
        }

        public LoginForm(bool hidePW):this()
        {
            if (hidePW)
                this.textBoxPassword.UseSystemPasswordChar = true;
            else
                this.textBoxPassword.UseSystemPasswordChar = false;
        }

        public LoginForm(bool hidePW, String login)
            : this(hidePW)
        {
            if (login != null)
            {
                this.textBoxLoginName.Text = login;
            }
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            this.loginName = this.textBoxLoginName.Text;
            this.password = this.textBoxPassword.Text;
        }
    }
}
