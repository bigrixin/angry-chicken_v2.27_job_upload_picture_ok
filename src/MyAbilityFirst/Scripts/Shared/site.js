var ajaxDataObj = (
	function () {
		var data;
		var pub = {};

		pub.getData = function () {
			return data;
		};
		pub.setData = function (newData) {
			data = newData;
		};
		pub.flipSelected = function () {
			data.Selected = !data.Selected;
		};
		return pub;
	}()
);

function PostAJAX(url, data, element, toggleClasses) {
	if (ajaxDataObj.getData() === null) {
		ajaxDataObj.setData(data);
	}
	ajaxDataObj.flipSelected();

	$(element).button('loading').delay(100).queue(function () {

		$.ajax({
			data: JSON.stringify(ajaxDataObj.getData()),
			contentType: "application/json; charset=utf-8",
			type: 'POST',
			cache: false,
			url: url,
			timeout: 1000000,
			success: function (data) {
				$(element).toggleClass(toggleClasses);
				$(element).button('reset');
				$(element).dequeue();
				ajaxDataObj.setData(data);
			},
			fail: function (XMLHttpRequest, textStatus, errorThrown) {
			}
		}).fail(function (errorThrown) {
			ajaxDataObj.flipSelected();
			$(element).button('reset');
			$(element).dequeue();
			console.log(errorThrown.statusText);
		});
	});

}
