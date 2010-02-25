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
                mapping.SetTable(mapping.ChildType.Name + "To" + mapping.ContainingEntityType.Name, SetMode.Internal);
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
                    mapping.OtherSide.SetTable(mapping.TableName, SetMode.Internal);
                else if (!mapping.IsSpecified(Attr.Table) && mapping.OtherSide.IsSpecified(Attr.Table))
                    mapping.SetTable(mapping.OtherSide.TableName, SetMode.Internal);
                else
                {
                    var tableName = mapping.Member.Name + "To" + mapping.OtherSide.Member.Name;

                    mapping.SetTable(tableName, SetMode.Internal);
                    mapping.OtherSide.SetTable(tableName, SetMode.Internal);
                }
            }
        }
    }
}