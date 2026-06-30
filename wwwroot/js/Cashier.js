document.addEventListener("DOMContentLoaded", function () {

    // ===== Shift Closing =====
    function openShiftClosing() {
        document.getElementById("shiftClosingBox").style.display = "flex";
    }
    function closeShiftClosing() {
        document.getElementById("shiftClosingBox").style.display = "none";
    }
    function saveClosing() {
        const amount = document.getElementById("closingBalance").value;
        const shiftId = document.getElementById("shiftId").value;

        if (amount && parseFloat(amount) > 0) {
            fetch('/CashierUI/CloseShift', {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify({ ShiftId: shiftId, ClosingBalance: amount })
            })
                .then(response => response.json())
                .then(data => {
                    if (data.success) {
                        alert(data.message + " Actual Sales: " + data.actualSales + " MMK");
                        closeShiftClosing();
                        document.querySelector("#actualAmount").innerText =
                            "Actual Amount: " + data.actualSales + " MMK";
                    } else {
                        alert("Error: " + data.message);
                    }
                })
                .catch(err => alert("Request failed: " + err));
        } else {
            alert("Please enter a valid amount.");
        }
    }
    window.openShiftClosing = openShiftClosing;
    window.closeShiftClosing = closeShiftClosing;
    window.saveClosing = saveClosing;

    // ===== Scanner + Cart =====
    const scannerToggle = document.getElementById('phoneScannerToggle');
    const scanBtn = document.getElementById('scanBtn');
    const scanInput = document.getElementById('barcodeInput');
    const cartTableBody = document.querySelector("#cartTable tbody");
    const cartTotal = document.getElementById("cartTotal"); // ✅ declare once only

    function updateScannerMode() {
        if (scannerToggle.checked) {
            scanBtn.style.display = 'none';
            scanInput.placeholder = 'Enter scanned result (auto)';
        } else {
            scanBtn.style.display = 'inline-flex';
            scanInput.placeholder = 'Scan or enter barcode';
        }
    }
    scannerToggle.addEventListener('change', updateScannerMode);
    updateScannerMode();

    function addToCart(product) {
        let existingRow = cartTableBody.querySelector(`[data-barcode='${product.barcode}']`);
        if (existingRow) {
            // qty တိုး (scan ဖတ်/scan button နှိပ်တဲ့အခါ)
            let qtySpan = existingRow.querySelector(".qty-value");
            let qty = parseInt(qtySpan.innerText) + 1;
            qtySpan.innerText = qty;

            const unitPrice = product.salePrice;
            const rowSubtotal = qty * unitPrice;
            const discountAmount = 0;
            const rowTotal = rowSubtotal - discountAmount;

            existingRow.querySelector("td:nth-child(4)").innerText = rowSubtotal.toLocaleString() + " MMK";
            existingRow.querySelector("td:nth-child(5)").innerText = discountAmount.toLocaleString() + " (0%)";
            existingRow.querySelector("td:nth-child(6)").innerText = rowTotal.toLocaleString() + " MMK";
        } else {
            let row = document.createElement("tr");
            row.setAttribute("data-barcode", product.barcode);

            const unitPrice = product.salePrice;
            const subtotal = unitPrice;

            row.innerHTML = `
            <td>${product.productName}</td>
            <td class="qty">
                <button type="button" class="qty-minus bg-green">−</button>
                <span class="qty-value">1</span>
                <button type="button" class="qty-plus bg-green">+</button>
            </td>
            <td>${unitPrice.toFixed(2)} MMK</td>
            <td>${subtotal.toFixed(2)} MMK</td>
            <td>0 (0%)</td>
            <td class="total">${subtotal.toFixed(2)} MMK</td>
            <td><button class="remove-btn">Remove</button></td>
        `;
            cartTableBody.appendChild(row);
        }
        updateCartTotal();
    }

    function updateCartTotal() {
        let total = 0;
        cartTableBody.querySelectorAll(".total").forEach(cell => {
            total += parseFloat(cell.innerText.replace("MMK", "").replace(/,/g, "").trim());
        });
        if (cartTotal) {
            cartTotal.innerText = "Grand Total: " + total.toLocaleString() + " MMK";
        }
    }

    // ===== Qty Control =====
    document.addEventListener("click", function (e) {
        const row = e.target.closest("tr[data-barcode]");
        if (!row) return;

        const qtySpan = row.querySelector(".qty-value");
        if (!qtySpan) return;

        let qty = parseInt(qtySpan.innerText);
        const unitPrice = parseFloat(
            row.querySelector("td:nth-child(3)").innerText.replace("MMK", "").replace(/,/g, "").trim()
        );

        // Minus button
        if (e.target.classList.contains("qty-minus")) {
            qty--;
            if (qty <= 0) {
                row.remove(); // ✅ remove row if qty is 0
                updateCartTotal();
                return;
            }
            qtySpan.innerText = qty;
        }

        // Plus button
        if (e.target.classList.contains("qty-plus")) {
            qty++;
            qtySpan.innerText = qty;
        }

        const memberDiscountPercent = 0;
        const rowSubtotal = qty * unitPrice;
        const discountAmount = rowSubtotal * (memberDiscountPercent / 100);
        const rowTotal = rowSubtotal - discountAmount;

        row.querySelector("td:nth-child(4)").innerText = rowSubtotal.toLocaleString() + " MMK";
        row.querySelector("td:nth-child(5)").innerText = discountAmount.toLocaleString() + " (" + memberDiscountPercent + "%)";
        row.querySelector("td:nth-child(6)").innerText = rowTotal.toLocaleString() + " MMK";

        updateCartTotal();
    });

    // ===== Remove Button =====
    document.addEventListener("click", function (e) {
        if (e.target.classList.contains("remove-btn")) {
            const row = e.target.closest("tr[data-barcode]");
            if (row) {
                row.remove();
                updateCartTotal();
            }
        }
    });


    // ===== Barcode Lookup =====
    function lookupBarcode(barcode) {
        fetch('/CashierUI/LookupBarcode?code=' + encodeURIComponent(barcode))
            .then(response => response.json())
            .then(data => {
                if (data.success) {
                    addToCart(data.product); // scan ဖတ်ပြီး qty တိုး
                    scanInput.value = "";
                    scanInput.focus();
                } else {
                    alert("Product not found for barcode: " + barcode);
                }
            })
            .catch(err => alert("Request failed: " + err));
    }

    // Manual mode → click button
    scanBtn.addEventListener("click", function () {
        const code = scanInput.value.trim();
        if (code) lookupBarcode(code); // scan button နှိပ်ပြီး qty တိုး
    });

    // Auto mode → when barcode length reaches 13
    scanInput.addEventListener("input", function () {
        if (scannerToggle.checked) {
            const code = scanInput.value.trim();
            if (code.length === 13) {
                lookupBarcode(code); // auto scan qty တိုး
            }
        }
    });


    //t his is end of qty



    // ===== Member Search =====
    const searchBtn = document.getElementById("memberSearchBtn");
    const searchInput = document.getElementById("memberSearchInput");

    // ✅ Only Grand Total label (declare once)
    const grandTotalLabel = document.getElementById("cartTotal");

    // ✅ Function to recalc totals (with optional discount)
    function updateCartTotals(memberDiscountPercent = 0) {
        let grandTotal = 0;

        const rows = document.querySelectorAll("#cartTable tbody tr[data-barcode]");
        rows.forEach(row => {
            // ✅ qty-value span ကို သုံး
            const qty = parseInt(row.querySelector(".qty-value").innerText);
            const unitPrice = parseFloat(
                row.querySelector("td:nth-child(3)").innerText.replace("MMK", "").replace(/,/g, "").trim()
            );
            const rowSubtotal = qty * unitPrice;

            // Apply discount if memberDiscountPercent > 0
            const discountAmount = rowSubtotal * (memberDiscountPercent / 100);
            const rowTotal = rowSubtotal - discountAmount;

            // Update row cells
            row.querySelector("td:nth-child(4)").innerText = rowSubtotal.toLocaleString() + " MMK";
            row.querySelector("td:nth-child(5)").innerText = discountAmount.toLocaleString() + " (" + memberDiscountPercent + "%)";
            row.querySelector("td:nth-child(6)").innerText = rowTotal.toLocaleString() + " MMK";

            grandTotal += rowTotal;
        });

        // ✅ Always update Grand Total safely
        if (grandTotal < 0) grandTotal = 0;
        if (grandTotalLabel) {
            grandTotalLabel.innerText = "Grand Total: " + grandTotal.toLocaleString() + " MMK";
        }
    }

    // ===== Member Search =====
    searchBtn.addEventListener("click", function () {
        const cardNumber = searchInput.value.trim();

        if (!cardNumber) {
            alert("Card number မထည့်ထားပါ");
            updateCartTotals(0); // no discount, just recalc
            return;
        }

        fetch('/CashierUI/LookupMember?cardNumber=' + encodeURIComponent(cardNumber))
            .then(response => response.json())
            .then(data => {
                if (data.success) {
                    const apply = confirm(
                        "Member တွေ့ပါပြီ!\n\n" +
                        "Name : " + data.member.memberName +
                        "\nLevel : " + data.member.levelName +
                        "\nDiscount : " + data.member.discountPercent + "%" +
                        "\n\nCart မှာ discount ထည့်မလား?"
                    );

                    if (!apply) {
                        updateCartTotals(0);
                        return;
                    }

                    updateCartTotals(data.member.discountPercent);
                    alert("Discount ထည့်ပြီးပါပြီ။");
                } else {
                    alert(data.message);
                    updateCartTotals(0);
                }
            })
            .catch(err => {
                alert("Request failed: " + err.message);
                updateCartTotals(0);
            });
    });

    // ===== Remove Button =====
    document.addEventListener("click", function (e) {
        if (e.target && e.target.classList.contains("remove-btn")) {
            const row = e.target.closest("tr[data-barcode]");
            if (row) {
                const barcode = row.getAttribute("data-barcode");

                fetch('/CashierUI/RemoveItem', {
                    method: 'POST',
                    headers: { 'Content-Type': 'application/json' },
                    body: JSON.stringify(barcode)
                })
                    .then(response => response.json())
                    .then(data => {
                        if (data.success) {
                            // ✅ Remove row
                            row.remove();

                            // ✅ Recalculate totals after removal
                            updateCartTotals();
                        }
                    })
                    .catch(err => {
                        console.error("RemoveItem error:", err);
                        alert("RemoveItem failed: " + err.message);
                    });
            }
        }
    });


});


