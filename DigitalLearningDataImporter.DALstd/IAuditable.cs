using System;

namespace DigitalLearningDataImporter.DALstd
{
    public interface IAuditable
    {
        string CreatedBy { get; set; }
        DateTime CreatedDate { get; set; }
        string UpdatedBy { get; set; }
        DateTime? LastModifiedDate { get; set; }
    }
}
