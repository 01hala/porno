const { createProxyMiddleware } = require('http-proxy-middleware');

module.exports = function(app) {
  app.use(
    '/user_login',
    createProxyMiddleware({
      target: 'http://drawing.ucat.games:8001/',
      changeOrigin: true,
    })
  );
  app.use(
    '/guest_login',
    createProxyMiddleware({
      target: 'http://drawing.ucat.games:8001/',
      changeOrigin: true,
    })
  );
  app.use(
    '/user_create',
    createProxyMiddleware({
      target: 'http://drawing.ucat.games:8001/',
      changeOrigin: true,
    })
  );
  app.use(
    '/query_home',
    createProxyMiddleware({
      target: 'http://drawing.ucat.games:8001/',
      changeOrigin: true,
    })
  );
}