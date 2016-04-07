import { HttpCacheService } from './services/http-cache-service';
import { HttpService } from './services/http-service';
import { UrlService } from './services/url-service';

import { LoggerFactory } from './services/logger-factory';

let commonServices = angular.module('osp.common.services', [])
    .service('OspHttpCacheService', HttpCacheService)
    .service('OspHttpService', HttpService)
    .service('OspUrlService', UrlService)
    .service('OspLoggerFactory', LoggerFactory)
    .name;

export { commonServices };