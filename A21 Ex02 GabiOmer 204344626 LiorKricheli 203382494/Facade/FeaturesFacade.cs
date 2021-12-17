﻿using FacebookWrapper.ObjectModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A21_Ex02_GabiOmer_204344626_LiorKricheli_203382494
{

    public class FeaturesFacade
    {

        private NearestFriends m_CloseFriends;

        private FriendAnalyzer m_FriendAnalyzer;

        private MatchMyFriends m_FriendMatcher;

        private User m_loggedUser;

        public FeaturesFacade(User i_UserLogged)
        {

            m_loggedUser = i_UserLogged;

        }


        public List<FriendByCoordinate> InitiateCloseFriends(int i_radius)
        {

            m_CloseFriends = new NearestFriends(m_loggedUser);

            return m_CloseFriends.NearestFriendsMeth(i_radius);

        }

        public double InitiateFriendAnalyzer(User i_user)
        {

            m_FriendAnalyzer = new FriendAnalyzer();

            return m_FriendAnalyzer.Analyze(i_user);

        }

        public List<User> InitiateFriendMatcher()
        {

            m_FriendMatcher = new MatchMyFriends(m_loggedUser);

            return m_FriendMatcher.MatchimgMyFriends();

        }

    }

}
