const uri = "https://localhost:7177/api/users";

const tableBody = document.querySelector("#tableBody");

fetch(uri)
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
