using System;
using System.Collections.Generic;
using System.Diagnostics;
using FluentNHibernate.Mapping.Providers;
using FluentNHibernate.MappingModel;
using FluentNHibernate.MappingModel.ClassBased;
using FluentNHibernate.MappingModel.Structure;
using FluentNHibernate.Utils;

namespace FluentNHibernate.Mapping
{
    public class SubclassMap<T> : ClasslikeMapBase<T>, IIndeterminateSubclassMappingProvider
    {
        readonly IMappingStructure<SubclassMapping> structure;

        // this is a bit weird, but we need a way of delaying the generation of the subclass mappings until we know
        // what the parent subclass type is...
        private readonly IDictionary<Type, IIndeterminateSubclassMappingProvider> indetermineateSubclasses = new Dictionary<Type, IIndeterminateSubclassMappingProvider>();
        private bool nextBool = true;
        private IList<JoinMapping> joins = new List<JoinMapping>();
        IMappingStructure<KeyMapping> keyStructure;

        public SubclassMap()
            : this(Structures.Subclass(SubclassType.Unknown, typeof(T)))
        {}

        SubclassMap(IMappingStructure<SubclassMapping> structure)
            : base(structure)
        {
            this.structure = structure;
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public SubclassMap<T> Not
        {
            get
            {
                nextBool = !nextBool;
                return this;
            }
        }

        IUserDefinedMapping IMappingProvider.GetUserDefinedMappings()
        {
            //var mapping = new SubclassMapping(SubclassType.Unknown);

            //GenerateNestedSubclasses(mapping);

            //attributes.SetDefault(x => x.Type, typeof(T));
            //attributes.SetDefault(x => x.Name, typeof(T).AssemblyQualifiedName);
            //attributes.SetDefault(x => x.DiscriminatorValue, typeof(T).Name);

            //// TODO: un-hardcode this
            //var key = new KeyMapping();
            //key.AddDefaultColumn(new ColumnMapping { Name = typeof(T).BaseType.Name + "_id" });

            //attributes.SetDefault(x => x.TableName, GetDefaultTableName());
            //attributes.SetDefault(x => x.Key, key);

            //// TODO: this is nasty, we should find a better way
            //mapping.OverrideAttributes(attributes.CloneInner());

            //foreach (var join in joins)
            //    mapping.AddJoin(join);

            //foreach (var property in properties)
            //    mapping.AddProperty(property.GetPropertyMapping());

            //foreach (var component in components)
            //    mapping.AddComponent(component.GetComponentMapping());

            //foreach (var oneToOne in oneToOnes)
            //    mapping.AddOneToOne(oneToOne.GetOneToOneMapping());

            //foreach (var collection in collections)
            //    mapping.AddCollection(collection.GetCollectionMapping());

            //foreach (var reference in references)
            //    mapping.AddReference(reference.GetManyToOneMapping());

            //foreach (var any in anys)
            //    mapping.AddAny(any.GetAnyMapping());

            return new FluentMapUserDefinedMappings(typeof(T), structure);
        }

        public HibernateMapping GetHibernateMapping()
        {
            throw new NotImplementedException();
        }

        private void GenerateNestedSubclasses(SubclassMapping mapping)
        {
            foreach (var subclassType in indetermineateSubclasses.Keys)
            {
                var userMappings = indetermineateSubclasses[subclassType].GetUserDefinedMappings();
                var subclassMapping = (SubclassMapping)userMappings.Structure;
                subclassMapping.SubclassType = mapping.SubclassType;

                mapping.AddSubclass(subclassMapping);
            }
        }

        private string GetDefaultTableName()
        {
            var tableName = EntityType.Name;

            if (EntityType.IsGenericType)
            {
                // special case for generics: GenericType_GenericParameterType
                tableName = EntityType.Name.Substring(0, EntityType.Name.IndexOf('`'));

                foreach (var argument in EntityType.GetGenericArguments())
                {
                    tableName += "_";
                    tableName += argument.Name;
                }
            }

            return "`" + tableName + "`";
        }

        public void Abstract()
        {
            structure.SetValue(Attr.Abstract, nextBool);
            nextBool = true;
        }

        public void DynamicInsert()
        {
            structure.SetValue(Attr.DynamicInsert, nextBool);
            nextBool = true;
        }

        public void DynamicUpdate()
        {
            structure.SetValue(Attr.DynamicUpdate, nextBool);
            nextBool = true;
        }

        public void LazyLoad()
        {
            structure.SetValue(Attr.Lazy, nextBool);
            nextBool = true;
        }

        public void Proxy<TProxy>()
        {
            Proxy(typeof(TProxy));
        }

        public void Proxy(Type proxyType)
        {
            structure.SetValue(Attr.Proxy, proxyType.AssemblyQualifiedName);
        }

        public void SelectBeforeUpdate()
        {
            structure.SetValue(Attr.SelectBeforeUpdate, nextBool);
            nextBool = true;
        }

        public void Subclass<TSubclass>(Action<SubclassMap<TSubclass>> subclassDefinition)
        {
            var subclass = new SubclassMap<TSubclass>();

            subclassDefinition(subclass);

            indetermineateSubclasses[typeof(TSubclass)] = subclass;
        }

        public void DiscriminatorValue(object discriminatorValue)
        {
            structure.SetValue(Attr.DiscriminatorValue, discriminatorValue);
        }

        public void Table(string table)
        {
            structure.SetValue(Attr.Table, table);
        }

        public void Schema(string schema)
        {
            structure.SetValue(Attr.Schema, schema);
        }

        public void Check(string constraint)
        {
            structure.SetValue(Attr.Check, constraint);
        }

        public void KeyColumn(string columnName)
        {
            if (keyStructure == null)
            {
                keyStructure = Structures.Key(typeof(T));
                structure.AddChild(keyStructure);
            }

            var column = Structures.Column(keyStructure);

            new ColumnPart(column)
                .Name(columnName);
        }

        public void Subselect(string subselect)
        {
            structure.SetValue(Attr.Subselect, subselect);
        }

        public void Persister<TPersister>()
        {
            structure.SetValue(Attr.Persister, new TypeReference(typeof(TPersister)));
        }

        public void Persister(Type type)
        {
            structure.SetValue(Attr.Persister, new TypeReference(type));
        }

        public void Persister(string type)
        {
            structure.SetValue(Attr.Persister, new TypeReference(type));
        }

        public void BatchSize(int batchSize)
        {
            structure.SetValue(Attr.BatchSize, batchSize);
        }

        public void EntityName(string entityname)
        {
            structure.SetValue(Attr.EntityName, entityname);
        }

        /// <summary>
        /// Sets additional tables for the class via the NH 2.0 Join element, this only works if
        /// the hierarchy you're mapping has a discriminator.
        /// </summary>
        /// <param name="tableName">Joined table name</param>
        /// <param name="action">Joined table mapping</param>
        public void Join(string tableName, Action<JoinPart<T>> action)
        {
            var joinStructure = Structures.Join();
            var join = new JoinPart<T>(joinStructure);

            if (!string.IsNullOrEmpty(tableName))
                join.Table(tableName);

            action(join);

            structure.AddChild(joinStructure);
        }
    }
}