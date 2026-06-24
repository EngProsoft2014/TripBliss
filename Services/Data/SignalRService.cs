using CommunityToolkit.Maui.Alerts;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using System.Threading.Tasks;


namespace TripBliss.Services.Data
{
    public class SignalRService
    {
        private HubConnection _hubConnection;
        private bool _isReconnecting = false;

        public event Action<string> OnMessageReceived;

        public async Task InitSignalR(string userId, string role)
        {
            _hubConnection = new HubConnectionBuilder()
                .WithUrl($"{Helpers.Utility.ServerUrl}HubSignal/NotificationHub?userId={userId}&role={role}")
                .WithAutomaticReconnect()
                .Build();

            _hubConnection.Closed += async (error) =>
            {
                Console.WriteLine("SignalR Disconnected. Retrying in 2 seconds...");
                await Task.Delay(2000);
                await StartAsync();
            };

            _hubConnection.On<string>("ReceiveNotification", (message) =>
            {
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    var toast = Toast.Make(message, CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                    toast.Show();
                });

                OnMessageReceived?.Invoke(message);
            });

            await StartAsync();
        }

        public async Task StartAsync()
        {
            if (_hubConnection.State == HubConnectionState.Connected || _isReconnecting)
                return;

            _isReconnecting = true;

            try
            {
                await _hubConnection.StartAsync();
                Console.WriteLine("✅ SignalR Connected.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ SignalR Connection Failed: {ex.Message}");
            }
            finally
            {
                _isReconnecting = false;
            }
        }

        public async Task Disconnect()
        {
            if (_hubConnection != null)
            {
                await _hubConnection.StopAsync();
                await _hubConnection.DisposeAsync();
                Console.WriteLine("🔴 SignalR Disconnected.");
            }
        }
    }

}
