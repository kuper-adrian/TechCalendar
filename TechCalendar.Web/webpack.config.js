const path = require('path');
const webpack = require('webpack');
const CopyWebpackPlugin = require('copy-webpack-plugin');

// assets.js
const Assets = require('./assets');

module.exports = {
  entry: {
    site: "./wwwroot/js/site.js",
  },
  output: {
    path: __dirname + "/wwwroot/dist",
    filename: "[name].bundle.js"
  },
  module: {
    rules: [
      { test: /\.css?$/, use: ['style-loader', 'css-loader'] },
    ]
  },
  plugins: [
    new CopyWebpackPlugin(
      Assets.map(asset => {
        return {
          from: path.resolve(__dirname, `./node_modules/${asset.from}`),
          to: path.resolve(__dirname, `./wwwroot/lib/${asset.to}`)
        };
      })
    )
  ]
};