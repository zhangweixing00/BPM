function alert(msg) {
    top.window.ymPrompt.alert({ title: '系统提示', message: msg.replace(/\n/g, ";").split(";")[0] });
}