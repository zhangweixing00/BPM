//插入到某个位置
Array.prototype.InsertAt = function (obj, pos) {
    this.splice(pos, 0, obj);
};

//清空
Array.prototype.clear = function () {
    this.length = 0;
};

//删除某个元素
Array.prototype.removeAt = function (pos) {
    this.splice(pos, 1);
};