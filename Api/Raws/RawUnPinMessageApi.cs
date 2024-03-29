﻿using MyBot.Api.Types;
namespace MyBot.Api.Raws; 

public struct RawUnPinMessageApi : Base.IRawApiInfo
{
    public string Version => "1.0";

    
    public string Url => "/channels/{channel_id}/pins/{message_id}";

    public MethodType Method => MethodType.DELETE;
}