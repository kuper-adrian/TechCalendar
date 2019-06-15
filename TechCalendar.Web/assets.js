const JS = [
  // bootstrap
  {
    from: 'bootstrap/dist/js/bootstrap.min.js',
    to: 'bootstrap/',
  },
  {
    from: 'bootstrap/dist/js/bootstrap.bundle.min.js',
    to: 'bootstrap/',
  },

  // jquery
  {
    from: 'jquery/dist/jquery.min.js',
    to: 'jquery/',
  },
  {
    from: 'jquery-validation/dist/jquery.validate.min.js',
    to: 'jquery-validation/',
  },
  {
    from: 'jquery-validation-unobtrusive/dist/jquery.validate.unobtrusive.min.js',
    to: 'jquery-validation-unobtrusive/',
  },

  // fullcalendar
  {
    from: '@fullcalendar/core/main.min.js',
    to: 'fullcalendar/core/',
  },
  {
    from: '@fullcalendar/daygrid/main.min.js',
    to: 'fullcalendar/daygrid/',
  },
  {
    from: '@fullcalendar/list/main.min.js',
    to: 'fullcalendar/list/',
  },
];

const CSS = [
  // bootstrap
  {
    from: 'bootstrap/dist/css/bootstrap.min.css',
    to: 'bootstrap/',
  },
  
  // fullcalendar
  {
    from: '@fullcalendar/core/main.min.css',
    to: 'fullcalendar/core/',
  },
  {
    from: '@fullcalendar/daygrid/main.min.css',
    to: 'fullcalendar/daygrid/',
  },
  {
    from: '@fullcalendar/list/main.min.css',
    to: 'fullcalendar/list/',
  },
];

module.exports = [...JS, ...CSS];