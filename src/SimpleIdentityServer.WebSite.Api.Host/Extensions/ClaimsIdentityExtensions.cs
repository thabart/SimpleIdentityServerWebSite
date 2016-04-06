#region copyright
// Copyright 2015 Habart Thierry
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//     http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
#endregion

using System.Linq;
using System.Security.Claims;

namespace SimpleIdentityServer.WebSite.Api.Host.Extensions
{
    internal static class ClaimsIdentityExtensions
    {
        #region Public static methods

        public static string GetSubject(this ClaimsIdentity claimsIdentity)
        {
            if (claimsIdentity.Claims == null)
            {
                return null;
            }

            var claim = claimsIdentity.Claims.FirstOrDefault(c => c.Type == Constants.ClaimNames.Subject);
            if (claim == null)
            {
                return null;
            }

            return claim.Value;
        }

        #endregion
    }
}
