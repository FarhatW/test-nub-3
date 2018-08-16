import { AuthConfig } from 'angular-oauth2-oidc';

export const authConfig = {
  // issuer: 'http://localhost:5000',
  // clientId : 'jceFront',
  // redirectUri : 'http://localhost:5002/#/?',
  // responseType : 'id_token token',
  // scope : 'openid profile jce' ,
  // postLogoutRedirectUri : 'http://localhost:5002/#/dashboard',


  authority: 'http://localhost:5000',
  client_id: 'jceFront',
  redirect_uri: 'http://localhost:4200/signin-callback.html',
  post_logout_redirect_uri: 'http://localhost:4200',
  response_type: 'id_token token',
  scope: 'openid profile jce',
  silent_redirect_uri: 'http://localhost:4200/silent-renew.html',
  automaticSilentRenew: true,
  accessTokenExpiringNotificationTime: 4,
  // silentRequestTimeout:10000,
  filterProtocolClaims: true,
  loadUserInfo: true
}
