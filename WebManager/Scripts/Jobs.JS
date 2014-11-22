var baseurl= "http://" + location.host + "/api";
var servive = {
    pauseTrigger:baseurl+"/jobcommand/PauseTrigger"
};
var job = {
    pauseTrigger: function (name, group) {
        $.post(servive.pauseTrigger, { name: name, group: group }, function (data) {
            alert(data);
        });
    }
};