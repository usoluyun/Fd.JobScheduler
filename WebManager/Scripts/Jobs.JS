var baseurl= "http://" + location.host + "/api";
var servive = {
    pauseTrigger: baseurl + "/jobcommand/PauseTrigger",
    resumeTrigger: baseurl + "/jobcommand/ResumeTrigger"
};
var job = {
    pauseTrigger: function (name, group, instance) {
        $.post(servive.pauseTrigger, { name: name, group: group }, function (data) {
            alert(data);
            location.reload()
        });
    },
    resumeTrigger: function (name, group, instance) {
       
        $.post(servive.resumeTrigger, { name: name, group: group }, function (data) {
            alert(data);
            location.reload()
        });
    }
};