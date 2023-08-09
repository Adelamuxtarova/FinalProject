using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace MongoDB.Model
{
    public class InsertRecordRequest
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public string CreatedDate { get; set; }

        public string UpdateDate { get; set; }

        [Required]
        [BsonElement(elementName: "Name")]
        public string FirstName { get; set; }
        [Required]
        public int Age { get; set; }

        [Required]
        public string Contact { get; set; }


        public double Salary { get; set; }

    }

    public class InsertRecordResponse 
    {
        public bool IsSucces { get; set; }

        public string Message { get; set; }

    }

}
