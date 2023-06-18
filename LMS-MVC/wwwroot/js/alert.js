var connection = new signalR.HubConnectionBuilder().withUrl("/HandleHub").build();

//Disable the send button until connection is established.



connection.start().then(function () {
    connection.on("ReceiveMessage", function (message) {
        document.getElementById("total_books").innerText = message;
        alert("i recieved called");
    });

}).catch(function (err) {
    return console.error(err.toString());
});


function generate_ALert(connection)
{
    alert("i am called from genreate ");
    connection.invoke("SendMessage","this is a test message").catch(function (err) {
    return console.error(err.toString());
});
}