using System;

namespace DigitalLearningIntegration.DAL
{
    public class Auditable : IAuditable
    {
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? LastModifiedDate { get; set; }
    }
}
