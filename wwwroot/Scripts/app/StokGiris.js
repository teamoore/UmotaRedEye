var barcodeData = [];

function SaveStok(barcode) {
    $.umota.callService({
        service: '../StokData/SaveStok',
        request: { Barcode: barcode, Format: 'EAN_13'},
        onSuccess: onResponseSuccess,               
        onError: function (response) {
            $.tg.playError();
        }
    });
}

function onResponseSuccess(response) {
    if (response === undefined)
        return;

    if (response.success) {
        barcodeData.push(response.value);
        displayData(response.value);

        $.tg.playSuccess();
    }
    else {
        displayData(response.value);
        $.tg.playError();
    }
        
}

function displayData(data) {
    let ul = document.getElementById("ulBarcodes");
    if (ul === undefined) return;

    let item = document.createElement("li");
    let itemTxt = document.createTextNode(data);
    item.appendChild(itemTxt);
    ul.appendChild(item);
}

function onScanSuccess(decodedText, decodedResult) {
    console.log(`Code scanned = ${decodedText}`, decodedResult);

    SaveStok(decodedText);
}

var html5QrcodeScanner = new Html5QrcodeScanner(
    "qr-reader", { fps: 10, qrbox: 250 });
html5QrcodeScanner.render(onScanSuccess);