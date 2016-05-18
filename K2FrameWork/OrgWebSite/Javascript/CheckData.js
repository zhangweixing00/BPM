// 时间比较函数，如果开始时间不大于结束时间则提示错误信息并返回 false
//bdate 开始时间ID ,edate 结束时间ID
function CheckDate(bdate, edate) {
    
    var bvalue = document.getElementById(bdate).value;
    var evalue = document.getElementById(edate).value;
    if (bvalue != null && bvalue != "undefind" && evalue != null && evalue != "undefind") {
        if (bvalue.length > 9 && evalue.length > 9) {
            var bvalus = bvalue.split("-");
            var evalus = evalue.split("-");

            var d1 = new Date(bvalus[0], bvalus[1], bvalus[2]);
            var d2 = new Date(evalus[0], evalus[1], evalus[2]);
            if (Date.parse(d1) - Date.parse(d2) >= 0) {
                alert("开始时间不能大于结束时间");
                return false;
            }
        }
    }
}