using Raven.Client.Indexes;
using RavenExample.People;
using System.Linq;

namespace RavenExample.Data
{
    public class People_ByBirthdate : AbstractIndexCreationTask<Person>
    {
        public People_ByBirthdate()
        {
            Map = people => people.Select(x =>
            // Must be an anonymous object
            new { x.Birthdate });

            
            // Not needed for dates. What about integers?
            // Sort(x => x.Birthdate, SortOptions.String);
        }
    }

    public class People_ByFirstNameAndBirthdate : AbstractIndexCreationTask<Person>
    {
        public People_ByFirstNameAndBirthdate()
        {
            Map = people => people.Select(x =>
            new
            {
                x.FirstName,
                x.Birthdate
            });
        }
    }

    #region Index on combined field
    public class People_ByFullName : AbstractIndexCreationTask<Person>
    {
        public People_ByFullName()
        {
            Map = people => people.Select(x =>
            new
            {
                FullName = x.FirstName + x.LastName
            });
        }
    }
    #endregion

    #region Map/Reduce index
    public class People_GroupedByFirstName : AbstractIndexCreationTask<Person, People_GroupedByFirstName.Result>
    {
        public class Result
        {
            public string FirstName { get; set; }
            public int Count { get; set; }
        }

        public People_GroupedByFirstName()
        {
            Map = people => people.Select(x =>
            new
            {
                x.FirstName,
                Count = 1
            });

            Reduce = people => people.GroupBy(x => x.FirstName)
            .Select(group =>
            new
            {
                FirstName = group.Key,
                Count = group.Sum(x => x.Count)
            });
        }
    }
    #endregion
}