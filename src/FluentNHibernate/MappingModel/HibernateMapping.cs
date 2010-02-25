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

        public HibernateMapping()
            : this(null)
        {}

        public HibernateMapping(AttributeStore underlyingStore)
        {
            if (underlyingStore != null)
                ReplaceAttributes(underlyingStore);

            classes = new List<ClassMapping>();
            filters = new List<FilterDefinitionMapping>();
            imports = new List<ImportMapping>();

            SetDefaultAttribute(Attr.DefaultCascade, "none");
            SetDefaultAttribute(Attr.DefaultAccess, "property");
            SetDefaultAttribute(Attr.DefaultLazy, true);
            SetDefaultAttribute(Attr.AutoImport, true);
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
            get { return (string)GetAttribute(Attr.Catalog); }
            set { SetAttribute(Attr.Catalog, value); }
        }

        public string DefaultAccess
        {
            get { return (string)GetAttribute(Attr.DefaultAccess); }
            set { SetAttribute(Attr.DefaultAccess, value); }
        }

        public string DefaultCascade
        {
            get { return (string)GetAttribute(Attr.DefaultCascade); }
            set { SetAttribute(Attr.DefaultCascade, value); }
        }

        public bool AutoImport
        {
            get { return (bool)GetAttribute(Attr.AutoImport); }
            set { SetAttribute(Attr.AutoImport, value); }
        }

        public string Schema
        {
            get { return (string)GetAttribute(Attr.Schema); }
            set { SetAttribute(Attr.Schema, value); }
        }

        public bool DefaultLazy
        {
            get { return (bool)GetAttribute(Attr.DefaultLazy); }
            set { SetAttribute(Attr.DefaultLazy, value); }
        }

        public string Namespace
        {
            get { return (string)GetAttribute(Attr.Namespace); }
            set { SetAttribute(Attr.Namespace, value); }
        }

        public string Assembly
        {
            get { return (string)GetAttribute(Attr.Assembly); }
            set { SetAttribute(Attr.Assembly, value); }
        }

        public bool Equals(HibernateMapping other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return other.classes.ContentEquals(classes) &&
                other.filters.ContentEquals(filters) &&
                other.imports.ContentEquals(imports) &&
                base.Equals(other);
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
                result = (result * 397) ^ base.GetHashCode();
                return result;
            }
        }
    }
}