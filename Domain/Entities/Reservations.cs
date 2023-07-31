using Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Reservations : BaseEntity
    {
        [ForeignKey(nameof(Area))]
        public Guid AreaId { get; set; }
        public virtual Area Area { get; set; }

        [ForeignKey(nameof(PricingPlan))]
        public Guid PricingPlanId { get; set; }
        public virtual PricingPlan PricingPlan { get; set; }

        [ForeignKey(nameof(Client))]
        public Guid ClientId { get; set; }
        public virtual Client Client { get; set; }

        public DateTimeOffset FromDate { get; set; }
        public DateTimeOffset? ToDate { get; set; }

        public string Notes { get; set; }

        public decimal TotalAmount { get; set; }
        public decimal DownPayment { get; set; }
    }
}
