﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FacebookWrapper;
using Facebook;
using FacebookWrapper.ObjectModel;
using System.Runtime.CompilerServices;


namespace A21_Ex01_Gabi_204344626_Lior_203382494
{

    public partial class Form1 : Form
    {

    public Form1(User i_UserLogged, LoginResult i_Login)
    {
            
        InitializeComponent();

        m_LoginResult = i_Login;
        m_LoggedInUser = i_UserLogged;
        r_Features = new Features(m_LoggedInUser);
        fetchLoggedInUser();

    }

    private User m_LoggedInUser;
    private LoginResult m_LoginResult;
    public static AppSettings s_AppSettingss;
    private readonly Features r_Features;

    private void fetchLoggedInUser()
    {

        this.Text = String.Format("logged in as " + m_LoggedInUser.FirstName + " " + m_LoggedInUser.LastName);
        pbProfilePicture.LoadAsync(m_LoggedInUser.PictureNormalURL);
        labelFullName.Text = m_LoggedInUser.Name;
        labelBirthday.Text = m_LoggedInUser.Birthday;
        labelHometown.Text = m_LoggedInUser.Location.Name;
                
        fetchPosts();
        fetchAlbums();
        featchFriendList();
        fetchNewsFeed();
        fetchFavoriteTeams();

    }

    protected override void OnShown(EventArgs e)
    {

        base.OnShown(e);
        tabControl1.Enabled = true;
        pbProfilePicture.Visible = true;

    }

    protected override void OnFormClosing(FormClosingEventArgs e)
    {

        base.OnFormClosing(e);

        s_AppSettingss.RecentWindowSize = this.Size;
        s_AppSettingss.RecentWindowLocation = this.Location;

        if (s_AppSettingss.RememberUser)
        {

            s_AppSettingss.RecentAccessToken = m_LoginResult.AccessToken;

        }
        else
        {

            s_AppSettingss.RecentAccessToken = null;

        }

        s_AppSettingss.SaveToFile();

        Application.ExitThread();
        
    }

    /// <summary>
    /// Posts
    /// </summary>
    private void fetchPosts()
    {

        listBoxPosts.Items.Clear();
            
        foreach (Post posts in m_LoggedInUser.Posts)
        {

            if (posts.Message != null)
            {

                listBoxPosts.Items.Add(posts.Message);

            }
            else if (posts.Caption != null)
            {

                listBoxPosts.Items.Add(posts.Caption);

            }
            else
            {

                listBoxPosts.Items.Add(string.Format("[{0}]", posts.Type));

            }
            if (m_LoggedInUser.Posts.Count == 0)
            {

                MessageBox.Show("No Posts to retrieve :(");

            }

        }

    }
    
    /// <summary>
    /// News feed
    /// </summary>
    private void fetchNewsFeed()
    {
          
        listBoxNewsFeed.Items.Clear();

        foreach (Post newsFeed in m_LoggedInUser.NewsFeed)
        {

            if (newsFeed.Message != null)
            {

                listBoxNewsFeed.Items.Add(newsFeed.Message);

            }
            else if (newsFeed.Caption != null)
            {

                listBoxNewsFeed.Items.Add(newsFeed.Caption);

            }
            else
            {

                listBoxNewsFeed.Items.Add(string.Format("[{0}]", newsFeed.Type));

            }

            if (m_LoggedInUser.Posts.Count == 0)
            {

                MessageBox.Show("No NewFeeds to retrieve :(");

            }

        }

    }

    private void displaySelectedPost()
    {

        if (listBoxPosts.SelectedItems.Count == 1) //chose a line in the list box
        {

            Post chosenPost = listBoxPosts.SelectedItem as Post;

            if (chosenPost.Comments != null)
            {

                listBoxComments.Items.Add(chosenPost.Comments);

            }
            else
            {

                listBoxComments.Items.Add("No Comments on this post");

            }

        }

    }

    private void listBoxPosts_SelectedIndexChanged_1(object sender, EventArgs e)
    {

            Post selected = m_LoggedInUser.Posts[listBoxPosts.SelectedIndex];
            listBoxComments.DisplayMember = "Message";
            listBoxComments.DataSource = selected.Comments;
            
    }

    /// <summary>
    /// Albums
    /// </summary>
    private void fetchAlbums()
    {

        listBoxAlbums.Items.Clear();
        listBoxAlbums.DisplayMember = "Name";

        foreach (Album album in m_LoggedInUser.Albums)
        {

            listBoxAlbums.Items.Add(album);
               
        }

        if (m_LoggedInUser.Albums.Count == 0)
        {

            MessageBox.Show("No Albums to retrieve :(");

        }

    }

        private void displaySelectedAlbum()
        {

            if(listBoxAlbums.SelectedItems.Count == 1) //chose a line in the list box
            {

                Album chosenAlbum = listBoxAlbums.SelectedItem as Album;

                if(chosenAlbum.PictureAlbumURL != null)
                {

                    pictureBoxAlbums.LoadAsync(chosenAlbum.PictureAlbumURL);

                }
                else
                {

                    pbProfilePicture.Image = pbProfilePicture.ErrorImage;

                }
                  
            }
        
        }

        private void listBoxAlbums_SelectedIndexChanged(object sender, EventArgs e)
        {

            displaySelectedAlbum();

        }

        /// <summary>
        /// Friends list
        /// </summary>
        private void featchFriendList()
        {

            labelCountOfFriend.Text = String.Format("({0})", m_LoggedInUser.Friends.Count);
            listBoxFriendsList.DisplayMember = "Name";

            foreach (User friend in m_LoggedInUser.Friends)
            {

                listBoxFriendsList.Items.Add(friend);
                
            }

        }

        private void displaySelectedFriend()
        {

            if (listBoxFriendsList.SelectedItems.Count == 1) //chose a line in the list box
            {
                User chosenFriend = listBoxFriendsList.SelectedItem as User;
                 
                if (chosenFriend != null)
                {

                    pictureBoxFriendProfile.LoadAsync(chosenFriend.PictureNormalURL);
                    labelNameOfFriend.Text = chosenFriend.Name;

                }
                else
                {

                    pictureBoxFriendProfile.Image = pictureBoxFriendProfile.ErrorImage;

                }

            }

        }

        private void listBoxFriendsList_SelectedIndexChanged(object sender, EventArgs e)
        {

            displaySelectedFriend();

        }

        private void buttonShowFirendList_Click(object sender, EventArgs e)
        {

            featchFriendList();

        }

        /// <summary>
        /// Logout
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btLogout_Click(object sender, EventArgs e)
        {

            FacebookService.Logout(new Action(loggedOutFinished));

        }

        private void loggedOutFinished()
        {

            s_AppSettingss = new AppSettings();
            s_AppSettingss.SaveToFile();
            MessageBox.Show("You are now logged out!");
            Application.ExitThread();

        }

        /// <summary>
        /// Create post
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonCreatePost_Click(object sender, EventArgs e)
        {

            try
            {

                if (!string.IsNullOrEmpty(textBoxStatus.Text))
                {

                    Status postedStatus = m_LoggedInUser.PostStatus(textBoxStatus.Text);
                    MessageBox.Show("Status posted successfully");

                }
                else
                {

                    textBoxStatus.Clear();

                }

            }
            catch
            {

                MessageBox.Show("No authorization from facebook");

            }

        }

        /// <summary>
        /// Favorite
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listBoxFavorite_SelectedIndexChanged_1(object sender, EventArgs e)
        {

            selectFavoriteTeam();

        }

        private void fetchFavoriteTeams()
        {

            listBoxFavorite.Items.Clear();
            listBoxFavorite.DisplayMember = "Name";
            try
            {


            }
            catch 
            {

                listBoxFavorite.Items.Add("No teams to retrieve: (");

            }
           
        }

        private void selectFavoriteTeam()
        {

            if (listBoxFavorite.SelectedItems.Count == 1)
            {


            }

        }

       /* Features*/

        private void buttonSortGender_Click(object sender, EventArgs e)
        {

            listBoxGenderFriend.Items.Clear();

            if (checkBoxMale.Checked)
            {

                checkBoxFemale.Enabled = false;

            }

            else if (checkBoxFemale.Checked)
            {

                checkBoxMale.Enabled = false;

            }

            else if (!checkBoxMale.Checked && !checkBoxFemale.Checked)
            {

                checkBoxFemale.Enabled = true;
                checkBoxMale.Enabled = true;
                listBoxGenderFriend.Items.Clear();

            }

            featchSortGenderFriend();

        }

        private void featchSortGenderFriend()
        {

            listBoxGenderFriend.DisplayMember = "Name";

            if (checkBoxMale.Checked)
            {

                List<User> maleList = r_Features.InitiateGenderSorter(User.eGender.male);

                if (maleList.Any())
                {

                    foreach (User user in maleList)
                    {

                        listBoxGenderFriend.Items.Add(user);

                    }

                }
                else
                {

                    listBoxGenderFriend.Items.Add("No male Friends :(");

                }

            }


            if (checkBoxFemale.Checked)
            {

                checkBoxMale.Enabled = false;
                List<User> femaleList = r_Features.InitiateGenderSorter(User.eGender.female);

                if (femaleList.Any())
                {

                    foreach (User user in femaleList)
                    {

                        listBoxGenderFriend.Items.Add(user.Name);

                    }

                }
                else
                {

                    listBoxGenderFriend.Items.Add("No female friends :(");

                }

            }

        }

        private void bNearestFriends_Click(object sender, EventArgs e)
        {

            lbClosestFriends.Items.Clear();
            featchNearesrFriend();

        }

        private void featchNearesrFriend()
        {

            List<NearestFriends.FriendByCoordinate> listOfNearesrFriend = r_Features.InitiateCloseFriends();

            if (listOfNearesrFriend == null)
            {

                lbClosestFriends.Items.Add("No Friends :(");

            }
            else if (listOfNearesrFriend != null)
            {

                foreach (NearestFriends.FriendByCoordinate item in listOfNearesrFriend)
                {

                    lbClosestFriends.Items.Add(item.m_Name);

                }

            }

        }

        /// <summary>
        /// Upload Picture
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonUploadPic_Click(object sender, EventArgs e)
        {

            fecthUploadPicture();

        }

        private void fecthUploadPicture()
        {

            string imageLocation = String.Empty;

                OpenFileDialog fileDialog = new OpenFileDialog();
                fileDialog.Filter = "jpg files(*.jpg)|*.jpg| PNG files(*.png)|*.png| All Files(*.*)|*.*";

            try
            {

                if (fileDialog.ShowDialog() == DialogResult.OK)
                {

                    imageLocation = fileDialog.FileName;
                    pictureBoxPostPic.ImageLocation = imageLocation;
                    m_LoggedInUser.PostPhoto(pictureBoxPostPic.ImageLocation);

                }

            }
            catch
            {

                MessageBox.Show("No authorization from facebook");

            }

        }

    }

}
