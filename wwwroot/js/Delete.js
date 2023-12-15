const url = "api/Vols1"; 
const connection = new signalR.HubConnectionBuilder().withUrl("/volHub").build(); 
connection.start()
    .catch(function (err) { return console.error(err.toString()) });

document.getElementById("deletebt").addEventListener("click", function (event) {
    var id = document.getElementById("id").value; 

    fetch(url + "/" + id, {
        method: "DELETE",
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        }
    })
        .then(response => response.json())
        .then(() => {
            connection.invoke("SendMessage").catch(function (err) {
                return console.error(err.toString());
            });
        })
        .catch(error => alert("API Error"));

    event.preventDefault();
});
