'use strict';var __extends=this.__extends||function(c,m){function q(){this.constructor=c}for(var n in m)m.hasOwnProperty(n)&&(c[n]=m[n]);q.prototype=m.prototype;c.prototype=new q},js;
(function(c){var m=function(){function a(){this._slaves=[]}a.prototype.manage=function(b){this._slaves.push(b);b.init()};a.prototype.dispose=function(){for(var b=0;b<this._slaves.length;b++)this._slaves[b].dispose()};a.prototype.getViewModel=function(){return null};a.prototype.getParent=function(){return null};a.prototype.onUnrender=function(){return new u};return a}();c.ExplicitManager=m;c.dom;c.observableValue=function(){return new r};c.observableList=function(){return new v};c.dependentValue=function(a){for(var b=
[],f=0;f<arguments.length-1;f++)b[f]=arguments[f+1];return new y(a,b)};c.init=function(){var a=new z,b=(new A).registerFetcher(w.Value,new B).registerFetcher(w.CheckedAttribute,new C),f=new D(a),a=new E(function(b){return b.toString()},a,f,b),b=new F(a,f,b);f.setDomFactory(b);c.dom=b.create(new G($(document)),new m)};var q=function(){function a(b,f,a,e,c){this._manager=b;this._event=f;this._target=a;this._options=e;this._fetcherFactory=c}a.prototype.react=function(b,f){var a=null,a=this._options&&
this._options.fetch?this._fetcherFactory.getByKey(this._options.fetch):this._fetcherFactory.getForElement(this._target),e=null;a&&(e=function(b){return a.valueFromElement(b)});var c=f||this._manager.getViewModel(),e=new n(e,this._event,b,this._target,c);this._manager.manage(e)};return a}();c.CommandConfig=q;var n=function(){function a(b,f,a,e,c){this._argumentFetcher=b;this._eventType=f;this._callback=a;this._target=e;this._context=c}a.prototype.dispose=function(){this._target.detachEventHandler(this._eventType,
this._handlerRef)};a.prototype.init=function(){var b=this;this._handlerRef=this._target.attachEventHandler(this._eventType,function(){var f=null==b._argumentFetcher?null:b._argumentFetcher(b._target);null==f?b._callback.call(b._context):b._callback.call(b._context,f)})};return a}();c.CommandWire=n;(function(a){a.text="text";a.html="html";a.unknown="unknown"})(c.ValueType||(c.ValueType={}));var l=c.ValueType,T=function(){function a(b,f,a,e,c){this._rootElement=b;this._manager=f;this._renderListenerFactory=
a;this._viewFactory=e;this._fetcherFactory=c;this.root=b;this.$=b.$;this.manager=f}a.prototype.find=function(b){b=new S(this._rootElement.findRelative(b),this._manager,this._renderListenerFactory,this._viewFactory,this._fetcherFactory);return k.wrapObjectWithSelfFunction(b,function(b,a,e){b.observes(a,e)})};a.prototype.dispose=function(){this._manager.dispose()};a.prototype.onUnrender=function(){return this._manager.onUnrender()};return a}(),S=function(){function a(b,a,d,e,c){var h=this;this._rootElement=
b;this._manager=a;this._renderListenerFactory=d;this._viewFactory=e;this._fetcherFactory=c;this.root=b;b=new s(this._manager,function(b){return h.createRenderListener(b,{valueType:l.text})});a=new s(this._manager,function(b){return h.createRenderListener(b,{valueType:l.html})});this.$=this._rootElement.$;this.text=k.wrapObjectWithSelfFunction(b,function(b,a){return b.observes(a)});this.html=k.wrapObjectWithSelfFunction(a,function(b,a){return b.observes(a)});this.manager=this._manager}a.prototype.listener=
function(b){var a=this;return new s(this._manager,function(d){return a.createRenderListener(d,{renderer:new H(b)})})};a.prototype.className=function(b){return this.listener(function(a,d){d?a.addClass(b):a.removeClass(b)})};a.prototype.observes=function(b,a){var d=t.getObservable(b);a&&k.isFunction(a)&&(a={view:a});d=this.createRenderListener(d,a);this._manager.manage(d)};a.prototype.render=function(b,a){var d=this._viewFactory.resolve(this._rootElement,b,a,this._manager);this._manager.manage(d)};
a.prototype.on=function(b,a){return new q(this._manager,b,this._rootElement,a,this._fetcherFactory)};a.prototype.createRenderListener=function(b,a){return this._renderListenerFactory.createListener(b,this,a)};return a}(),s=function(){function a(b,a){this._manager=b;this.factory=a}a.prototype.observes=function(b){b=t.getObservable(b);b=this.factory(b);this._manager.manage(b)};return a}();c.ObservationConfig=s;var t;(function(a){a.getObservable=function(b){return b&&b.getValue&&b.listen?b:new c.StaticObservableValue(b)}})(t||
(t={}));var F=function(){function a(b,a,d){this._renderListenerFactory=b;this._viewFactory=a;this._fetcherFactory=d}a.prototype.create=function(b,a){var d=new T(b,a,this._renderListenerFactory,this._viewFactory,this._fetcherFactory);return k.wrapObjectWithSelfFunction(d,function(b,a){return b.find(a)})};return a}();c.DomFactory=F;var u=function(){function a(){this._listeners=[]}a.prototype.listen=function(b){var a=this;this._listeners.push(b);return{dispose:function(){a.removeListener(b)}}};a.prototype.trigger=
function(b){for(var a=0;a<this._listeners.length;a++)this._listeners[a](b)};a.prototype.dispose=function(){this._listeners=null};a.prototype.getListenersCount=function(){return null===this._listeners?0:this._listeners.length};a.prototype.hasListeners=function(){return 0<this.getListenersCount()};a.prototype.removeListener=function(b){x.removeItem(this._listeners,b)};return a}();c.Event=u;var w=function(){function a(){}a.Value="value";a.CheckedAttribute="checkedAttribute";return a}();c.FetcherType=
w;var B=function(){function a(){}a.prototype.isSuitableFor=function(b){var a=b.getNodeName();return a&&(a=a.toUpperCase(),"TEXTAREA"===a||"SELECT"===a||"INPUT"===a&&(b=b.getAttribute("type"),!b||"TEXT"===b.toUpperCase()))?!0:!1};a.prototype.valueToElement=function(b,a){a.setValue(b)};a.prototype.valueFromElement=function(b){return b.getValue()};return a}();c.ValueFetcher=B;var C=function(){function a(){}a.prototype.isSuitableFor=function(b){var a=b.getNodeName();return a?(a=a.toUpperCase(),b=b.getAttribute("type"),
"INPUT"===a&&b&&"CHECKBOX"===b.toUpperCase()):!1};a.prototype.valueToElement=function(b,a){a.setProperty("checked",b)};a.prototype.valueFromElement=function(b){var a=!1;b.getProperty("checked")&&(a=!0);return a};return a}();c.CheckedAttributeFetcher=C;var A=function(){function a(){this._items={}}a.prototype.getForElement=function(b){for(var a in this._items){var d=this._items[a];if(d.isSuitableFor(b))return d}return null};a.prototype.getByKey=function(b){return this._items[b]};a.prototype.registerFetcher=
function(b,a){this._items[b]=a;return this};return a}();c.FetcherFactory=A;var G=function(){function a(b){this._target=this.$=b}a.prototype.empty=function(){this._target.empty()};a.prototype.appendHtml=function(b){if(null===b||void 0===b)throw Error("Could not append empty string!");if("string"!==typeof b)throw Error("Expected string markup but was"+b);b=$($.parseHTML(b));this._target.append(b);return new a(b)};a.prototype.getNodeName=function(){return 1==this._target.length?this._target[0].nodeName:
null};a.prototype.findRelative=function(b){var f=this._target.filter(b);0==f.length&&(f=this._target.find(b));return new a(f)};a.prototype.remove=function(){this._target.remove()};a.prototype.getTarget=function(){return this._target};a.prototype.setText=function(b){this._target.text(b)};a.prototype.setHtml=function(b){this._target.html(b)};a.prototype.addClass=function(b){this._target.addClass(b)};a.prototype.removeClass=function(b){this._target.removeClass(b)};a.prototype.attachEventHandler=function(b,
f){var d=function(){f(new a($(this)));return!1};this._target.on(b,d);return d};a.prototype.detachEventHandler=function(b,a){this._target.off(b,a)};a.prototype.getValue=function(){return this._target.val()};a.prototype.setValue=function(b){return this._target.val(b)};a.prototype.getAttribute=function(b){return this._target.attr(b)};a.prototype.setAttribute=function(b,a){this._target.attr(b,a)};a.prototype.getProperty=function(b){return this._target.prop(b)};a.prototype.setProperty=function(b,a){this._target.prop(b,
a)};return a}();c.JQueryElement=G;var z=function(){function a(){}a.prototype.resolve=function(b){var a;if(b instanceof jQuery)a=b;else try{a=$(b)}catch(d){return b}if(0<a.parent().length)return a.html();if("string"===typeof b)return b;if(b instanceof jQuery)return $("<p>").append(b).html();throw Error("Could not resolve markup by object "+b);};return a}();c.JQueryMarkupResolver=z;var H=function(){function a(b){this.payload=b}a.prototype.render=function(b,a){this.payload(a,b);return{element:a,dispose:function(){}}};
return a}();c.CustomListenerRenderer=H;var I=function(){function a(b,a,d){this._observable=b;this._contentDestination=a;this._renderer=d}a.prototype.init=function(){var b=this;this._link=this._observable.listen(function(a){return b.doRender(a)})};a.prototype.dispose=function(){this._link&&this._link.dispose();this.disposeCurrentValue()};a.prototype.doRender=function(b){this.disposeCurrentValue();this._currentValue=this._renderer.render(b,this._contentDestination)};a.prototype.disposeCurrentValue=
function(){this._currentValue&&this._currentValue.dispose()};return a}();c.RenderValueListener=I;var J=function(){function a(b,a,d){this._observable=b;this._contentDestination=a;this._renderer=d;this._renderedValues=[]}a.prototype.dispose=function(){this._link&&this._link.dispose();for(var b=0;b<this._renderedValues.length;b++)this._renderedValues[b].renderedValue.dispose&&this._renderedValues[b].renderedValue.dispose()};a.prototype.init=function(){var b=this;this._link=this._observable.listen(function(a,
d,e){return b.doRender(e.portion,e.reason)})};a.prototype.findRenderedValue=function(b){for(var a=0;a<this._renderedValues.length;a++)if(this._renderedValues[a].value===b)return this._renderedValues[a].renderedValue;return null};a.prototype.removeRenderedValue=function(b){for(var a=-1,d=0;d<this._renderedValues.length;d++)this._renderedValues[d].renderedValue===b&&(a=d);0<=a&&this._renderedValues.splice(a,1)};a.prototype.doRender=function(b,a){if(2==a)for(var d=0;d<b.length;d++){var e=this.findRenderedValue(b[d]);
e&&(e.dispose(),this.removeRenderedValue(e))}else 1!=a&&(this._renderedValues=[],this._contentDestination.empty()),this.appendItems(b)};a.prototype.appendItems=function(b){if(b)for(var a=0;a<b.length;a++){var d=b[a],e=this._renderer.render(d,this._contentDestination);this._renderedValues.push({value:d,renderedValue:e})}};return a}();c.RenderListHandler=J;var E=function(){function a(b,a,d,e){this._defaultFormatter=b;this._markupResolver=a;this._viewFactory=d;this._fetcherFactory=e}a.prototype.createListener=
function(b,a,d){var e=a.root;d||(d={});if(!d.renderer)if(d.view)d.renderer=new K(this._viewFactory,d.view,a.manager);else{if(!d.valueType)if(d.formatter)d.valueType=l.unknown;else{var c=!0;void 0!==d.encode&&(c=d.encode);d.valueType=c?l.text:l.html}d.formatter||(d.formatter=this._defaultFormatter);d.renderer=this.getRenderer(d,a,b)}return this.isList(b)?new J(b,e,d.renderer):new I(b,e,d.renderer)};a.prototype.getRenderer=function(b,a,d){var e=null;if(b.fetch){if(e=this._fetcherFactory.getByKey(b.fetch),
!e)throw Error("Fetcher "+b.fetch+" not found");}else if(e=this._fetcherFactory.getForElement(a.root),!1!==b.bidirectional){var c=b.command,h=b.commandContext,k=b.event||"change";!c&&d.setValue&&(c=d.setValue,h=d);c&&a.on(k).react(c,h)}if(e)return new L(e);switch(b.valueType){case l.text:return new M(b.formatter);case l.html:return new N(b.formatter);case l.unknown:return new O(b.formatter,this._markupResolver);default:throw Error("Unknown value type: "+b.valueType);}};a.prototype.isList=function(b){return b instanceof
v||b&&b.getValue()instanceof Array?!0:!1};return a}();c.RenderListenerFactory=E;(function(a){a[a.replace=0]="replace";a[a.add=1]="add";a[a.remove=2]="remove";a[a.initial=3]="initial"})(c.DataChangeReason||(c.DataChangeReason={}));var P=function(){function a(b,a){this.allListeners=b;this.currentListener=a}a.prototype.dispose=function(){x.removeItem(this.allListeners,this.currentListener)};return a}();c.ListenerLink=P;var r=function(){function a(){this._listeners=[]}a.prototype.getValue=function(){return this._value};
a.prototype.setValue=function(b){var a=this._value;this._value=b;this.notifyListeners(b,a,{reason:0,portion:b})};a.prototype.listen=function(b,a){this._listeners.push(b);(void 0===a||!0===a)&&this.notifyListeners(this.getValue(),this.getValue(),{reason:3,portion:this.getValue()});return new P(this._listeners,b)};a.prototype.getListenersCount=function(){return this._listeners.length};a.prototype.getListener=function(b){return this._listeners[b]};a.prototype.notifyListeners=function(b,a,d){for(var c=
0;c<this._listeners.length;c++)this._listeners[c](b,a,d)};a.prototype.hasValue=function(){return null==this._value||void 0==this._value?!1:!0};return a}();c.ObservableValue=r;var v=function(a){function b(){a.call(this);a.prototype.setValue.call(this,[])}__extends(b,a);b.prototype.setValue=function(b){if(b&&!(b instanceof Array))throw Error("Observable list supports only array values");a.prototype.setValue.call(this,b);this.notifyCountListeners()};b.prototype.add=function(){for(var b=[],a=0;a<arguments.length-
0;a++)b[a]=arguments[a+0];for(var a=this.getValue().slice(0),c=this.getValue(),g=0;g<b.length;g++)c.push(b[g]);this.reactOnChange(this.getValue(),a,{reason:1,portion:b})};b.prototype.remove=function(){for(var b=[],a=0;a<arguments.length-0;a++)b[a]=arguments[a+0];for(var a=this.getValue().slice(0),c=this.getValue(),g=0;g<b.length;g++)x.removeItem(c,b[g]);this.reactOnChange(this.getValue(),a,{reason:2,portion:b})};b.prototype.clear=function(){var a=this.getValue().splice(0,this.getValue().length);this.reactOnChange(this.getValue(),
a,{reason:2,portion:a})};b.prototype.count=function(){this._count||(this._count=new r,this.notifyCountListeners());return this._count};b.prototype.forEach=function(a,b){this.getValue().forEach(a,b)};b.prototype.reactOnChange=function(b,d,c){a.prototype.notifyListeners.call(this,b,d,c);this.notifyCountListeners()};b.prototype.notifyCountListeners=function(){this._count&&(this.getValue()?this._count.setValue(this.getValue().length):this._count.setValue(0))};return b}(r);c.ObservableList=v;var y=function(a){function b(b,
d){a.call(this);this._dependencies=d;this._evaluateValue=b;this._dependencyValues=[];for(var c=this,g=0;g<d.length;g++){var h=d[g];h.listen(function(a,b,d){0!==d.reason&&(a=h.getValue());c.notifyDependentListeners(h,a)},!1);this._dependencyValues[g]=h.getValue()}}__extends(b,a);b.prototype.getValue=function(){return this._evaluateValue.apply(this,this._dependencyValues)};b.prototype.setValue=function(a){throw Error("Could not set dependent value");};b.prototype.notifyDependentListeners=function(a,
b){for(var c=this.getValue(),g=0;g<this._dependencies.length;g++)this._dependencies[g]===a&&(this._dependencyValues[g]=b);for(var h=this.getValue(),g=0;g<this.getListenersCount();g++)this.getListener(g)(h,c,{portion:h,reason:0})};return b}(r);c.DependentValue=y;var p=function(){function a(a){this._value=a}a.prototype.getValue=function(){return this._value};a.prototype.listen=function(a){a(this.getValue(),null,{reason:3,portion:this.getValue()});return{dispose:function(){}}};return a}();c.StaticObservableValue=
p;p=function(){function a(a){this._formatter=a}a.prototype.render=function(a,c){var d=this,e=this._formatter(k.isNullOrUndefined(a)?"":a);this.doRender(e,c);return{dispose:function(){d.doRender("",c)}}};a.prototype.doRender=function(a,c){};return a}();c.FormatterBasedRenderer=p;var M=function(a){function b(b){a.call(this,b)}__extends(b,a);b.prototype.doRender=function(a,b){b.setText(a)};return b}(p);c.TextRenderer=M;var N=function(a){function b(b){a.call(this,b)}__extends(b,a);b.prototype.doRender=
function(a,b){b.setHtml(a)};return b}(p);c.HtmlRenderer=N;var O=function(a){function b(b,d){a.call(this,b);this._markupResolver=d}__extends(b,a);b.prototype.doRender=function(a,b){var c=this._markupResolver.resolve(a);b.setHtml(c)};return b}(p);c.ResolvableMarkupRenderer=O;var K=function(){function a(a,c,d){this._parent=d;this._viewFactory=a;this._viewDescriptor=c}a.prototype.render=function(a,c){if(k.isNullOrUndefined(a))return Q.noopDisposable;var d=this._viewFactory.resolve(c,this._viewDescriptor,
a,this._parent);d.init();return{dispose:function(){d.dispose()}}};return a}();c.ViewValueRenderer=K;var L=function(){function a(a){this._fetcher=a}a.prototype.render=function(a,c){this._fetcher.valueToElement(a,c);return Q.noopDisposable};return a}();c.FetcherToRendererAdapter=L;(function(a){a.isNullOrUndefined=function(a){return null===a||void 0===a};a.isFunction=function(a){var c={};return a&&"[object Function]"===c.toString.call(a)};a.wrapObjectWithSelfFunction=function(a,c){var d=function(){for(var a=
[],b=0;b<arguments.length-0;b++)a[b]=arguments[b+0];a.splice(0,0,d);return c.apply(this,a)},e;for(e in a)d[e]=a[e];return d}})(c.Utils||(c.Utils={}));var k=c.Utils;(function(a){a.removeItem=function(a,c){for(var d=-1,e=0;e<a.length;e++)a[e]===c&&(d=e);0<=d&&a.splice(d,1)}})(c.ArrayUtils||(c.ArrayUtils={}));var x=c.ArrayUtils;(function(a){a.noop=function(){};a.noopDisposable={dispose:a.noop}})(c.DisposingUtils||(c.DisposingUtils={}));var Q=c.DisposingUtils,R=function(){function a(a,c,d,e,g,h){this._viewData=
a;this._viewModel=c;this._markupResolver=d;this._destination=e;this._domFactory=g;this._parent=h;this._slaves=[];this._unrender=new u}a.prototype.onUnrender=function(){return this._unrender};a.prototype.manage=function(a){this._slaves.push(a)};a.prototype.init=function(){var a=this._markupResolver.resolve(this._viewData.template),a=this._destination.appendHtml(a);this.attachViewToRoot(a)};a.prototype.getViewModel=function(){return this._viewModel};a.prototype.getParent=function(){return this._parent};
a.prototype.attachViewToRoot=function(a){this._root=a;if(this._viewData.init)if(a=this._domFactory.create(this._root,this),0<this._viewData.deep){var c=this.fetchViewModels(this._viewData.deep);c.splice(0,0,this._viewModel);c.splice(0,0,a);this._viewData.init.apply(this._viewData,c)}else this._viewData.init(a,this._viewModel);for(a=0;a<this._slaves.length;a++)this._slaves[a].init();this._viewModel&&this._viewModel.initState&&this._viewModel.initState()};a.prototype.getRootElement=function(){return this._root};
a.prototype.unrenderView=function(){this._unrender.hasListeners()?this._unrender.trigger():this.getRootElement().remove()};a.prototype.dispose=function(){this.unrenderView();this._viewModel&&this._viewModel.releaseState&&this._viewModel.releaseState();for(var a=0;a<this._slaves.length;a++)this._slaves[a].dispose()};a.prototype.fetchViewModels=function(a){for(var c=[],d=this.getParent(),e=1;e<=a&&null!==d;)c.push(d.getViewModel()),d=d.getParent(),e++;return c};return a}();c.ComposedView=R;var D=function(){function a(a){this._markupResolver=
a}a.prototype.setDomFactory=function(a){this._domFactory=a};a.prototype.resolve=function(a,c,d,e){if(!c)throw Error("Expected view data object was not defined");if(k.isFunction(c))return c=new c,this.resolve(a,c,d,e);if(c.template)return new R(c,d,this._markupResolver,a,this._domFactory,e);if(c.renderTo&&c.getRootElement)return c;throw Error("Could not resolve view data by provided descriptor");};return a}();c.DefaultViewFactory=D})(js||(js={}));js.init();
