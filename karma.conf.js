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
        'spec-bundle.js': ['webpack'] 
    },
    webpack: testWebpackConfig,
    // test results reporter to use
    // possible values: 'dots', 'progress'
    // available reporters: https://npmjs.org/browse/keyword/karma-reporter
    // reporters: [ 'mocha', 'coverage' ],
    port: 9876,
    colors: true,
    logLevel: config.LOG_INFO,
    autoWatch: false,
    browsers: [
      'PhantomJS'
    ],
    singleRun: true
  });
};