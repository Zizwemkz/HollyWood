using System;
using System.Collections.Generic;

using AutoMapper;
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
  public class UserController : Controller
  {
    private readonly IUserService _userService;
    private IMapper _mapper;

    public UserController(
        IUserService userService,
        IMapper mapper)
    {
      _userService = userService;
      _mapper = mapper;
    }

    [AllowAnonymous]
    [HttpPost]
    public ActionResult Post([FromBody]UserDto userDto)
    {
      // map dto to entity
      var user = _mapper.Map<User>(userDto);

        _userService.Create(user, userDto.Password);
        return Ok();
    }

    [HttpGet]
    public ActionResult Get()
    {
      var users = _userService.GetAll();
      var userDtos = _mapper.Map<IList<UserDto>>(users);
      return Ok(userDtos);
    }

    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
      var user = _userService.GetById(id);
      var userDto = _mapper.Map<UserDto>(user);
      return Ok(userDto);
    }

    [HttpPut("{id}")]
    public ActionResult Put(int id, [FromBody]UserDto userDto)
    {
      var user = _mapper.Map<User>(userDto);
      user.Id = id;

       _userService.Update(user, userDto.Password);
       return Ok();
    }

    [HttpDelete("{id}")]
    public ActionResult Delete(int id)
    {
      _userService.Delete(id);
      return NoContent();
    }
  }
}
