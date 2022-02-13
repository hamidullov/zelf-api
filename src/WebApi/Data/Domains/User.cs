using System.Collections.Generic;
using System.Linq;
using WebApi.Exceptions;

namespace WebApi.Data.Domains
{
    /// <summary>
    /// Пользователь
    /// </summary>
    public class User
    {
        protected User(){}
        
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="name">Имя пользователя</param>
        public User(string name) : this(0, name)
        {
        }
        
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="id">Ид</param>
        /// <param name="name">Имя пользователя</param>
        public User(int id, string name)
        {
            Id = id;
            Name = name;
            Subscriptions = new List<User>();
            Followers = new List<User>();
        }
        
        /// <summary>
        /// Ид
        /// </summary>
        public int Id { get; private set; }
        
        /// <summary>
        /// Имя пользователя
        /// </summary>
        public string Name { get; private set; }
        
        /// <summary>
        /// Подписчики
        /// </summary>
        public List<User> Followers { get; private set; }
        
        /// <summary>
        /// Подписки
        /// </summary>
        public List<User> Subscriptions { get; private set; }

        
        /// <summary>
        /// Подписаться
        /// </summary>
        /// <param name="subUser">Пользователь</param>
        public void Subscribe(User subUser)
        {
            if(Id == subUser.Id)
                throw new DomainException($"Equals users [{subUser.Id}]");
            
            if (Subscriptions.Any(x => x.Id == subUser.Id))
                throw new DomainException($"The user [{subUser.Id}] is already in the list of subscriptions");
            
            Subscriptions.Add(subUser);
        }
    }
}