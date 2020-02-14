using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AppApi.Data.Entities;
using AppApi.Models;
using AppApi.Helpers;
using AppApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AppApi.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private IUserService _userService;
        private IItemService _itemService;
        private IOrderService _orderService;

        public UsersController(IUserService userService, IOrderService orderService, IItemService itemService)
        {
            _orderService = orderService;
            _itemService = itemService;
            _userService = userService;
        }

        [NonAction]
        private bool GetAndVAlidateUser(out User user)
        {
            user = null;

             var claimsIdentity = this.User.Identity as ClaimsIdentity;
            var userIdStr = claimsIdentity.FindFirst(ClaimTypes.Name)?.Value;

            if (!int.TryParse(userIdStr, out var userId))
                return false;

            user = _userService.Get(userId);

            if (user == null)
                return false;

            return true;
        }


        [HttpGet]
        public IActionResult GetActiveUser()
        {
            if (!GetAndVAlidateUser(out var user))
                return BadRequest(new { message = "User not signed in" });

            return Ok(user.WithoutPassword());
        }

        [AllowAnonymous]
        [HttpPost("auth")]
        public IActionResult Auth([FromBody] AuthModel model)
        {
            var user = _userService.Auth(model.Name, model.Password);

            if (user == null)
                return BadRequest(new { message = "User not exists or password is incorrect" });

            return Ok(user);
        }


        [AllowAnonymous]
        [HttpPost("reg")]
        public IActionResult Register([FromBody] AuthModel model)
        {
            var user = _userService.RegAndAuth(model.Name, model.Password);

            if (user == null)
                return BadRequest(new { message = "User already exists" });

            return Ok(user);
        }

        [HttpPut("addItem/{itemId}")]
        public IActionResult AddItemToCart(int itemId)
        {
            if (!GetAndVAlidateUser(out var user))
                return BadRequest(new { message = "User not found" });

            Order currentOrder = user.Orders?.FirstOrDefault(o => o.Status == OrderStatus.New);
            if (currentOrder == null)
                currentOrder = _orderService.Create(user);

            var item = _itemService.Get(itemId);
            if (item == null)
                return BadRequest(new { message = "Item not found" });

            currentOrder.Cart.Add(item);

            return Ok(currentOrder);
        }


        [HttpDelete("delItem/{itemId}")]
        public IActionResult DeleteItemFromCart(int itemId)
        {
            if (!GetAndVAlidateUser(out var user))
                return BadRequest(new { message = "User not found" });

            Order currentOrder = user.Orders.FirstOrDefault(o => o.Status == OrderStatus.New);
            if (currentOrder == null)
                return Ok();

            currentOrder.Cart = currentOrder.Cart.AsQueryable().RemoveById(itemId).ToList(); //fix it later

            return Ok(currentOrder);
        }


        [HttpPost("pay/{orderId}")]
        public IActionResult PayOrder(int orderId, [FromBody] PaycheckModel paycheck)
        {
            if (!GetAndVAlidateUser(out var user))
                return BadRequest(new { message = "User not found" });

            var currentOrder = user.Orders?.SingleOrDefault(o => o.Id == orderId);
            if (currentOrder == null)
                return BadRequest(new { message = "This order does not belongs to current user" });

            currentOrder = _orderService.ProccedPayment(currentOrder, paycheck.FullName, paycheck.CardNumber);

            return Ok(currentOrder);
        }
    }
}