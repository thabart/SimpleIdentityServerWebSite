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

using Docker.DotNet;
using Docker.DotNet.Models;
using Docker.DotNet.X509;
using SimpleIdentityServer.WebSite.Api.Core.Factories;
using SimpleIdentityServer.WebSite.Api.Core.Validators;
using System;
using System.IO;
using System.Net;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;

namespace SimpleIdentityServer.WebSite.Api.Core.Controllers.Dockers.Operations
{
    public interface IStartDockerContainerOperation
    {
        void Execute(string name);
    }

    internal class StartDockerContainerOperation : IStartDockerContainerOperation
    {
        private readonly IContainerValidator _containerValidator;

        private readonly IDockerClientFactory _dockerClientFactory;

        #region Constructor

        public StartDockerContainerOperation(
            IContainerValidator containerValidator,
            IDockerClientFactory dockerClientFactory)
        {
            _containerValidator = containerValidator;
        }

        #endregion

        #region Public methods

        public void Execute(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            _containerValidator.CheckContainerExist(name);
            var dockerClient = _dockerClientFactory.GetDockerClient();
            // dockerClient.Containers.
            /*
            var assembly = Assembly.GetExecutingAssembly();
            ServicePointManager.ServerCertificateValidationCallback += (o, c, ch, er) => true;
            X509Certificate2 x509Certificate;
            using (var stream = assembly.GetManifestResourceStream("SimpleIdentityServer.WebSite.Api.Core.key.pfx"))
            {
                x509Certificate = new X509Certificate2(ReadStream(stream), string.Empty);
            }
            var certificateCredentials = new CertificateCredentials(x509Certificate);
            var configuration = new DockerClientConfiguration(new Uri("https://192.168.99.100:2376"), certificateCredentials);
            var dockerClient = configuration.CreateClient();
            var images = dockerClient.Images.ListImagesAsync(new ListImagesParameters
            {
                All = true
            }).Result;

            string s = "";
            */
        }

        #endregion
    }
}
