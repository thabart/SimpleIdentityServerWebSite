const AUTHORIZATION_URL = 'http://localhost:5002';
const API_URL = 'http://localhost:5001/api';
const metadata = {
    title: 'Simple Identity Server',
    baseUrl: '/',
    host: 'localhost',
    port: 3000,
    ENV: 'development',
    AUTHORIZATION_URL: AUTHORIZATION_URL,
    API_URL: API_URL
};

var path = require('path');
var currentUrl = "http://" + metadata.host + ":" + metadata.port;
var commonConfig = require('./webpack.common');
var webPackMerge = require('webpack-merge');
var DefinePlugin = require('webpack/lib/DefinePlugin');
var helpers = require('./helpers');

module.exports = webPackMerge(commonConfig, {
  metadata: metadata,
  plugins: [
      new DefinePlugin({
          'AUTHORIZATION_URL': JSON.stringify(metadata.AUTHORIZATION_URL),
          'API_URL': JSON.stringify(metadata.API_URL),
          'CURRENT_URL': JSON.stringify(currentUrl)
      })
  ],
  devServer: {
      port: metadata.port,
      host: metadata.host,
      outputPath: path.resolve('./builds')
  }
});
