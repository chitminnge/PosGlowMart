// Example: Chart.js integration for Profit Trend
document.addEventListener("DOMContentLoaded", function () {
    const ctx = document.getElementById("profitChart");
    if (ctx) {
        new Chart(ctx, {
            type: "bar",
            data: {
                labels: ["Jan", "Feb", "Mar", "Apr", "May", "Jun"],
                datasets: [{
                    label: "Profit (MMK)",
                    data: [12000000, 15000000, 18000000, 14000000, 20000000, 17000000],
                    backgroundColor: "#3498db"
                }]
            },
            options: {
                responsive: true,
                plugins: {
                    legend: { display: false }
                }
            }
        });
    }
});

// Sidebar active link highlight
document.querySelectorAll(".sidebar a").forEach(link => {
    link.addEventListener("click", function () {
        document.querySelectorAll(".sidebar a").forEach(l => l.classList.remove("active"));
        this.classList.add("active");
    });
});
