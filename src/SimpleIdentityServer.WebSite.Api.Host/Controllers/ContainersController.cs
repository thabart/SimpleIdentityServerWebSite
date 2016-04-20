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

using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Authorization;
using System;
using SimpleIdentityServer.WebSite.Api.Core.Controllers.Dockers.Operations;

namespace SimpleIdentityServer.WebSite.Api.Host.Controllers
{
    [Route(Constants.RouteValues.Container)]
    public class ContainersController : Controller
    {
        private readonly IStartDockerContainerOperation _startDockerContainerOperation;
        
        #region Constructor

        public ContainersController(IStartDockerContainerOperation startDockerContainerOperation)
        {
            _startDockerContainerOperation = startDockerContainerOperation;
        }

        #endregion

        #region Public methods

        [HttpGet(Constants.DockerActions.Start)]
        // [Authorize]
        public bool GetStart(string name)
        {
            _startDockerContainerOperation.Execute("name");
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            return true;
        }
        
        [HttpGet(Constants.DockerActions.Stop)]
        // [Authorize]
        public bool GetStop(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            return true;
        }

        #endregion
    }
}
