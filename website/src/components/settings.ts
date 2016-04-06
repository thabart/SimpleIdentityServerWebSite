export class Settings {
    getAuthorizationUrl() {
        return AUTHORIZATION_URL;
    }
    getCurrentUrl() {
        return CURRENT_URL;
    }
    getApiUrl() {
        return API_URL;
    }
    getClientId() {
        return "SimpleIdentityServerWebSite";
    }
    getClientSecret() {
        return "SimpleIdentityServerWebSite";
    }
}