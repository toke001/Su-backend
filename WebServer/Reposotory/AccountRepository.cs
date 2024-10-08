﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Security.Claims;
using WebServer.Data;
using WebServer.Dtos;
using WebServer.Helpers;
using WebServer.Interfaces;
using WebServer.Models;

namespace WebServer.Reposotory
{
    public class AccountRepository : IAccount
    {
        private readonly WaterDbContext _context;
        private readonly DbSet<Account> _dbSet;
        private readonly DbSet<Account_Roles> _dbSetAcRoles;
        private readonly IHttpContextAccessor _httpContext;
        private readonly IConfiguration _configuration;

        public AccountRepository(
            WaterDbContext context,
            IHttpContextAccessor httpContext,
            IConfiguration configuration)
        {
            _context = context;
            _httpContext = httpContext;
            _dbSet = _context.Set<Account>();
            _configuration = configuration;
            _dbSetAcRoles = _context.Set<Account_Roles>();
        }
        public async Task<AccountSignInResponseDto> SignIn(AccountSignInRequestDto request)
        {
            try
            {
                if (request.Equals(null) || (String.IsNullOrEmpty(request.login) || String.IsNullOrEmpty(request.pwd)))
                {
                    throw new Exception("Данные не могут быть пустыми");
                }
                var usr = await _dbSet.FirstOrDefaultAsync(x => x.Login.ToLower() == request.login.ToLower());
                if (usr == null)
                {
                    throw new Exception("Неверный логин или пароль");
                }
                if (!PasswordHelper.VerifyPassword(usr.PasswordHash, request.pwd))
                {
                    throw new Exception("Неверный логин или пароль");
                }
                var secretKey = _configuration.GetSection("tokenParams").GetSection("symKey").Value;
                var validIssuer = _configuration.GetSection("tokenParams").GetSection("validIssuer").Value;
                var validAudience = _configuration.GetSection("tokenParams").GetSection("validAudience").Value;
                var claims = new List<System.Security.Claims.Claim>();
                claims.Add(new Claim("uid", usr.Id.ToString()));
                claims.Add(new Claim("kato", usr.KatoCode.ToString()));
                if (usr.Bin != null)
                {
                    claims.Add(new Claim("bin", usr.Bin.ToString()));
                }

                var token = new TokenHelper().GenerateToken(secretKey, validIssuer, validAudience, 540, claims.ToArray());

                return new AccountSignInResponseDto()
                {
                    token = token,
                    rem = request.rem,
                    login = usr.Login
                };

            }
            catch (Exception)
            {
                throw;
            }
        }
    
        //SingUp()
        //add-migraion initCreate
        //update-database
        public async Task<AccountSignUpResponseDto> SignUp(AccountSignUpRequestDto request)
        {            
            var oldAccount = await _dbSet.FirstOrDefaultAsync(x => x.Login.ToLower() == request.Login.ToLower());
            if (oldAccount != null) throw new Exception("Пользователь с таким логином уже существует");
            if (!_dbSet.Any(x => x.Id == request.AuthorId)) throw new Exception("Автора с таким Id не существует");

            //var salt = GenerateSalt();
            var saltedPassword = PasswordHelper.HashPassword(request.Password);

            var account = new Account
            {
                Login = request.Login,
                KatoCode = request.KatoCode,                  
                PasswordHash = saltedPassword,
                CreateDate = DateTime.UtcNow,
                Email = request.Email,
                AuthorId = request.AuthorId
            };
            await _dbSet.AddAsync(account);
            //await _context.SaveChangesAsync();

            var list = new List<Account_Roles>();

            foreach (var t in request.Roles)
            {
                var accountRole = new Account_Roles
                {
                    RoleId = t,
                    AccountId = account.Id,
                    CreateDate = DateTime.UtcNow
                };
                list.Add(accountRole);  
            }
            if (list.Count > 0)
            {
                await _dbSetAcRoles.AddRangeAsync(list);                    
            }
            await _context.SaveChangesAsync();
            return new AccountSignUpResponseDto()
            {
                Login = request.Login,
                Password = request.Password,
            };            
        }

        public async Task<object> GetUsers(AccountGetUsersRequestDto model)
        {
            var query = _dbSet.Where(x=>x.IsDel==false).AsQueryable();
            if(!string.IsNullOrEmpty(model.Login)) query = query.Where(x=>x.Login == model.Login);
            if(!string.IsNullOrEmpty(model.KatoCode)) query = query.Where(x=>x.KatoCode.ToString() == model.KatoCode);
            if(!string.IsNullOrEmpty(model.Email)) query = query.Where(x=>x.Email == model.Email);
            if (model.RoleId.HasValue)
            {

                query = from q in query
                        join r in _dbSetAcRoles on q.Id equals r.AccountId
                        where r.RoleId == model.RoleId.Value
                        select q;
            }

            return await (from q in query                                                
                        select new
                        {
                            q.Id,
                            q.Login,
                            q.KatoCode,
                            q.Email,
                            Roles = _dbSetAcRoles.Where(x=>x.AccountId==q.Id).ToList()
                        }).ToListAsync();

            //return await query.Select(x => new {x.Id, x.Login, x.KatoCode, x.Email, }).ToListAsync();
        }
    }
}
