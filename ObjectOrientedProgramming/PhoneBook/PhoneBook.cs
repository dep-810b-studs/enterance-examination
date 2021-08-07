using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace ObjectOrientedProgramming
{
    public interface IPhoneBook
    {
        void addPerson(string name, string phone);
        string searchByName(string name);
        string searchByPhone(string phone);
        List<string> getAllPersons();
    }
    
    public class PhoneBook : IPhoneBook
    {
        private readonly Dictionary<string, string> _phonesByNumbers = new (StringComparer.OrdinalIgnoreCase);
        
        public void addPerson(string name, string phone)
        {
            ValidateName(name);
            ValidatePhoneNumber(phone);

            _phonesByNumbers.Add(name, phone);
        }

        public string searchByName(string name)
        {
            ValidateName(name);
            CheckNameExisting(name);
            
            return _phonesByNumbers[name];
        }

        public string searchByPhone(string phone)
        {
            ValidatePhoneNumber(phone);

            var needRecord = _phonesByNumbers
                .FirstOrDefault(person => person.Value == phone);

            if (needRecord is ("", ""))
                throw new ArgumentException($"There is no person with phone {phone} in phone book");

            return needRecord.Key;
        }

        public List<string> getAllPersons()
        {
            var persons = _phonesByNumbers
                .Select(person => person.Key);

            return persons.ToList();
        }

        private void ValidateName(string name)
        {
            if (name == string.Empty)
                throw new ArgumentException($"Name couldn't be empty");
        }
        
        private void ValidatePhoneNumber(string phoneNumber)
        {
            var match = Regex.Match(phoneNumber, @"((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}");
            if (!match.Success)
                throw new ArgumentException($"Phone number {phoneNumber} is incorrect");
        }

        private void CheckNameExisting(string name)
        {
            if (_phonesByNumbers.ContainsKey(name))
                throw new ArgumentException($"There is no name {name} in phone book");
        }
    }
}