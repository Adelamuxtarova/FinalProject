using MongoDB.Model;

namespace MongoDB.DataAccessLayer
{
    public interface ICrudoperations
    {
        public Task<InsertRecordResponse> InsertRecord(InsertRecordRequest record);
         
    }
}
