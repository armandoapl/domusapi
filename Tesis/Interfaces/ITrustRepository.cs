using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tesis.DTOs;
using Tesis.Entities;

namespace Tesis.Interfaces
{
    public interface ITrustRepository
    {
        // go and get an individual TrustUser register
        Task<TrustUser> GetUserTrust(int sourceUserId, int TrustedUserId);

        //here I go and get the list of users that the current user has given a trust vote 
        Task<AppUser> GetUserWithTrust(int userId);

        //here we are going to look for a list of user that one gave a trust vote or a list of user
        //that has given us a trust vote , the predicate parameter is to say choose between those two options
        //and the userId parameter just tells you what user are we interested in know
        Task<IEnumerable<TrustDto>> GetUserTrusts(string predicate, int userId);
    }
}
