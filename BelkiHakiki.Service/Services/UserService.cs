using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using BelkiHakiki.API.Helpers;
using BelkiHakiki.Core;
using BelkiHakiki.Core.DTOs;
using BelkiHakiki.Core.Repositories;
using BelkiHakiki.Core.Services;
using BelkiHakiki.Core.UnitOfWorks;
using BelkiHakiki.Repository;
using BelkiHakiki.Service.Exceptions;
using System.IdentityModel.Tokens.Jwt;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text;

namespace BelkiHakiki.Service.Services
{
    public class UserService : IUserService
    {

        private readonly IGenericRepository<AppUsers> _repository;
        private readonly IUnitOfWork _unitOfWork;
        public UserService(IGenericRepository<AppUsers> repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<AppUsers> AddAsync(AppUsers entity)
        {
            try
            {
                await _repository.AddAsync(entity);
                await _unitOfWork.CommitAsync();
                return entity;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public async Task<IEnumerable<AppUsers>> AddRangeAsync(IEnumerable<AppUsers> entities)
        {
            await _repository.AddRangeAsync(entities);
            await _unitOfWork.CommitAsync();
            return entities;
        }

        public async Task<bool> AnyAsync(Expression<Func<AppUsers, bool>> expression)
        {
            return await _repository.AnyAsync(expression);
        }

        public async Task<bool> CheckUser(AppUsers entity)
        {
            var hasUser = _repository.Where(o => o.Username == entity.Username || o.Password == entity.Password).Any();
            if (hasUser)
            {
                return true;
            }
            return false;

        }

        public async Task<IEnumerable<AppUsers>> GetAllAsync()
        {
            return await _repository.GetAll().ToListAsync();
        }

        public Task<AppUsers> GetByGuidAsync(Guid guid)
        {
            return _repository.Where(o => o.Guid == guid && o.InUse == true).FirstOrDefaultAsync();
        }
        public Task<AppUsers> GetByResetGuidAsync(Guid resetGuid)
        {
            return _repository.Where(o => o.ResetGuid == resetGuid).FirstOrDefaultAsync();
        }

        public async Task<AppUsers> GetByIdAsync(int id)
        {
            var hasUser = await _repository.GetByIdAsync(id);

            if (hasUser == null)
            {
                throw new NotFoundExcepiton($"{typeof(AppUsers).Name}({id}) not found");
            }
            return hasUser;
        }

        public AppUsers GetByUserName(string userName)
        {
            return _repository.Where(o => o.Username == userName && o.InUse == true).FirstOrDefault();
        }

        public async Task<AppUsers> GetUserInfo(AppUsers appUsers)
        {
            return await _repository.Where(o => o.Username == appUsers.Username).FirstOrDefaultAsync();
        }

        public async Task RemoveAsync(AppUsers entity)
        {

            _repository.Remove(entity);
            await _unitOfWork.CommitAsync();
        }

        public async Task RemoveRangeAsync(IEnumerable<AppUsers> entities)
        {
            _repository.RemoveRange(entities);
            await _unitOfWork.CommitAsync();
        }

        public async Task UpdateAsync(AppUsers entity)
        {
            _repository.Update(entity);
            await _unitOfWork.CommitAsync();
        }

        public IQueryable<AppUsers> Where(Expression<Func<AppUsers, bool>> expression)
        {
            return _repository.Where(expression);
        }
    }
}
