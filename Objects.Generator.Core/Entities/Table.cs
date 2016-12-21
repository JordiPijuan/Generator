namespace Objects.Generator.Core.Entities
{
    using System.Collections.Generic;
    
    public class Table
    {

        public Table()
        {
            FieldsList = new List<Field>();
            ReferentialList = new List<ReferentialTable>();
        }

        public string Name { get; set; }
        public string TypeObject { get; set; }
        public bool IsSelect { get; set; }
        public IList<Field> FieldsList { get; set; }
        public IList<ReferentialTable> ReferentialList { get; set; }

    }

}
