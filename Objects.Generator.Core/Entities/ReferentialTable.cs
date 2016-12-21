namespace Objects.Generator.Core.Entities
{

    public class ReferentialTable
    {

        public string Tablename { get; set; }
        public string Columnname { get; set; }
        public string Foreign { get; set; }
        public string Primary { get; set; }
        public bool IsNullable { get; set; }

    }

}
