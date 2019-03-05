using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebSecurityAssignment.Data
{
    

    public class Seeder
    {
        private ApplicationDbContext _context;

        public Seeder(ApplicationDbContext context)
        {
            this._context = context;
            SeedData();
        }

        public void SeedData()
        {
            if (_context.Addresses.Count() == 0)
            {
                Address[] seedAddresses = new Address[]
                {
                    new Address{ addressID = 1, streetAddress = "1280 Waverley Avenue", city = "Vancouver", province = "BC", postalCode = "V5W2C3" },
                    new Address{ addressID = 2, streetAddress = "6088 Beresford Street", city = "Burnaby", province = "BC", postalCode = "V5G1K2" },
                    new Address{ addressID = 3, streetAddress = "11049 Fuller Crescent Street", city = "Delta", province = "BC", postalCode = "V4C2C8" },
                    new Address{ addressID = 4, streetAddress = "1102 Salter Street", city = "New Westminster", province = "BC", postalCode = " V3M6W7" },
                };

                _context.Addresses.AddRange(seedAddresses);
                _context.SaveChanges();
            }

            //If the only account existing is the default admin account, seed more users
            if (_context.Users.Count() == 1)
            {
                ApplicationUser[] seedUsers = new ApplicationUser[]
                {
                    new ApplicationUser{ Id = "2",
                        UserName = "donald@trump.com",
                        NormalizedUserName = "DONALD@TRUMP.COM",
                        Email = "donald@trump.com",
                        NormalizedEmail = "DONALD@TRUMP.COM",
                        PasswordHash = "AQAAAAEAACcQAAAAEBNc1s4n5EQEzAicTebraZfGvYeLuaQZft8K6r4CmtYx/MUXxlRUjwI3NJptJDHSNg==",
                        SecurityStamp = "EVUP2UYLR27PS5DCZL44FY4YBT7J7KAS",
                        ConcurrencyStamp = "EVUP2UYLR27PS5DCZL44FY4YBT7J7KAS",
                        LockoutEnabled = true,
                        FirstName = "Donald",
                        LastName = "Trump"
                    }
                };

                _context.Users.AddRange(seedUsers);
                _context.SaveChanges();
            }

            DateTime dateExpired = new DateTime(2019, 03, 30, 10, 00, 00);
            DateTime dateNeeded = DateTime.Now;

            if (_context.Jobs.Count() == 0)
            {
                Job[] seedJobs = new Job[]
                {
                    new Job{ jobID = 1, title = "Mow my lawn",
                        description = "Need my lawn mowed since I hurt my back and cant push the mower anymore. Should take no longer than afew hours",
                        employerID = "2",
                        amount = 23,
                        dateNeeded = dateNeeded,
                        dateExpired = dateExpired,
                        addressID = 1 
                    },
                    new Job{ jobID = 2, title = "Organize my storage",
                        description = "I have a 10 x 18 ft garage and I need all items in it cleaned, sorted and labeled. I'll provide all packaging materials.",
                        employerID = "2",
                        amount = 75,
                        dateNeeded = dateNeeded,
                        dateExpired = dateExpired,
                        addressID = 2
                    },
                    new Job{ jobID = 3, title = "Roof repair",
                        description = "Our roof has been leaking for almost a week now, Need someone to check and do the necessary repairs.",
                        employerID = "2",
                        amount = 60,
                        dateNeeded = dateNeeded,
                        dateExpired = dateExpired,
                        addressID = 3
                    },
                    new Job{ jobID = 4, title = "Tire swap",
                        description = "Swap out winter tires to all-season. Vehicle: Ford F-150.",
                        employerID = "2",
                        amount = 10,
                        dateNeeded = dateNeeded,
                        dateExpired = dateExpired,
                        addressID = 4
                    },

                };

                _context.Jobs.AddRange(seedJobs);
                _context.SaveChanges();
            }
        }
    }


}
