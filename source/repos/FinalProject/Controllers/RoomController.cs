using AutoMapper;
using FinalProject.DAL;
using FinalProject.Entities;
using FinalProject.Services.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FinalProject.Models.DTO;
using BLLayer.Models.DTO;
using BLLayer.Services.Abstractions;
using BLLayer.Responce;
using FluentValidation;
using System;
using System.ComponentModel.DataAnnotations;

namespace FinalProject.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        IWebHostEnvironment _env;
        private readonly IRoomService _roomService;
        public RoomController(IWebHostEnvironment environment,
            IRoomService roomService)
        {
            _env = environment;
            _roomService = roomService;
        }
        [HttpPost]
        public async Task<GenericResponceModel<Room>> CreateRoom([FromForm] RoomAddDTO addRoom)
        {
            var responce = new GenericResponceModel<Room>();
            try
            {
                Room myAddRoomEntity = await _roomService.AddAsync(addRoom, _env.WebRootPath);
                responce.Success(myAddRoomEntity, default, "Room has been successfully added!");
            }
            catch (Exception ex)
            {
                responce.Error(400, "Error");
            }
            return responce;
        }

        [HttpPut]
        public GenericResponceModel<RoomUpdateDTO> UpdateRoom([FromForm] RoomUpdateDTO EditedRoom)
        {
            var res = new GenericResponceModel<RoomUpdateDTO>();
            try
            {
                _roomService.Update(EditedRoom);
                res.Success(EditedRoom, default, "Room has been successfully updated!");
            }
            catch (Exception ex)
            {
                res.Error(default, "Error");
            }
            return res;
        }

        [HttpDelete]
        public GenericResponceModel<Room> Delete(int id)
        {
            var responce = new GenericResponceModel<Room>();
            try
            {
                _roomService.Delete(id);
                responce.Deleted(default, "Room has been successfully deleted!");
            }
            catch (Exception ex)
            {
                responce.Error(default, "Error");
                }
            return responce;
        }
        [HttpGet]
        public async Task<GenericResponceModel<Room>> GetAllRooms()
        {
            var responce = new GenericResponceModel<Room>();
            try
            {
                var roomlist = await _roomService.GetAll();
                responce.AllEntities(roomlist, default, "Successed");
            }
            catch (Exception ex)
            {
                responce.Error(default, "Error");
            }
            return responce;

        }
        [HttpGet("Id")]
        public GenericResponceModel<Room> FindRoomById(int Id)
        {
            var responce = new GenericResponceModel<Room>();
            try
            {
                var SelectedRoom = _roomService.GetById(Id);
                responce.Success(SelectedRoom,default,"Successed");
            }
            catch (Exception)
            {
                responce.Error(default,"Error");
            }
            return responce;
        }
    }
}
