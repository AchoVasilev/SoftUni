var RequestClass = /** @class */ (function () {
    function RequestClass(method, uri, version, message) {
        this.method = method;
        this.uri = uri;
        this.version = version;
        this.message = message;
        this.response = undefined;
        this.fulfilled = true;
    }
    return RequestClass;
}());
var myData = new RequestClass('GET', 'http://google.com', 'HTTP/1.1', '');
console.log(myData);
