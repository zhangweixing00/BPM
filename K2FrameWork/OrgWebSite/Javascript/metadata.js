/*
 * Metadata jQuery Plugin v1.0
 * Populates the jQuery Data Object from HTML5 custom data-attributes
 * Copyright (C) 2010  John B. Strickler
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



(function($) {

	$.fn.metadata = function(options) 
	{
		return this.each(function() {
			$that = $(this);
			
			if(this.dataset) {
				$that.data(this.dataset);
			}
			else {
				$.each(this.attributes, function(index, attrib) {
					if(attrib.name.indexOf("data-") == 0) {
						$that.data(attrib.name.substring(5), attrib.value);
					}
				});
			}
						
		});
	}
		
	$.metadata = true;

})(jQuery);