import { HttpCacheService } from './services/http-cache-service';
import { HttpService } from './services/http-service';
import { UrlService } from './services/url-service';

import { LoggerFactory } from './services/logger-factory';

let commonServices = angular.module('angular-namespace-prefix.common.services', [])
    .service('AngularComponentNamePrefixHttpCacheService', HttpCacheService)
    .service('AngularComponentNamePrefixHttpService', HttpService)
    .service('AngularComponentNamePrefixUrlService', UrlService)
    .service('AngularComponentNamePrefixLoggerFactory', LoggerFactory)
    .name;

export { commonServices };