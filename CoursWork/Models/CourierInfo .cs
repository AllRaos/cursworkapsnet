using System.ComponentModel.DataAnnotations;

namespace CoursWork.Models
{
    public class CourierInfo
    {
        [Key]
        public int CourierId {  get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? PhoneNumber { get; set; }
        public decimal? DeliveryPay {  get; set; }
    }
}
