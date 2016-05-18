K2.Office.PermissionsPanel = function(config) {
    K2.Office.PermissionsPanel.superclass.constructor.call(this, config);        //调用父对象构造函数

    //创建Tab页
    var tabs = new Ext.TabPanel({
        region: 'center',
        margins: '3 3 3 0',
        activeTab: 0,
        defaults: {
            autoScroll: true,
            autoHeight: true
        },
        height: 500,
        items: [{
            title: '表单权限',
            defaults: {
                autoScroll: true,
                autoHeight: true
            },
            html: '<iframe id="iFormPermission" src="../Admin/JobFormPermMNG.aspx" width="100%" frameborder="0" height="370"></iframe>'
        },
        {
            title: '岗位权限',
            defaults: {
                autoScroll: true,
                autoHeight: true
            },
            html: '<iframe id="iJobPermission" src="../Admin/JobPermMNG.aspx" width="100%" frameborder="0" height="370"></iframe>'
        }
        ]
    });
    this.add(tabs);
}

Ext.extend(K2.Office.PermissionsPanel, Ext.Panel, {});      //继承Ext.Panel类