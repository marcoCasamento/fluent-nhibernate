using System;
using System.Collections.Generic;
using System.Reflection;
using FluentNHibernate.Mapping;
using FluentNHibernate.Visitors;

namespace FluentNHibernate.MappingModel.Identity
{
    public class IdMapping : ColumnBasedMappingBase, IIdentityMapping, IMapping, IMemberMapping
    {
        readonly ValueStore values = new ValueStore();
        readonly Member member;

        public IdMapping(Member member)
        {
            this.member = member;

            Generator = GetDefaultGenerator(member);
        }

        private static GeneratorMapping GetDefaultGenerator(Member property)
        {
            var generatorStructure = new BucketStructure<GeneratorMapping>();
            var defaultGenerator = new GeneratorBuilder(generatorStructure, property.PropertyType);

            if (property.PropertyType == typeof(Guid))
                defaultGenerator.GuidComb();
            else if (property.PropertyType == typeof(int) || property.PropertyType == typeof(long))
                defaultGenerator.Identity();
            else
                defaultGenerator.Assigned();

            return generatorStructure.CreateMappingNode(new DefaultMappingFactory());
        }


        public Member Member { get; set; }
        public GeneratorMapping Generator { get; set; }

        public override void AcceptVisitor(IMappingModelVisitor visitor)
        {
            visitor.ProcessId(this);

            foreach (var column in Columns)
                visitor.Visit(column);

            if (Generator != null)
                visitor.Visit(Generator);
        }

        public string Name
        {
            get { return values.Get(Attr.Name); }
            set { values.Set(Attr.Name, value); }
        }

        public string Access
        {
            get { return values.Get(Attr.Access); }
            set { values.Set(Attr.Access, value); }
        }

        public TypeReference Type
        {
            get { return values.Get<TypeReference>(Attr.Type); }
            set { values.Set(Attr.Type, value); }
        }

        public string UnsavedValue
        {
            get { return values.Get(Attr.UnsavedValue); }
            set { values.Set(Attr.UnsavedValue, value); }
        }

        public Type ContainingEntityType { get; set; }

        public bool Equals(IdMapping other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return base.Equals(other) && Equals(other.Member, Member) && Equals(other.ContainingEntityType, ContainingEntityType);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return Equals(obj as IdMapping);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int result = base.GetHashCode();
                result = (result * 397) ^ (Member != null ? Member.GetHashCode() : 0);
                result = (result * 397) ^ (ContainingEntityType != null ? ContainingEntityType.GetHashCode() : 0);
                return result;
            }
        }

        public override void AddChild(IMapping child)
        {
        }

        public void UpdateValues(IEnumerable<KeyValuePair<Attr, object>> values)
        {
        }

        public bool HasValue(Attr attr)
        {
            return values.HasValue(attr);
        }
    }
}