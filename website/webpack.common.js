'use strict';

var path = require('path'),
    srcPath = path.join(__dirname, 'src'),
    helpers = require('./helpers');
var HtmlWebpackPlugin = require('html-webpack-plugin'),
    ProvidePlugin = require('webpack/lib/ProvidePlugin'),
    DefinePlugin = require('webpack/lib/DefinePlugin'),
    CopyWebpackPlugin = require('copy-webpack-plugin');
    
module.exports = {
  entry: {
      'vendors': path.join(srcPath, 'vendors.ts'),
      'main': path.join(srcPath, 'main.ts')
  },  
  output: {
      path: path.resolve('./builds'),
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
                  helpers.root('src/index.html')
              ]
          },
          {
              test: /\.scss$/,
              loaders: [ 'raw', 'sass' ]
          },
          {
              test: /\.css$/,
              loaders: ['style', 'css']
          },
          {
              test: /bootstrap\/dist\/js\/umd\//,
              loader: 'imports?jQuery=jquery'
          },
          {
              test: /\.(png|jpe?g|gif|svg|woff|woff2|ttf|eot|ico)$/, 
              loader: 'file?name=fonts/[name].[hash].[ext]?'
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
      }),
      new CopyWebpackPlugin([{
        from: helpers.root('src/styles')
      }])
  ]
};