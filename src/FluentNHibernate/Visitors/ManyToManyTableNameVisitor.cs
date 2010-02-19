using FluentNHibernate.MappingModel;
using FluentNHibernate.MappingModel.Collections;

namespace FluentNHibernate.Visitors
{
    public class ManyToManyTableNameVisitor : DefaultMappingModelVisitor
    {
        protected override void ProcessCollection(ICollectionMapping mapping)
        {
            if (!(mapping.Relationship is ManyToManyMapping))
                return;

            if (mapping.OtherSide == null)
            {
                // uni-directional
                mapping.SetDefaultValue(Attr.Table, mapping.ChildType.Name + "To" + mapping.ContainingEntityType.Name);
            }
            else
            {
                // bi-directional
                if (mapping.IsSpecified(Attr.Table) && mapping.OtherSide.IsSpecified(Attr.Table))
                {
                    // TODO: We could check if they're the same here and warn the user if they're not
                    return;
                }

                if (mapping.IsSpecified(Attr.Table) && !mapping.OtherSide.IsSpecified(Attr.Table))
                    mapping.OtherSide.SetDefaultValue(Attr.Table, mapping.TableName);
                else if (!mapping.IsSpecified(Attr.Table) && mapping.OtherSide.IsSpecified(Attr.Table))
                    mapping.SetDefaultValue(Attr.Table, mapping.OtherSide.TableName);
                else
                {
                    var tableName = mapping.Member.Name + "To" + mapping.OtherSide.Member.Name;

                    mapping.SetDefaultValue(Attr.Table, tableName);
                    mapping.OtherSide.SetDefaultValue(Attr.Table, tableName);
                }
            }
        }
    }
}