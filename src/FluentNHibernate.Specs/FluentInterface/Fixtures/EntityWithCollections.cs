using System;
using System.Collections.Generic;
using Iesi.Collections.Generic;

namespace FluentNHibernate.Specs.FluentInterface.Fixtures
{
    class EntityWithCollections
    {
        public EntityCollectionChild[] ArrayOfChildren { get; set; }
        public IList<EntityCollectionChild> BagOfChildren { get; set; }
        public ISet<EntityCollectionChild> SetOfChildren { get; set; }

        public IList<string> BagOfStrings { get; set; }
        public IList<int> BagOfInts { get; set; }
        public IList<double> BagOfDoubles { get; set; }
        public IList<long> BagOfLongs { get; set; }
        public IList<short> BagOfShorts { get; set; }
        public IList<float> BagOfFloats { get; set; }
        public IList<char> BagOfChars { get; set; }
        public IList<DateTime> BagOfDateTimes { get; set; }
        public IList<decimal> BagOfDecimals { get; set; }
        public IList<bool> BagOfBools { get; set; }

        public ISet<string> SetOfStrings { get; set; }
        public ISet<int> SetOfInts { get; set; }
        public ISet<double> SetOfDoubles { get; set; }
        public ISet<long> SetOfLongs { get; set; }
        public ISet<short> SetOfShorts { get; set; }
        public ISet<float> SetOfFloats { get; set; }
        public ISet<char> SetOfChars { get; set; }
        public ISet<DateTime> SetOfDateTimes { get; set; }
        public ISet<decimal> SetOfDecimals { get; set; }
        public ISet<bool> SetOfBools { get; set; }
    }

    class EntityWithFieldCollections
    {
        public IList<EntityCollectionChild> BagOfChildren;
    }

    class EntityCollectionChild
    {
        public int Position { get; set; }
    }
}
