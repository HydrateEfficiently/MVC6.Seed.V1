import { Injectable } from './../../utility/injectable';

export class HttpService extends Injectable {
    static get $inject() {
        return ['$http', 'ng_namespace_prefix_upper_LoggerFactory'];
    }

    constructor(...deps) {
        super(...deps);

        this.logger = this.ng_namespace_prefix_upper_LoggerFactory.createLogger('HttpService');
    }

    get(...args) {
        return this.$http.get(...args);

    }

    getModel(url, model) {
        let logger = this.logger;
        return this.get(url).then(result => {
            logger.debug(result);
            angular.merge(mode, result.data);
        });
    }
}