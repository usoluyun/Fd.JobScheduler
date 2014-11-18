﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebManager._Default" %>

<!DOCTYPE html>
<html lang="en" xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>CrystalQuartz Panel</title>
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <link href='Styles/bootstrap.css' rel='stylesheet' type='text/css' />
    <link href='Styles/main.css' rel='stylesheet' type='text/css' />
    <link href='Styles/Fonts/flaticon.css' rel='stylesheet' type='text/css' />
</head>
<body>
    <form>
    <div id="application">
        <header id="mainHeader" class="cq-main-header">
            <div class="cq-main-header-wrapper">
                <div class="container">
                    <div class="row">
                        <div class="cq-header-item cq-scheduler col-xs-9 col-md-5">
                            <div id="schedulerStatus" class="cq-scheduler-status" title="Status: undefined"></div>
                            <h1 id="schedulerName" class="cq-scheduler-name"></h1>
                        </div>

                        <div id="commandIndicator" class="col-xs-3 col-md-2 cq-header-item"><section class="cq-busy" style="display: none;"><div class="cq-busy-image"><img src="/Images/loading.gif"></div><div id="currentCommand" class="cq-current-command">Loading scheduler data</div></section></div>

                        <div class="col-xs-3 col-md-5 cq-header-item visible-md-block visible-lg-block">
                          <%--  <a class="cq-leave-link" href="/">return to the main site</a>--%>
                        </div>
                    </div>
                </div>
            </div>
        </header>

        <div class="cq-main-content">
            <section id="schedulerPropertiesContainer" class="cq-scheduler-properties">
                <div class="container">
                    <div class="row">
                        <div class="col-sm-12 col-md-2">
                            <a id="startSchedulerButton" class="cq-button disabled" href="#">
                                <span class="cq-icon flaticon-play"></span> Start scheduler
                            </a>
                            <a id="stopSchedulerButton" class="cq-button disabled" href="#">
                                <span class="cq-icon flaticon-power"></span> Shutdown
                            </a>
                            <a id="refreshData" class="cq-button" href="#">
                                <span class="cq-icon flaticon-refresh"></span> Refresh
                            </a>
                        </div>
                        <div class="col-sm-12 col-md-5">
                            <table class="cq-info-table">
                                <thead>
                                    <tr>
                                        <th colspan="2">Statistics</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr>
                                        <th>Running since:</th>
                                        <td class="runningSince"><%=sd.Data.RunningSince %></td>
                                    </tr>
                                    <tr>
                                        <th>Total Jobs:</th>
                                        <td class="totalJobs"></td>
                                    </tr>
                                    <tr>
                                        <th>Executed Jobs:</th>
                                        <td class="executedJobs"><%=sd.Data.JobsExecuted %></td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                        <div class="col-sm-12 col-md-5">
                            <table class="cq-info-table">
                                <thead>
                                    <tr>
                                        <th colspan="2">Properties</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr>
                                        <th>Name:</th>
                                        <td class="schedulerName"><%=sd.Data.SchedulerName %></td>
                                    </tr>
                                    <tr>
                                        <th>Instance ID:</th>
                                        <td class="instanceId"><%=sd.Data.InstanceId %></td>
                                    </tr>
                                    <tr>
                                        <th>Is remote:</th>
                                        <td class="isRemote"><%=sd.Data.IsRemote %></td>
                                    </tr>
                                    <tr>
                                        <th>Scheduler type:</th>
                                        <td class="schedulerType"><%=sd.Data.SchedulerType %></td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>

            </section>
        </div>

        <div class="container" style="margin-top:250px">
         
                <article class="cq-job-group">
                    <header class="cq-job-group-header">
                        <span class="status"><span class="cq-activity-status active" title="Status: Active"><span class="cq-activity-status-primary"></span><span class="cq-activity-status-secondary"></span></span></span>

                        <section class="actions">
                            <a href="#" class="pause cq-button flaticon-pause" title="Pause all jobs"></a>
                            <a href="#" class="resume cq-button flaticon-play disabled" title="Resume all jobs"></a>
                        </section>

                        <h2 class="cq-job-group-header-title name">CheckInJobs</h2>
                    </header>
                    <section class="content">
                        <article class="cq-job">
                            <header class="cq-job-header clearfix">
                                <div class="status"><span class="cq-activity-status active" title="Status: Active"><span class="cq-activity-status-primary"></span><span class="cq-activity-status-secondary"></span></span></div>

                                <a class="cq-job-header-title loadDetails name" href="#">AutoCancelJob</a>
                                <a class="cq-job-hide-details hideDetails" href="#" style="display: none;">hide details</a>

                                <section class="actions">
                                    <a href="#" class="execute cq-button">Execute Now</a>
                                    <a href="#" class="pause cq-button flaticon-pause" title="Pause all triggers"></a>
                                    <a href="#" class="resume cq-button flaticon-play disabled" title="Resume all triggers"></a>
                                </section>
                            </header>

                            <section class="detailsContainer"></section>

                            <table class="cq-triggers triggers">
                                <thead>
                                    <tr>
                                        <th>Trigger</th>
                                        <th>Schedule</th>
                                        <th>Start date</th>
                                        <th>End date</th>
                                        <th>Previous fire date</th>
                                        <th>Next fire date</th>
                                        <th></th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr>
                                        <td>
                                            <span class="status"><span class="cq-activity-status active" title="Status: Active"><span class="cq-activity-status-primary"></span><span class="cq-activity-status-secondary"></span></span></span>
                                            <span class="name">AutoCancelTrigger</span>
                                        </td>
                                        <td class="type">1 0/15 18-20 ? * *</td>
                                        <td class="startDate"><span class="cq-date">2014/11/14 17:27:15</span></td>
                                        <td class="endDate"><span class="cq-date"><span class="cq-none">[none]</span></span></td>
                                        <td class="previousFireDate"><span class="cq-date">2014/11/17 20:45:01</span></td>
                                        <td class="nextFireDate"><span class="cq-date">2014/11/18 18:00:01</span></td>
                                        <td class="">
                                            <section class="actions">
                                                <a href="#" class="pause cq-button flaticon-pause"></a>
                                                <a href="#" class="resume cq-button flaticon-play disabled"></a>
                                            </section>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </article>

                        <article class="cq-job">
                            <header class="cq-job-header clearfix">
                                <div class="status"><span class="cq-activity-status active" title="Status: Active"><span class="cq-activity-status-primary"></span><span class="cq-activity-status-secondary"></span></span></div>

                                <a class="cq-job-header-title loadDetails name" href="#">Push34Job</a>
                                <a class="cq-job-hide-details hideDetails" href="#" style="display: none;">hide details</a>

                                <section class="actions">
                                    <a href="#" class="execute cq-button">Execute Now</a>
                                    <a href="#" class="pause cq-button flaticon-pause" title="Pause all triggers"></a>
                                    <a href="#" class="resume cq-button flaticon-play disabled" title="Resume all triggers"></a>
                                </section>
                            </header>

                            <section class="detailsContainer"></section>

                            <table class="cq-triggers triggers">
                                <thead>
                                    <tr>
                                        <th>Trigger</th>
                                        <th>Schedule</th>
                                        <th>Start date</th>
                                        <th>End date</th>
                                        <th>Previous fire date</th>
                                        <th>Next fire date</th>
                                        <th></th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr>
                                        <td>
                                            <span class="status"><span class="cq-activity-status active" title="Status: Active"><span class="cq-activity-status-primary"></span><span class="cq-activity-status-secondary"></span></span></span>
                                            <span class="name">Push34Trigger</span>
                                        </td>
                                        <td class="type">1 0/15 7-23 ? * *</td>
                                        <td class="startDate"><span class="cq-date">2014/11/14 17:27:15</span></td>
                                        <td class="endDate"><span class="cq-date"><span class="cq-none">[none]</span></span></td>
                                        <td class="previousFireDate"><span class="cq-date">2014/11/18 10:45:01</span></td>
                                        <td class="nextFireDate"><span class="cq-date">2014/11/18 11:00:01</span></td>
                                        <td class="">
                                            <section class="actions">
                                                <a href="#" class="pause cq-button flaticon-pause"></a>
                                                <a href="#" class="resume cq-button flaticon-play disabled"></a>
                                            </section>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </article>

                        <article class="cq-job">
                            <header class="cq-job-header clearfix">
                                <div class="status"><span class="cq-activity-status active" title="Status: Active"><span class="cq-activity-status-primary"></span><span class="cq-activity-status-secondary"></span></span></div>

                                <a class="cq-job-header-title loadDetails name" href="#">Push8Job</a>
                                <a class="cq-job-hide-details hideDetails" href="#" style="display: none;">hide details</a>

                                <section class="actions">
                                    <a href="#" class="execute cq-button">Execute Now</a>
                                    <a href="#" class="pause cq-button flaticon-pause" title="Pause all triggers"></a>
                                    <a href="#" class="resume cq-button flaticon-play disabled" title="Resume all triggers"></a>
                                </section>
                            </header>

                            <section class="detailsContainer"></section>

                            <table class="cq-triggers triggers">
                                <thead>
                                    <tr>
                                        <th>Trigger</th>
                                        <th>Schedule</th>
                                        <th>Start date</th>
                                        <th>End date</th>
                                        <th>Previous fire date</th>
                                        <th>Next fire date</th>
                                        <th></th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr>
                                        <td>
                                            <span class="status"><span class="cq-activity-status active" title="Status: Active"><span class="cq-activity-status-primary"></span><span class="cq-activity-status-secondary"></span></span></span>
                                            <span class="name">Push8JobTrigger</span>
                                        </td>
                                        <td class="type">1 0/15 18-20 ? * *</td>
                                        <td class="startDate"><span class="cq-date">2014/11/14 17:27:15</span></td>
                                        <td class="endDate"><span class="cq-date"><span class="cq-none">[none]</span></span></td>
                                        <td class="previousFireDate"><span class="cq-date">2014/11/17 20:45:01</span></td>
                                        <td class="nextFireDate"><span class="cq-date">2014/11/18 18:00:01</span></td>
                                        <td class="">
                                            <section class="actions">
                                                <a href="#" class="pause cq-button flaticon-pause"></a>
                                                <a href="#" class="resume cq-button flaticon-play disabled"></a>
                                            </section>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </article>
                    </section>
                </article>
            
        </div>

        <footer id="mainFooter" class="cq-main-footer">
         <%--<div class="cq-footer-wrapper">
                <div class="container">
                    <div class="pull-left">
                        <div class="cq-version-container">CrystalQuartz Panel <span id="selfVersion" class="cq-version">v1.0.0.0</span> </div>
                        <div class="cq-version-container visible-lg-block">Quartz.NET <span id="quartzVersion" class="cq-version">v2.2.4.400</span> </div>
                        <div class="cq-version-container visible-lg-block">.NET <span id="dotNetVersion" class="cq-version">v4.0</span></div>
                    </div>

                    <div class="pull-right">
                        <span id="autoUpdateMessage" class="">next update at 18:00:01 GMT+0800 (中国标准时间)</span>
                    </div>
                </div>
            </div>--%>
        </footer>
    </div>


    </form>
    <script src="Scripts/jquery-1.11.1.min.js"></script>
    <script src="Scripts/john-smith-3.2.0.js"></script>
    <script src="Scripts/lodash.compat.min.js"></script>
</body>
</html>