namespace FluentNHibernate.MappingModel.Collections
{
    public class MemberNameFormatter
    {
        public string Format(Member member)
        {
            if (member.IsMethod)
            {
                // try to guess the backing field name (GetSomething -> something)
                if (member.Name.StartsWith("Get"))
                {
                    var name = member.Name.Substring(3);

                    if (char.IsUpper(name[0]))
                        name = char.ToLower(name[0]) + name.Substring(1);

                    return name;
                }
            }

            return member.Name;
        }
    }
}