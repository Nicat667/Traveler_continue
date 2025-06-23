"use strict"

async function setUserId(button) {
    const userId = button.getAttribute('userId');
    document.getElementById('modalUserId').value = userId;

    const roleSelect = document.getElementById('roleSelect');
    roleSelect.innerHTML = '';

    try {
        const response = await fetch(`/Admin/Account/GetAvailableRoles?userId=${userId}`);
        const roles = await response.json();

        if (roles.length === 0) {
            roleSelect.innerHTML = '<option disabled selected>No available roles</option>';
        } else {
            roles.forEach(role => {
                const option = document.createElement('option');
                option.value = role;
                option.textContent = role;
                roleSelect.appendChild(option);
            });
        }
    } catch (error) {
        console.error('Error fetching roles:', error);
    }
}

async function setUserIdForRemove(button) {
    const userId = button.getAttribute('userId');
    document.getElementById('removeModalUserId').value = userId;

    const roleSelect = document.getElementById('removeRoleSelect');
    roleSelect.innerHTML = '';

    try {
        const response = await fetch(`/Admin/Account/GetCurrentRoles?userId=${userId}`);
        const roles = await response.json();

        if (roles.length === 0) {
            roleSelect.innerHTML = '<option disabled selected>No roles to remove</option>';
        } else {
            roles.forEach(role => {
                const option = document.createElement('option');
                option.value = role;
                option.textContent = role;
                roleSelect.appendChild(option);
            });
        }
    } catch (error) {
        console.error('Error fetching user roles:', error);
    }
}

var deleteUser = document.querySelectorAll(".deleteUser");
deleteUser.forEach(btn => {    
    btn.addEventListener("click", function () {

        let userId = this.getAttribute("userId");
        fetch("https://localhost:7107/Admin/Account/Delete?id=" + userId, {
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

