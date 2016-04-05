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

using Moq;
using SimpleIdentityServer.WebSite.Api.Core.Controllers.Profiles.Actions;
using SimpleIdentityServer.WebSite.Api.Core.Errors;
using SimpleIdentityServer.WebSite.Api.Core.Exceptions;
using SimpleIdentityServer.WebSite.Api.Core.Models;
using SimpleIdentityServer.WebSite.Api.Core.Repositories;
using System;
using Xunit;

namespace SimpleIdentityServer.WebSite.Api.Core.Tests.Controllers.Profiles.Actions
{
    public class GetCurrentProfileActionFixture
    {
        private Mock<IProfileRepository> _profileRepositoryStub;

        private IGetCurrentProfileAction _getCurrentProfileAction;

        #region Exceptions

        [Fact]
        public void When_Passing_No_Subject_Then_Exception_Is_Thrown()
        {
            // ARRANGE
            InitializeFakeObjects();

            // ACT & ASSERT
            Assert.Throws<ArgumentNullException>(() => _getCurrentProfileAction.Execute(null));
        }

        [Fact]
        public void When_Profile_Doesnt_Exist_Then_Exception_Is_Thrown()
        {
            // ARRANGE
            const string subject = "subject";
            InitializeFakeObjects();
            _profileRepositoryStub.Setup(p => p.GetProfile(It.IsAny<string>()))
                .Returns(() => null);

            // ACT & ASSERT
            var exception = Assert.Throws<IdentityServerException>(() => _getCurrentProfileAction.Execute(subject));
            Assert.NotNull(exception);
            Assert.True(exception.Code == ErrorCodes.LoginRequiredCode);
            Assert.True(exception.Message == string.Format(ErrorDescriptions.TheProfileDoesntExist, subject));
        }

        [Fact]
        public void When_Profile_Is_Not_Active_Then_Exception_Is_Trhown()
        {
            // ARRANGE
            const string subject = "subject";
            InitializeFakeObjects();
            _profileRepositoryStub.Setup(p => p.GetProfile(It.IsAny<string>()))
                .Returns(new Profile
                {
                    IsActive = false
                });

            // ACT & ASSERT
            var exception = Assert.Throws<IdentityServerException>(() => _getCurrentProfileAction.Execute(subject));
            Assert.NotNull(exception);
            Assert.True(exception.Code == ErrorCodes.LoginRequiredCode);
            Assert.True(exception.Message == string.Format(ErrorDescriptions.TheProfileIsNotActive, subject));
        }

        #endregion

        #region Happy path

        [Fact]
        public void When_Requesting_Profile_Then_It_Is_Returned()
        {
            // ARRANGE
            const string subject = "subject";
            var profile = new Profile
            {
                IsActive = true,
                AuthorizationServerUrl = "authorization_server_url"
            };
            InitializeFakeObjects();
            _profileRepositoryStub.Setup(p => p.GetProfile(It.IsAny<string>()))
                .Returns(profile);

            // ACT
            var result = _getCurrentProfileAction.Execute(subject);

            // ASSERTS
            Assert.NotNull(result);
            Assert.True(result.AuthorizationServerUrl == profile.AuthorizationServerUrl);
        }

        #endregion

        private void InitializeFakeObjects()
        {
            _profileRepositoryStub = new Mock<IProfileRepository>();
            _getCurrentProfileAction = new GetCurrentProfileAction(_profileRepositoryStub.Object);
        }
    }
}
