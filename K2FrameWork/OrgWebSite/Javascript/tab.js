// JScript File
function TabControl(tabs, tabStrip, Id, currentTab, top) {
    //var tabs = new Array("","Address|divAddress|*", "Control Data|divControlData","Payment|divPayment","Post Infomation|divPostInfomation");
    var HTML = "";

    this.divTabStrip = tabStrip;
    this.docId = Id;
    this.tabOnClick = function(ID) {
        var oElement = null;
        for (var i = 1; i < tabs.length; i++) {
            var childID = i;
            var tab = tabs[i].split("|");
            divElement = document.getElementById(tab[1]);
            divElement.style.display = "none";

            oElement = document.getElementById(this.docId + childID);
            oElement.className = top ? "topOff" : "tabOff";
        }
        //alert(this.docId + ID);
        oElement = document.getElementById(this.docId + ID);
        oElement.className = top ? "topOn" : "tabOn";

        var tab = tabs[ID].split("|");
        divElement = document.getElementById(tab[1]);
        divElement.style.display = "block";
        divElement.className = top ? "top" : "tab";

        document.body.focus();
    }


    this.tabLoadParents = function() {

        for (var i = 1; i < tabs.length; i++) {
            var tab = tabs[i].split("|");
            HTML += "<INPUT TYPE='BUTTON' ID=" + this.docId + i + " CLASS='tabOff' VALUE='" + tab[0] + "' onClick='" + currentTab + ".tabOnClick(" + i + ")'>&nbsp";
        }
    }


    this.divElement = null;
    this.tabOnLoad = function() {
        var oElement = null;
        //HTML += "<TABLE BORDER='0' CELLPADDING='0' CELLSPACING='0' WIDTH='100%' STYLE='font-size:18px'>";
        if (top) {
            HTML += "<TABLE BORDER='0' CELLPADDING='0' CELLSPACING='0' WIDTH='100%' STYLE='font-size:18px;'>";

            HTML += "<TD ALIGN='Left'>&nbsp;&nbsp;";
        }
        else {
            HTML += "<TABLE BORDER='0' CELLPADDING='0' CELLSPACING='0' WIDTH='100%' STYLE='font-size:18px'>";

            HTML += "<TD ALIGN='LEFT'>&nbsp;&nbsp;";
        }

        this.tabLoadParents();
        //tabLoadChildren();

        HTML += "</TD></TR>";
        HTML += "</TABLE>";

        this.divTabStrip.innerHTML = HTML;

        for (var i = 1; i < tabs.length; i++) {
            var childID = i;
            var tab = tabs[i].split("|");
            divElement = document.getElementById(tab[1]);
            divElement.style.display = "none";

            oElement = document.getElementById(this.docId + childID);
            oElement.className = "tabOff";
        }

        for (var i = 1; i < tabs.length; i++) {
            var tab = tabs[i].split("|");

            if (tab[2] == "*") {
                this.tabOnClick(i);

                break;
            }
        }
    }
}