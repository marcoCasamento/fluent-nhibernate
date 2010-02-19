using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using FluentNHibernate.MappingModel.ClassBased;
using FluentNHibernate.Visitors;

namespace FluentNHibernate.MappingModel
{
    public class HibernateMapping : MappingBase
    {
        private readonly IList<ClassMapping> classes;
        private readonly IList<FilterDefinitionMapping> filters;
        private readonly IList<ImportMapping> imports;
        private readonly AttributeStore attributes;

        public HibernateMapping()
            : this(new AttributeStore())
        {}

        public HibernateMapping(AttributeStore underlyingStore)
        {
            attributes = underlyingStore.Clone();
            classes = new List<ClassMapping>();
            filters = new List<FilterDefinitionMapping>();
            imports = new List<ImportMapping>();

            attributes.SetDefault(Attr.DefaultCascade, "none");
            attributes.SetDefault(Attr.DefaultAccess, "property");
            attributes.SetDefault(Attr.DefaultLazy, true);
            attributes.SetDefault(Attr.AutoImport, true);
        }

        public override void AcceptVisitor(IMappingModelVisitor visitor)
        {
            visitor.ProcessHibernateMapping(this);

            foreach (var import in Imports)
                visitor.Visit(import);

            foreach (var classMapping in Classes)
                visitor.Visit(classMapping);

            foreach (var filterMapping in Filters)
                visitor.Visit(filterMapping);
        }

        public IEnumerable<ClassMapping> Classes
        {
            get { return classes; }
        }

        public IEnumerable<FilterDefinitionMapping> Filters
        {
            get { return filters; }
        }

        public IEnumerable<ImportMapping> Imports
        {
            get { return imports; }
        }

        public void AddClass(ClassMapping classMapping)
        {
            classes.Add(classMapping);            
        }

        public void AddFilter(FilterDefinitionMapping filterMapping)
        {
            filters.Add(filterMapping);
        }

        public void AddImport(ImportMapping importMapping)
        {
            imports.Add(importMapping);
        }

        public string Catalog
        {
            get { return attributes.Get(Attr.Catalog); }
            set { attributes.Set(Attr.Catalog, value); }
        }

        public string DefaultAccess
        {
            get { return attributes.Get(Attr.DefaultAccess); }
            set { attributes.Set(Attr.DefaultAccess, value); }
        }

        public string DefaultCascade
        {
            get { return attributes.Get(Attr.DefaultCascade); }
            set { attributes.Set(Attr.DefaultCascade, value); }
        }

        public bool AutoImport
        {
            get { return attributes.Get<bool>(Attr.AutoImport); }
            set { attributes.Set(Attr.AutoImport, value); }
        }

        public string Schema
        {
            get { return attributes.Get(Attr.Schema); }
            set { attributes.Set(Attr.Schema, value); }
        }

        public bool DefaultLazy
        {
            get { return attributes.Get<bool>(Attr.DefaultLazy); }
            set { attributes.Set(Attr.DefaultLazy, value); }
        }

        public string Namespace
        {
            get { return attributes.Get(Attr.Namespace); }
            set { attributes.Set(Attr.Namespace, value); }
        }

        public string Assembly
        {
            get { return attributes.Get(Attr.Assembly); }
            set { attributes.Set(Attr.Assembly, value); }
        }

        public override bool IsSpecified(Attr property)
        {
            return attributes.HasUserValue(property);
        }

        public bool HasValue(Attr property)
        {
            return attributes.HasAnyValue(property);
        }

        public bool Equals(HibernateMapping other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return other.classes.ContentEquals(classes) &&
                other.filters.ContentEquals(filters) &&
                other.imports.ContentEquals(imports) &&
                Equals(other.attributes, attributes);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(HibernateMapping)) return false;
            return Equals((HibernateMapping)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int result = (classes != null ? classes.GetHashCode() : 0);
                result = (result * 397) ^ (filters != null ? filters.GetHashCode() : 0);
                result = (result * 397) ^ (imports != null ? imports.GetHashCode() : 0);
                result = (result * 397) ^ (attributes != null ? attributes.GetHashCode() : 0);
                return result;
            }
        }
    }
}