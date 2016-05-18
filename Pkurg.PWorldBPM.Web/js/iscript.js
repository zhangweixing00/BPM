/// <reference path="jquery-1.4.1-vsdoc.js" />

/*!
* iscript JavaScript Library v0.0.9
*
* Copyright 2010, pcict.com
* Dual licensed under the MIT or GPL Version 2 licenses.
*
* Includes jQuery-1.4.2-min.js
* http://www.jquery.com/
* Copyright 2010, The jquery Foundation
* Released under the MIT, BSD, and GPL Licenses.
*
* Date: Sat Feb 13 22:33:48 2010 -0500
*/

(function(window, undefined) {
    var iScript = function(selector, context) {
        return new iscript.fn.init(selector, context);
    };

    iScript.fn = iScript.prototype = {
        init: function(selector, context) { $(selector, context); },
        ready: function(fn) { $(fn); },

        version: '0.0.9',
        createDate: '2010.03.23',

        test: function(string) { alert(string); }
    };

    window.iScript = iScript.fn;
})(window);