using Mapper;
using Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
   public class UserManager
    {
        public Users Login(string UserName, string Password)
        {
            UnitOfWork db = new UnitOfWork();
            try
            {
                Users user = db.UsersRepository.Get(users => (users.Email == UserName || users.PhoneNumber == UserName) && users.Password == Password).FirstOrDefault();
                return user;
            }
            finally
            {

                db.Dispose();
            }

        }
        public Users Register(Users users)
        {
            UnitOfWork db = new UnitOfWork();
            try
            {
               db.UsersRepository.Insert(users);
                if (!string.IsNullOrEmpty(users.Email))
                {
                    return db.UsersRepository.Get(x => x.Email == users.Email).FirstOrDefault();
                }
                else
                {
                    return db.UsersRepository.Get(x => x.PhoneNumber == users.PhoneNumber).FirstOrDefault();
                }

            }
            finally
            {

                db.Dispose();
            }

        }

        public Users UpdatePassword(string email,string phoneNumber,string password)
        {
            UnitOfWork db = new UnitOfWork();
            try
            {
                Users user = new Users();
                if (!string.IsNullOrEmpty(email))
                {
                    user = db.UsersRepository.Get(x => x.Email == email).FirstOrDefault();
                }
                else
                {
                    user = db.UsersRepository.Get(x => x.PhoneNumber == phoneNumber).FirstOrDefault();
                }
                if (user != null)
                {
                    db.UsersRepository.UpdateOnly(user, "PhoneNumber");
                }
                return user;
            }
            finally
            {

                db.Dispose();
            }

        }
        public bool IsValidCode(string code, string email, string phoneNumber) {
            UnitOfWork db = new UnitOfWork();
            try
            {
                Users user = new Users();
                if (!string.IsNullOrEmpty(email))
                {
                    user = db.UsersRepository.Get(x => x.Email == email).FirstOrDefault();
                }
                else
                {
                    user = db.UsersRepository.Get(x => x.PhoneNumber == phoneNumber).FirstOrDefault();
                }
                if (user.VerificationCode == code && user.VerificationCodeExpiry < DateTime.Now)
                {
                    return true;
                }
                return false;
            }
            finally
            {

                db.Dispose();
            }
        }
        public Users UpdateVarificationCode(string email, string phoneNumber, string password)
        {
            UnitOfWork db = new UnitOfWork();
            try
            {
                Users user = new Users();
                if (!string.IsNullOrEmpty(email))
                {
                    user = db.UsersRepository.Get(x => x.Email == email).FirstOrDefault();
                }
                else
                {
                    user = db.UsersRepository.Get(x => x.PhoneNumber == phoneNumber).FirstOrDefault();
                }
                if (user != null)
                {
                    Random generator = new Random();
                    string randomCode = generator.Next(0, 999999).ToString("D6");

                    user.VerificationCode = randomCode;
                    user.VerificationCodeExpiry = DateTime.Now.AddDays(1);
                    db.UsersRepository.UpdateOnly(user, "VerificationCode,VerificationCodeExpiry");
                }
                return user;
            }
            finally
            {

                db.Dispose();
            }

        }

    }
}
