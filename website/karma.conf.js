module.exports = function(config) {
  var testWebpackConfig = require('./webpack.test.js');
  config.set({
    basePath: '',
    frameworks: ['jasmine'],
    exclude: [ ],
    files: [
        { 
            pattern: 'spec-bundle.js', 
            watched: false 
        } 
    ],
    preprocessors: { 
        'spec-bundle.js': ['webpack', 'sourcemap'] 
    },
    webpack: testWebpackConfig,
    webpackServer: { noInfo: true },
    reporters: [ 'mocha'],
    port: 9876,
    colors: true,
    logLevel: config.LOG_INFO,
    autoWatch: false,
    browsers: [
      'PhantomJS'
    ],
    singleRun: true
    /*,
    plugins: [
        'karma-phantomjs-launcher'
    ]
    */
  });
};