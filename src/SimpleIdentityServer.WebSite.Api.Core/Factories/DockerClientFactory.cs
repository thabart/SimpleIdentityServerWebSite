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
using Docker.DotNet.BasicAuth;
using Docker.DotNet.X509;
using SimpleIdentityServer.WebSite.Api.Core.Errors;
using SimpleIdentityServer.WebSite.Api.Core.Exceptions;
using System.Net;

namespace SimpleIdentityServer.WebSite.Api.Core.Factories
{
    public interface IDockerClientFactory
    {
        DockerClient GetDockerClient();
    }

    internal class DockerClientFactory : IDockerClientFactory
    {
        private readonly WebSiteOptions _webSiteOptions;

        #region Constructor

        public DockerClientFactory(WebSiteOptions webSiteOptions)
        {
            _webSiteOptions = webSiteOptions;
        }

        #endregion

        #region Public methods

        public DockerClient GetDockerClient()
        {
            if (_webSiteOptions.DockerApiUri == null)
            {
                throw new IdentityServerException(
                    ErrorCodes.InternalError,
                    ErrorDescriptions.TheDockerApiUriIsMissing);
            }

            var credentials = GetCredentials(_webSiteOptions);

            if (_webSiteOptions.IsHttpsAuthentication &&
                _webSiteOptions.IsCertificateSelfSigned)
            {
                ServicePointManager.ServerCertificateValidationCallback += (o, c, ch, er) => true;
            }

            var configuration = new DockerClientConfiguration(_webSiteOptions.DockerApiUri, credentials);
            return configuration.CreateClient();
        }

        #endregion

        #region Private static methods

        private static Credentials GetCredentials(WebSiteOptions webSiteOptions)
        {
            if (webSiteOptions.IsHttpsAuthentication)
            {
                if (webSiteOptions.Certificate == null)
                {
                    throw new IdentityServerException(
                        ErrorCodes.InternalError,
                        ErrorDescriptions.TheCertificateIsMissing);
                }

                return new CertificateCredentials(webSiteOptions.Certificate);
            }

            if (string.IsNullOrWhiteSpace(webSiteOptions.UserName) ||
                string.IsNullOrWhiteSpace(webSiteOptions.Password))
            {
                throw new IdentityServerException(
                    ErrorCodes.InternalError,
                    ErrorDescriptions.TheUserCredentialsAreMissing);
            }

            return new BasicAuthCredentials(webSiteOptions.UserName, webSiteOptions.Password);
        }

        #endregion
    }
}
