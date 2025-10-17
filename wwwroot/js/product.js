function ProductViewModel() {
    var self = this;
    self.products = ko.observableArray([]);
    self.NAME = ko.observable("");
    self.CATEGORY = ko.observable("");
    self.PRICE = ko.observable("");

    self.loadProducts = function () {
        $.ajax({
            url: 'http://localhost:5132/api/Product',
            type: 'GET',
            success: function (data) {
                console.log("Data from API:", data);
                self.products(data);
            }
        });
    };

    self.Add = function () {
        var product = {
            name: self.NAME(),
            category: self.CATEGORY(),
            price: self.PRICE()
        };
            $.ajax({
                url: 'http://localhost:5132/api/Product',
                type: 'POST',
                contentType: 'application/json',
                data: JSON.stringify(product),
                success: function () {
                    self.NAME("");
                    self.CATEGORY("");
                    self.PRICE("");
                    self.loadProducts();
                    alert("Product added successfully");
                }
            });
        };
    self.Edit = function (product) {
        var updatedproduct = {
            id: product.id,
            name: prompt("Enter new name:", product.name),
            category: prompt("Enter new category:", product.category),
            price: parseFloat(prompt("Enter new price:", product.price))
        }
        $.ajax({
            url: 'http://localhost:5132/api/Product/'+ product.id,
            type: 'PATCH',
            contentType: 'application/json',
            data: JSON.stringify(updatedproduct),
            success: function () {
                alert("student updated")
                self.loadProducts();
            }
        });
    }
    self.Delete = function (product) {
        $.ajax({
            url: 'http://localhost:5132/api/Product/'+ product.id,
            type: 'DELETE',
            success: function () {
                self.products.remove(product)
                alert("Product deleted successfully!")
            }
        });
    };
    self.loadProducts();
}
    ko.applyBindings(new ProductViewModel());
