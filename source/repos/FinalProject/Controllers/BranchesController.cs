using AutoMapper;
using BLLayer.Models.DTO;
using BLLayer.Responce;
using BLLayer.Services.Abstractions;
using BLLayer.Services.Implementations;
using DATAlayer.Entities;
using FinalProject.DAL;
using FinalProject.Entities;
using FinalProject.Models.DTO;
using FinalProject.Services.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinalProject.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BranchesController : ControllerBase
    {
        public BranchesController(ApplicationDbContext context,
            IBranchService branchService)
        {
            _context = context;
            _branchService = branchService;
        }
        public ApplicationDbContext _context { get; }
        IBranchService _branchService { get; }

        [HttpPost]

        public async Task<GenericResponceModel<Branches>> AddBranch(BranchDTO branchInfo)
        {
            var responce = new GenericResponceModel<Branches>();
            try
            {
                var AddedBranch = _branchService.Add(branchInfo);
                responce.Success(AddedBranch, 200, "Success");

            }
            catch (Exception ex)
            {
                responce.Error(default, "Error");
            }
            return responce;
        }

        [HttpPut]
        public GenericResponceModel<Branches> UpdateBranch(BranchDTO NewBranch)
        {
            var res = new GenericResponceModel<Branches>();
            try
            {
                var UpdatedBranch= _branchService.Update(NewBranch);
                res.Success(UpdatedBranch, default, "Room has been successfully updated!");
            }
            catch (Exception ex)
            {
                res.Error(default, "Error");
            }
            return res;
        }

        [HttpDelete]
        public GenericResponceModel<Branches> Delete(int id)
        {
            var responce = new GenericResponceModel<Branches>();
            try
            {
                _branchService.Delete(id);
                responce.Deleted(default, "Reservation has been successfully deleted!");
            }
            catch (Exception ex)
            {
                responce.Error(default, "Error");
            }
            return responce;
        }
        [HttpGet]
        public async Task<GenericResponceModel<Branches>> GetAllBranches()
        {
            var responce = new GenericResponceModel<Branches>();
            try
            {
                var BranchesList = await _branchService.GetAll();
                responce.AllEntities(BranchesList, default, "Successed");
            }
            catch (Exception ex)
            {
                responce.Error(default, "Error");
            }
            return responce;
        }

    }
}
