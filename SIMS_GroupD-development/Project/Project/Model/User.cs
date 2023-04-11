using System;
using System.Collections.Generic;
using System.Windows.Documents;
using Project.Serializer;

namespace Project.Model
{

    public enum Role { OWNER, GUEST1, GUEST2, GUIDE }
    public class User : ISerializable
    {

        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public Role Role { get; set; }

        public User()
        {

        }

        public User(string username, string password, Role role)
        {
            Username = username;
            Password = password;
            Role = role;

        }

        public User(User u)
        {
            Id = u.Id;
            Username = u.Username;
            Password = u.Password;
            Role = u.Role;

        }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), Username, Password, RoleToString() };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Username = values[1];
            Password = values[2];
            Role = StringToRole(values[3]);
        }

        private string RoleToString()
        {
            if (Role == Role.OWNER)
            {
                return "OWNER";
            }
            else if (Role == Role.GUIDE)
            {
                return "GUIDE";
            }
            else if (Role == Role.GUEST1)
            {
                return "GUEST1";
            }
            else
                return "GUEST2";

        }

        private Role StringToRole(string str)
        {
            if (str == "OWNER")
            {
                return Role.OWNER;
            }
            else if (str == "GUEST1")
            {
                return Role.GUEST1;
            }
            else if (str == "GUEST2")
            {
                return Role.GUEST2;

            }
            else
                return Role.GUIDE;
        }
    }
}
