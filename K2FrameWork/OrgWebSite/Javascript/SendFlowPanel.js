K2.Office.SendflowPanel = function(config) {
    K2.Office.SendflowPanel.superclass.constructor.call(this, config);

    var panel = new Ext.Panel({
	    defaults: {
	        autoWidth: true,
	        autoScroll: true,
	        autoHeight: true
	    },
	    title: '发起流程',
	    html: '<iframe id="iMyWorkFlow" src="../WorkSpace/MyWorkFlow.aspx" width="100%" frameborder="0" height="370"></iframe>'
	});

	this.add(panel);
}

Ext.extend(K2.Office.SendflowPanel, Ext.Panel, {});