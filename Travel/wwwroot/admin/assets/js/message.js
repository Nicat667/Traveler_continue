"use strict"


var buttons = document.querySelectorAll(".deleteMessage");
buttons.forEach(btn => {
    btn.addEventListener("click", function () {

        let id = this.getAttribute("dataId");
        fetch("https://localhost:7107/Admin/Message/Delete?id=" + id, {
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

document.querySelectorAll(".detailButton").forEach(button => {
    button.addEventListener("click", function () {
        const id = this.getAttribute("data-id");

        fetch(`https://localhost:7107/Admin/Message/Update?id=${id}`, {
            method: "POST",
            headers: {
                "Content-type": "application/json; charset=UTF-8"
            }
        })
            .then(response => {
                if (response.ok) {
                    // After update, redirect to detail page:
                    window.location.href = `https://localhost:7107/Admin/Message/Detail/${id}`;
                } else {
                    alert("Failed to update message.");
                }
            })
            .catch(error => {
                console.error(error);
                alert("Error updating message.");
            });
    });
});
