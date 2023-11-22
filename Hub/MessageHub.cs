using Microsoft.AspNetCore.SignalR;
using System;
using System.Globalization;

namespace bookingtaxi_backend.Hub
{
    public class MessageHub : Hub<ITypeHub>
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
            DateTime localDate = DateTime.Now;
            var culture = new CultureInfo("vi-VN");

            await Clients.Group(groupName).ReceivedMessage(username, message, localDate.ToString(culture));
        }

        public async Task AddPersonToGroup(string ConnectionId, string groupName)
        {
            DateTime localDate = DateTime.Now;
            var culture = new CultureInfo("vi-VN");

            await Groups.AddToGroupAsync(ConnectionId, groupName);
            //await Clients.Caller.ReceivedMessage($"User {ConnectionId} added to group {groupName}", localDate.ToString(culture));
        }

        public async Task RemovePersonFromGroup(string ConnectionId, string groupName)
        {
            DateTime localDate = DateTime.Now;
            var culture = new CultureInfo("vi-VN");

            await Groups.RemoveFromGroupAsync(ConnectionId, groupName);
            await Clients.Caller.ReceivedMessage($"User {ConnectionId} removed from group {groupName}", localDate.ToString(culture));
        }

        public async Task AddMeToGroup(string groupName, string username)
        {
            DateTime localDate = DateTime.Now;
            var culture = new CultureInfo("vi-VN");
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
            //await Clients.Group(groupName).ReceivedMessage($"{username} is added to group chat {groupName}", localDate.ToString(culture));
        }

        public async Task RemoveMeFromGroup(string groupName, string username)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
            await Clients.Group(groupName).ReceivedMessage($"User {username} is removed from group {groupName}");
        }

        public override async Task OnConnectedAsync()
        {
            //await Clients.Caller.ReceivedMessage($"You are connected to the server! ConnectionId: {Context.ConnectionId}");
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            await base.OnDisconnectedAsync(exception);
        }

    }

}

