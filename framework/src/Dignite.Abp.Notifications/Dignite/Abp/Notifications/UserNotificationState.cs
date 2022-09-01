namespace Dignite.Abp.Notifications
{
    public enum UserNotificationState
    {
        /// <summary>
        /// Notification is not read by user yet.
        /// </summary>
        Unread = 0,

        /// <summary>
        /// Notification is read by user.
        /// </summary>
        Read
    }
}