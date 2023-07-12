window.dignite = {};

(() => {

    // Read the appsettings. json file
    // Store the read JSON data in dignite.appSettings
    // Automatically read after page loading
    function readAppSettingsJson() {
        var base = document.getElementsByTagName("base")[0];
        var url = base.getAttribute("href") + "appsettings.json";
        var request = new XMLHttpRequest();
        request.open("get", url);
        request.send(null);
        request.onload = function () {
            if (request.status == 200) {
                var data = JSON.parse(request.responseText);
                dignite.appSettings = data;
            }
        }
    }

    // Automatically read after page loading
    readAppSettingsJson();
})();