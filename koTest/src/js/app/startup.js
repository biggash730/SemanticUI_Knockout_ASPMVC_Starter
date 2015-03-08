define(["jquery", "knockout", "router", "semantic", "knockout-projections"], function($, ko, router) {
  // Components can be packaged as AMD modules, such as the following:
  ko.components.register('nav-bar', { require: 'components/nav-bar/nav-bar' });
  ko.components.register('home-page', { require: 'components/home-page/home' });
  ko.components.register('dashboard', { require: 'components/dashboard/dashboard' });
  ko.components.register('settings', { require: 'components/settings/settings' });
  ko.components.register('reports', { require: 'components/reports/reports' });
  ko.components.register('users', { require: 'components/users/users' });

  // ... or for template-only components, you can just point to a .html file directly:
  ko.components.register('about-page', {
    template: { require: 'text!components/about-page/about.html' }
  });

  // [Scaffolded component registrations will be inserted here. To retain this feature, don't remove this comment.]

  // Start the application
  ko.applyBindings({ route: router.currentRoute });
});