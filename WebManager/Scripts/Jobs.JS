var baseurl= "http://" + location.host + "/api";
var servive = {
    pauseTrigger: baseurl + "/jobcommand/PauseTrigger",
    resumeTrigger: baseurl + "/jobcommand/ResumeTrigger",
    rescheduleJob: baseurl + "/jobcommand/RescheduleJob",
    triggerJob: baseurl + "/jobcommand/TriggerJob",
    addHttpJob: baseurl + "/jobcommand/addHttpJob"
    
};
var job = {
    pauseTrigger: function (name, group) {
        $.post(servive.pauseTrigger, { name: name, group: group }, function (data) {
            alert(data);
            location.reload()
        });
    },
    resumeTrigger: function (name, group) {

        $.post(servive.resumeTrigger, { name: name, group: group }, function (data) {
            alert(data);
            location.reload()
        });
    },
    rescheduleJob: function (name, group,val) {
        $.post(servive.rescheduleJob, { name: name, group: group,cron:val }, function (data) {
            location.reload()
        });
    },
    triggerJob: function (name, group) {
        $.post(servive.triggerJob, { name: name, group: group }, function (data) {
            alert(data);
            location.reload()
        })
    },
    addHttpJob: function () {
        //var jobname = $("#httpjobname").val();
        //var groupname = $("#httpgroupname").val();
        //var cron = $("#httptriggercron").val();
        //var url = $("#httpjoburl").val();

        $.post(servive.addHttpJob, $(addjob).serialize(), function (data) {
            alert(data);
            location.href="/home/index"
        })

    }
};

var jbox = {
    cronUpdate: function (name,group,cron) {
        layer.prompt({ title: '要修改的值', val: cron }, function (val) {
            job.rescheduleJob(name, group, val);
        });
    },
    addHttpJob:function()
    {
        var pageii = $.layer({
            type: 1,
            area: ['310px', '300px'],
            closeBtn: [0, true], //去掉默认关闭按钮
            page: {
                html: $("#loginForm").html()
            }
        });

        ////自设关闭
        //$('#pagebtn').on('click', function () {
        //    layer.close(pageii);
        //});
    }


}
