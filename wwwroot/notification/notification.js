const connection = new signalR.HubConnectionBuilder()
    .withUrl("/notificationHub") // Use the correct URL for your SignalR hub
    .build();

// Start the connection to the SignalR hub
connection.start()
    .then(() => {
        console.log("Connected to the hub.");
    })
    .catch(error => {
        console.error(`Error while connecting to the hub: ${error}`);
    });

// Define a function to handle incoming notifications
connection.on("ReceiveNotification", message => {
    // Handle the incoming notification here
    // You can display the message to the user, play a sound, or perform other actions
    console.log(`Received notification: ${message}`);
});

// This function is just an example to simulate receiving a notification
function simulateNotification() {
    const targetUserId = "uniqueUserId"; // Replace with the actual user's ID
    const message = "Your laundry is ready for pickup.";

    // Send a test notification to the user
    connection.invoke("SendNotificationToUser", targetUserId, message)
        .catch(error => {
            console.error(`Error while sending the test notification: ${error}`);
        });
}

// You can call simulateNotification() to simulate receiving a notification

// Other functions or code as needed
