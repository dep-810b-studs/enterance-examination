using System;
using System.Collections.Generic;

namespace ObjectOrientedProgramming
{
    public interface ISocialNetwork
    {
        void addPerson(string name);
        void addConnection(string from, string to);
        List<string> getFriends(string from, int level);
    }

    public class SocialNetwork : ISocialNetwork
    {
        private readonly Dictionary<string, List<string>> _memberWithFriends = new ();

        public void addPerson(string name)
        {
            ValidateMemberName(name);
            if(_memberWithFriends.ContainsKey(name))
                throw new ArgumentException("Member with this key already added");
            
            _memberWithFriends.Add(name, new List<string>());
        }

        public void addConnection(string from, string to)
        {
            ValidateMemberName(from);
            ValidateMemberName(to);
            CheckMemberExisting(from);
            
            _memberWithFriends[from].Add(to);
        }

        public List<string> getFriends(string from, int level)
        {
            ValidateMemberName(from);
            CheckMemberExisting(from);
            
            switch (level)
            {
                case < 1:
                    throw new ArgumentException("Level must be positive not null number");
                case 1:
                    return _memberWithFriends[@from];
            }

            var friends = new List<string>();
            
            foreach (var friend in _memberWithFriends[from])
            {
                friends.Add(friend);
                friends.AddRange(getFriends(friend, level -1));
            }

            return friends;
        }

        private void ValidateMemberName(string memberName)
        {
            if(memberName is null or "")
                throw new ArgumentException("Member name is empty or null...");
        }

        private void CheckMemberExisting(string memberName)
        {
            if(!_memberWithFriends.ContainsKey(memberName))
                throw new ArgumentException("This member didnt found...");
        }
    }
}