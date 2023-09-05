using AutoMapper;
using BLLayer.Models.DTO;
using BLLayer.Responce;
using BLLayer.Services.Abstractions;
using FinalProject.Entities;
using FinalProject.Models.DTO;
using FinalProject.Services.Abstractions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinalProject.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
       public IAccountService _AccountService { get;}
      
       private readonly IUnitOfWork _UnitOfWork;

        public UserController(IAccountService accountService) 
        {
            _AccountService = accountService;
        }

        [HttpPost]
        public async Task<GenericResponceModel<IdentityUser>> Registration(RegisterDTO user, string roleName)
        {
            var responce = new GenericResponceModel<IdentityUser>();
            try
            {
                IdentityResult result = await _AccountService.AddUser(user, user.Password);
                if (result.Succeeded)
                {
                    var roleAddResult = await _AccountService.AddRole(user, roleName);
                    responce.Success(roleAddResult, 200, "User has been successfully added!");
                }
                else
                {
                    responce.Error(400,"Error");
                }
            }
            catch (Exception ex)
            {
                responce.Error(400, "Error");
            }
            return responce;
        }
        [HttpPost]
        public async Task<GenericResponceModel<IdentityUser>> CreateRole(IdentityRole newRole)
        {
            var responce = new GenericResponceModel<IdentityUser>();
            try
            {
                var result = await _AccountService.CreateRole(newRole);
                if (result.Succeeded)
                {
                    responce.Success(null,200,"Completed");
                }
                else
                {
                    responce.Error(400, "Error");
                }
            }
            catch (Exception ex)
            {
                responce.Error(400,"Error");
            }
            return responce;
        }
        [HttpPost]
        public async Task<GenericResponceModel<LoginDTO>> Login(LoginDTO User)
        {
            var responce = new GenericResponceModel<LoginDTO>();
            try
            {
                var result = await _AccountService.Login(User, User.Password); 
                if (result.Succeeded)
                {
                    responce.Success(User, 200, "Completed");
                }
            }
            catch (Exception ex)
            {
                responce.Error(400, "Error");
            }
            return responce;
        }
        [HttpGet]
        public async Task<GenericResponceModel<IdentityUser>> Logout()
        {
            var responce = new GenericResponceModel<IdentityUser>();
            try
            {
                await _AccountService.Logout();
                responce.Success(null, 200, "Completed");
            }
            catch (Exception ex)
            {

                responce.Error(400, "Error");
            }
            return responce;
        }
        [HttpGet]
        public async Task<GenericResponceModel<IdentityUser>> GetAllUsers()
        {
            var responce = new GenericResponceModel<IdentityUser>();
            try
            {
                var users = await _AccountService.GetAllUsers();
                responce.AllEntities(users, 200,"Completed");
            }
            catch (Exception ex)
            {
                responce.Error(400, "Error");
            }
            return responce;
        }
        [HttpGet]
        public async Task<GenericResponceModel<IdentityUser>> FindUser(string Email)
        {
            var responce = new GenericResponceModel<IdentityUser>();
            try
            {
                var user = await _AccountService.FindUser(Email);
                responce.Success(user, 200, "User is Found");
            }
            catch (Exception)
            {
                responce.Error(200, "User is not found");
            }
            return responce;
        }
    }
}
