namespace FluentNHibernate.MappingModel
{
    public interface IMapping
    {
        void AddChild(IMapping child);
    }

    /// <summary>
    /// Denotes a mapping node that is directly related to a member (property, any, bag, etc...)
    /// </summary>
    public interface IMemberMapping : IMapping
    {}

    /// <summary>
    /// Denotes a mapping node that represents a type (class, subclass, etc...)
    /// </summary>
    public interface ITypeMapping : IMapping
    {}
}