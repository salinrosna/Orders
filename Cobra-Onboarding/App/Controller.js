/// <reference path="C:\Users\salin\onedrive\documents\visual studio 2015\Projects\Cobra-Onboarding\Cobra-Onboarding\Scripts/angular.js" />

var customer = function ($scope, myService) {
    debugger;
    Get();
    function Get() {
        var getcust = myService.getCustomers();
        getcust.then(function(response) {
            $scope.User = response.data;
            debugger;
        });
    }

    $scope.formdata = {}
    $scope.Addcustomer = function () {  
        debugger;
        var data = {
            Name: $scope.formdata.Name,
            Address1: $scope.formdata.Address1,
            Address2: $scope.formdata.Address2,
            City: $scope.formdata.City
        };
        var addcustomer = myService.AddCustomer(data);
        addcustomer.then(function (msg) {
            Get();
        })
    }
    function Clearfields() {
        $scope.formdata.Name = "";
        $scope.formdata.Address1 = "";
        $scope.formdata.Address2 = "";
        $scope.formdata.City = "";

    }
    $scope.Addcustclick = function () {
        Clearfields();
    }
    $scope.getCustomer = function (customer) {
        debugger;
        $scope.Customer = customer;
        $scope.Customer.Id = customer.Id;
        $scope.Customer.Name = customer.Name;
        $scope.Customer.Address1 = customer.Address1;
        $scope.Customer.Address2 = customer.Address2;
        $scope.Customer.City = customer.City;
    }

    $scope.Customer = {}
    $scope.Editcustomer = function () {
        debugger;
        var editcust = {
            Name: $scope.Customer.Name,
            Address1: $scope.Customer.Address1,
            Address2: $scope.Customer.Address2,
            City:$scope.Customer.City
        }
        var editcustomer = myService.Editcustomer($scope.Customer.Id, editcust);
        editcustomer.then(function (msg) {
            Get();
        })
    }

    $scope.DeleteCustomer = function () {
        debugger;
        var deletecustomer = myService.Deletecust($scope.Customer.Id);
        deletecustomer.then(function (result) {
            alert("Successfully deleted ");
            Get();
        })
    }
}

myapp.filter("mydate", function () {
    var re = /\/Date\(([0-9]*)\)\//;
    return function (x) {
        var m = x.match(re);
        if (m) { return new Date(parseInt(m[1])); }
        else return null;
    };
});

myapp.controller("Customer", customer);

var Order = function ($scope,$filter, OrderService) {
    debugger;
    $scope.Action = "Add";

    $scope.flag = false;
    GetOrders();
    function GetOrders() {
        var getorder = OrderService.getorders();
        getorder.then(function (response) {
            $scope.Orders = response.data;
            debugger;
        })
    }
    $scope.getCP = function () {
        debugger;
        var getcpr = OrderService.getcp();
        getcpr.then(function(result) {
            $scope.CP = result.data;
            $scope.Namelist = $scope.CP.Person;
            $scope.Productlist = $scope.CP.Product;
           
        })
    }
    $scope.AddClick = function () {
        debugger;   
        Clearfields();
        $scope.getCP();
        $scope.Action = "Add"
        $scope.flag = false;

    }
    $scope.productoption = function (item) {
        debugger;
        $scope.product = $filter('filter')($scope.Productlist, { "Id": item });
        $scope.newOrder.Price = $scope.product["0"].Price;  
    }
    
    $scope.newOrder={}
    $scope.Addorder=function () {
        debugger;
        var data = {
            CustomerId: $scope.newOrder.CustomerId,
            ProductId: $scope.newOrder.productId          
        };
        if ($scope.Action == "Add") {
            var addorder = OrderService.AddOrder(data);
            addorder.then(function (msg) {
                GetOrders();
            })
        }
        else {
            
            var editorder = OrderService.EditOrder($scope.newOrder.Id, data);
            editorder.then(function (msg) {
                GetOrders();
            })
        }
    }
   
    $scope.getOrder = function (item) {
        debugger;
        $scope.Action = "Edit";
        $scope.flag = true;
        $scope.getCP();
        debugger;
        $scope.newOrder.Id = item.Id;
        $scope.newOrder.date = $filter('mydate')(item.OrderDate);;
        $scope.newOrder.CustomerId = item.peo.Id;
        $scope.newOrder.productId = item.pro.Id;
        $scope.newOrder.Price = item.pro.Price;
    }
    $scope.DeleteOrder = function (order) {
        debugger;
        var deleteorder = OrderService.Deleteorder(order.Id);
        deleteorder.then(function (msg) {
            GetOrders();
        })

    }
    function Clearfields() {
        $scope.newOrder.CustomerId = "";
        $scope.newOrder.productId = "";
        $scope.newOrder.Price = "";
    }
}
myapp.controller('Order',Order);
