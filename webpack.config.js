const metadata = {
    title: 'Simple Identity Server',
    baseUrl: '/',
    host: 'localhost',
    port: 3000,
    ENV: 'development'
};

var path = require('path'),
    srcPath = path.join(__dirname, 'src'),
    webpack = require('webpack');
var HtmlWebpackPlugin = require('html-webpack-plugin'),
    ProvidePlugin = require('webpack/lib/ProvidePlugin');

var root = function(args) {
  args = Array.prototype.slice.call(arguments, 0);
  return path.join.apply(path, [__dirname].concat(args));
}

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
          },
          {
              test: /\.html$/,
              loader: 'raw',
              exclude: [
                  root('src/index.html')
              ]
          }, 
          {
              test: /bootstrap\/dist\/js\/umd\//,
              loader: 'imports?JQuery=jquery'
          }
      ]
  },
  plugins: [
      new HtmlWebpackPlugin({
          template: 'src/index.html',
          chunksSortMode: 'none'
      }),
      new ProvidePlugin({
            jQuery: 'jquery',
            $: 'jquery',
            jquery: 'jquery'
      })
  ],
  devServer: {
      port: metadata.port,
      host: metadata.host
  }
};