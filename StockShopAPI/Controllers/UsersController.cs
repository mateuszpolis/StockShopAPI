using System;
using Microsoft.AspNetCore.Mvc;
using StockShopAPI.Data;
using StockShopAPI.Models;
using StockShopAPI.Models.Dto;

namespace StockShopAPI.Controllers
{
	// [Route("api/[controller]")]
	[Route("api/Users")]
	[ApiController]
	public class UsersController : ControllerBase
	{
		[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public ActionResult<IEnumerable<UserDTO>> GetUsers()
		{
			var userList = UserStore.userList;
			if (userList == null)
			{
				return NotFound();
			}
			return Ok(userList);
		}

		[HttpGet("{id:int}", Name = "GetUser")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public ActionResult<UserDTO> GetUser(int id)
		{
			if (id <= -1)
			{
				return BadRequest();
			}
			var user = UserStore.userList.FirstOrDefault(user => user.Id == id);
			if (user == null)
			{
				return NotFound();
			}
			return Ok(user);
		}

		[HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<UserDTO> CreateUser([FromBody]UserDTO userDTO)
		{
			if (userDTO == null)
			{
				return BadRequest(userDTO);
			}
			if (userDTO.Id > 0)
			{
				return StatusCode(StatusCodes.Status500InternalServerError);
			}
			userDTO.Id = UserStore.userList.OrderByDescending(user => user.Id).FirstOrDefault().Id + 1;
			UserStore.userList.Add(userDTO);
			return CreatedAtRoute("GetUser", new { id = userDTO.Id }, userDTO);
			// 1:09:29
		}
	}
}
