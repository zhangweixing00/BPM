K2.Office.MydrafPanel = function(config) {
    K2.Office.MydrafPanel.superclass.constructor.call(this, config);

    var panel = new Ext.Panel({
        defaults: {
            autoWidth: true,
            autoScroll: true,
            autoHeight: true
        },
        title: '草稿箱',
        html: '<iframe id="iMyDraft" src="../WorkSpace/MyDraft.aspx" width="100%" frameborder="0" height="370"></iframe>'
    });

    this.add(panel);

}
Ext.extend(K2.Office.MydrafPanel, Ext.Panel, {});