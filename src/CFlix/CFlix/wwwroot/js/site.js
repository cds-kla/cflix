// Write your Javascript code.
$(document)
    .ready(function () {

        // create sidebar and attach to menu open
        $('.ui.sidebar').sidebar('attach events', '.toc.item');

        $('#user-display-name').popup({ inline: true, on: 'click' });

        if (typeof moment !== "undefined") {
            moment.locale('fr');
        }

        $(".date").each(function (index, elem) {
            $(elem).text(moment.utc($(elem).data("comment-date")).fromNow());
        });

        $(".eastereggdate").each(function (index, elem) {
            $(elem).text(moment.utc($(elem).data("easter-egg-date")).fromNow());
        });

        $(".newsreleasedate").each(function (index, elem) {
            $(elem).text(moment.utc($(elem).data("news-release-date")).fromNow());
        });

        $(".special.cards .blurring.image").dimmer({ on: 'hover' });

        $(".ui.star.rating").rating();

        $('.ui.dropdown').dropdown('restore defaults');

        $(".mediadate").each(function (index, elem) {
            $(elem).text(moment.utc($(elem).data("date")).calendar());
        });

        $('.ui.rating').rating('setting', 'onRate', function (value) {
            var rater = $(this);
            $.ajax({
                url: "/Achievement/Rate?easterEggId=" + rater.data("easter-egg-id") + "&value=" + value,
                type: "GET"
            }).done(function () {
                rater.rating('disable');
                rater.parent().append("<br /><br /><span>Merci :)</span>");
            });
        });

        $('.ui.embed').embed();
    });