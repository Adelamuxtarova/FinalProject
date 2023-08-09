using MongoDB.Driver;
using MongoDB.Model;

namespace MongoDB.DataAccessLayer
{
    public class CrudOperations : ICrudoperations
    {
        private readonly IConfiguration _configuration;
        private readonly MongoClient _client;
        private readonly IMongoCollection<InsertRecordRequest> _mongoCollection;

        public CrudOperations(IConfiguration configuration)
        {
            _configuration = configuration;
            _client = new MongoClient(_configuration["DatabaseSettings:ConnectionString"]);
            var _MongoDatabase = _client.GetDatabase(_configuration["DatabaseSettings:DatabaseName"]);
            _mongoCollection = _MongoDatabase.GetCollection<InsertRecordRequest>(_configuration["DatabaseSettings:CollectionName"]);
        }

        public async Task<InsertRecordResponse> InsertRecord(InsertRecordRequest record)
        {
            InsertRecordResponse responce = new InsertRecordResponse();
            responce.IsSucces = true;
            responce.Message = "Data Success";
            try
            {
                record.CreatedDate = DateTime.Now.ToString();
                record.UpdateDate = DateTime.Now.ToString();

                await _mongoCollection.InsertOneAsync(record);
            }
            catch (Exception ex)
            {

                responce.IsSucces = false;
                responce.Message = ex.Message;
            }

            return responce;
        }
    }
}
