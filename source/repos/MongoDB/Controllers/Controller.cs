using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.DataAccessLayer;
using MongoDB.Model;

namespace MongoDB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Controller : ControllerBase
    {
        private readonly ICrudoperations _crudoperations;
         public Controller(ICrudoperations crudoperations)
        {
            _crudoperations = crudoperations;
        }

        [HttpPost]

        public async Task<IActionResult> InsertRecord(InsertRecordRequest request)
        {
            InsertRecordResponse responce = new InsertRecordResponse();

            try
            {
                responce = await _crudoperations.InsertRecord(request);
            }
            catch (Exception ex)
            {

                responce.IsSucces = false;
                responce.Message = "Exception Occurs :" + ex.Message;
            }

            return Ok(responce);
        }

    }
}
