using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Project.Model;
using Project.Serializer;

namespace Project.Repository
{
    public class UserRepository
    {

        private const string FilePath = "../../../Resources/Data/users.csv";

        private readonly Serializer<User> serializer;

        private List<User> users;

        public UserRepository()
        {
            serializer = new Serializer<User>();
            users = serializer.FromCSV(FilePath);
        }

        public User GetByUsername(string username)
        {
            users = serializer.FromCSV(FilePath);
            return users.FirstOrDefault(u => u.Username == username);
        }

        public User GetById(int id)
        {
            users = serializer.FromCSV(FilePath);
            return users.Find(v => v.Id == id);
        }

    }
}
