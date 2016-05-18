K2.Office.MyapplicantPanel = function(config) {
	K2.Office.MyapplicantPanel.superclass.constructor.call(this, config);

//	var proxy = new Ext.data.HttpProxy( {
//		url : 'myapplicant.aspx'
//	});

//	var recordType = new Ext.data.Record.create([ {
//		name : "id",
//		type : "int"
//	}, {
//		name : "comNum",
//		type : "string"
//	}, {
//		name : "comName",
//		type : "string"
//	}, {
//		name : "comAddress",
//		type : "string"
//	}]);

//	// 定义分析器
//	var reader = new Ext.data.JsonReader( {
//		totalProperty : "results",
//		root : "rows",
//		id : "id"
//	}, recordType);

//	// 定义store
//	var ds = new Ext.data.Store( {
//		proxy : proxy,
//		reader : reader
//	});
//    this.ds=ds;
//	// 第二，讲一下cm,grid

//var sm= new Ext.grid.CheckboxSelectionModel();
//	var cm = new Ext.grid.ColumnModel( {
//		defaultSortable : true,
//		defaultWidth : 180,
//		columns : [ sm,{
//			header : '编号',
//			dataIndex : 'comNum'
//		}, {
//			header : '名称',
//			dataIndex : 'comName'
//		}, {
//			header : '公司地址',
//			width : 300,
//			dataIndex : 'comAddress'
//		}]
//	});
//var pagingBar = new Ext.PagingToolbar({
//        pageSize: 10,
//        store: ds,
//        displayInfo: true,
//        displayMsg: '共有 {2}，当前显示 {0} - {1}条',
//        emptyMsg: "没有数据"
//    });
//    
//	var grid = new Ext.grid.GridPanel( {
//		cm : cm,
//		sm:sm,
//		stripeRows :true,
//		store : ds,
//		width : 660,
//		height : 400,
//		bbar:pagingBar,
//		loadMask:{msg:'正在载入数据,请稍等...'},
//		title : '公司列表'
//	});
//	//ds.load();

//	this.add(grid);
	//	// 第三、调整，tbar分页,工具栏
	var panel = new Ext.Panel({
	    defaults: {
	        autoWidth: true,
	        autoScroll: true,
	        autoHeight: true
	    },
	    title: '我的申请',
	    html: '<iframe id="iMyApplicant" src="../WorkSpace/MyStarted.aspx" width="100%" frameborder="0" height="370"></iframe>'
	});

	this.add(panel);
	
}
Ext.extend(K2.Office.MyapplicantPanel, Ext.Panel, {});