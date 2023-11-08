
using System.ComponentModel.DataAnnotations;

namespace talenthubBE.Models
{
    public class CreateProcessRequest
    {
        [Required]
        public required Guid DeveloperId { get; set; }
        [Required]
        public required Guid JobId { get; set; }
    }
}