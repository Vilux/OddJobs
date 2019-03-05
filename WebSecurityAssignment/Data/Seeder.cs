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
                        UserName = "ricky@ps.com",
                        NormalizedUserName = "RICKY@PS.COM",
                        Email = "ricky@ps.com",
                        NormalizedEmail = "RICKY@PS.COM",
                        PasswordHash = "AQAAAAEAACcQAAAAELBUqQ1i5+J9PSH5LOHlBFtE93wrB8UKEkA2kqVOrNNIag+CNgIsWFUXnRxQChx+Vg==",
                        SecurityStamp = "YGC5UJXPPZ6BFZJY7PK5VZWARCM537SJ",
                        ConcurrencyStamp = "99e5d2b6-cd1c-48fa-ab83-654ff84466e0",
                        LockoutEnabled = true,
                        FirstName = "Rick",
                        LastName = "Harrison"
                    },

                     new ApplicationUser{ Id = "3",
                        UserName = "jspeaker@gmail.com",
                        NormalizedUserName = "JSPEAKER@GMAIL.COM",
                        Email = "jspeaker@gmail.com",
                        NormalizedEmail = "JSPEAKER@GMAIL.COM",
                        PasswordHash = "AQAAAAEAACcQAAAAEBmevrDH0L3a/kJXGrfcPRf6Ef3oXVN8i59t+Z2pkEVA/o69NIpgJcZ/p1aItXnjIA==",
                        SecurityStamp = "XI3HUVQAFILRZTU4FZT6PCMWIJG2EXDT",
                        ConcurrencyStamp = "eb3e0151-4293-4792-b48a-b4b79e71599a",
                        LockoutEnabled = true,
                        FirstName = "Jennine",
                        LastName = "Speaker"
                    },

                     new ApplicationUser{ Id = "4",
                        UserName = "asudderth@gmail.com",
                        NormalizedUserName = "ASUDDERTH@GMAIL.COM",
                        Email = "asudderth@gmail.com",
                        NormalizedEmail = "ASUDDERTH@GMAIL.COM",
                        PasswordHash = "AQAAAAEAACcQAAAAELpPDTtEpfrdAbMSkkc24vUyEQkfWjyiwJvygoaR918YyCk3dhOs9RSDE79VNUwONQ==",
                        SecurityStamp = "SIKHUKSW6LOG7XQBTJG3CYNL7JABMVIO",
                        ConcurrencyStamp = "59b8a048-8d9a-4d81-a564-7a3aba376ebe",
                        LockoutEnabled = true,
                        FirstName = "Ashley",
                        LastName = "Sudderth"
                    },

                     new ApplicationUser{ Id = "5",
                        UserName = "cbossi@yahoo.com",
                        NormalizedUserName = "CBOSSI@YAHOO.COM",
                        Email = "cbossi@yahoo.com",
                        NormalizedEmail = "CBOSSI@YAHOO.COM",
                        PasswordHash = "AQAAAAEAACcQAAAAEDuF8h/GDr6elFxj6ACl/BfXHmhAiVkJHh8rOqTt9+Lz7jHJGKmZ+ZMiyPifDoBL4g==",
                        SecurityStamp = "AG43ZRFGDI3TO543XTH24TRE7WO5HL4Y",
                        ConcurrencyStamp = "22dc6fc9-5596-40d8-97e6-26c51a5aa0f6",
                        LockoutEnabled = true,
                        FirstName = "Carly",
                        LastName = "Bossi"
                    },


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
