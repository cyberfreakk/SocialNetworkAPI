using Ocelot.Authorisation;
using Ocelot.DownstreamRouteFinder.UrlMatcher;
using Ocelot.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ApiGateway.Decorators
{
    public class ClaimAuthorizerDecorator : IClaimsAuthoriser
    {
        private readonly ClaimsAuthoriser _authoriser;

        public ClaimAuthorizerDecorator(ClaimsAuthoriser authoriser)
        {
            _authoriser = authoriser;
        }

        Response<bool> IClaimsAuthoriser.Authorise(ClaimsPrincipal claimsPrincipal, 
            Dictionary<string, string> routeClaimsRequirement,
            List<PlaceholderNameAndValue> urlPathPlaceholderNameAndValues)

        {
            var newRouteClaimsRequirement = new Dictionary<string, string>();
            foreach (var kvp in routeClaimsRequirement)
            {
                if (kvp.Key.StartsWith("http///"))
                {
                    var key = kvp.Key.Replace("http///", "http://");
                    newRouteClaimsRequirement.Add(key, kvp.Value);
                }
                else
                {
                    newRouteClaimsRequirement.Add(kvp.Key, kvp.Value);
                }
            }

            return _authoriser.Authorise(claimsPrincipal, newRouteClaimsRequirement, urlPathPlaceholderNameAndValues);
        }

        
    }
}