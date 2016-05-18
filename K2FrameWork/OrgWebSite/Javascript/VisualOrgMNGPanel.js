K2.Office.VisualorgmngPanel = function(config) {
    K2.Office.VisualorgmngPanel.superclass.constructor.call(this, config);

//    var panel = new Ext.Panel({
//        defaults: {
//            autoWidth: true,
//            autoScroll: true,
//            autoHeight: true
//        },
//        title: '虚拟组织管理',
//        html: '<iframe id="iVOrgMNG" src="../Admin/VisualOrgMNG.aspx" width="100%" frameborder="0" height="370"></iframe>'
//    });

//    this.add(panel);
    var tabs = new Ext.TabPanel({
        region: 'center',
        margins: '3 3 3 0',
        activeTab: 0,
        defaults: {
            autoScroll: true,
            autoHeight: true
        },
        height: 400,
        items: [{
            title: '虚拟角色',
            defaults: {
                autoScroll: true,
                autoHeight: true
            },
            html: '<iframe id="iVOrgMNG" src="../Admin/VisualRoleMNG.aspx" width="100%" frameborder="0" height="370"></iframe>'
        },
        {
            title: '虚拟组织',
            defaults: {
                autoScroll: true,
                autoHeight: true
            },
            html: '<iframe id="iVUM" src="../Admin/VisualOrgMNG.aspx" width="100%" frameborder="0" height="370"></iframe>'
        }
        ]
    });
    this.add(tabs);
}

Ext.extend(K2.Office.VisualorgmngPanel, Ext.Panel, {});