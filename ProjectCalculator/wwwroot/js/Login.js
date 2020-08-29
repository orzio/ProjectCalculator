function Login() {
    const command = GetCommand();
    const registerUrl = "https://localhost:44310/login";
        PostQuery(registerUrl, command);
}

function GetCommand() {
    return {
        Email: document.getElementById("email").value,
        Password: document.getElementById("passwd").value,
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
    }).then(response => response.json())
        .then(data => {
            localStorage.setItem("token", data.token);
        })
        .then(() => {
            fetch("https://localhost:44310/users", {
                method: 'GET',
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json',
                    'Authorization': `Bearer ${localStorage.getItem("token")}`
                }
            }).then(resp => resp.json())
                .then(data => console.log(data))
                .then(() => {
                    window.location.href = "index.html";
                })
        })
        .catch(error => console.error('Unable to sign in.', error));
}
