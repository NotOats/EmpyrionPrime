using Eleon;
using Eleon.Modding;

namespace EmpyrionPrime.Plugin.Extensions
{
    public static class ModApiExtensions
    {
        /// <summary>
        /// Converts a <see cref="MessageData"/> into the legacy <see cref="ChatInfo"/> variant
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public static ChatInfo ToChatInfo(this MessageData message)
        {
            // Weird mapping, sourced from EmpyrionNetApiAccess & server testing
            var type = -1;
            switch (message.Channel)
            {
                case Eleon.MsgChannel.Global:
                    type = 3;
                    break;
                case Eleon.MsgChannel.Faction:
                case Eleon.MsgChannel.Alliance:
                    type = 5;
                    break;
                case Eleon.MsgChannel.SinglePlayer:
                    type = 8;
                    break;
                case Eleon.MsgChannel.Server:
                    type = 9;
                    break;
            }

            return new ChatInfo(message.SenderEntityId, message.Text,
                message.RecipientEntityId, message.RecipientFaction.Id, (byte)type);
        }
    }
}
