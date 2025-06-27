"use strict"


var buttons = document.querySelectorAll(".deleteHotel");
buttons.forEach(btn => {
    btn.addEventListener("click", function () {

        let id = this.getAttribute("dataId");
        fetch("https://localhost:7107/Admin/Hotel/Delete?id=" + id, {
            method: "POST",
            headers: {
                "Content-type": "application/json; charset=UTF-8"
            }
        }).then(response => {
            if (response.ok) {
                const row = this.closest("tr");
                if (row) row.remove();
            } else {
                alert("Failed to delete user.");
            }
        }).catch(error => {
            console.error(error);
            alert("Error deleting user.");
        });
    });
});


