using System.Collections.Generic;
using QQChannelFramework.Models;

namespace QQChannelFramework.Expansions.Bot;

sealed partial class ChannelBot
{
    private static Dictionary<string, Action<CommandInfo>?> _commands = new();

    /// <summary>
    /// 注册指令
    /// </summary>
    /// <param name="command">文字指令 (字符敏感)</param>
    /// <param name="commandAction">指令执行</param>
    /// <returns></returns>
    public ChannelBot RegisterCommand(string command, Action<CommandInfo> commandAction)
    {
        if(_commands.ContainsKey(command) is true)
        {
            throw new Exception($"指令 {command} 已存在");
        }

        _commands.Add(command, commandAction);

        return this;
    }

    /// <summary>
    /// 卸载指令
    /// </summary>
    /// <param name="command">指令</param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public ChannelBot UnloadCommand(string command)
    {
        if(_commands.ContainsKey(command) is false)
        {
            throw new Exception($"指令 {command} 不存在");
        }

        _commands.Remove(command);

        return this;
    }

    private void InvokeCommand(CommandInfo commandInfo)
    {
        if(_commands.ContainsKey(commandInfo.Key) is true)
        {
            _commands[commandInfo.Key](commandInfo);
        }
    }
}