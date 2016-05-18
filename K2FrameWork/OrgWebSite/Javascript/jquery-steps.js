/**
* jquery.steps-0.1,js - Steps plugin for jQuery
* ================================================
* (C) 2011 José Ramón Díaz - jrdiazweb@gmail.com
* 
* Instantiation: $('.stepsDiv').steps()
* 
* Functions:
*     $('.stepsDiv').steps('getStep') - Returns current step for stepDiv
*     $('.stepsDiv').steps('start')   - Resets state to initial step
*     $('.stepsDiv').steps('finish')  - Sets state to completed
*     $('.stepsDiv').steps('next')    - Sets state to next state
*     $('.stepsDiv').steps('prev')    - Sets state to previous state
*     
* Classes needed and meaning
*     N/A              - Uncompleted step. Greyed by default
*     end              - Last uncompleted step. Greyed and no "arrow"
*     last             - Boundary class. Marks last completed step and last step
*     current          - Current step
*     completed        - Completed step
*     completedLast    - Last completed step
*     completedLastEnd - Last step when all steps are completed
* 
* Extending steps with representation
*     You can also use the steps CSS. You can find it at
*     http://3nibbles.blogspot.com/2011/06/pasos-de-wizard.html
*     
* Legal stuff
*     You are free to use this CSS, but you must give credit or keep header intact.
*     Please, tell me if you find it useful. An email will be enough.
*     If you enhance this code or correct a bug, please, tell me.
* 
*/
(function ($) {

    $.fn.steps = function (options) {

        var self = this;


        //////////////////////////////////////////////////////////////////////////////////
        // DEFAULT VALUES
        var defaults = {};

        //////////////////////////////////////////////////////////////////////////////////
        // PRIVATE FUNCTIONS
        var init = function (options) {

            // If options exist, merge them with default settings
            if (options) $.extend(defaults, options);

            var $this = $(this);
            var data = $this.data('steps');

            return this.each(function () {

                var $this = $(this);
                data = $this.data('steps');

                // The plugin hasn't been initialized yet
                if (!data) {

                    // Initializes the plugin data
                    $(this).data('steps', {
                        target: $this,
                        step: 0
                    });

                };
            });

        };

        var destroy = function () {
            return this.each(function () { self.removeData('steps'); })
        };

        //////////////////////////////////////////////////////////////////////////////////
        // PUBLIC FUNCTIONS
        var methods = {
            getStep: function () { return self.data('steps')['step']; },
            start: function () { return methods.setStep(0); },
            finish: function () {
                var l = self.find('ul li').length;
                methods.setStep(l);
                self.data('steps')['step'] = l;
                return l;
            },
            prev: function () {
                var step = methods.getStep();
                var l = self.find('ul li').length;
                if (step == l) step = step - 1;
                return methods.setStep(step - 1);
            },
            next: function () {
                return methods.setStep(methods.getStep() + 1);
            },
            setStep: function (stepNumber) {
                // Sets the step number
                var l = self.find('ul li').length;
                if (stepNumber < 0) stepNumber = 0;
                if (stepNumber > l) stepNumber = l;

                // Resets styles
                self.find('ul li').removeClass('current completed completedLast last end completedLastEnd');

                // Styles for intermediate steps
                self.find('ul li:lt(' + ((stepNumber < l) ? stepNumber : l - 1) + ')').addClass('completed');

                if (stepNumber > 0 && stepNumber < l)
                    self.find('ul li:nth(' + (stepNumber - 1) + ')').addClass('completedLast');
                if (stepNumber < l)
                    self.find('ul li:nth(' + stepNumber + ')').addClass('current');

                // Last step style
                if (stepNumber == l)
                    self.find('ul li:last').addClass('completedLastEnd');
                //if(stepNumber == l-1)  
                //    self.find('ul li:last').addClass('currentEnd');
                else
                    self.find('ul li:last').addClass('end');

                self.data('steps')['step'] = stepNumber;
                return stepNumber;
            }

        };

        /////////////////////////////////////////////////////////////////////////////////////
        // Decides what to do
        if (methods[options]) {
            return methods[options].apply(self, Array.prototype.slice.call(arguments, 1));
        } else if (typeof options === 'object' || !options) {
            return init.apply(self, arguments);
        } else {
            //$.error( 'Method ' +  options + ' does not exist on jQuery.tooltip' );
            return init.apply(self, {});
        }

    };
})(jQuery);