using Microsoft.AspNetCore.SignalR;
using System;

namespace bookingtaxi_backend.BookingHub
{
    public class BookingHub : Hub<ITypeHub>
    {
        public async Task BroadcastMessage(string username, string message)
        {
            await Clients.All.ReceivedMessage(username, message);
        }

        public async Task SendToOthers(string username, string message)
        {
            await Clients.Others.ReceivedMessage(username, message);
        }

        public async Task SendToPerson(string ToConnectionId, string username, string message)
        {
            await Clients.Client(ToConnectionId).ReceivedMessage(username, message);
        }

        public async Task SendToGroup(string groupName, string username, string message)
        {
            await Clients.Group(groupName).ReceivedMessage(username, message);
        }

        public async Task AddPersonToGroup(string ConnectionId, string groupName)
        {
            await Groups.AddToGroupAsync(ConnectionId, groupName);
            await Clients.Caller.ReceivedMessage($"User {ConnectionId} added to group {groupName}");
        }

        public async Task RemovePersonFromGroup(string ConnectionId, string groupName)
        {
            await Groups.RemoveFromGroupAsync(ConnectionId, groupName);
            await Clients.Caller.ReceivedMessage($"User {ConnectionId} removed from group {groupName}");
        }

        public async Task AddMeToGroup(string groupName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
            await Clients.Caller.ReceivedMessage($"User {Context.ConnectionId} added to group {groupName}");
        }

        public async Task RemoveMeFromGroup(string groupName)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
            await Clients.Caller.ReceivedMessage($"User {Context.ConnectionId} removed from group {groupName}");
        }

        public override async Task OnConnectedAsync()
        {
            await Clients.Caller.ReceivedMessage($"You are connected to Booking chanel! ConnectionId: {Context.ConnectionId}");
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            await base.OnDisconnectedAsync(exception);
        }

    }

}

