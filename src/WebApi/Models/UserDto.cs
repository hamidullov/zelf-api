using System.Collections.Generic;

namespace WebApi.Models
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        
        public List<UserDto> Followers { get; set; }
    }
}