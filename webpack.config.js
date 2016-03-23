module.exports = {
  entry: './src/main.ts',
  output: {
      path: 'builds',
      filename: 'bundle.js'
  },
  module: {
      loaders: [
          {
              test: /\.ts$/,
              loader: 'ts'
          }
      ]
  }
};