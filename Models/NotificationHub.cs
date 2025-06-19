using Microsoft.AspNetCore.SignalR;

public class NotificationHub : Hub
{
    public async Task SendNotificationToUser(string userId, string message)
    {
        // This method sends a notification to a specific user.
        await Clients.User(userId).SendAsync("ReceiveNotification", message);
    }
}
