using BC.Data;
using BC.Data.Context;
using Blog.Core;
using System;
using System.Text;

namespace BC.Models
{
    public interface IUserService
    {
        bool Login(string username, string password, ref string error);
        void Create(string username, string email, string password);
    }

    public class UserService : IUserService
    {
        private IRepository<BC_User> _genericRepository = new Repository<BC_User>(new DBDataContext());

        public bool Login(string username, string password, ref string error)
        {
            BC_User user;

            try {
                user = _genericRepository.First(p => p.Username == username);
            }
            catch
            {
                error = "Invalid username/password!";
                return false;
            }

            if (!Security.ComparePasswords(user.Password.ToArray(), password, user.PasswordSalt.ToArray()))
            {
                error = "Invalid username/password!";
                return false;
            }

            return true;
        }

        public void Create(string username, string email, string password)
        {
            BC_User user = new BC_User();
            var salt = Guid.NewGuid().ToString();

            user.Email = email;
            user.Username = username;
            user.Password = Security.GenerateSaltedHash(password, salt);
            user.PasswordSalt = Encoding.UTF8.GetBytes(salt);

            _genericRepository.Insert(user);
            _genericRepository.SaveAll();
        }
    }
}