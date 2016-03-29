var path = require('path'),
    helpers = require('./helpers');
var root = function(args) {
  args = Array.prototype.slice.call(arguments, 0);
  return path.join.apply(path, [__dirname].concat(args));
}

module.exports = {
    resolve: {
        cache: false,
        extensions: ['', '.ts', '.js', '.json', '.css', '.html']
    },    
    devtool: 'inline-source-map',
    module: {
        loaders: [
            {
                test: /\.ts$/,
                loader: 'awesome-typescript',
                query: {
                    'compilerOptions': {
                        'removeComments': true,
                        'noEmitHelpers': true,
                        'experimentalDecorators': true
                    }
                },
                exclude: [/node_modules/]
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