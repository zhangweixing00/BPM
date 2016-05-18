K2.Office.MyhandledPanel = function(config) {
    K2.Office.MyhandledPanel.superclass.constructor.call(this, config);

    var panel = new Ext.Panel({
        defaults: {
            autoWidth: true,
            autoScroll: true,
            autoHeight: true
        },
        title: '我的已办',
        html: '<iframe id="iMyHandled" src="../WorkSpace/MyJoined.aspx" width="100%" frameborder="0" height="370"></iframe>'
    });

    this.add(panel);

}
Ext.extend(K2.Office.MyhandledPanel, Ext.Panel, {});