//保留两位小数
function RoundingFunc(floatNumber){
    if(isNaN(floatNumber)){
        alert('请输入数字');
        return;
    }
    else{
        return (Math.round(floatNumber * 100)/100);
    }
}

//检查日期格式是否合法
function   IsValidDate(ID) 
{     
    var   sDate=ID.value.replace(/(^\s+|\s+$)/g, " ");   //去两边空格; 
    if(sDate== " ") 
    return   true; 
    var   s   =   sDate.replace(/[\d]{4,4}[\-/]{1}[\d]{1,2}[\-/]{1}[\d]{1,2}/g, " "); 
    if   (s== " ")
    { 
    var   t=new   Date(sDate.replace(/\-/g, "/ ")); 
    var   ar   =   sDate.split(/[-/   :]/); 
    var   k   =   false; 
    if(ar[0]   !=   t.getYear()   ||   ar[1]   !=   t.getMonth()+1   ||   ar[2]   !=   t.getDate()) 
    k   =   true; 
    if(k) 
    { 
    //alert( "错误的日期格式！格式为：YYYY-MM-DD"); 
    ID.focus(); 
    return   false; 
    } 
    } 
    else 
    { 
    //alert( "错误的日期格式！格式为：YYYY-MM-DD"); 
    ID.focus(); 
    return   false; 
    } 
    return   true; 
}

String.prototype.endWith=function(str){  
  var reg=new RegExp(str+"$");  
  return reg.test(this);     
}

String.prototype.startWith=function(str){  
  var reg=new RegExp("^"+str);  
  return reg.test(this);     
} 

//将一个标准化金额转换为浮点数
function ConvertToFloat(fee){
    var strList = fee.split(',');
    for(var i = 0;i < strList.length;i++){
        fee = fee.replace(',','');
    }
    return fee;
}

//只能输入数字和小数点（两位小数）
function clearNoNum(obj , e)
{
    if (!e)
        var e = window.event;
    
    //放行左右键及修改键
    if(e.keyCode == 37 || e.keyCode == 39 || e.keyCode == 8){
        return;
    }
    
    //先把非数字的都替换掉，除了数字和.
    obj.value = obj.value.replace(/[^\d.]/g,"");
    //必须保证第一个为数字而不是.
    obj.value = obj.value.replace(/^\./g,"");
    //保证只有出现一个.而没有多个.
    obj.value = obj.value.replace(/\.{2,}/g,".");
    //保证.只出现一次，而不能出现两次以上
    obj.value = obj.value.replace(".","$#$").replace(/\./g,"").replace("$#$",".");
    
    //查找是否已经输入了
    obj.value = obj.value.replace(/^(\-)*(\d+)\.(\d\d).*$/,'$1$2.$3');
}

//只能输入数字和小数点（可以输入任意位小数）
function clearNoNumSaveDecimal(obj , e){
    if (!e)
        var e = window.event;
    
    //放行左右键及修改键
    if(e.keyCode == 37 || e.keyCode == 39 || e.keyCode == 8){
        return;
    }
    
    //先把非数字的都替换掉，除了数字和.
	obj.value = obj.value.replace(/[^\d.]/g,"");
	//必须保证第一个为数字而不是.
	obj.value = obj.value.replace(/^\./g,"");
	//保证只有出现一个.而没有多个.
	obj.value = obj.value.replace(/\.{2,}/g,".");
	//保证.只出现一次，而不能出现两次以上
	obj.value = obj.value.replace(".","$#$").replace(/\./g,"").replace("$#$",".");
    
    //查找是否已经输入了
    obj.value = obj.value.replace(/^(\-)*(\d+)\.(\d\d\d\d).*$/,'$1$2.$3');
}

//从一个Dropdownlist和select控件的转换函数
function ddlToSelect(ddl,selectId){
    if(ddl == null)
        return null;
    else{
        if(ddl.options.length == 0)
            return '';
        
        var returnHTML = '<select id="' + selectId +'">';
        for(var i = 0; i < ddl.options.length ; i++){
            returnHTML += '<option value="';
            returnHTML += ddl.options[i].value;
            if(ddl.options[i].selected)
            {
                returnHTML += '" selected="selected" '
                returnHTML += '>';
            }
            else
                returnHTML += '">';
            returnHTML += ddl.options[i].text;
            returnHTML += '</option>';
        }
        returnHTML += '</select>';
        
        return returnHTML;
    }
}

//取得DropDownList选中的Text
function getDropdownlistText(ddlObj){
    if(ddlObj == null)
        return null;
    else{
        var index = ddlObj.selectedIndex;
        if(index == -1)
            return '';
        return ddlObj.options[index].text;
    }
}

//取得DropDownList选中的Value
function getDropdownlistValue(ddlObj){
    if(ddlObj == null)
        return null;
    else{
        var index = ddlObj.selectedIndex;
        if(index == -1)
            return '';
        return ddlObj.options[index].value;
    }
}

//金额格式化脚本
function fomatMoney(money){
if(/[^0-9\.]/.test(money)) return money;
money=money.replace(/^(\d*)$/,"$1.");
money=(money+"00").replace(/(\d*\.\d\d)\d*/,"$1");
money=money.replace(".",",");
var re=/(\d)(\d{3},)/;
while(re.test(money)){
  money=money.replace(re,"$1,$2");
}
money=money.replace(/,(\d\d)$/,".$1");
return money.replace(/^\./,"0.")
}

//设置默认选中的值（Value）
function setSelected(selectObj,optionsText){
    for(var i=0;i<selectObj.options.length;i++){
        if(selectObj.options[i].value == optionsText){
            selectObj.options[i].selected=true;
            return i;
        }
    }
}


//设置默认选中的值（Text）
function setSelectedText(selectObj,optionsText){
    for(var i=0;i<selectObj.options.length;i++){
        if(selectObj.options[i].text == optionsText){
            selectObj.options[i].selected = true;
            return i;
        }
    }
}

//设置下拉框为只读
function SetReadOnly(obj){
    if(obj){
        obj.onbeforeactivate = function(){return false;};
        obj.onfocus = function(){obj.blur();};
        obj.onmouseover = function(){obj.setCapture();};
        obj.onmouseout = function(){obj.releaseCapture();};
    }
}

//判断是否是整数
function IsInteger(num){
    var reg = /^\d+$/;
    if(reg.test(num)){
        return true;
    }
    else{
        return false;
    }
}

//JSON->Date
function ConvertJSONDateToJSDateObject(JSONDateString) { 
    var date = new Date(parseInt(JSONDateString.replace("/Date(", "").replace(")/", ""), 10)); 
    return date; 
}

//JSON->DateString(yyyy-MM-dd)
function ConvertJSONDateToJSDateString(JSONDateString) { 
    var date = new Date(parseInt(JSONDateString.replace("/Date(", "").replace(")/", ""), 10)).toLocaleString(); 
    return date.replace(/[年]|[月]/g,"-").replace(/[日]/g,"");; 
}