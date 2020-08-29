using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.RegularExpressions;

namespace ProjectCalculator.Core.Domain
{
    public class User
    {
        private static readonly Regex NameRegex = new Regex("^(?![_.-])(?!.*[_.-]{2})[a-zA-Z0-9._.-]+(?<![_.-])$");
        [Key]
        public Guid Id { get; protected set; }
        [Required]
        public string Email { get; protected set; }
        [Required]
        public byte[] Password { get; protected set; }
        public byte[] Salt { get; protected set; }
        [Required]
        public string FirstName { get; protected set; }
        public string LastName { get; protected set; }
        public string Role { get; protected set; }
        public DateTime CreatedAt { get; protected set; }
        public DateTime UpdatedAt { get; protected set; }

        public Token Token { get; set; }
        protected User()
        {
        }

        public User(Guid userId, string email, string firstName, string lastName, string role,
            byte[] password, byte[] salt)
        {
            Id = userId;
            SetEmail(email);
            SetFirstName(firstName);
            SetLastName(lastName);
            SetRole(role);
            SetPassword(password, salt);
            CreatedAt = DateTime.UtcNow;
        }

        public void SetFirstName(string firstName)
        {
            if (!NameRegex.IsMatch(firstName))
            {
                throw new Exception("Username is invalid.");
            }

            if (String.IsNullOrEmpty(firstName))
            {
                throw new Exception("Username is invalid.");
            }

            FirstName = firstName;
            UpdatedAt = DateTime.UtcNow;
        }



        public void SetLastName(string lastName)
        {
            if (!NameRegex.IsMatch(lastName))
            {
                throw new Exception("Username is invalid.");
            }

            if (String.IsNullOrEmpty(lastName))
            {
                throw new Exception("Username is invalid.");
            }

            LastName = lastName;
            UpdatedAt = DateTime.UtcNow;
        }

        public void SetEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new Exception("Email can not be empty.");
            }
            if (Email == email)
            {
                return;
            }

            Email = email.ToLowerInvariant();
            UpdatedAt = DateTime.UtcNow;
        }

        public void SetRole(string role)
        {
            if (string.IsNullOrWhiteSpace(role))
            {
                throw new Exception("Role can not be empty.");
            }
            if (Role == role)
            {
                return;
            }
            Role = role;
            UpdatedAt = DateTime.UtcNow;
        }

        public void SetPassword(byte[] password, byte[] salt)
        {
            if (password.Length == 0)
            {
                throw new Exception("Password can not be empty.");
            }
            if (salt.Length == 0)
            {
                throw new Exception("Salt can not be empty.");
            }
            if (password.Length < 4)
            {
                throw new Exception("Password Exception contain at least 4 characters.");
            }
            if (password.Length > 100)
            {
                throw new Exception("Password can not contain more than 100 characters.");
            }
            if (Password == password)
            {
                return;
            }
            Password = password;
            Salt = salt;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
