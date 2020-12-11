using Assignment1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment1.Data
{
    public static class DbInitializer
    {
        public static void Initialize(SchoolCommunityContext context)
        {
            context.Database.EnsureCreated();

            if (context.Students.Any())
            {
                return;   // DB has been seeded
            }

            var students = new Student[]
            {
            new Student{FirstName="Carson",LastName="Alexander",EnrollmentDate=DateTime.Parse("2005-09-01")},
            new Student{FirstName="Meredith",LastName="Alonso",EnrollmentDate=DateTime.Parse("2002-09-01")},
            new Student{FirstName="Arturo",LastName="Anand",EnrollmentDate=DateTime.Parse("2003-09-01")},
            new Student{FirstName="Gytis",LastName="Barzdukas",EnrollmentDate=DateTime.Parse("2002-09-01")},
            };
            foreach (Student s in students)
            {
                context.Students.Add(s);
            }
            context.SaveChanges();

            var communities = new Community[]
            {
            new Community{ID="A1",Title="Alpha",Budget=300},
            new Community{ID="B1",Title="Beta",Budget=130},
            new Community{ID="O1",Title="Omega",Budget=390},
            };
            foreach (Community c in communities)
            {
                context.Communities.Add(c);
            }
            context.SaveChanges();

            var memberships = new CommunityMembership[]
            {
            new CommunityMembership{StudentID=1,CommunityID="A1"},
            new CommunityMembership{StudentID=1,CommunityID="B1"},
            new CommunityMembership{StudentID=1,CommunityID="O1"},
            new CommunityMembership{StudentID=2,CommunityID="A1"},
            new CommunityMembership{StudentID=2,CommunityID="B1"},
            new CommunityMembership{StudentID=3,CommunityID="A1"},
            };
            foreach (var m in memberships)
            {
                context.CommunityMemberships.Add(m);
            }
            context.SaveChanges();

        }
    }

}
