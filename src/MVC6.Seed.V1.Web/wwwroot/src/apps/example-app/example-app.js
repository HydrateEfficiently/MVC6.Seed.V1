import { ExampleAppController } from './example-app-controller';

import { commonServices } from './../../common/common-services';
import { commonFilters } from './../../common/common-filters';

let app = angular.module('ng_namespace_prefix_lower_.example-app.app', [
    commonServices,
    commonFilters
])
.controller('ExampleAppController', ExampleAppController);

import { appRunnerFactory } from './../../utility/app-runner-factory';
let run = appRunnerFactory(app);
export { run };