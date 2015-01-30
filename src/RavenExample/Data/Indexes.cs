using Raven.Client.Indexes;
using RavenExample.People;
using System.Linq;

namespace RavenExample.Data
{
    public class People_ByBirthdate : AbstractIndexCreationTask<Person>
    {
        public People_ByBirthdate()
        {
            Map = people => people.Select(x => new { x.Birthdate });
        }
    }
}