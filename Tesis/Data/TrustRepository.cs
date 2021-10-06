
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Tesis.DTOs;
using Tesis.Entities;
using Tesis.Extensions;
using Tesis.Interfaces;

namespace Tesis.Data
{
    public class TrustRepository : ITrustRepository
    {
        private readonly DataContext _context;
        public TrustRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<TrustUser> GetUserTrust(int sourceUserId, int TrustedUserId)
        {
            return await _context.Trusts.FindAsync(sourceUserId, TrustedUserId);
        }

        public async Task<IEnumerable<TrustDto>> GetUserTrusts(string predicate, int userId)
        {
            var users = _context.Users.OrderBy(x => x.UserName).AsQueryable();
            var trusts = _context.Trusts.AsQueryable();

            if (predicate =="trusted")// this are the users that the currently log in user has trusted
            {
                trusts = trusts.Where(trust => trust.SourceUserId == userId);// go ang get the list of trust table where the source user Id is the id of the parameter.
                users = trusts.Select(trust => trust.TrustedUser);// we select the users that the id is in the trustedUser collumn of the Trust table
            }

            if(predicate == "trustedBy")
            {// here is the same as before but exactly the other way around.
                trusts = trusts.Where(trust => trust.TrustedUserId == userId);
                users = trusts.Select(trust => trust.SourceUser);
            }

            return await users.Select(user => 
                new TrustDto {
                    Username = user.UserName,
                    knownAs = user.KnownAs,
                    Age = user.DateOfBirth.CalculateAge(),
                    PhotoUrl = user.Photos.FirstOrDefault( x=> x.IsMain).Url,
                    city = user.City,
                    Id = user.Id
                    
                }).ToListAsync();
        }

        public async Task<AppUser> GetUserWithTrust(int userId)
        {
            return await _context.Users
                .Include(x => x.TrustedUsers)
                .FirstOrDefaultAsync(x => x.Id == userId);
        }
    }
}
