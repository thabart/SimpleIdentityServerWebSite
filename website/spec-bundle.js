'use strict';

Error.stackTraceLimit = Infinity;
require('phantomjs-polyfill');
require('es6-promise');
require('es6-shim');
require('es7-reflect-metadata/dist/browser');

// require('zone.js/dist/zone-microtask.js');
// require('zone.js/dist/long-stack-trace-zone.js');
// require('zone.js/dist/jasmine-patch.js');
// require('jquery/dist/jquery.js');

var appContext = require.context('./src', true, /\.spec\.ts/);
appContext.keys().forEach(appContext);

var domAdapter = require('angular2/src/platform/browser/browser_adapter');
domAdapter.BrowserDomAdapter.makeCurrent();