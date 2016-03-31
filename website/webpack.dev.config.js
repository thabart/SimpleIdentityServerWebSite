const AUTHORIZATION_URL = 'http://localhost:5002';
const metadata = {
    title: 'Simple Identity Server',
    baseUrl: '/',
    host: 'localhost',
    port: 3000,
    ENV: 'development',
    AUTHORIZATION_URL: AUTHORIZATION_URL
};

var currentUrl = "http://" + metadata.host + ":" + metadata.port;
var commonConfig = require('./webpack.common');
var webPackMerge = require('webpack-merge');
var DefinePlugin = require('webpack/lib/DefinePlugin');

module.exports = webPackMerge(commonConfig, {
  metadata: metadata,
  plugins: [
      new DefinePlugin({
          'AUTHORIZATION_URL': JSON.stringify(metadata.AUTHORIZATION_URL),
          'CURRENT_URL': JSON.stringify(currentUrl)
      })
  ],
  devServer: {
      port: metadata.port,
      host: metadata.host
  }
});