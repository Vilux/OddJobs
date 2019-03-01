using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebSecurityAssignment.Data;

namespace WebSecurityAssignment.Repositories
{
    public class AddressesRepo
    {
        ApplicationDbContext _context;

        public AddressesRepo(ApplicationDbContext context)
        {
            this._context = context;
        }

        public List<Address> GetAllAddresses()
        {
            var addresses = _context.Addresses;
            List<Address> addressList = new List<Address>();

            foreach (var item in addresses)
            {
                addressList.Add(new Address()
                {
                    addressID = item.addressID,
                    streetAddress = item.streetAddress,
                    city = item.city,
                    province = item.province,
                    postalCode = item.postalCode
                });
            }
            return addressList;
        }

        public Address GetAddress(int addressID)
        {
            var address = _context.Addresses.Where(a => a.addressID == addressID).FirstOrDefault();
            if (address != null)
            {
                return new Address()
                {
                    addressID = address.addressID,
                    streetAddress = address.streetAddress,
                    city = address.city,
                    province = address.province,
                    postalCode = address.postalCode
                };
            }
            return null;
        }

        public bool RemoveAddress(int addressID)
        {
            var address = _context.Addresses.Where(a => a.addressID == addressID).FirstOrDefault();

            _context.Addresses.Remove(address);
            _context.SaveChanges();
            return true;
        }

        public bool UpdateAddress(int addressID, string streetAddress, string city, string province, string postalCode)
        {
            var address = _context.Addresses.Where(a => a.addressID == addressID).FirstOrDefault();
            // Remember you can't update the primary key without 
            // causing trouble.  Just update the review and score
            // for now.
            address.addressID = addressID;
            address.streetAddress = streetAddress;
            address.city = city;
            address.province = province;
            address.postalCode = postalCode;

            _context.SaveChanges();
            return true;
        }

        public bool CreateAddress(int addressID, string streetAddress, string city, string province, string postalCode)
        {
            var address = GetAddress(addressID);
            if (address != null)
            {
                return false;
            }
            _context.Addresses.Add(new Address
            {
                addressID = addressID,
                streetAddress = streetAddress,
                city = city,
                province = province,
                postalCode = postalCode
            });
            _context.SaveChanges();
            return true;
        }
    }
}
