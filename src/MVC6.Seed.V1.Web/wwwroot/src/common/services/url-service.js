import { Injectable } from './../../utility/injectable';

export class UrlService extends Injectable {
    static get $inject() {
        return ['ng_namespace_prefix_upper_ServerData'];
    }

    constructor(...deps) {
        super(...deps);

        this.serverData = this.ng_namespace_prefix_upper_ServerData;
    }

    getUrls() {
        return this.serverData && this.serverData.urls ? this.serverData.urls : null;
    }

    getUrl(path, params) {
        let currentItem = this.getUrls();
        if (!currentItem) {
            return null;
        }

        let pathComponents = path.split('.');
        for (let i = 0; i < pathComponents.length; i++) {
            currentItem = currentItem[pathComponents[i]];
            if (!currentItem) {
                return null;
            }
        }

        if (typeof currentItem !== 'string') {
            return null;
        }

        return this.resolveUrl(currentItem, params);
    }

    resolveUrl(url, params) {
        if (params) {
            let paramsToReplace = url.split('/').filter(s =>
                s.indexOf(encodeURIComponent(':').toLowerCase()) === 0);

            if (typeof params !== 'object') {
                if (paramsToReplace.length === 1) {
                    url = url.replace(paramsToReplace[0], params);
                    return url;
                }
            }

            for (let key in params) {
                let param = encodeURIComponent(`:${key}`).toLowerCase();
                url = url.replace(param, params[key]);
            }
        }
        return url;
    }
}