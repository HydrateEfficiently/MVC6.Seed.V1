import { Injectable } from './../../utility/injectable';

class Logger {
    constructor($log, categoryName) {
        this.$log = $log;
        this.categoryName = categoryName;
    }

    info(...args) {
        this._log('info', ...args);
    }

    debug(...args) {
        this._log('debug', ...args);
    }

    error(...args) {
        this._log('error', ...args);
    }

    warn(...args) {
        this._log('warn', ...args);
    }

    _log(logFuncName, ...args) {
        args.splice(0, 0, this.categoryName);
        args.splice(0, 0, logFuncName.toUpperCase());
        this.$log[logFuncName](...args);
    }
}

export class LoggerFactory extends Injectable {
    static get $inject() {
        return ['$log'];
    }

    constructor(...deps) {
        super(...deps);
    }

    createLogger(categoryName) {
        return new Logger(this.$log, categoryName);
    }
}