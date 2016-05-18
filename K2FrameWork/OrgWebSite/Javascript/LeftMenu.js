Ext.ns("K2", "K2.Office", "K2.Util", "K2.Office.Department");
/*Ext.apply(obj, config, [defaults]) 将config对象的所有属性都复制到另一个对象obj上， 
第三个参数defaults可以用来提供默认值， 不过通常指用前两个参数就够了。 
这个函数主要用在构造函数中， 用来将配置复制到对象上*/
K2.Office.LeftMenu = function(config) {
	var d = Ext.apply( {// default set
				width : 200,
				split : true,
				region : 'west',
				defaults : {
					border : false
				},
				layoutConfig : {
					animate : true
				}
			}, config || {});

	config = Ext.apply(d, {
		layout : 'accordion',
		collapsible : true
	});
	
	K2.Office.LeftMenu.superclass.constructor.call(this, config);
	
	//改进，并为增加了个配置项tree!
	for(var i=0;i<this.trees.length;i++)		 
	 	this.add({title:this.trees[i].getRootNode().text,items:[this.trees[i]]});	

	// 事件处理
	this.addEvents('nodeClick');
	this.initTreeEvent();
	}

Ext.extend(K2.Office.LeftMenu, Ext.Panel, {
	initTreeEvent : function() {
		if(!this.items) return;
		for (var i = 0;i < this.items.length; i++) {
			var p = this.items.itemAt(i);
			if (p)
			var t = p.items.itemAt(0);
			if(t)
			t.on( {
				'click' : function(node, event) {
					if (node && node.isLeaf()) {
						event.stopEvent();
						this.fireEvent('nodeClick', node.attributes);
					}
				},
				scope : this
			});
		}
	}
})
