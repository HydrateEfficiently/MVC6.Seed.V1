import { HttpCacheService } from './services/http-cache-service';
import { HttpService } from './services/http-service';
import { UrlService } from './services/url-service';

import { LoggerFactory } from './services/logger-factory';

let commonServices = angular.module('ng_namespace_prefix_lower_.common.services', [])
    .service('ng_namespace_prefix_upper_HttpCacheService', HttpCacheService)
    .service('ng_namespace_prefix_upper_HttpService', HttpService)
    .service('ng_namespace_prefix_upper_UrlService', UrlService)
    .service('ng_namespace_prefix_upper_LoggerFactory', LoggerFactory)
    .name;

export { commonServices };