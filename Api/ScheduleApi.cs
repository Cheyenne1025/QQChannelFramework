using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using QQChannelFramework.Api.Base;
using QQChannelFramework.Api.Raws;
using QQChannelFramework.Api.Types;
using QQChannelFramework.Models;

namespace QQChannelFramework.Api;

sealed partial class QQChannelApi
{
    private static ScheduleApi _scheduleApi;

    public ScheduleApi GetScheduleApi()
    {
        if (_scheduleApi is null)
        {
            _scheduleApi = new(apiBase);
        }

        return _scheduleApi;
    }
}

/// <summary>
/// 日程Api
/// </summary>
public class ScheduleApi
{
    readonly ApiBase _apiBase;

    public ScheduleApi(ApiBase apiBase)
    {
        _apiBase = apiBase;
    }


    /// <summary>
    /// 获取指定 日程子频道 中指定结束时间后的当日活动日程
    /// </summary>
    /// <param name="childChannel">日程子频道Id</param>
    /// <param name="since">结束时间</param>
    /// <returns>元组 (日程集合，数量)</returns>
    public async Task<(List<Schedule>,int)> GetSchedulesAsync(string childChannel, DateTime since)
    {
        RawGetSchedulesApi rawGetSchedulesApi;

        var processedInfo = ApiFactory.Process(rawGetSchedulesApi, new Dictionary<ParamType, string>()
        {
            {ParamType.channel_id,childChannel }
        });

        JToken requestData;

        if (since != default)
        {
            requestData = await _apiBase
                .WithData(new Dictionary<string, object>()
                {
                    {"since", new DateTimeOffset(since).ToUnixTimeMilliseconds() }
                })
                .RequestAsync(processedInfo);
        }
        else
        {
            requestData = await _apiBase
                .WithData(new Dictionary<string, object>()
                {
                    {"since","" }
                })
                .RequestAsync(processedInfo);
        }

        List<Schedule> schedules = new();

        if(requestData is null)
        {
            return (schedules, 0);
        }

        var array = requestData as JArray;

        foreach (var info in array)
        {
            schedules.Add(info.ToObject<Schedule>());
        }

        return (schedules, schedules.Count);
    }

    /// <summary>
    /// 获取当日所有日程
    /// </summary>
    /// <param name="childChannel">日程子频道Id</param>
    /// <returns>元组 (日程集合，数量)</returns>
    public async Task<(List<Schedule>, int)> GetToDaySchedulesAsync(string childChannel)
    {
        return await GetSchedulesAsync(childChannel, default(DateTime));
    }

    /// <summary>
    /// 获取指定的日程信息
    /// </summary>
    /// <param name="childChannel">子频道Id</param>
    /// <param name="schedule_id">日程ID</param>
    /// <returns></returns>
    public async Task<Schedule> GetScheduleInfoAsync(string childChannel, string schedule_id)
    {
        RawGetScheduleApi rawGetScheduleApi;

        var processedInfo = ApiFactory.Process(rawGetScheduleApi, new Dictionary<ParamType, string>()
        {
            {ParamType.channel_id,childChannel },
            {ParamType.schedule_id,schedule_id }
        });

        var requestData = await _apiBase.RequestAsync(processedInfo);

        return requestData.ToObject<Schedule>();
    }

    /// <summary>
    /// 创建日程
    /// </summary>
    /// /// <param name="childChannel">日程子频道ID</param>
    /// <param name="newSchedule">不需要ID的新日程信息</param>
    /// <returns>创建的日程信息</returns>
    public async Task<Schedule> CreateAsync(string childChannel,Schedule newSchedule)
    {
        RawCreateScheduleApi rawCreateScheduleApi;

        var processedInfo = ApiFactory.Process(rawCreateScheduleApi, new Dictionary<ParamType, string>()
        {
            {ParamType.channel_id,childChannel }
        });

        if(newSchedule is not null)
        {
            newSchedule.Id = null;
        }

        var requestData = await _apiBase
            .WithData(new Dictionary<string, object>()
            {
                {"schedule",newSchedule }
            })
            .RequestAsync(processedInfo);

        return requestData.ToObject<Schedule>();
    }

    /// <summary>
    /// 更新日程
    /// </summary>
    /// <param name="childChannel">日程所在子频道ID</param>
    /// <param name="schedule_id">日程ID</param>
    /// <returns>修改后的日程信息</returns>
    public async Task<Schedule> UpdateAsync(string childChannel, string schedule_id, Schedule newSchedule)
    {
        RawAddMemberToRoleApi rawAddMemberToRoleApi;

        var processedInfo = ApiFactory.Process(rawAddMemberToRoleApi, new Dictionary<ParamType, string>()
        {
            {ParamType.channel_id,childChannel },
            {ParamType.schedule_id,schedule_id }
        });

        if(newSchedule.Id is not null)
        {
            newSchedule.Id = null;
        }

        var requestData = await _apiBase
            .WithData(new Dictionary<string, object>()
            {
                {"schedule",newSchedule }
            })
            .RequestAsync(processedInfo);


        return requestData.ToObject<Schedule>();
    }

    /// <summary>
    /// 删除日程
    /// </summary>
    /// <param name="childChannel">日程所在子频道ID</param>
    /// <param name="schedule_id">日程ID</param>
    /// <returns></returns>
    public async ValueTask DeleteAsync(string childChannel,string schedule_id)
    {
        RawDeleteScheduleApi rawDeleteScheduleApi;

        var processedInfo = ApiFactory.Process(rawDeleteScheduleApi, new Dictionary<ParamType, string>()
        {
            {ParamType.channel_id,childChannel },
            {ParamType.schedule_id,schedule_id }
        });

        await _apiBase.RequestAsync(processedInfo);
    }
}