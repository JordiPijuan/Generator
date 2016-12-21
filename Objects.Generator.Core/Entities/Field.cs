namespace Objects.Generator.Core.Entities
{
    public class Field
    {

        public string Name { get; set; }
        public string FieldType { get; set; }
        public int Position { get; set; }
        public bool IsSelect { get; set; }
        public bool IsNullable { get; set; }

    }

}
