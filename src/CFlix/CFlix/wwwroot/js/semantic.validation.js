$(function () {
    $('.field-validation-error')
        .removeClass("hidden").addClass("visible")
        .parents(".field").addClass("error");

    function escapeAttributeValue(value) {
        // As mentioned on http://api.jquery.com/category/selectors/
        return value.replace(/([!"#$%&'()*+,./:;<=>?@\[\\\]^`{|}~])/g, "\\$1");
    }

    if ($.validator) {
        $.validator.setDefaults({
            highlight: function (element, errorClass, validClass) {
                $(element).closest('.field')
                    .addClass('error')
                    .find("[data-valmsg-for='" + escapeAttributeValue(element.name) + "']")
                    .filter(".hidden").transition("drop");
            },
            unhighlight: function (element, errorClass, validClass) {
                $(element).closest('.field')
                    .removeClass('error')
                    .find("[data-valmsg-for='" + escapeAttributeValue(element.name) + "']")
                    .transition("hide");
            }
        });

        $.validator.unobtrusive.options = {
            errorClass: "error",
            errorElement: "span",
            invalidHandler: function (error, inputElement) {
                $(this).find("[data-valmsg-summary=true]")
                    .addClass("visible");
            }
        };
    }
});