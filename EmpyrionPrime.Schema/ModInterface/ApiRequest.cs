using Eleon.Modding;
using System;

namespace EmpyrionPrime.Schema.ModInterface
{
    public class ApiRequest
    {
        public CmdId CommandId { get; }
        public Type ArgumentType { get; }
        public CmdId? ResponseId { get; }
        public Type ResponseType { get; }

        public ApiRequest(CmdId commandId, Type argumentType, CmdId? responseId, Type responseType = null)
        {
            CommandId = commandId;
            ArgumentType = argumentType;
            ResponseId = responseId;
            ResponseType = responseType;

            // Try to figure out the responseType from responseId
            if (ResponseType == null && 
                responseId != null && responseId.HasValue &&
                ApiSchema.ApiEvents.TryGetValue(responseId.Value, out Type type))
            {
                ResponseType = type;
            }
        }
    }
}
