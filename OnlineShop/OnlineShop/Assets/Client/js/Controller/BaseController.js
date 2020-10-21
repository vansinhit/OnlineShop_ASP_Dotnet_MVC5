var common = {
    init: function () {
        common.registerEvent();
    },
    registerEvent: function () {
        $("#txtSeachString").autocomplete({
            minLength: 0,
            source: function (request, response) {
                $.ajax({
                    url: "/ListProduct/Index",
                    dataType: "json",
                    data: {
                        q: request.term
                    },
                    success: function (res) {
                        response(res.data);
                    }
                });
            },
            focus: function (event, ui) {
                $("#txtSeachString").val(ui.item.label);
                return false;
            },
            select: function (event, ui) {
                $("#txtSeachString").val(ui.item.label);
                return false;
            }
        })
            .autocomplete("instance")._renderItem = function (ul, item) {
                return $("<li>")
                    .append("<a>" + item.label + "</a>")
                    .appendTo(ul);
            };
    }
}
common.init();