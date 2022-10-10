const uri = "https://localhost:7177/api/users";

const tableBody = document.querySelector("#tableBody");

const usernameField = document.getElementById("username");
const passwordField = document.getElementById("pass");
const sendButton = document.querySelector("#sendData");

fetch(uri, { mode: "cors" })
  .then((response) => response.json())
  .then((users) => {
    console.log(users);
    showData(users);
  })
  .catch((error) => console.log(error));

function showData(users) {
  users.forEach((user) => {
    var tr = document.createElement("tr");

    // username
    let td1 = document.createElement("td");
    td1.appendChild(document.createTextNode(`${user.username}`));

    // password
    let td2 = document.createElement("td");
    td2.appendChild(document.createTextNode(`${user.password}`));

    tr.appendChild(td1);
    tr.appendChild(td2);
    tableBody.appendChild(tr);
  });
}

sendButton.addEventListener("click", () => {
  var username = usernameField.value;
  var password = passwordField.value;

  addUser(username, password);
});

function addUser(username, password) {
  fetch(uri, {
    mode: "cors",
    method: "POST",
    body: JSON.stringify({ username: username, password: password }),
    headers: {
      "Content-type": "application/json; charset=UTF-8",
    },
    // credentials: "include",
  })
    .then((response) => console.log(response))
    .catch((error) => console.log(error));
}
