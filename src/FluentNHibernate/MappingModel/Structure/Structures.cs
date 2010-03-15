using System;
using System.Collections.Generic;
using System.Linq;
using FluentNHibernate.MappingModel.ClassBased;
using FluentNHibernate.MappingModel.Collections;
using FluentNHibernate.MappingModel.Identity;
using FluentNHibernate.Utils;

namespace FluentNHibernate.MappingModel.Structure
{
    public static class Structures
    {
        public static IMappingStructure<AnyMapping> Any(Member member)
        {
            return Member<AnyMapping>(member);
        }

        public static IMappingStructure<IdMapping> Id(Member member)
        {
            return Member<IdMapping>(member);
        }

        public static IMappingStructure<VersionMapping> Version(Member member)
        {
            return Member<VersionMapping>(member);
        }

        public static IMappingStructure<OneToOneMapping> OneToOne(Member member)
        {
            return Member<OneToOneMapping>(member);
        }

        public static IMappingStructure<PropertyMapping> Property(Member member)
        {
            return Member<PropertyMapping>(member);
        }

        public static IMappingStructure<ManyToOneMapping> ManyToOne(Member member)
        {
            return Member<ManyToOneMapping>(member);
        }

        public static IMappingStructure<ReferenceComponentMapping> ReferenceComponent(Member member)
        {
            return Member<ReferenceComponentMapping>(member);
        }

        public static IMappingStructure<CompositeIdMapping> CompositeId(Member member)
        {
            return Member<CompositeIdMapping>(member);
        }

        public static IMappingStructure<ParentMapping> Parent(Member member)
        {
            return Member<ParentMapping>(member);
        }

        public static IMappingStructure<KeyPropertyMapping> KeyProperty(Member member)
        {
            return Member<KeyPropertyMapping>(member);
        }

        public static IMappingStructure<KeyManyToOneMapping> KeyManyToOne(Member member)
        {
            return Member<KeyManyToOneMapping>(member);
        }

        public static IMappingStructure<IndexMapping> Index(Member member)
        {
            return Member<IndexMapping>(member);
        }

        public static IMappingStructure<KeyMapping> Key(Type type)
        {
            return Type<KeyMapping>(type);
        }

        public static IMappingStructure<OneToManyMapping> OneToMany(Type type)
        {
            return Type<OneToManyMapping>(type);
        }

        public static IMappingStructure<ManyToManyMapping> ManyToMany(Type type)
        {
            return Type<ManyToManyMapping>(type);
        }

        public static IMappingStructure<ExternalComponentMapping> ExternalComponent(Type type)
        {
            return Type<ExternalComponentMapping>(type);
        }

        public static IMappingStructure<MetaValueMapping> MetaValue(Type type)
        {
            return Type<MetaValueMapping>(type);
        }

        public static IMappingStructure<ClassMapping> Class(Type type)
        {
            return Type<ClassMapping>(type);
        }

        public static IMappingStructure<DiscriminatorMapping> Discriminator(Type type)
        {
            return Type<DiscriminatorMapping>(type);
        }

        public static IMappingStructure<ImportMapping> Import(Type type)
        {
            return Type<ImportMapping>(type);
        }

        public static IMappingStructure<ComponentMapping> Component(ComponentType componentType, Member member, Type type)
        {
            return new ComponentStructure(componentType, member, type);
        }

        public static IMappingStructure<IndexManyToManyMapping> IndexManyToMany(Type type)
        {
            return Type<IndexManyToManyMapping>(type);
        }

        public static IMappingStructure<CompositeElementMapping> CompositeElement(Type type)
        {
            return Type<CompositeElementMapping>(type);
        }

        public static IMappingStructure<CollectionMapping> Collection(Type type, Member member)
        {
            return TypeAndMember<CollectionMapping>(type, member);
        }

        public static IMappingStructure<ColumnMapping> Column(IMappingStructure parent)
        {
            return new ColumnStructure(parent);
        }

        public static IMappingStructure<SubclassMapping> Subclass(SubclassType subclassType, Type type)
        {
            return new SubclassStructure(subclassType, type);
        }

        public static IMappingStructure<CacheMapping> Cache()
        {
            return Free<CacheMapping>();
        }

        public static IMappingStructure<JoinMapping> Join()
        {
            return Free<JoinMapping>();
        }

        public static IMappingStructure<HibernateMapping> Container()
        {
            return Free<HibernateMapping>();
        }

        public static IMappingStructure<NaturalIdMapping> NaturalId()
        {
            return Free<NaturalIdMapping>();
        }

        public static IMappingStructure<ElementMapping> Element()
        {
            return Free<ElementMapping>();
        }

        public static IMappingStructure<StoredProcedureMapping> StoredProcedure()
        {
            return Free<StoredProcedureMapping>();
        }

        public static IMappingStructure<FilterMapping> Filter()
        {
            return Free<FilterMapping>();
        }

        public static IMappingStructure<TuplizerMapping> Tuplizer()
        {
            return Free<TuplizerMapping>();
        }

        public static IMappingStructure<GeneratorMapping> Generator()
        {
            return Free<GeneratorMapping>();
        }

        public static IMappingStructure<ParamMapping> Param()
        {
            return Free<ParamMapping>();
        }

        private static IMappingStructure<T> Member<T>(Member member)
            where T : IMemberMapping, new()
        {
            return new MemberStructure<T>(member);
        }

        private static IMappingStructure<T> Type<T>(Type type)
            where T : ITypeMapping, new()
        {
            return new TypeStructure<T>(type);
        }

        private static IMappingStructure<T> TypeAndMember<T>(Type type, Member member)
            where T : ITypeAndMemberMapping, new()
        {
            return new TypeAndMemberStructure<T>(type, member);
        }

        private static IMappingStructure<T> Free<T>()
            where T : IMapping, new()
        {
            return new FreeStructure<T>();
        }

        private class MemberStructure<T> : BaseMappingStructure<T>
            where T : IMemberMapping, new()
        {
            readonly Member member;

            internal MemberStructure(Member member)
            {
                this.member = member;
            }

            public override T CreateMappingNode()
            {
                var mapping = new T();

                mapping.Initialise(member);

                Children
                    .Select(x => x.CreateMappingNode())
                    .Each(mapping.AddChild);

                return mapping;
            }
        }

        private class TypeStructure<T> : BaseMappingStructure<T>
            where T : ITypeMapping, new()
        {
            readonly Type type;

            internal TypeStructure(Type type)
            {
                this.type = type;
            }

            public override T CreateMappingNode()
            {
                var mapping = new T();

                mapping.Initialise(type);

                Children
                    .Select(x => x.CreateMappingNode())
                    .Each(mapping.AddChild);

                return mapping;
            }
        }

        private class TypeAndMemberStructure<T> : BaseMappingStructure<T>
            where T : ITypeAndMemberMapping, new()
        {
            readonly Type type;
            readonly Member member;

            internal TypeAndMemberStructure(Type type, Member member)
            {
                this.type = type;
                this.member = member;
            }

            public override T CreateMappingNode()
            {
                var mapping = new T();

                mapping.Initialise(type, member);


                Children
                    .Select(x => x.CreateMappingNode())
                    .Each(mapping.AddChild);

                return mapping;
            }
        }

        private class ColumnStructure : BaseMappingStructure<ColumnMapping>
        {
            readonly IMappingStructure parent;

            public ColumnStructure(IMappingStructure parent)
            {
                this.parent = parent;
            }

            public override ColumnMapping CreateMappingNode()
            {
                var mapping = new ColumnMapping();

                Children
                    .Select(x => x.CreateMappingNode())
                    .Each(mapping.AddChild);

                return mapping;
            }

            public override IEnumerable<KeyValuePair<Attr, object>> Values
            {
                get
                {
                    return Merge(base.Values, parent.Values);
                }
            }

            /// <summary>
            /// Merges two key collections, overwriting any values in the left with ones from the
            /// right.
            /// </summary>
            private static IEnumerable<KeyValuePair<Attr, object>> Merge(IEnumerable<KeyValuePair<Attr, object>> left, IEnumerable<KeyValuePair<Attr, object>> right)
            {
                var merged = new Dictionary<Attr, object>();

                left.Each(x => merged[x.Key] = x.Value);
                right.Each(x => merged[x.Key] = x.Value);

                return merged;
            }
        }

        private class ComponentStructure : BaseMappingStructure<ComponentMapping>
        {
            readonly ComponentType componentType;
            readonly Member member;
            readonly Type type;

            public ComponentStructure(ComponentType componentType, Member member, Type type)
            {
                this.componentType = componentType;
                this.member = member;
                this.type = type;
            }

            public override ComponentMapping CreateMappingNode()
            {
                var mapping = new ComponentMapping(componentType);

                mapping.Initialise(type);
                mapping.Initialise(member);

                Children
                    .Select(x => x.CreateMappingNode())
                    .Each(mapping.AddChild);

                return mapping;
            }
        }

        private class SubclassStructure : BaseMappingStructure<SubclassMapping>
        {
            readonly SubclassType subclassType;
            readonly Type type;

            public SubclassStructure(SubclassType subclassType, Type type)
            {
                this.subclassType = subclassType;
                this.type = type;
            }

            public override SubclassMapping CreateMappingNode()
            {
                var mapping = new SubclassMapping(subclassType);

                mapping.Initialise(type);

                Children
                    .Select(x => x.CreateMappingNode())
                    .Each(mapping.AddChild);

                return mapping;
            }
        }

        /// <summary>
        /// Structure that has no real category (key, discriminator...)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        private class FreeStructure<T> : BaseMappingStructure<T>
            where T : IMapping, new()
        {
            public override T CreateMappingNode()
            {
                var mapping = new T();

                Children
                    .Select(x => x.CreateMappingNode())
                    .Each(mapping.AddChild);

                return mapping;
            }
        }
    }
}