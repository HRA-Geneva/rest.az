using System;

namespace Rest.Models
{
    public class Promotion:Entity
    {

        public int UserId { get; set; }
        public virtual User User { get; set; }
        public int BlogId { get; set; }
        public virtual Blog Blog { get; set; }
        public bool IsPaymentCompleted { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime PaidDate { get; set; }

        public int PromotionStatusId { get; set; }


    }
}
