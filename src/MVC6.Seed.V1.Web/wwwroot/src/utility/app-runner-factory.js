export function appRunnerFactory(app) {
    return function (data) {
        app.constant('ng_namespace_prefix_upper_ServerData', data);
        angular.element(document).ready(() => angular.bootstrap(document, [app.name]));
    };
}