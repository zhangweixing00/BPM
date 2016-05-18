var divArray = new Array();         //保存所有流程结点对象
var selectedPosition = -1;          //当前的选择位置
var intervalHeight = 105;           //结点之间上下间隔高度
var intervalLeft = 500;             //结点左侧间隔距离
var sign = false;                   //判断单、双击事件

//定义流程结点类型
var FlowPoint = function(x,y){
    this.xPoint = x;                    //x坐标
    this.yPoint = y;                    //y坐标
    this.title = '';                    //当前结点Title
    this.enabled = true;                //是否可用（默认为true）
    this.selected = false;              //是否被选中
    this.deleted = true;                //是否可以删除
    this.isView = false;                //是否是查看模式
    this.userId = '';                   //选择的ID账号
    this.userName = '';                 //用户名
    this.current = false;               //是否是当前结点(type = current)
    this.counterSigns = new Array();    //保存会签人数组
};

//定义会签结点类
var CounterSignClass = function(){
    this.counterSignId = '';            //会签人Id
    this.counterSignName = '';          //会签人姓名
    this.enabled = true;                //是否可用
}

//插入到某个位置
Array.prototype.InsertAt = function(obj , pos){
    this.splice(pos,0,obj);
};

//清空
Array.prototype.clear = function(){
    this.length = 0;
};

//删除某个元素
Array.prototype.removeAt = function(pos){
    this.splice(pos,1);
};


//添加结点
function AddNode(){
    var top = (divArray.length + 1) * intervalHeight;
    var left = intervalLeft;                                    //左边距
    var tmpFlowPoint = new FlowPoint(left,top);                 //添加到数组保存
    if(selectedPosition == -1){
        divArray.InsertAt(tmpFlowPoint,divArray.length);        //没有元素被选中，则添加到最后
    }
    else{
        divArray.InsertAt(tmpFlowPoint,selectedPosition + 1);   //添加到选中元素的后面
        
        //调整数组的yPoint、xPoint值
        for(var i = 0;i < divArray.length;i++){
            divArray[i].yPoint = (i + 1) * intervalHeight;
            divArray[i].xPoint = intervalLeft;
        }
    }
    DisplayFlow();                                              //生成div并显示
}

//删除相应的结点
function CloseDiv(divObject){
    var ret = confirm('确定删除?');
    if(ret){
        if(divObject.parentNode.parentNode != null){
            for(var i = 0;i < $('.flowNode').length;i++){
                if($('.flowNode')[i].id === divObject.parentNode.parentNode.id){
                    divArray.removeAt(i);                           //从数组从删除相应元素
                    
                    //调整元素显示位置
                    for(var j = 0;j < divArray.length;j++){
                        divArray[j].yPoint = (j + 1) * intervalHeight;
                        divArray[j].xPoint = intervalLeft;
                    }
                    break;
                }
            }
            DisplayFlow();                                          //生成流程图
        }
    }
}

//删除会签结点
function CloseSignNode(divId,delId,delName){
    var ret = confirm('确定删除?');
    if(ret){
        if(divId != null){
            for(var i = 0;i < $('.flowNode').length;i++){
                if($('.flowNode')[i].id == divId){
                    for(var j = 0;j < divArray[i].counterSigns.length;j++){
                        if(divArray[i].counterSigns[j].counterSignId == delId && divArray[i].counterSigns[j].enabled)
                            divArray[i].counterSigns.removeAt(j);
                    }
                    break;
                }
            }
            DisplayFlow();                  //重新绘制流程图
        }
    }
}

//添加会签人
function counterSign(imgObject){
    var returnVal = window.showModalDialog('../CommonPage/SelectPersonnel.aspx',null,'dialogHeight:375px;dialogWidth:575px;scroll:0');
    if(returnVal == null || returnVal.length == 0){
        return;
    }
    else if(returnVal[0] == "" && returnVal[1] == ""){
        return;
    }
    else{
        if(imgObject.parentNode.parentNode != null){
            for(var i = 0;i < $('.flowNode').length;i++){
                if($('.flowNode')[i].id === imgObject.parentNode.parentNode.id){
                    var tmpIds = returnVal[0].split(';');
                    var tmpNames = returnVal[1].split(';');
                    var tmpPNames = returnVal[2].split(';');
                    var tmpdeptNames = returnVal[3].split(';');
                    var isOk = true;                            //标示是否可以添加
                    for(var j = 0;j < tmpIds.length;j++){
                        for(var k = 0;k < divArray[i].counterSigns.length;k++){
                            if(divArray[i].counterSigns[k].enabled && divArray[i].counterSigns[k].counterSignId == tmpIds[j]){
                                isOk = false;                   //当所选择人中含有已存在的，则不添加
                            }
                        }
                        if(isOk){
                            var counterSign = new CounterSignClass();
                            counterSign.counterSignId = tmpIds[j];
                            counterSign.counterSignName = '&nbsp;&nbsp;&nbsp;姓名：' + tmpNames[j] + '(' + tmpPNames[j] + ')' + '\n&nbsp&nbsp&nbsp部门：' + tmpdeptNames[j];
                            counterSign.enabled = true;
                            divArray[i].counterSigns.push(counterSign);
                        }
                    }
                }
            }
            DisplayFlow();              //重新产生流程图
        }
    }
}

//根据数组中的内容显示流程图
function DisplayFlow(){
    var Area = document.getElementById('InteractiveArea');      //取得活动区域
    Area.innerHTML = '';
    var maxWidth = 0;              //标示最大宽度
    for(var i = 0;i < divArray.length;i++){
        if(divArray[i].userName == null)
            divArray[i].userName = '';
        //如果未走到该结点 (可选中、可删除、可修改)
        if(divArray[i].enabled && !divArray[i].current){
            if(divArray[i].selected && !divArray[i].isView){           //被选中并且不是查看模式
                Area.innerHTML += '<div id="divPanel'+ i +'" class="flowNode" style="position:absolute; top:'+ divArray[i].yPoint +'px; left:'+ divArray[i].xPoint +'px; border-right: #ff0000 1px dashed; border-top: #ff0000 1px dashed; border-left: #ff0000 1px dashed; border-bottom: #ff0000 1px dashed; background-image:url()"><div id="divTitle'+ i +'" class="flowTitle"><span style="float:left; padding-top:7px; padding-left:5px;">'+ divArray[i].title +'</span><img src="../Images/close.gif" style="float:right; padding-right:3px; padding-top:8px;" alt="删除结点" onclick="CloseDiv(this);" /></div><div style="width:100%; height:68px;" onclick="SelectDiv(this);" ondblclick="SelectUser(this);"><span class="nodeName" ondblclick="SelectUser(this.parentNode);">'+ divArray[i].userName.replace(/\n/g,"<br/>") +'</span></div></div>';
            }
            else if(!divArray[i].selected && !divArray[i].isView){      //未被选中并且不是查看模式
                Area.innerHTML += '<div id="divPanel'+ i +'" class="flowNode" style="position:absolute; top:'+ divArray[i].yPoint +'px; left:'+ divArray[i].xPoint +'px;"><div id="divTitle'+ i +'" class="flowTitle"><span style="float:left; padding-top:7px; padding-left:5px;">'+ divArray[i].title +'</span><img src="../Images/close.gif" style="float:right; padding-right:3px; padding-top:8px;" alt="删除结点" onclick="CloseDiv(this);" /></div><div style="width:100%; height:68px;" onclick="SelectDiv(this);" ondblclick="SelectUser(this);"><span class="nodeName">'+ divArray[i].userName.replace(/\n/g,"<br/>") +'</span></div></div>';
            }
            else{
                Area.innerHTML += '<div id="divPanel'+ i +'" class="flowNode" style="position:absolute; top:'+ divArray[i].yPoint +'px; left:'+ divArray[i].xPoint +'px;"><div id="divTitle'+ i +'" class="flowTitle"><span style="float:left; padding-top:7px; padding-left:5px;">'+ divArray[i].title +'</span></div><div style="width:100%; height:100%;"><span class="nodeName">'+ divArray[i].userName.replace(/\n/g,"<br/>") +'</span></div></div>';
            }
        }
        //如果该结点是当前正在走的结点（可选中、不可删除、不可修改、可会签）
        else if(divArray[i].enabled && divArray[i].current){
            if(divArray[i].selected && !divArray[i].isView){           //被选中
                Area.innerHTML += '<div id="divPanel'+ i +'" class="flowNode" style="position:absolute; top:'+ divArray[i].yPoint +'px; left:'+ divArray[i].xPoint +'px; border-right: #ff0000 1px dashed; border-top: #ff0000 1px dashed; border-left: #ff0000 1px dashed; border-bottom: #ff0000 1px dashed; background-image:url()"><div id="divTitle'+ i +'" class="flowTitle"><span style="float:left; padding-top:7px; padding-left:5px;">'+ divArray[i].title +'</span><img src="../Images/additem.gif" style="float:right; padding-right:3px; padding-top:8px;" alt="会签" onclick="counterSign(this);" /></div><div style="width:100%; height:100%;" onclick="SelectDiv(this);"><span class="nodeName">'+ divArray[i].userName.replace(/\n/g,"<br/>") +'</span></div></div>';
            }
            else if(!divArray[i].selected && !divArray[i].isView){
                Area.innerHTML += '<div id="divPanel'+ i +'" class="flowNode" style="position:absolute; top:'+ divArray[i].yPoint +'px; left:'+ divArray[i].xPoint +'px;"><div id="divTitle'+ i +'" class="flowTitle"><span style="float:left; padding-top:7px; padding-left:5px;">'+ divArray[i].title +'</span><img src="../Images/additem.gif" style="float:right; padding-right:3px; padding-top:8px;" alt="会签" onclick="counterSign(this);" /></div><div style="width:100%; height:100%;" onclick="SelectDiv(this);"><span class="nodeName">'+ divArray[i].userName.replace(/\n/g,"<br/>") +'</span></div></div>';
            }
            else{
                Area.innerHTML += '<div id="divPanel'+ i +'" class="flowNode" style="position:absolute; top:'+ divArray[i].yPoint +'px; left:'+ divArray[i].xPoint +'px;"><div id="divTitle'+ i +'" class="flowTitle"><span style="float:left; padding-top:7px; padding-left:5px;">'+ divArray[i].title +'</span></div><div style="width:100%; height:100%;"><span class="nodeName">'+ divArray[i].userName.replace(/\n/g,"<br/>") +'</span></div></div>';
            }
        }
        //如果该结点已经走过（不可选中、不可删除、不可修改）
        else if(!divArray[i].enabled){
            Area.innerHTML += '<div id="divPanel'+ i +'" class="flowNode" style="position:absolute; top:'+ divArray[i].yPoint +'px; left:'+ divArray[i].xPoint +'px;"><div id="divTitle'+ i +'" class="unavailableFlowTitle"><span style="float:left; padding-top:7px; padding-left:5px;">'+ divArray[i].title +'</span></div><div style="width:100%; height:100%;"><span class="nodeName">'+ divArray[i].userName.replace(/\n/g,"<br/>") +'</span></div></div>';
        }
        
        //产生会签人
        if(divArray[i].counterSigns.length != 0){                 //会签
            for(var k = 0;k < divArray[i].counterSigns.length;k++){
                var counterSignLeft = (divArray[i].xPoint + 230) + 160 * k;      //会签左边距
                if(maxWidth < counterSignLeft)
                    maxWidth = counterSignLeft;
                var counterSignTop = divArray[i].yPoint + 15;                         //会签高度
                if(divArray[i].counterSigns[k].enabled){
                    Area.innerHTML += '<div class="counterSignNode" style="position:absolute; left:'+ counterSignLeft +'px; top:'+ counterSignTop +'px;"><div class="counterSignTitle"><img alt="删除" src="../Images/close.gif" onclick="CloseSignNode(\'divPanel'+ i +'\','+ divArray[i].counterSigns[k].counterSignId +',\'' + divArray[i].counterSigns[k].counterSignName.replace(/\n/g,"<br/>") + '\');" style="float:right; padding-right:3px; padding-top:2px;" /></div><div ondblclick="EditSignNode(\'divPanel'+ i +'\','+ divArray[i].counterSigns[k].counterSignId +',\'' + divArray[i].counterSigns[k].counterSignName.replace(/\n/g,"<br/>") + '\');">'+ divArray[i].counterSigns[k].counterSignName.replace(/\n/g,"<br/>") +'</div></div>';
                }
                else{
                    Area.innerHTML += '<div class="counterSignNode" style="position:absolute; left:'+ counterSignLeft +'px; top:'+ counterSignTop +'px; background-color:Gray;"><div class="counterSignTitle"></div><div>'+ divArray[i].counterSigns[k].counterSignName.replace(/\n/g,"<br/>") +'</div></div>';
                }
            }
        }
    }
    
    if(divArray != null && divArray.length != 0)
    {
        //调整div高度
        var divHeight = divArray[divArray.length - 1].yPoint + 50;
        $('#InteractiveArea').css('height',divHeight);
        if(maxWidth != 0){
            $($('#divFlow')[0].children[0]).css('width',((divArray[0].xPoint + maxWidth + 400)/2));
            $('#divFlow').css('width',((divArray[0].xPoint + maxWidth + 400)/2));
        }
    }
}

//选择人
function SelectUser(divObject){
    sign = true;
    var returnVal = window.showModalDialog('../CommonPage/SelectPersonnel.aspx',null,'dialogHeight:375px;dialogWidth:575px;scroll:0');
    if(returnVal == null || returnVal.length == 0){
        return;
    }
    else if(returnVal[0] == "" && returnVal[1] == ""){
        return;
    }
    else{
        if(divObject.parentNode != null){
            for(var i = 0;i < $('.flowNode').length;i++){
                if($('.flowNode')[i].id === divObject.parentNode.id){
                    divArray[i].userId = returnVal[0].split(';')[0];
                    divArray[i].userName = '\n&nbsp;&nbsp;&nbsp;姓名：' + returnVal[1].split(';')[0] + '(' + returnVal[2].split(';')[0] + ')' + '\n--------------------------------------------------\n' + '&nbsp;&nbsp;&nbsp;部门：' + returnVal[3].split(';')[0];
                }
            }
            
            DisplayFlow();              //重新产生流程图
        }
    }
}

//编辑会签结点
function EditSignNode(divId,userId,userName){
    var returnVal = window.showModalDialog('../CommonPage/SelectPersonnel.aspx',null,'dialogHeight:375px;dialogWidth:575px;scroll:0');
    if(returnVal == null || returnVal.length == 0){
        return;
    }
    else if(returnVal[0] == "" && returnVal[1] == ""){
        return;
    }
    else{
        for(var i = 0;i < $('.flowNode').length;i++){
            if($('.flowNode')[i].id == divId){              //查找对应的divArray的index
                for(var j = 0;j < divArray[i].counterSigns.length;j++){
                    if(divArray[i].counterSigns[j].enabled && divArray[i].counterSigns[j].counterSignId == userId){
                        divArray[i].counterSigns[j].counterSignId = returnVal[0].split(';')[0];
                        divArray[i].counterSigns[j].counterSignName = '&nbsp;&nbsp;&nbsp;姓名：' + returnVal[1].split(';')[0] + '(' + returnVal[2].split(';')[0] + ')' + '\n&nbsp;&nbsp;&nbsp;部门：' + returnVal[3].split(';')[0];
                        break;
                    }
                }
            }
        }
        DisplayFlow();
    }
}

//点选某个结点
function SelectDiv(divObject){
    setTimeout(function(){
        if(sign){
            sign = false;
            return;
        }
        if(divObject != null){
            for(var i = 0; i<divArray.length; i++){
                if(divObject.parentNode != null && divObject.parentNode.id != null && divObject.parentNode.id == ('divPanel' + i)){
                    if(divArray[i].enabled && !divArray[i].selected){
                        divArray[i].selected = true;
                        selectedPosition = i;
                    }
                    else{
                        divArray[i].selected = false;
                        selectedPosition = -1;
                    }
                }
                else if(divArray[i].enabled){
                    divArray[i].selected = false;
                }
            }DisplayFlow();
        }
    },200);
}

//验证流程图信息是否填写完整
function VerificationFlowIntegrity(){
    if(divArray != null && divArray.length != 0){
        for(var i = 0;i < divArray.length;i++){
            if(divArray[i].userId == '' || divArray[i].userName == '' || divArray[i].userName == '未找到用户信息'){
                return false;
            }
            if(divArray[i].counterSigns != null && divArray[i].counterSigns.length != 0){       //检查会签人
                for(var j = 0;j < divArray[i].counterSigns.length;j++){
                    if(divArray[i].counterSigns[j].counterSignId == '' || divArray[i].counterSigns[j].counterSignName == ''){
                        return false;
                    }
                }
            }
        }
        return true;
    }
    else{
        return false;
    }
}

$(document).ready(function(){
    ResumeFlowInfo();
});