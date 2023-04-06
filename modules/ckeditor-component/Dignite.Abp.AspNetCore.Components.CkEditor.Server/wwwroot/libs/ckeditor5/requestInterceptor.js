

(function (send) {
    XMLHttpRequest.prototype.send = function (data) {
        var token = abp.utils.getCookieValue('XSRF-TOKEN');
        this.setRequestHeader("RequestVerificationToken", token)
        return send.call(this, data);
    };
})(XMLHttpRequest.prototype.send);