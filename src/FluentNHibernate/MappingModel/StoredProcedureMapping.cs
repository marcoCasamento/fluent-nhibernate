using System;
using System.Linq.Expressions;
using FluentNHibernate.Visitors;

namespace FluentNHibernate.MappingModel
{
    public class StoredProcedureMapping : MappingBase
    {
        private readonly AttributeStore attributes;

        public StoredProcedureMapping() : this("sql-insert", "")
        {
        }

        public StoredProcedureMapping(AttributeStore attributes)
        {
            this.attributes = attributes.Clone();
        }

        public StoredProcedureMapping(string spType, string innerText): this(spType, innerText, new AttributeStore())
        {
        }

        public StoredProcedureMapping(string spType, string innerText, AttributeStore underlyingStore)
        {
            attributes = underlyingStore.Clone();
            SPType = spType;
            Query = innerText;
            Check = "none";

        }

        public string Name
        {
            get { return attributes.Get(Attr.Name); }
            set { attributes.Set(Attr.Name, value); }
        }

        public Type Type
        {
            get { return attributes.Get<Type>(Attr.Type); }
            set { attributes.Set(Attr.Type, value); }
        }

        public override void AcceptVisitor(IMappingModelVisitor visitor)
        {
            visitor.ProcessStoredProcedure(this);
        }

        public override bool IsSpecified(Attr property)
        {
            return attributes.HasUserValue(property);
        }

        public string Check
        {
            get { return attributes.Get(Attr.Check); }
            set { attributes.Set(Attr.Check, value); }
        }

        public string SPType
        {
            get { return attributes.Get(Attr.SPType); }
            set { attributes.Set(Attr.SPType, value); }
        }     
        
        public string Query
        {
            get { return attributes.Get(Attr.Query); }
            set { attributes.Set(Attr.Query, value); }
        }

        public void SetDefaultValue<TResult>(Attr property, TResult value)
        {
            attributes.SetDefault(property, value);
        }

        public bool Equals(StoredProcedureMapping other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(other.attributes, attributes);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return Equals(obj as StoredProcedureMapping);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                {
                    return (base.GetHashCode() * 397) ^ (attributes != null ? attributes.GetHashCode() : 0);
                }
            }
        }
    }
}
