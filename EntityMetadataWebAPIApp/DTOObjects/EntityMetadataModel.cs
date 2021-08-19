
namespace DTOObjects
{    
    public class EntityFieldModel
    {
        public string EntityName { get; set; }
        public string FieldName { get; set; }
        public bool IsRequired { get; set; }
        public int MaxLength { get; set; }
        public string FieldSource { get; set; }

    }
}
