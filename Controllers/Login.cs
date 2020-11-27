using ApiTarea.Models;
using ApiTarea.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace ApiTarea.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly BookService _bookService;
        readonly ITokenService tokenService;

        public LoginController(BookService bookService, ITokenService tokenService)
        {
            _bookService = bookService;
            this.tokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService));
        }

        [HttpPost]
        public ActionResult<User> Create(User book)
        {
            if (book.Username == null)
            {
                return NotFound();
            }

            var user = _bookService.GetByName(book.Username);

            if (user == null)
            {
                return NotFound();
            }

            if(user.password == book.password)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.Role, user.role)
                };
                var accessToken = tokenService.GenerateAccessToken(claims);
                var refreshToken = tokenService.GenerateRefreshToken();
                user.RefreshToken = refreshToken;
                user.RefreshTokenExpiryTime = DateTime.Now.AddDays(7);
                _bookService.Update(user.Id, user);
                return Ok(new
                {
                    Token = accessToken,
                    RefreshToken = refreshToken
                });
            }


            _bookService.Create(book);

            return CreatedAtRoute("GetBook", new { id = book.Id.ToString() }, book);
        }
        
    }
}