
using Microsoft.AspNetCore.SignalR.Client;
using System.Text.Json;
using TripBliss.Models;

namespace TripBliss.Services.Data
{
    public interface INotificationService
    {
        Task ConnectAsync(string token);
        Task DisconnectAsync();
        Task SendNotificationReadAsync(string notificationId);
        Task SendNotificationClickAsync(string notificationId);

        void Dispose();

        event EventHandler<NotificationDto> OnNotificationReceived;
        event EventHandler<NotificationDto>? OnNotificationClicked;
        bool IsConnected { get; }
    }

    public class NotificationService : INotificationService
    {
        private HubConnection _hubConnection;
        private string _token;
        private readonly string _baseUrl;

        // ✅ استخدام EventHandler العادي مع WeakReference
        public event EventHandler<NotificationDto> OnNotificationReceived;
        public event EventHandler<NotificationDto>? OnNotificationClicked;


        public bool IsConnected => _hubConnection?.State == HubConnectionState.Connected;

        public NotificationService()
        {
            _baseUrl = Helpers.Utility.ServerUrl;
        }

        public async Task ConnectAsync(string token)
        {
            try
            {
                _token = token;

                if (_hubConnection?.State == HubConnectionState.Connected)
                    return;

                var hubUrl = $"{_baseUrl}notificationsHub";
                Console.WriteLine($"🔗 Connecting to: {hubUrl}");

                _hubConnection = new HubConnectionBuilder()
                    .WithUrl(hubUrl, options =>
                    {
                        options.AccessTokenProvider = () => Task.FromResult(_token);
                    })
                    .WithAutomaticReconnect(new[]
                    {
                        TimeSpan.FromSeconds(0),
                        TimeSpan.FromSeconds(2),
                        TimeSpan.FromSeconds(5),
                        TimeSpan.FromSeconds(10)
                    })
                    .Build();

                // ✅ تسجيل استقبال الإشعارات
                _hubConnection.On<NotificationDto>("ReceiveNotification", (notification) =>
                {
                    Console.WriteLine($"📩 Notification received: {notification.Title}");

                    // ✅ رفع الحدث مباشرة
                    OnNotificationReceived?.Invoke(this, notification);
                });

                // استقبال إشعارات النقر
                _hubConnection.On<NotificationDto>("NotificationClicked", (notification) =>
                {
                    Console.WriteLine($"👆 Notification clicked: {notification.Title}");
                    OnNotificationClicked?.Invoke(this, notification);
                });

                // معالجة إعادة الاتصال
                _hubConnection.Reconnecting += (error) =>
                {
                    Console.WriteLine($"🔄 SignalR Reconnecting: {error?.Message}");
                    return Task.CompletedTask;
                };

                _hubConnection.Reconnected += (connectionId) =>
                {
                    Console.WriteLine($"✅ SignalR Reconnected: {connectionId}");
                    return Task.CompletedTask;
                };

                _hubConnection.Closed += (error) =>
                {
                    Console.WriteLine($"❌ SignalR Closed: {error?.Message}");
                    return Task.CompletedTask;
                };

                await _hubConnection.StartAsync();
                Console.WriteLine("✅ SignalR Connected successfully");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error connecting to SignalR: {ex.Message}");
                throw;
            }
        }

        public async Task DisconnectAsync()
        {
            try
            {
                if (_hubConnection != null && _hubConnection.State == HubConnectionState.Connected)
                {
                    await _hubConnection.StopAsync();
                    await _hubConnection.DisposeAsync();
                    _hubConnection = null;
                    Console.WriteLine("✅ SignalR Disconnected");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error disconnecting SignalR: {ex.Message}");
            }
        }

        public async Task SendNotificationReadAsync(string notificationId)
        {
            try
            {
                if (IsConnected)
                {
                    await _hubConnection.InvokeAsync("MarkNotificationAsRead", notificationId);
                    Console.WriteLine($"✅ Notification {notificationId} marked as read");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error marking notification as read: {ex.Message}");
            }
        }

        public async Task SendNotificationClickAsync(string notificationId)
        {
            try
            {
                if (IsConnected)
                {
                    await _hubConnection.InvokeAsync("NotificationClicked", notificationId);
                    Console.WriteLine($"👆 Notification click sent: {notificationId}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error sending notification click: {ex.Message}");
            }
        }

        public void Dispose()
        {
            _hubConnection?.DisposeAsync().AsTask().Wait();
        }
    }
}

// NotificationDto.cs - في مجلد Models
namespace TripBliss.Models
{
    public class NotificationDto
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Type { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public string? RequestId { get; set; }
        public string? OfferId { get; set; }
        public string? SenderId { get; set; }
        public string? SenderName { get; set; }
        public string? DistributorId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public bool IsRead { get; set; } = false;
        public object? Data { get; set; }
        public string? NavigationRoute { get; set; }
        public Dictionary<string, object>? NavigationParameters { get; set; }
    }
}
