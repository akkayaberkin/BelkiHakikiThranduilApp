using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using BelkiHakiki.API.Filters;
using BelkiHakiki.API.Tools;
using BelkiHakiki.Core;
using BelkiHakiki.Core.DTOs;
using BelkiHakiki.Core.Services;
using System.IdentityModel.Tokens.Jwt;

namespace BelkiHakiki.API.Controllers
{

    //Authorize attribute ile bu sınıfı sadece yetkisi yani tokenı olan kişilerin girmesini söylüyorum.

    public class AuthController : CustomBaseController
    {
        private readonly IMapper _mapper;
        private readonly IUserService _service;

        public AuthController(IMapper mapper, IUserService userService)
        {
            _mapper = mapper;
            _service = userService;

        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Register(UserRegisterDto userDto)
        {
            var checkUserMap = _mapper.Map<AppUsers>(userDto);
            if (await _service.CheckUser(checkUserMap))
            {
                return CreateActionResult(CustomResponseDto<NoContentDto>.Fail(500, "Bu bilgilere sahip kullanıcı bulunmakta."));
            }
            var user = await _service.AddAsync(_mapper.Map<AppUsers>(userDto));
            var usersDto = _mapper.Map<UserRegisterDto>(user);
            return CreateActionResult(CustomResponseDto<UserRegisterDto>.Success(201, usersDto));
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> SignIn(UserSignInDto userDto)
        {
            AppUsers user = new()
            {
                Username = userDto.Username,
                Password = userDto.Password
            };


            if (!await _service.CheckUser(user))
            {
                return BadRequest("Kullanıcı Adı veya Şifre hatalı");
            }

            var userInfo = await _service.GetUserInfo(user);

            var token = JwtTokenGenerator.GenerateToken(userInfo);

            return CreateActionResult(CustomResponseDto<string>.Success(201, token));

        }
        [HttpPost("[action]")]
        public async Task<IActionResult> Logout()
        {
            return RedirectToAction("Index", "Home");
        }


    }
}