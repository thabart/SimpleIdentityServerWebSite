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

using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Mvc;
using SimpleIdentityServer.WebSite.Api.Core.Controllers.Profiles;
using SimpleIdentityServer.WebSite.Api.Core.Exceptions;
using SimpleIdentityServer.WebSite.Api.Host.DTOs.Requests;
using SimpleIdentityServer.WebSite.Api.Host.DTOs.Responses;
using SimpleIdentityServer.WebSite.Api.Host.Errors;
using SimpleIdentityServer.WebSite.Api.Host.Extensions;
using System;
using System.Net;
using System.Security.Claims;

namespace SimpleIdentityServer.WebSite.Api.Host.Controllers
{
    [Route(Constants.RouteValues.Profile)]
    public class ProfileController : Controller
    {
        private readonly IProfileActions _profileActions;

        #region Constructor

        public ProfileController(IProfileActions profileActions)
        {
            _profileActions = profileActions;
        }

        #endregion

        #region Public methods

        [HttpGet(Constants.ProfileActions.CurrentProfile)]
        [Authorize]
        public ProfileResponse GetCurrentProfile()
        {
            var subject = GetSubject();
            return _profileActions
                .GetCurrentProfile(subject)
                .ToResponse();
        }

        [HttpPost]
        [Authorize]
        public ProfileResponse PostProfile([FromBody] PostProfileRequest postProfileRequest)
        {
            if (postProfileRequest == null)
            {
                throw new ArgumentNullException(nameof(postProfileRequest));
            }

            var subject = GetSubject();
            var parameter = postProfileRequest.ToParameter();
            parameter.Subject = subject;
            var profile = _profileActions.AddProfile(parameter);
            if (profile == null)
            {
                Response.StatusCode = (int)HttpStatusCode.NoContent;
                return null;
            }

            return profile.ToResponse();
        }

        #endregion

        #region Private methods

        private string GetSubject()
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            if (claimsIdentity == null)
            {
                throw new IdentityServerException(ErrorCodes.InternalErrorCode,
                    ErrorDescriptions.TheClaimsCannotBeRetrieved);
            }

            var subject = claimsIdentity.GetSubject();
            if (string.IsNullOrWhiteSpace(subject))
            {
                throw new IdentityServerException(ErrorCodes.InternalErrorCode,
                    ErrorDescriptions.TheSubjectCannotBeRetrieved);
            }

            return subject;
        }

        #endregion
    }
}
