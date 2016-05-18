/// <reference path="vswd-ext_2.2.js" />
/*!
* Ext JS Library 3.3.1
* Copyright(c) 2006-2010 Sencha Inc.
* licensing@sencha.com
* http://www.sencha.com/license
*/
Ext.onReady(function() {

    Ext.QuickTips.init();
    Ext.form.Field.prototype.msgTarget = 'side';

    var ds = new Ext.data.ArrayStore({
        data: [],
        fields: ['value', 'text'],
        sortInfo: {
            field: 'value',
            direction: 'ASC'
        }
    });

    var ds2 = new Ext.data.ArrayStore({
        data: [],
        fields: ['value', 'text'],
        sortInfo: {
            field: 'value',
            direction: 'ASC'
        }
    });

    /*
    * Ext.ux.form.ItemSelector Example Code
    */
    var isForm = new Ext.form.FormPanel({
        id: 'selectedForm',
        title: '虚拟角色选择',
        width: 700,
        bodyStyle: 'padding:10px;',
        renderTo: 'itemselector',
        items: [{
            xtype: 'itemselector',
            name: 'itemselector',
            imagePath: '../pic/',
            multiselects: [{
                width: 250,
                height: 300,
                store: ds,
                displayField: 'text',
                valueField: 'value'
            }, {
                width: 250,
                height: 300,
                store: ds2,
                displayField: 'text',
                valueField: 'value',
                tbar: [{
                    text: '删除所有',
                    handler: function() {
                        isForm.getForm().findField('itemselector').reset();
                    }
}]
}]
}],

                    buttons: [{
                        text: '保 存',
                        handler: function() {
                            if (isForm.getForm().isValid()) {
                                //                                Ext.Msg.alert('Submitted Values', 'The following will be sent to the server: <br />' +
                                //                        isForm.getForm().getValues(true));
                                //document.getElementById('selectedValue').value = isForm.getForm().getValues(true);
                                //parent.document.getElementById("btnSave").click();      //触发提交
                                var selectedItemStore = isForm.getForm().findField('itemselector').multiselects[1].store;
                                var retValue = '';      //返回值
                                for (var i = 0; i < selectedItemStore.data.items.length; i++) {
                                    retValue += selectedItemStore.data.items[i].data.value + ';';
                                } 
                                window.returnValue = retValue;
                                window.close();
                            }
                        }
}]
                    });
                    var selectItem = isForm.getForm().findField('itemselector').multiselects[0].store;      //取得可选择的Item
                    var selectedItem = isForm.getForm().findField('itemselector').multiselects[1].store;    //已经选择的Item
                    //debugger;
                    var voItemId = document.getElementById('hfVOItemId').value;
                    //debugger;
                    //通过Ajax取得已经选择的角色信息
                    Ext.Ajax.request({
                        url: '../Ajax/AjaxSupport.ashx?op=GetSelectedRoles&voItemId=' + voItemId,
                        success: function(response, options) {
                            var jsonResponse = Ext.util.JSON.decode(response.responseText);     //取得JSON结果信息
                            for (var i = 0; i < jsonResponse.length; i++) {
                                var record = new Object();
                                record.value = jsonResponse[i].VJobId;
                                record.text = jsonResponse[i].VJobName;
                                var records = new Ext.data.Record(record);
                                selectedItem.add(records);
                            }
                        },
                        failure: function(response, options) {
                            Ext.Msg.alert('无法提交请求');
                        }
                    });

                    Ext.Ajax.request({
                        url: '../Ajax/AjaxSupport.ashx?op=GetAvailableRoles&voItemId=' + voItemId,
                        success: function(response, options) {
                            var jsonResponse = Ext.util.JSON.decode(response.responseText);     //取得JSON结果信息
                            for (var i = 0; i < jsonResponse.length; i++) {
                                var record = new Object();
                                record.value = jsonResponse[i].VJobId;
                                record.text = jsonResponse[i].VJobName;
                                var records = new Ext.data.Record(record);
                                selectItem.add(records);
                            }
                        },
                        failure: function(response, options) {
                            Ext.Msg.alert('无法提交请求');
                        }
                    });
                });
