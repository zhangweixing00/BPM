/// <reference path="jquery-1.4.1-vsdoc.js" />

var outerLayout, innerLayout;

/*
*#######################
* INNER LAYOUT SETTINGS
*#######################
*
* These settings are set in 'list format' - no nested data-structures
* Default settings are specified with just their name, like: fxName:"slide"
* Pane-specific settings are prefixed with the pane name + 2-underscores: north__fxName:"none"
*/
layoutSettings_Inner = {
    applyDefaultStyles: true			// basic styling for testing & demo purposes
	, minSize: 20						// TESTING ONLY
	, spacing_closed: 14
	, north__spacing_closed: 8
	, south__spacing_closed: 8
	, north__togglerLength_closed: -1	// = 100% - so cannot 'slide open'
	, south__togglerLength_closed: -1
	, fxName: "slide"					// do not confuse with "slidable" option!
	, fxSpeed_open: 1000
	, fxSpeed_close: 2500
	, fxSettings_open: { easing: "easeInQuint" }
	, fxSettings_close: { easing: "easeOutQuint" }
	, north__fxName: "none"
	, south__fxName: "drop"
	, south__fxSpeed_open: 100
	, south__fxSpeed_close: 100
    //,	initClosed: true
	, center__minWidth: 200
	, center__minHeight: 200
};

/*
*#######################
* OUTER LAYOUT SETTINGS
*#######################
*
* This configuration illustrates how extensively the layout can be customized
* ALL SETTINGS ARE OPTIONAL - and there are more available than shown below
*
* These settings are set in 'sub-key format' - ALL data must be in a nested data-structures
* All default settings (applied to all panes) go inside the defaults:{} key
* Pane-specific settings go inside their keys: north:{}, south:{}, center:{}, etc
*/
var layoutSettings_Outer = {
    name: "outerLayout" // NO FUNCTIONAL USE, but could be used by custom code to 'identify' a layout
    // options.defaults apply to ALL PANES - but overridden by pane-specific settings
	, defaults: {
	    size: "auto"
		, minSize: 50
		, paneClass: "pane" 				// default = 'ui-layout-pane'
		, resizerClass: "resizer"			// default = 'ui-layout-resizer'
		, togglerClass: "toggler"			// default = 'ui-layout-toggler'
		, buttonClass: "button"				// default = 'ui-layout-button'
		, contentSelector: ".content"		// inner div to auto-size so only it scrolls, not the entire pane!
		, contentIgnoreSelector: "span"		// 'paneSelector' for content to 'ignore' when measuring room for content
		, togglerLength_open: 35			// WIDTH of toggler on north/south edges - HEIGHT on east/west edges
		, togglerLength_closed: 35			// "100%" OR -1 = full height
		, hideTogglerOnSlide: true			// hide the toggler when pane is 'slid open'
		, togglerTip_open: "关闭区域"
		, togglerTip_closed: "打开区域"
		, resizerTip: "改变大小"
	    //	effect defaults - overridden on some panes
		, fxName: "slide"					// none, slide, drop, scale
		, fxSpeed_open: 100
		, fxSpeed_close: 100
		, fxSettings_open: { easing: "easeInQuint" }
		, fxSettings_close: { easing: "easeOutQuint" }
	}
	, north: {
	    spacing_open: 1						// cosmetic spacing
		, togglerLength_open: 0				// HIDE the toggler button
		, togglerLength_closed: -1			// "100%" OR -1 = full width of pane
		, resizable: false
		, slidable: false
	    //	override default effect
		, fxName: "none"
	}
    //	, south: {
    //	    maxSize: 200
    //		, spacing_closed: 8					// HIDE resizer & toggler when 'closed'
    //		, slidable: false					// REFERENCE - cannot slide if spacing_closed = 0
    //		, initClosed: true
    //		
    //	    //	CALLBACK TESTING...
    //		//, onhide_start: function() { return confirm("START South pane hide \n\n onhide_start callback \n\n Allow pane to hide?"); }
    //		//, onhide_end: function() { alert("END South pane hide \n\n onhide_end callback"); }
    //		//, onshow_start: function() { return confirm("START South pane show \n\n onshow_start callback \n\n Allow pane to show?"); }
    //		//, onshow_end: function() { alert("END South pane show \n\n onshow_end callback"); }
    //		//, onopen_start: function() { return confirm("START South pane open \n\n onopen_start callback \n\n Allow pane to open?"); }
    //		//, onopen_end: function() { alert("END South pane open \n\n onopen_end callback"); }
    //		//, onclose_start: function() { return confirm("START South pane close \n\n onclose_start callback \n\n Allow pane to close?"); }
    //		//, onclose_end: function() { alert("END South pane close \n\n onclose_end callback"); }
    //	    ////, onresize_start: function () { return confirm("START South pane resize \n\n onresize_start callback \n\n Allow pane to be resized?)"); }
    //		//, onresize_end: function() { alert("END South pane resize \n\n onresize_end callback \n\n NOTE: onresize_start event was skipped."); }
    //	}
	, west: {
	    size: 180
		, spacing_closed: 21				// wider space when closed
		, togglerLength_closed: 21			// make toggler 'square' - 21x21
		, togglerAlign_closed: "top"		// align to top of resizer
		, togglerLength_open: 0				// NONE - using custom togglers INSIDE west-pane
		, togglerTip_open: "关闭区域"
		, togglerTip_closed: "打开区域"
		, resizerTip_open: "改变大小"
		, slideTrigger_open: "click" 		// default  mouseover
		, initClosed: false
	    // add 'bounce' option to default 'slide' effect
	    //, fxSettings_open: { easing: "easeOutBounce" }
	}
    //	, east: {
    //	    size: 250
    //		, spacing_closed: 21				// wider space when closed
    //		, togglerLength_closed: 21			// make toggler 'square' - 21x21
    //		, togglerAlign_closed: "top"		// align to top of resizer
    //		, togglerLength_open: 0 			// NONE - using custom togglers INSIDE east-pane
    //		, togglerTip_open: "关闭区域"
    //		, togglerTip_closed: "打开区域"
    //		, resizerTip_open: "改变大小"
    //		, slideTrigger_open: "mouseover"
    //		, initClosed: true
    //	    //	override default effect, speed, and settings
    //		, fxName: "drop"
    //		, fxSpeed: "normal"
    //		, fxSettings: { easing: ""}			// nullify default easing
    //	}
	, center: {
	    paneSelector: "#maincontent" 		// sample: use an ID to select pane instead of a class
		, onresize: "innerLayout.resizeAll"	// resize INNER LAYOUT when center pane resizes
		, minWidth: 200
		, minHeight: 200
	}
};

/*
*#######################
*     ON PAGE LOAD
*#######################
*/
$(document).ready(function() {

    // create the OUTER LAYOUT
    outerLayout = $("body").layout(layoutSettings_Outer);

    /*******************************
    ***  CUSTOM LAYOUT BUTTONS  ***
    *******************************
    *
    * Add SPANs to the east/west panes for customer "close" and "pin" buttons
    *
    * COULD have hard-coded span, div, button, image, or any element to use as a 'button'...
    * ... but instead am adding SPANs via script - THEN attaching the layout-events to them
    *
    * CSS will size and position the spans, as well as set the background-images
    */

    // BIND events to hard-coded buttons in the NORTH toolbar
    //    outerLayout.addToggleBtn("#tbarToggleNorth", "north");
    //outerLayout.addOpenBtn("#tbarOpenSouth", "south");
    //outerLayout.addCloseBtn("#tbarCloseSouth", "south");
    //outerLayout.addPinBtn("#tbarPinWest", "west");
    //outerLayout.addPinBtn("#tbarPinEast", "east");

    // save selector strings to vars so we don't have to repeat it
    // must prefix paneClass with "body > " to target ONLY the outerLayout panes
    var westSelector = "#ui-west"; // outer-west pane
    //var eastSelector = "#ui-east"; // outer-east pane

    // CREATE SPANs for pin-buttons - using a generic class as identifiers
    $("<span class='pin'></span>").addClass("pin-button").prependTo(westSelector);
    //$("<span></span>").addClass("pin-button").prependTo(eastSelector);
    // BIND events to pin-buttons to make them functional
    outerLayout.addPinBtn(westSelector + " .pin-button", "west");
    //outerLayout.addPinBtn(eastSelector + " .pin-button", "east");

    // CREATE SPANs for close-buttons - using unique IDs as identifiers
    $("<span class='pin'></span>").attr("id", "west-closer").prependTo(westSelector);
    //$("<span></span>").attr("id", "east-closer").prependTo(eastSelector);
    // BIND layout events to close-buttons to make them functional
    outerLayout.addCloseBtn("#west-closer", "west");
    //outerLayout.addCloseBtn("#east-closer", "east");


    /* Create the INNER LAYOUT - nested inside the 'center pane' of the outer layout
    * Inner Layout is create by createInnerLayout() function - on demand
    *
    innerLayout = $("div.pane-center").layout( layoutSettings_Inner );
    *
    */

    // DEMO HELPER: prevent hyperlinks from reloading page when a 'base.href' is set
    $("a").each(function() {
        var path = document.location.href;
        if (path.substr(path.length - 1) == "#") path = path.substr(0, path.length - 1);
        if (this.href.substr(this.href.length - 1) == "#") this.href = path + "#";
    });
});