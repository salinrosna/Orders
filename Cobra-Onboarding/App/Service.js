
myapp.service('myService', function ($http) {
    debugger;
    this.getCustomers = function () {
        return $http.get('/Cobra/Customertable');
    };

    this.AddCustomer = function (data) {
        var addcust = $http({
            method: 'POST',
            url: '/Cobra/Customer',
            data: data,
            headers: { 'Content-Type': 'application/Json' }
        });
      return addcust;
    }
   
    this.Editcustomer = function (ID, data) {
        debugger;
        var editcust = $http({
            method: 'POST',
            url: '/Cobra/EditCust',
            data: { id: ID, Pers: data },
            headers:{'Content-Type':'application/json'}
        })
        return editcust;
    }

    this.Deletecust = function (Id) {
        debugger;
        var dltcust = $http({
            method: 'POST',
            url: '/Cobra/DeleteCustomer',
            data:{data:JSON.stringify(Id) },
            headers: { 'Content-Type': 'application/json' }
        })
        return dltcust;
    }
});


myapp.service('OrderService', function ($http) {
    debugger;
    this.getorders = function () {
        return $http.get('/CobraOrder/Getorders');
    };
    this.getcp = function () {
       
        return $http.get("/CobraOrder/GetCP");
    };
    this.AddOrder = function (data) {
        var addorder = $http({
            method: "POST",
            url: "/CobraOrder/AddOrder",
            data:data,
            headers:{ "Content-Type":'application/json' }
            
        })
        return addorder;
    }
    this.EditOrder = function (id,data) {
        debugger;
        var editorder = $http({
            method: "POST",
            url: "/CobraOrder/EditOrder",
            data: {Id: id,Ord:data},
            headers:{"Content-Type":'application/json'}
        })
        return editorder;
    }
    this.Deleteorder = function (item) {
        debugger;
        var deleteorder = $http({
            method: "POST",
            url: "/CobraOrder/DeleteOrder",
            data: { orderId: JSON.stringify(item) },
            headers: { "Content-Type": 'application/json' }
        })
        return deleteorder;
    }
    
});



