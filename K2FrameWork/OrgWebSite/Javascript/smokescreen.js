/*
 * SmokeScreen v1.0 - Create Informative Textboxes
 * Copyright (C) 2010  John Strickler
 *
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with this program.  If not, see <http://www.gnu.org/licenses/>.
 *
 */

jQuery('<style type="text/css">.smokescreen {color: #AAA}</style>').appendTo('head');

(function($) {

  $.fn.smokescreen = function(options) {

    return this.each(function() {
      $this = $(this);
      if ((this.tagName != "INPUT" || 
          $this.attr('type').toLowerCase() != "text") && (this.tagName != "TEXTAREA")) {
          return; //Exit.
      }

      //Options Precedence (Low to High)
      //Default Options -- Passed Options -- Metadata 
      opts = $.extend({}, $.fn.smokescreen.defaults, options, $this.data()); 


      //Add textbox class and value
      $this.addClass(opts.smokeclass).val(opts.smoke);

      //Add class/value to element's data cache
      $this.data('text.smoke', opts.smoke)
           .data('class.smoke', opts.smokeclass);

	  $this.unbind('.smoke');

      $this.bind('focus.smoke', function() {
        $(this).removeClass($(this).data('class.smoke'));
        if ($(this).val() == $(this).data('text.smoke'))  $(this).val('');
      });
			
      $this.bind('blur.smoke', function() {
        if ($.trim($(this).val()) == "")
        {
          $(this).addClass($(this).data('class.smoke'))
                 .val($(this).data('text.smoke'));
        }
      });
    });
  }
	
  $.fn.smokescreen.defaults = 
  { 
    smoke : 'Enter text...',
    smokeclass : 'smokescreen'
  }; 

})(jQuery);