function Login() {
    const command = GetCommand();
    const loginUrl = "https://localhost:44310/login";
    PostQuery(loginUrl, command);
}

function GetCommand() {
    return {
        Email: document.getElementById("email").value,
        Password: document.getElementById("passwd").value,
        Role: "user"
    };
}

function PostQuery(url, command) {
    console.log(url);
    fetch(url, {
        method: 'POST',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(command)
    }).then(response => response.json())
        .then(data => {
            console.log(data.jwtToken);
            console.log(data.refreshToken);
            localStorage.setItem("jwtToken", data.jwtToken);
            localStorage.setItem("refreshToken", data.refreshToken);
        })
        //.then(() => {
        //fetch("https://localhost:44310/users", {
        //    method: 'GET',
        //    headers: {
        //        'Accept': 'application/json',
        //        'Content-Type': 'application/json',
        //        'Authorization': `Bearer ${localStorage.getItem("token")}`
        //    }
        //}).then(resp => resp.json())
        .then (() =>  redirectMainPage())


        .catch(error => console.error('Unable to sign in.', error));
}

function redirectMainPage() {
    window.location.href = "index.html";
}

function logOut() {
    localStorage.removeItem("jwtToken");
    localStorage.removeItem("refreshToken");
    window.location.href = "index.html";
}
