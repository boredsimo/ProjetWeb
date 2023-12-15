const url = "api/Vols1"; // Assuming the endpoint for Vols API
const connection = new signalR.HubConnectionBuilder().withUrl("/VolHub").build(); // Assuming the hub for VolHub
connection.start()
    .catch(function (err) { return console.error(err.toString()); });

document.getElementById("savebt").addEventListener("click", function (event) {
    var id = document.getElementById("volId").value; // Assuming you have an input field with the id "volId"
    var compagnie = document.getElementById("compagnie").value;
    var codeVol = document.getElementById("codeVol").value;
    var ville = document.getElementById("ville").value;
    var heurePrevue = document.getElementById("heurePrevue").value;
    var heureRevisee = document.getElementById("heureRevisee").value;
    var statut = document.getElementById("statut").value;

    const vol = {
        id: id,
        compagnie: compagnie,
        codeVol: codeVol,
        ville: ville,
        heurePrevue: heurePrevue,
        heureRevisee: heureRevisee,
        statut: statut
    };

    fetch(url + "/" + id, {
        method: "PUT",
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(vol)
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
