function createUser() {
    const isChecked = document.getElementById("checkbox").checked;
    const policyLabel = document.getElementById("policy-accept");
    const command = GetCommand();
    const registerUrl = "https://localhost:44310/users";

    if (isChecked) {
        PostQuery(registerUrl, command);
        policyLabel.style.color = "black";
    }
    else
        policyLabel.style.color = "red";
}

function GetCommand() {
    return {
        Email: document.getElementById("email").value,
        Password: document.getElementById("passwd").value,
        FirstName: document.getElementById("first-name").value,
        LastName: document.getElementById("last-name").value,
        Role: "user"
    };
}

function PostQuery(url, command) {
    console.log(command);
    fetch(url, {
        method: 'POST',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(command)
    })
        .catch(error => console.error('Unable to add item.', error));
}
