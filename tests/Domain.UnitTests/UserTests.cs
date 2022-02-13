using System;
using Shouldly;
using WebApi.Data.Domains;
using WebApi.Exceptions;
using Xunit;

namespace Domain.UnitTests
{
    public class UserTests
    {
        
        [Fact]
        public void AddSubscriber_success()
        {
            var user1 = new User(1,"User1");
            var user2 = new User(2, "User2");
            var user3 = new User(3, "User3");
            
            user1.Subscribe(user2);
            user1.Subscribe(user3);
            
            user1.Subscriptions.Count.ShouldBe(2);
        }
        
        [Fact]
        public void AddSubscriber_some_user_exception()
        {
            var user1 = new User(1,"User1");
           
            Should.Throw<DomainException>(() =>
            {
                user1.Subscribe(user1);
            });
            
        }
        [Fact]
        public void AddSubscriber_duplicate_user()
        {
            var user1 = new User(1,"User1");
            var user2 = new User(2, "User2");
            user1.Subscribe(user2);
           
            Should.Throw<DomainException>(() =>
            {
                user1.Subscribe(user2);
            });
            
        }
    }
}