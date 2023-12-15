const url = "api/Vols1";
const connection = new signalR.HubConnectionBuilder().withUrl("/volHub").build();

connection.start()
    .catch(function (err) { return console.error(err.toString()) });

document.getElementById("createbt").addEventListener("click", function (event) {
    var compagnie = document.getElementById("compagnie").value;
    var codeVol = document.getElementById("codeVol").value;
    var ville = document.getElementById("ville").value;
    var heurePrevue = document.getElementById("heurePrevue").value;
    var heureRevisee = document.getElementById("heureRevisee").value;
    var statut = document.getElementById("statut").value;

    const vol = {
        id: 0,
        compagnie: compagnie,
        codeVol: codeVol,
        ville: ville,
        heurePrevue: heurePrevue,
        heureRevisee: heureRevisee,
        statut: statut
    };

    console.log(vol);

    fetch(url, {
        method: "POST",
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
