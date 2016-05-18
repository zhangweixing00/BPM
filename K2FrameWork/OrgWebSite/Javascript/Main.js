Ext.onReady(function() {

    Ext.BLANK_IMAGE_URL = '../pic/s.gif';
    Ext.QuickTips.init();
    Ext.lib.Ajax.defaultPostHeader += ";charset=utf-8";

    // 1、创建head部分
    var head = new Ext.Panel({
        region: 'north',
        border: false,
        html: '<table cellspacing="0" cellpadding="0" border="0" width="100%"><tr><td valign="top" align="left" background="../pic/top-bg.jpg"><img height="98" width="302" src="../pic/portal-logo.jpg"></td></tr></table>',
        height: 98
    });

    // 2、创建foot部分
    var foot = new Ext.Panel({
        region: 'south',
        html: '<div style="background-color:#F2F2F2; height:30px; text-align:right"><img src="../pic/bottom.jpg" width="232" height="25" />'
					+ '</div>',
        height: 35
    });

    // 3、创建leftMenu部分
    // var leftmenu = new Ext.Panel( {
    // region : 'west',
    // html : '<div>导航菜单</div>',
    // width : 200
    // });

    // 4、创建主内容部分
    // var mainTab = new Ext.Panel( {
    // region : 'center',
    // html : '<div>主内容部分</div>'
    // });

    var t1 = new Ext.tree.TreePanel({
        border: false,
        rootVisible: false,
        root: new Ext.tree.AsyncTreeNode({
            text: "工作流平台",
            expanded: true,
            children: [{
                id: "sendFlow",
                text: "发起流程",
                leaf: true
            }, {
                id: "myApplicant",
                text: "我的申请",
                leaf: true
            }, {
                id: "myWait",
                text: "我的待办",
                leaf: true
            }, {
                id: "myHandled",
                text: "我的已办",
                leaf: true
            }, {
                id: 'myDraf',
                text: '草稿箱',
                leaf: true
            }
				]
        })
    });

    var t2 = new Ext.tree.TreePanel({
        border: false,
        rootVisible: false,
        root: new Ext.tree.AsyncTreeNode({
            text: "系统管理",
            expanded: true,
            children: [{
                id: "orgMNG",
                text: "组织结构管理",
                leaf: true
            }, {
                id: "visualOrgMNG",
                text: "虚拟组织管理",
                leaf: true
            }, {
                id: 'flowRulesMNG',
                text: '流程规则管理',
                leaf: true
            }, {
                id: 'dicMNG',
                text: '数据字典管理',
                leaf: true
            }, {
                id: 'flowMNG',
                text: '流程管理',
                leaf: true
            }, {
                id: "permissions",
                text: "权限管理",
                leaf: true
                //					children : [ {
                //						id : "permission",
                //						text : "权限管理",
                //						leaf : true
                //					}, {
                //						id : "permissionType",
                //						text : "权限类别",
                //						leaf : true
                //					}]
}]
            })

        });

        //自定义流程树
        var t3 = new Ext.tree.TreePanel({
            border: false,
            rootVisible: false,
            root: new Ext.tree.AsyncTreeNode({
                text: "自定义流程",
                expanded: true,
                children: [{
                    id: "appTableMNG",
                    text: "数据表管理",
                    leaf: true
                },
                {
                    id: "customizeFormMNG",
                    text: "表单模板管理",
                    leaf: true
                },
                {
                    id: "policyMNG",
                    text: "流程规则管理",
                    leaf: true
                }
                ]
        })
    });

    var leftmenu = new K2.Office.LeftMenu({
        title: 'K2BPM',
        trees: [t1, t2, t3]
    });

    var mainTab = new K2.Office.MainingPanel({
        style: 'padding:0 6px 0 0',
        autoScroll: true,
        region: 'center',
        deferredRender: false,
        activeTab: 0,
        resizeTabs: true,
        inTabWidth: 100,
        tabWidth: 110,
        enableTabScroll: true
    });

    // 5、建立leftmenu和mainTab两者之间的关系
    leftmenu.on("nodeClick", function(nodeAttr) {
        mainTab.loadTab(nodeAttr);
    });
    // 6、创建布局
    var viewport = new Ext.Viewport({
        layout: 'border',
        style: 'border:#024459 2px solid;',
        items: [head, foot, leftmenu, mainTab]
    });
});