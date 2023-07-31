using Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Client : BaseEntity
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string MobileNumber { get; set; }
        public string ProfessionalTitle { get; set; }
        public string Interests { get; set; }
        public string Source { get; set; }

        [ForeignKey(nameof(Brand))]
        public Guid BrandId { get; set; }
        public virtual Brand Brand { get; set; }
    }
}
