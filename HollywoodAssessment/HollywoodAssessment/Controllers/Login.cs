using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using HollywoodAssessment.Common.Helper;
using HollywoodAssessment.Common.Interfaces;
using HollywoodAssessment.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HollywoodAssessment.API.Controllers
{
  [Route("api/[controller]")]
  public class Login : Controller
  {
    private readonly IUserService _userService;
    private readonly appSettings _appSettings;

    public Login(
      IUserService userService, IOptions<appSettings> appSettings)
    {
      _userService = userService;
      _appSettings = appSettings.Value;
    }

    [AllowAnonymous]
    [HttpPost]
    public IActionResult Post([FromBody]UserDto userDto)
    {
      var user = _userService.Authenticate(userDto.Username, userDto.Password);

      var tokenHandler = new JwtSecurityTokenHandler();
      var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
      var tokenDescriptor = new SecurityTokenDescriptor
      {
        Subject = new ClaimsIdentity(new Claim[]
        {
          new Claim(ClaimTypes.Name, user.Id.ToString()),
          new Claim(ClaimTypes.Role, user.Role) 
        }),
        Expires = DateTime.UtcNow.AddDays(7),
        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
      };
      var token = tokenHandler.CreateToken(tokenDescriptor);
      var tokenString = tokenHandler.WriteToken(token);

      // return basic user info (without password) and token to store client side
      return Ok(new
      {
        Id = user.Id,
        Username = user.Username,
        FirstName = user.FirstName,
        LastName = user.LastName,
        Token = tokenString
      });
    }
  }
}
