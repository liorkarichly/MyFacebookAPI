using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Facebook;
using FacebookWrapper;
using FacebookWrapper.ObjectModel;

namespace A21_Ex01_Gabi_204344626_Lior_203382494
{

    public partial class Login : Form
    {

        private User m_LoggedInUser;
        private LoginResult m_LoginResult;
        private Form1 m_ReloadFormMain;

        public Login()
        {

            InitializeComponent();
            FacebookService.s_CollectionLimit = 200;
            LoginToForm1();

        }

        private void LoginToForm1()
        {

            m_LoginResult = FacebookService.Login("747979639134063",
                    "public_profile",
                    "email",
                    "publish_to_groups",
                    "user_birthday",
                    "user_age_range",
                    "user_gender",
                    "user_link",
                    "user_tagged_places",
                    "user_videos",
                    "publish_to_groups",
                    "groups_access_member_info",
                    "user_friends",
                    "user_events",
                    "user_likes",
                    "user_location",
                    "user_photos",
                    "user_posts",
                    "user_hometown");

            if (!string.IsNullOrEmpty(m_LoginResult.AccessToken))
            {

                m_LoggedInUser = m_LoginResult.LoggedInUser;
                
            }
            else
            {

                MessageBox.Show(m_LoginResult.ErrorMessage);

            }
           
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            this.Hide();
            m_ReloadFormMain = new Form1(m_LoggedInUser, m_LoginResult);
            m_ReloadFormMain.ShowDialog();
            this.Visible = false;
            

        }

        protected override void OnShown(EventArgs e)
        {
            
            base.OnShown(e);
            Form1.s_AppSettingss = AppSettings.LoadFromFile();
          
            if (Form1.s_AppSettingss.RememberUser && !string.IsNullOrEmpty(Form1.s_AppSettingss.RecentAccessToken))
            {

                m_LoginResult = FacebookService.Connect(Form1.s_AppSettingss.RecentAccessToken);
                this.Hide();
                m_ReloadFormMain = new Form1(m_LoggedInUser, m_LoginResult);
                m_ReloadFormMain.ShowDialog();

            }

        }

        private void cbRememberMe_CheckedChanged(object sender, EventArgs e)
        {

            bool buttonRemmberMe = false;

            if (cbRememberMe.Checked)
            {

                Form1.s_AppSettingss.RememberUser = !buttonRemmberMe;
            
            }

        }

    }

}
