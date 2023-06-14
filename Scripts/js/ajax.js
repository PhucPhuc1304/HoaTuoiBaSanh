function addToCart(productId) {
    var model = {};
    model.Id = productId;
    model.Quantity = 1;

    $.ajax({
        type: "POST",
        url: "/Shop/AddToCart",
        data: JSON.stringify(model),
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function () {
            $('#CartCount').text(parseInt('@Session["count"]') + 1);
            toastr.success("Sản phẩm đã được thêm vào giỏ hàng thành công.", "", {
                timeOut: 2000,
                progressBar: true,
                closeButton: true
            });
        },
        error: function () {
            toastr.error("Lỗi... Thêm sản phẩm vào giỏ hàng không thành công.", "", {
                timeOut: 2000,
                progressBar: true,
                closeButton: true
            });
        }
    });
}
$(document).ready(function () {
    $(".add-to-cart, .button-cart").click(function (event) {
        var productId = $(this).data("product-id");
        addToCart(productId);
        return false;
    });
});