using System.Collections.Generic;

namespace EntityMetadataWebAPI.Models.EntityMetadataFacadeModel
{
    public class EntityMetadataModel
    {
        public string EntityName { get; set; }
        public List<FieldModel> Fields { get; set; }
    }

    public class FieldModel
    {
        public string FieldName { get; set; }
        public bool IsRequired { get; set; }
        public int MaxLength { get; set; }
        public string FieldSource { get; set; }

    }
    public enum SourceFields
    {
        Fields
    }

}
