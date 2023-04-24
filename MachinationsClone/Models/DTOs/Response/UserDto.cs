using System;
using MachinationsClone.Models.Entities;

namespace MachinationsClone.Models.DTOs.Response
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Token { get; set; }
        
        public static UserDto FromUser(User user, string token)
        {
            return new UserDto
            {
                Id = user.Id,
                Username = user.Username,
                Token = token
            };
        }
    }
}