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
      filename: '[name].js'
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
              test: /\.scss$/,
              loaders: [ 'raw', 'sass' ]
          },
          {
              test: /\.css$/,
              loader:'raw'
          },
          {
              test: /bootstrap\/dist\/js\/umd\//,
              loader: 'imports?jQuery=jquery'
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
      }),
      new ProvidePlugin({
          "window.Tether": "tether"
      })
  ],
  devServer: {
      port: metadata.port,
      host: metadata.host
  }
};