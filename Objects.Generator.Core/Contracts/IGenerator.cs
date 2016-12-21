namespace Objects.Generator.Core.Contracts
{
    using System.CodeDom;

    public interface IGenerator
    {
        CodeNamespace Generate();
    }

}
