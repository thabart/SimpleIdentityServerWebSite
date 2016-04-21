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

using SimpleIdentityServer.WebSite.Api.Core.Controllers.Dockers.Operations;

namespace SimpleIdentityServer.WebSite.Api.Core.Controllers.Dockers
{
    public interface IDockerOperations
    {
        void StartContainer(string containerName);

        void StopContainer(string containerName);
    }

    public class DockerOperations : IDockerOperations
    {
        private readonly IStartDockerContainerOperation _startDockerContainerOperation;

        private readonly IStopDockerContainerOperation _stopDockerContainerOperation;

        #region Constructor

        public DockerOperations(
            IStartDockerContainerOperation startDockerContainerOperation,
            IStopDockerContainerOperation stopDockerContainerOperation)
        {
            _startDockerContainerOperation = startDockerContainerOperation;
            _stopDockerContainerOperation = stopDockerContainerOperation;
        }

        #endregion

        #region Public methods

        public void StartContainer(string containerName)
        {
            _startDockerContainerOperation.Execute(containerName);
        }

        public void StopContainer(string containerName)
        {
            _stopDockerContainerOperation.Execute(containerName);
        }

        #endregion
    }
}
