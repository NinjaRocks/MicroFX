using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using MicroFx;
using MicroFx.Data;
using MicroFx.Data.Uow;
using MicroService.Library.Domain;
using MicroService.Library.v1.Contracts;
using ValidationException = System.ComponentModel.DataAnnotations.ValidationException;

namespace MicroService.Library.v1.Impl
{
    public class UserResource : IUserResource
    {
        private readonly IRepository<User> repository;

        public UserResource(IRepository<User> repository)
        {
            this.repository = repository;
        }

        public UserDocument Get(int bookId)
        {
            var book = repository.Get(bookId);

            if (book == null)
                throw new ResourceNotFoundException("User not Found");

            return Mapper.Map<UserDocument>(book);
        }

        [Transaction]
        public UserDocument Update(int userId, UserDocument document)
        {
            var user = repository.Get(userId);

            if (user == null)
                throw new ResourceNotFoundException("er not Found");

            if (document == null)
                throw new ValidationException("request was empty");

            user.Email = document.Email;
            user.Password = document.Password;
            repository.Update(user);

            return Mapper.Map<UserDocument>(user);
        }

        [Transaction]
        public void Delete(int userId)
        {
            var user = repository.Get(userId);

            if (user == null)
                throw new ResourceNotFoundException("user not Found");

            repository.Delete(user);
        }

        [Transaction]
        public UserDocument Post(UserDocument document)
        {
            if (document == null) throw new ValidationException("request was empty");

            var user = new User
            {
                Email = document.Email,
                Password = document.Password
            };

            repository.Save(user);

            return Mapper.Map<UserDocument>(user);
        }

        public IEnumerable<UserDocument> Get(string email, string password)
        {
            var books = repository.Query().Where(x=> x.Email == email && x.Password== password);
            return Mapper.Map<UserDocument[]>(books);
        }
    }
}