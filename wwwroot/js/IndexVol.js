const url = "api/Vols1";
let $vols = $("#vols");
getVols();

function getVols() {
    fetch(url)
        .then(response => response.json())
        .then(data => data.forEach(vol => {
            let template = `<tr>
                                <td>${vol.compagnie}</td>
                                <td>${vol.codeVol}</td>
                                <td>${vol.ville}</td>
                                <td>${vol.heurePrevue}</td>
                                <td>${vol.heureRevisee}</td>
                                <td>${vol.statut}</td>
                                <td>
                                    <a href="/Vols/Details/${vol.id}">Details</a> |
                                    <a href="/Vols/Edit/${vol.id}">Edit</a> |
                                    <a href="/Vols/Delete/${vol.id}">Delete</a>
                                </td>
                             </tr>`;
            $vols.append($(template));
        }))
        .catch(error => alert("Erreur API"));
}

const connection = new signalR.HubConnectionBuilder().withUrl("/volHub").build();
connection.start()
    .catch(function (err) { return console.error(err.tostring()) })
connection.on("VolChange", function () {
    $vols.empty();
    getVols();
});
