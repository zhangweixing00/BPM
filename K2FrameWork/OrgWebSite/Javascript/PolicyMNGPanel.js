K2.Office.PolicymngPanel = function(config) {
    K2.Office.PolicymngPanel.superclass.constructor.call(this, config);
    
    var panel = new Ext.Panel({
        defaults: {
            autoWidth: true,
            autoScroll: true,
            autoHeight: true
        },
        title: '流程规则管理',
        html: '<iframe id="iPolicyMNG" src="../Admin/ProcessPolicyMain.aspx" width="100%" frameborder="0" height="370"></iframe>'
    });

    this.add(panel);

}
Ext.extend(K2.Office.PolicymngPanel, Ext.Panel, {});