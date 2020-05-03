var myApp = angular.module('myDirective', []);


myApp.directive('convertToNumber', function () {
    return {
        require: 'ngModel',
        link: function (scope, element, attrs, ngModel) {
            ngModel.$parsers.push(function (val) {
                return parseInt(val, 10);
            });
            ngModel.$formatters.push(function (val) {
                return '' + val;
            });
        }
    };
});


myApp.directive('fileModel', ['$parse', function ($parse) {
    return {
        restrict: 'A',
        link: function (scope, element, attrs) {
            var model = $parse(attrs.fileModel);
            var modelSetter = model.assign;

            element.bind('change', function () {
                scope.$apply(function () {
                    modelSetter(scope, element[0].files[0]);
                });
            });
        }
    };
}]);

myApp.service('fileUpload', ['$http', function ($http) {
    this.uploadFileToUrl = function (file, uploadUrl, data, callback) {
        var fd = new FormData();
        fd.append('file', file);
        //console.log(JSON.stringify(data));
        fd.append('data', JSON.stringify(data));

        $http(
            {
                method: 'POST',
                url: uploadUrl,
                data: fd,
                headers: {
                    'Content-Type': undefined
                },
                transformRequest: angular.identity
            }
        ).then(function success(res) {
            callback(res);
        });
    }
}]);

var app2 = angular.module('ngMathJax', []);
app2.directive('ngMathJax', [
    '$document', function ($document) {
        return {
            link: function (scope, element) {
                if (element.find('math').length > 0) {
                    if (!window.MathJax) {
                        return $.getScript('https://cdnjs.cloudflare.com/ajax/libs/mathjax/2.7.1/MathJax.js', function () {
                            return MathJax.Hub.Queue(["Typeset", MathJax.Hub, element[0]]);
                        });
                    } else {
                        return MathJax.Hub.Queue(["Typeset", MathJax.Hub, element[0]]);
                    }
                }
            }
        };
    }
]);

