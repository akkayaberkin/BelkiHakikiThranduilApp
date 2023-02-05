using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using BelkiHakiki.API.Filters;
using BelkiHakiki.Core;
using BelkiHakiki.Core.DTOs;
using BelkiHakiki.Core.Services;

namespace BelkiHakiki.API.Controllers.Admin
{
    public class AdminUserController : CustomBaseController
    {

        private readonly IMapper _mapper;
        private readonly IUserService _service;

        public AdminUserController(IMapper mapper, IUserService userService)
        {
            _mapper = mapper;
            _service = userService;
        }
        // GET api/users
        [HttpGet]
        public async Task<IActionResult> All()
        {
            var users = await _service.GetAllAsync();
            var userDtos = _mapper.Map<List<UserDto>>(users.ToList());
            return CreateActionResult(CustomResponseDto<List<UserDto>>.Success(200, userDtos));
        }

        [HttpGet("{guid}")]
        public async Task<IActionResult> GetByGuid(Guid guid)
        {
            var users = await _service.GetByGuidAsync(guid);
            var userDto = _mapper.Map<UserDto>(users);
            return CreateActionResult(CustomResponseDto<UserDto>.Success(200, userDto));
        }
      
        // DELETE api/products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            var user = await _service.GetByIdAsync(id);
            await _service.RemoveAsync(user);
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }

        [HttpPut]
        public async Task<IActionResult> Update(UserUpdateDto userDto)
        {
            await _service.UpdateAsync(_mapper.Map<AppUsers>(userDto));

            return CreateActionResult(CustomResponseDto<UserUpdateDto>.Success(200, userDto));
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> Save(UserDto userDto)
        {
            var user = await _service.AddAsync(_mapper.Map<AppUsers>(userDto));
            var usersDto = _mapper.Map<UserDto>(user);
            return CreateActionResult(CustomResponseDto<UserDto>.Success(201, usersDto));
        }

       
    }
}
