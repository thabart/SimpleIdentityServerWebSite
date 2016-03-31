-- Insert the client
insert into dbo.clients (ClientId, ApplicationType, ClientName, ClientSecret, GrantTypes, IdTokenSignedResponseAlg, RedirectionUrls, ResponseTypes, TokenEndPointAuthMethod, DefaultMaxAge, RequireAuthTime)
VALUES ('SimpleIdentityServerWebSite', 0, 'Identity Server Web Site', 'SimpleIdentityServerWebSite', '0,1', 'RS256', 'http://localhost:3000', '0,1,2', 1, 0, 0)

-- Insert the scopes
insert into dbo.clientScopes VALUES('SimpleIdentityServerWebSite', 'openid');
insert into dbo.clientScopes VALUES('SimpleIdentityServerWebSite', 'profile');
insert into dbo.clientScopes VALUES('SimpleIdentityServerWebSite', 'role');