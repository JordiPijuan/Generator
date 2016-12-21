namespace Objects.Generator.Core.Entities
{
    using System.Collections.Generic;

    public class Procedure
    {

        public Procedure()
        {
            Parameters = new List<Parameter>();    
        }

        public string Name { get; set; }
        public bool IsSelect { get; set; }

        public IList<Parameter> Parameters { get; set; }

    }

}
