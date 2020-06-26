using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TaskManager.Domain.Models
{
    [Table("User")]
    public class User
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

        public string EncryptPassword()
        {
            this.Password = Convert.ToBase64String(Encoding.UTF8.GetBytes(this.Password));
            return this.Password;
        }
    }
}
