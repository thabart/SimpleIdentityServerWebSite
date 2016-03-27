var path = require('path'),
    helpers = require('./helpers');
var root = function(args) {
  args = Array.prototype.slice.call(arguments, 0);
  return path.join.apply(path, [__dirname].concat(args));
}

module.exports = {
    module: {
        loaders: [
            {
                test: /\.ts$/,
                loader: 'awesome-typescript'
            },
            {
              test: /\.scss$/,
              loaders: [ 'raw', 'sass' ]
            },
            {
              test: /\.css$/,
              loader:'raw'
            }
        ]
    }
};