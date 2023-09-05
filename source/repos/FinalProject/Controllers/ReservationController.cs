using AutoMapper;
using BLLayer.Models.DTO;
using BLLayer.Responce;
using BLLayer.Services.Abstractions;
using BLLayer.Services.Implementations;
using FinalProject.DAL;
using FinalProject.Entities;
using FinalProject.Models.DTO;
using FinalProject.Services.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinalProject.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        public ReservationController(ApplicationDbContext context,
            IMapper mapper, 
            IReservationService reservationService)
        {
            _context = context;
            _mapper = mapper;
            _reservationService = reservationService;
        }
        public ApplicationDbContext _context { get; }
        IMapper _mapper;
        IReservationService _reservationService { get;}
        private readonly IUnitOfWork _unitOfWork;

        [HttpPost]

        public async Task<GenericResponceModel<Reservation>> Reservation(ReservationDTO ReservationInfo)
        {
            var responce = new GenericResponceModel<Reservation>();
            try
            {
                var AddedRoom = _reservationService.Add(ReservationInfo);
                responce.Success(AddedRoom, 200, "Success");

            }
            catch (Exception ex)
            {
                responce.Error(default, "Error");
            }
            return responce;
        }

        [HttpPut]
        public GenericResponceModel<Reservation> UpdateReservation(ReservationUpdateDTO UpdatedReservation)
        {
            var res = new GenericResponceModel<Reservation>();
            try
            {
                var UpdateddRoom = _reservationService.Update(UpdatedReservation);
                 res.Success(UpdateddRoom, default, "Room has been successfully updated!");
            }
            catch (Exception ex)
            {
                res.Error(default, "Error");
            }
            return res;
        }

        [HttpDelete]
        public GenericResponceModel<Reservation> Delete(int id)
        {
            var responce = new GenericResponceModel<Reservation>();
            try
            {
               _reservationService.Delete(id);
                responce.Deleted(default, "Reservation has been successfully deleted!");
            }
            catch (Exception ex)
            {
                responce.Error(default, "Error");
            }
            return responce;
        }
        [HttpGet]
        public async Task<GenericResponceModel<Reservation>> GetAllReservations()
        {
            var responce = new GenericResponceModel<Reservation>();
            try
            {
                var ReservationList =  await _reservationService.GetAll();
                responce.AllEntities(ReservationList, default, "Successed");
            }
            catch (Exception ex)
            {
                responce.Error(default, "Error");
            }
            return responce;
        }

    }
}
