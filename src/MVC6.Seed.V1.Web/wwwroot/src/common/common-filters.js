function sanitizeHtml($sce) {
    // TODO: ng annotate
    return html => {
        return $sce.trustAsHtml(html);
    };
}

let commonFilters = angular.module('rr.common.filters', [])
    .filter('ng_namespace_prefix_lower_SanitizeHtml', sanitizeHtml)
    .name;

export { commonFilters };


