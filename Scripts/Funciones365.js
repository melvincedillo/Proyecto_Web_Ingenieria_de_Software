function addProduct(nameProduct,  priceProduct, id) {
    let tbl = document.getElementById("productsAdded");

    let newDivRow = document.createElement("div");
    newDivRow.className = "row";

    let newDivNameProduct = document.createElement("div");
    newDivNameProduct.className = "row";

    newDivNameProduct.nodeValue = nameProduct;
    newDivRow.appendChild(newDivNameProduct);

    newDivNameProduct.nodeValue = priceProduct;
    newDivRow.appendChild(newDivNameProduct);
}



function llenarPrecio(price) {    
    var priceProduct = document.getElementById("priceProduct");
    priceProduct.innerHTML = price;
}