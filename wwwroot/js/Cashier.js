const scannerToggle = document.getElementById("scannerToggle");
const scanInputGroup = document.getElementById("scanInputGroup");
const scannerBox = document.getElementById("scannerBox");
const switchLabelText = document.getElementById("switchLabelText");

scannerToggle.addEventListener("change", function () {
    if (this.checked) {
        // Switch ON → show scanner input, hide manual input
        scanInputGroup.style.display = "none";
        scannerBox.style.display = "block";
        switchLabelText.textContent = "Scanner Mode";
    } else {
        // Switch OFF → show manual input, hide scanner input
        scanInputGroup.style.display = "flex";
        scannerBox.style.display = "none";
        switchLabelText.textContent = "Manual Input";
    }
});
