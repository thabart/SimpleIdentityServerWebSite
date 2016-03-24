const metadata = {
    title: 'Simple Identity Server',
    baseUrl: '/',
    host: 'localhost',
    port: 3000,
    ENV: 'development'
};

var path = require('path'),
    srcPath = path.join(__dirname, 'src'),
    webpack = require('webpack'),
    HtmlWebpackPlugin = require('html-webpack-plugin');

module.exports = {
  metadata: metadata,
  entry: {
      'vendors': path.join(srcPath, 'vendors.ts'),
      'main': path.join(srcPath, 'main.ts')
  },
  output: {
      path: 'builds',
      filename: '[name].js',
      sourceMapFileName: '[name].js.map',
      chunkFileName: '[id].chunk.js'
  },
  module: {
      loaders: [
          {
              test: /\.ts$/,
              loader: 'awesome-typescript'
          }
      ]
  },
  plugins: [
      new HtmlWebpackPlugin({
          template: 'src/index.html',
          chunksSortMode: 'none'
      })
  ],
  devServer: {
      port: metadata.port,
      host: metadata.host
  }
};