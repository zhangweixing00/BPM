Ext.ns("K2", "K2.Office", "K2.Util", "K2.Office.Department");
K2.Office.MainingPanel = Ext.extend(Ext.TabPanel, {
    initComponent: function() {
        // 一些初始化工作
        K2.Office.MainingPanel.superclass.initComponent.call(this);
        this._cache = {};
    },
    loadTab: function(node) {
        var n = this.getComponent(node.id);
        if (n) {
            this.setActiveTab(n);
        } else {
            var c = {
                'id': node.id,
                'title': node.text,
                closable: true
            };
            var pn = this.findPanel(node.id);
            n = this.add(pn ? new pn(c) : Ext.apply(c, {
                html: '您无访问权限！'
            }))

            n.show().doLayout();
        }
        if (n.ds)
            n.ds.load({ params: { start: 0, limit: 10} });
    },
    findPanel: function(name) {
        var ret = this._cache[name];
        if (!ret) {
            var pn = (this.ns ? this.ns : 'K2.Office') + "."
					+ Ext.util.Format.capitalize(name) + 'Panel';
            var ret = eval(pn);
        }
        return ret;
    },
    addPanel: function(name, panel) {
        if (!this._cache)
            this._cache = {};
        this._cache[name] = panel;
    }
});