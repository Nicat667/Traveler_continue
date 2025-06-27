"use strict"


document.querySelectorAll(".deleteRoom").forEach(btn => {
    btn.addEventListener("click", function () {

        let button = this;  // store reference to the clicked button
        let id = button.getAttribute("data-id");  // assuming you rename dataId to data-id in HTML

        fetch("https://localhost:7107/Admin/Room/Delete?id=" + id, {
            method: "POST",
            headers: {
                "Content-type": "application/json; charset=UTF-8"
            }
        })
            .then(response => {
                if (response.ok) {
                    const row = button.closest("tr");
                    if (row) row.remove();
                } else {
                    alert("Failed to delete room.");
                }
            })
            .catch(error => {
                console.error(error);
                alert("Error deleting room.");
            });
    });
});