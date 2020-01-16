// Toggle print view
function togglePrintView(triggerPrinter) {
    if (triggerPrinter == undefined) triggerPrinter = false;

    var element = document.getElementById("page");
    element.classList.toggle("print");
    if (triggerPrinter) {
        window.print();
        element.classList.toggle("print");
    }
}

// Export to CSV
function exportToCsv(fileName) {
    var csv = [];
    var tables = document.getElementById("sqlResult").getElementsByTagName('table');

    for (var t = 0; t < tables.length; t++) { // tables
        var rows = tables[t].querySelectorAll("tr");

        for (var i = 0; i < rows.length; i++) { // rows
            var row = [], cols = rows[i].querySelectorAll("td, th");

            for (var j = 0; j < cols.length; j++) { // cols
                var cellvalue = cols[j].innerText;
                cellvalue = cellvalue.replace(/\"/g, "\"\""); // escape single quotes
                row.push('"' + cellvalue + '"');
            }

            csv.push(row.join(","));
        }
    }

    downloadCSV(csv.join("\n"), fileName+'.csv'); // download csv
}

// Download CSV file
function downloadCSV(csv, filename) {
    var csvFile;
    var downloadLink;

    if (window.Blob == undefined || window.URL == undefined || window.URL.createObjectURL == undefined) {
        alert("Your browser doesn't support Blobs");
        return;
    }

    csvFile = new Blob([csv], { type: "text/csv" });
    downloadLink = document.createElement("a");
    downloadLink.download = filename;
    downloadLink.href = window.URL.createObjectURL(csvFile);
    downloadLink.style.display = "none";
    document.body.appendChild(downloadLink);
    downloadLink.click();
    document.body.removeChild(downloadLink);
}