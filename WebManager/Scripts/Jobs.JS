var servive = {
    baseurl: "http://" + location.host + "/api",
    PauseTrigger: servive.baseurl + "/jobcommand/PauseTrigger"
};
var command = {
    PauseTrigger: function (name, group) {
        $.post(servive.pause, { name: name, group: group }, function (data) {
            alert(data);
        });
    }
};