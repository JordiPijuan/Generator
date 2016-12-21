namespace Objects.Generator.Core.Contracts
{
    using System.CodeDom;

    public abstract class Generator : IGenerator
    {

        public abstract CodeNamespace Generate();

    }

}
