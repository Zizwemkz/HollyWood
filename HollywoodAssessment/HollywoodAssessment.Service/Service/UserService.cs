﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HollywoodAssessment.Common.Exceptions;
using HollywoodAssessment.Common.Interfaces;
using HollywoodAssessment.Data.Models;

namespace HollywoodAssessment.Service.Service
{
  public class UserService : IUserService
  {
    private HollywoodAssessmentDbContext _context;

    public UserService(HollywoodAssessmentDbContext context)
    {
      _context = context;
    }

    public User Authenticate(string username, string password)
    {
      if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        return null;

      var user = _context.User.SingleOrDefault(x => x.Username == username);

      // check if username exists
      if (user == null)
        return null;

      // check if password is correct
      if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
        return null;

      // authentication successful
      return user;
    }

    public IEnumerable<User> GetAll()
    {
      return _context.User;
    }

    public User GetById(int id)
    {
      return _context.User.Find(id);
    }

    public User Create(User user, string password)
    {
      // validation
      if (string.IsNullOrWhiteSpace(password))
      throw new HttpStatusCodeException(400, "Password is required");

      if (_context.User.Any(x => x.Username == user.Username))
      throw new HttpStatusCodeException(400, "Username \"" + user.Username + "\" is already taken");

      byte[] passwordHash, passwordSalt;
      CreatePasswordHash(password, out passwordHash, out passwordSalt);

      user.PasswordHash = passwordHash;
      user.PasswordSalt = passwordSalt;

      _context.User.Add(user);
      _context.SaveChanges();

      return user;
    }

    public void Update(User userParam, string password = null)
    {
      var user = _context.User.Find(userParam.Id);

      if (user == null)
      throw new HttpStatusCodeException(400, "User not found");

      if (userParam.Username != user.Username)
      {
        // username has changed so check if the new username is already taken
        if (_context.User.Any(x => x.Username == userParam.Username))
        throw new HttpStatusCodeException(400, "Username " + userParam.Username + " is already taken");
      }

      // update user properties
      user.FirstName = userParam.FirstName;
      user.LastName = userParam.LastName;
      user.Username = userParam.Username;

      // update password if it was entered
      if (!string.IsNullOrWhiteSpace(password))
      {
        byte[] passwordHash, passwordSalt;
        CreatePasswordHash(password, out passwordHash, out passwordSalt);

        user.PasswordHash = passwordHash;
        user.PasswordSalt = passwordSalt;
      }

      _context.User.Update(user);
      _context.SaveChanges();
    }

    public void Delete(int id)
    {
      var user = _context.User.Find(id);
      if (user != null)
      {
        _context.User.Remove(user);
        _context.SaveChanges();
      }
    }

    // private helper methods

    private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
    {
      if (password == null) throw new ArgumentNullException("password");
      if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");

      using (var hmac = new System.Security.Cryptography.HMACSHA512())
      {
        passwordSalt = hmac.Key;
        passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
      }
    }

    private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
    {
      if (password == null) throw new ArgumentNullException("password");
      if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");
      if (storedHash.Length != 64) throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");
      if (storedSalt.Length != 128) throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordHash");

      using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
      {
        var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        for (int i = 0; i < computedHash.Length; i++)
        {
          if (computedHash[i] != storedHash[i]) return false;
        }
      }

      return true;
    }
  }
}
