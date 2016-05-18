K2.Office.MywaitPanel = function(config) {
    K2.Office.MywaitPanel.superclass.constructor.call(this, config);

    var panel = new Ext.Panel({
        defaults: {
            autoWidth: true,
            autoScroll: true,
            autoHeight: true
        },
        title: '我的待办',
        html: '<iframe id="iMyWait" src="../WorkSpace/MyWorklist.aspx" width="100%" frameborder="0" height="370"></iframe>'
    });

    this.add(panel);

}
Ext.extend(K2.Office.MywaitPanel, Ext.Panel, {});