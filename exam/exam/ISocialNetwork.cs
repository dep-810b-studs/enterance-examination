using System;
using System.Collections.Generic;

namespace exam
{
    public interface ISocialNetwork
    {
        void addPerson(string name);
        void addConnection(string from, string to);
        List<string> getFriends(string from, int level);
    }

    public class SocialNetwork : ISocialNetwork
    {
        private Dictionary<string, List<string>> _memberWithFriends;

        public SocialNetwork()
        {
            _memberWithFriends = new Dictionary<string, List<string>>();
        }
        
        public void addPerson(string name)
        {
            if(name is null || name == string.Empty)
                throw new ArgumentException("Name is empty or null...");
            
            if(_memberWithFriends.ContainsKey(name))
                throw new ArgumentException("Member with this key already added");
            
            _memberWithFriends.Add(name, new List<string>());
        }

        public void addConnection(string from, string to)
        {
            if(from is null || from == string.Empty)
                throw new ArgumentException("From is empty or null...");
            
            if(to is null || to == string.Empty)
                throw new ArgumentException("To is empty or null...");

            if(!_memberWithFriends.ContainsKey(from))
                throw new ArgumentException("This member didnt found...");
            
            _memberWithFriends[from].Add(to);
        }

        public List<string> getFriends(string from, int level)
        {
            if(from is null || from == string.Empty)
                throw new ArgumentException("From is empty or null...");
            
            if(!_memberWithFriends.ContainsKey(from))
                throw new ArgumentException("This member didnt found...");
            
            if(level < 1)
                throw new ArgumentException("Level must be positive not null number");

            if (level == 1)
                return _memberWithFriends[from];

            var friends = new List<string>();
            
            foreach (var friend in _memberWithFriends[from])
            {
                friends.Add(friend);
                friends.AddRange(getFriends(friend, level -1));
            }

            return friends;
        }
    }
}